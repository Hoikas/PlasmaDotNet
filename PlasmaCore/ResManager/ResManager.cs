﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {

    public abstract class hsResMgr {

        protected plVersion fVersion = plVersion.MystOnline;

        /// <summary>
        /// Gets or sets the Version this ResManager is currently using
        /// </summary>
        /// <remarks>
        /// Note: This value will be set by the first page you read in. 
        /// If you change or otherwise set the value, we will assume you 
        /// want that value to be saved and not touch it. However, all page 
        /// reads will continue to use the auto-determined versions. On the 
        /// other hand, creatables will need this set BEFORE you read them in!
        /// </remarks>
        public plVersion Version {
            get { return fVersion; }
            set { fVersion = value; }
        }

        internal virtual plKey ReRegister(plUoid uoid) {
            // Returns an unchecked key.
            return new plKey(uoid); 
        }

        /// <summary>
        /// Reads an arbitrary Creatable from the current position in the stream
        /// </summary>
        /// <param name="s">Stream to read from</param>
        /// <returns></returns>
        public plCreatable ReadCreatable(hsStream s) {
            plCreatable pCre = plFactory.Create(plManagedType.Read(s));
            if (pCre != null)
                pCre.Read(s, this);
            return pCre;
        }

        /// <summary>
        /// Reads a key reference from the stream
        /// </summary>
        /// <param name="s">Stream containing a key reference</param>
        /// <returns>The key referenced in the stream or null (if null reference)</returns>
        public plKey ReadKey(hsStream s) {
            if (!s.Version.IsPlasma21)
                if (!s.ReadBool()) return null;

            return ReadUoid(s);
        }


        public plKey ReadUoid(hsStream s) {
            plUoid uoid = new plUoid();
            uoid.Read(s);

            // Plasma21+ uses an Invalid plLocation to indicate NULL keys
            if (s.Version.IsPlasma21 && uoid.fLocation.Invalid)
                return null;
            else
                return ReRegister(uoid);
        }

        public void WriteCreatable(hsStream s, plCreatable pCre) {
            if (pCre == null)
                plManagedType.Write(s, plCreatableID.NULL);
            else {
                plManagedType.Write(s, plFactory.ClassIndex(pCre));
                pCre.Write(s, this);
            }
        }

        public void WriteKey(hsStream s, plKey key) {
            if (s.Version.IsPlasma20) {
                if (key == null) {
                    s.WriteBool(false);
                    return;
                } else {
                    s.WriteBool(true);
                    key.Uoid.Write(s);
                }
            } else {
                if (key == null)
                    new plUoid().Write(s); // Invalid...
                else
                    key.Uoid.Write(s);
            }
        }

        public void WriteUoid(hsStream s, plKey uoid) {
            if (uoid == null) {
                plUoid nil = new plUoid();
                nil.Write(s);
            } else
                uoid.Uoid.Write(s);
        }
    }

    public class plResManager : hsResMgr {
        class HoldingKey {
            public plKey fKey;
            public int fOffset;
            public int fSize;
            public hsStream fStream;
        }

        public event plOperationProgress OnProgress;
        public event plOperationDescriptor OnProgressStart;

        plKeyCollector fKeyCollector = new plKeyCollector();
        Dictionary<plKey, hsKeyedObject> fObjects = new Dictionary<plKey, hsKeyedObject>();
        List<HoldingKey> fHeldKeys = new List<HoldingKey>();
        List<plPageInfo> fPages = new List<plPageInfo>();
        

        /// <summary>
        /// Gets a list of all Registry Pages that we have read in or created
        /// </summary>
        public List<plPageInfo> Pages {
            get { return fPages; }
        }

        public plKey FindKey(plLocation loc, plCreatableID type, string name) {
            return fKeyCollector.FindKey(loc, type, name);
        }

        internal override plKey ReRegister(plUoid uoid) {
            plKey key = fKeyCollector.FindKey(uoid);
            if (key == null) {
                key = new plKey(uoid);
                fKeyCollector.AddKey(key);
            }

            return key;
        }

        public IList<plKey> GetKeys(plLocation loc) {
            return fKeyCollector.GetKeys(loc);
        }

        public IList<plKey> GetKeys(plLocation loc, plCreatableID type) {
            return fKeyCollector.GetKeys(loc, type);
        }

        public IList<plCreatableID> GetTypes(plLocation loc) {
            return fKeyCollector.GetTypes(loc);
        }

        private List<HoldingKey> IReadKeyring(hsStream s, plLocation loc) {
            List<HoldingKey> keyring = new List<HoldingKey>();

            int types = s.ReadInt();
            for (int i = 0; i < types; i++) {
                plCreatableID type = plManagedType.Read(s);
                if (s.Version >= plVersion.MystOnline) {
                    s.ReadInt();  // Size until the next type
                    s.ReadByte(); // Flags???
                }

                int count = s.ReadInt();

                // Some optimizations
                keyring.Capacity += count;
                fKeyCollector.Reserve(loc, type, count);

                for (int j = 0; j < count; j++) {
                    HoldingKey key = new HoldingKey();
                    key.fKey = ReadUoid(s);
                    key.fOffset = s.ReadInt();
                    key.fSize = s.ReadInt();
                    key.fStream = s;

                    keyring.Add(key);
                }
            }

            plDebugLog.GetLog("ResManager").Debug(String.Format("\t* Keyring: {0} Keys", keyring.Count));
            return keyring;
        }

        private void IReadHoldingKey(HoldingKey key) {
            // Prep the source stream...
            hsStream s = key.fStream;
            s.Seek(key.fOffset);

            // Let's dump this object into a protected stream
            MemoryStream ms = new MemoryStream();
            ms.Write(s.ReadBytes(key.fSize), 0, key.fSize);
            ms.Position = 0;

            //Now read the creatable from that protected stream
            hsStream pStream = new hsStream(ms);
            pStream.Version = s.Version;
            plCreatable pCre = ReadCreatable(pStream);

            // Do we need to use a plHexBuffer (unimplemented data)
            if (pCre == null) {
                plHexBuffer buf = new plHexBuffer();
                buf.Read(pStream, this);
                fObjects.Add(key.fKey, buf);

                plDebugLog.GetLog("ResManager").Warn(String.Format(
                    "Unimplemented KeyedObject '{0}' in {1}",
                    plManagedType.ClassName(key.fKey.ClassType),
                    key.fKey.Location));
            } else
                fObjects.Add(key.fKey, (hsKeyedObject)pCre);

            //Need to warn about read/size mismatch?
            if (ms.Length > ms.Position)
                plDebugLog.GetLog("ResManager").Warn(
                    String.Format("Read Error: {0} has {1} bytes left over", key.fKey.ObjectName, (ms.Length - ms.Position)));

            // Clean up
            pStream.Close();
            ms.Close();
        }

        /// <summary>
        /// Attempt to read in an object whose Key is being held by the ResManager
        /// </summary>
        /// <param name="key">The Key of the object to read</param>
        /// <returns>The object read in. NULL if the Key is not being held or is no longer in the collection.</returns>
        public hsKeyedObject ReadObject(plKey key) {
            if (fObjects.ContainsKey(key)) {
                plDebugLog.GetLog("ResManager").Warn("Redundant attempt to ReadObject: " + key.ToString());
                return fObjects[key];
            }

            for (int i = 0; i < fHeldKeys.Count; i++)
                if (fHeldKeys[i].fKey.Equals(key)) {
                    IReadHoldingKey(fHeldKeys[i]);
                    fHeldKeys.RemoveAt(i);
                    return fObjects[key];
                }

            // Still here?
            plDebugLog.GetLog("ResManager").Error("Tried to ReadObject whose key is not being held: " + key.ToString());
            return null;
        }

        /// <summary>
        /// Reads in a Plasma Registry Page
        /// </summary>
        /// <param name="filename">Path to the Plasma Registry Page</param>
        /// <param name="readObjects">Whether or not we should read in the KeyedObjects</param>
        /// <returns>PageInfo representing this Plasma Registry Page</returns>
        public plPageInfo ReadPage(string filename, bool readObjects = false) {
            return ReadPage(filename, readObjects, true);
        }

        /// <summary>
        /// Reads in a Plasma Registry Page
        /// </summary>
        /// <param name="filename">Path to the Plasma Registry Page</param>
        /// <param name="readObjects">Whether or not we should read in the KeyedObjects</param>
        /// <param name="progress">Whether or not this should dispatch a plOperationProgress event</param>
        /// <returns>PageInfo representing this Plasma Registry Page</returns>
        public plPageInfo ReadPage(string filename, bool readObjects = false, bool progress = true) {
            plDebugLog.GetLog("ResManager").Debug("Reading PRP: " + Path.GetFileName(filename));
            hsStream s = new hsStream(filename);

            plPageInfo info = new plPageInfo();
            info.Read(s);
            fPages.Add(info);

            plDebugLog.GetLog("ResManager").Debug("\t* Plasma Version: " + s.Version.ToString());
            plDebugLog.GetLog("ResManager").Debug("\t* Location: " + info.Location.ToString());
            //if (fVersion == null)
                fVersion = s.Version;

            s.Seek(info.IndexStart);
            List<HoldingKey> keys = IReadKeyring(s, info.Location);

            if (!readObjects) {
                //Add them to the held list and go away!
                //Note: Don't close the stream! It will be
                //      used later to read the objects!
                fHeldKeys.AddRange(keys);
            } else {
                if (OnProgressStart != null && progress)
                    OnProgressStart(plOperationType.ReadingPageObjects, info.GetFilename(s.Version));

                for (int i = 0; i < keys.Count; i++) {
                    IReadHoldingKey(keys[i]);
                    if (OnProgress != null)
                        OnProgress(keys.Count, i + 1);
                }
            }

            return info;
        }

        public List<plPageInfo> ReadPages(string[] pages, bool readObjects = false) {
            if (OnProgressStart != null)
                OnProgressStart(plOperationType.ReadingPages, String.Format("Reading {0} Registry Pages", pages.Length));

            List<plPageInfo> retval = new List<plPageInfo>();
            for (int i = 0; i < pages.Length; i++) {
                plPageInfo info = ReadPage(pages[i], readObjects, false);
                retval.Add(info);

                if (OnProgress != null)
                    OnProgress(pages.Length, i + 1);
            }

            return retval;
        }
    }
}

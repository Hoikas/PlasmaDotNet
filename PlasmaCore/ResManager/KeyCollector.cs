using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plKeyCollector {
        class KeyList : IList<plKey> {

            Dictionary<int, plKey> fKeys = new Dictionary<int, plKey>();

            public int IndexOf(plKey item) {
                foreach (KeyValuePair<int, plKey> key in fKeys) {
                    if (key.Value.Equals(item))
                        return key.Key;
                }

                return -1;
            }

            public void Insert(int index, plKey item) {
                fKeys.Add(index, item);
            }

            public void RemoveAt(int index) {
                if (fKeys.ContainsKey(index))
                    fKeys[index] = null;
            }

            public plKey this[int index] {
                get {
                    if (fKeys.ContainsKey(index))
                        return fKeys[index];
                    else
                        return null;
                }
                set {
                    if (fKeys.ContainsKey(index))
                        fKeys[index] = value;
                    else
                        fKeys.Add(index, value);
                }
            }

            public void Add(plKey item) {
                fKeys.Add((int)item.ObjectID, item);
            }

            public ReadOnlyCollection<plKey> AsReadOnly() {
                return fKeys.Values.ToList().AsReadOnly();
            }

            public void Clear() {
                fKeys.Clear();
            }

            public bool Contains(plKey item) {
                return fKeys.ContainsValue(item);
            }

            public void CopyTo(plKey[] array, int arrayIndex) {
                // Lazy
                throw new NotImplementedException();
            }

            public int Count {
                get { return fKeys.Count; }
            }

            public bool IsReadOnly {
                get { return false; }
            }

            public bool Remove(plKey item) {
                int remove = -1;
                foreach (KeyValuePair<int, plKey> key in fKeys) {
                    if (key.Value.Equals(item)) {
                        remove = key.Key; ;
                        break;
                    }
                }

                if (remove != -1)
                    fKeys[remove] = null;
                return (remove != -1);
            }

            public IEnumerator<plKey> GetEnumerator() {
                return fKeys.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return fKeys.Values.GetEnumerator();
            }
        }

        Dictionary<plLocation, Dictionary<plCreatableID, KeyList>> fDict =
            new Dictionary<plLocation, Dictionary<plCreatableID, KeyList>>();

        public void AddKey(plKey key) {
            if (!fDict.ContainsKey(key.Location))
                fDict.Add(key.Location, new Dictionary<plCreatableID, KeyList>());
            if (!fDict[key.Location].ContainsKey(key.ClassType))
                fDict[key.Location].Add(key.ClassType, new KeyList());

            KeyList temp = fDict[key.Location][key.ClassType];
            if (key.Uoid.fObjectID.HasValue) {
                if (temp.Count > key.ObjectID &&
                    temp[(int)key.ObjectID] != null)
                    throw new plResMgrException("Failed to add Key to collection: duplicate object ID");
                temp.Insert((int)key.ObjectID, key);
            } else {
                key.Uoid.fObjectID = (uint)temp.Count;
                temp.Add(key);
            }
        }

        public void DropKeys() {
            fDict.Clear();
        }

        public void DropKeys(plLocation t) {
            if (fDict.ContainsKey(t)) {
                fDict[t].Clear();
                fDict.Remove(t);
            }
        }

        internal plKey FindKey(plUoid uoid) {
            if (!fDict.ContainsKey(uoid.fLocation))
                return null;
            if (!fDict[uoid.fLocation].ContainsKey(uoid.fClassType))
                return null;

            if (uoid.fObjectID.HasValue)
                if (fDict[uoid.fLocation][uoid.fClassType].Count < uoid.fObjectID)
                    return null;
                else
                    return fDict[uoid.fLocation][uoid.fClassType][(int)uoid.fObjectID];
            else {
                // Ugly, slow search :(
                foreach (plKey key in fDict[uoid.fLocation][uoid.fClassType])
                    if (key.Uoid.Equals(uoid))
                        return key;
            }

            return null;
        }

        public plKey FindKey(plLocation loc, plCreatableID type, string name) {
            if (!fDict.ContainsKey(loc))
                return null;
            if (!fDict[loc].ContainsKey(type))
                return null;

            // Slow search :(
            foreach (plKey key in fDict[loc][type])
                if (key.ObjectName == name) 
                    return key;

            return null;
        }

        public ReadOnlyCollection<plKey> GetKeys(plLocation loc) {
            if (!fDict.ContainsKey(loc)) 
                return new List<plKey>().AsReadOnly();

            List<plKey> keys = new List<plKey>();
            foreach (KeyList entry in fDict[loc].Values)
                keys.AddRange(entry);
            return keys.AsReadOnly();
        }

        public ReadOnlyCollection<plKey> GetKeys(plLocation loc, plCreatableID type) {
            if (!fDict.ContainsKey(loc))
                return new ReadOnlyCollection<plKey>(null);
            if (!fDict[loc].ContainsKey(type))
                return new ReadOnlyCollection<plKey>(null);
            return fDict[loc][type].AsReadOnly();
        }

        /// <summary>
        /// Gets all the types we have keys for
        /// </summary>
        /// <param name="loc">The location to search in</param>
        /// <returns>A list of Managed Types</returns>
        public ReadOnlyCollection<plCreatableID> GetTypes(plLocation loc) {
            if (!fDict.ContainsKey(loc)) 
                return new List<plCreatableID>().AsReadOnly();
            List<plCreatableID> list = new List<plCreatableID>(fDict[loc].Keys);
            return list.AsReadOnly();
        }

        /// <summary>
        /// Increases the capacity of a collector to prevent excessive resizing of internal collections
        /// </summary>
        /// <param name="loc">The Location to reserve space in</param>
        /// <param name="count">The number of Keys to make room for</param>
        public void Reserve(plLocation loc, plCreatableID type, int count) {
            /*
            if (!fDict.ContainsKey(loc))
                fDict.Add(loc, new Dictionary<plCreatableID, List<plKey>>());
            if (!fDict[loc].ContainsKey(type))
                fDict[loc].Add(type, new List<plKey>(count));
            else if (fDict[loc][type].Capacity < count)
                fDict[loc][type].Capacity = count;
            */
        }
    }
}

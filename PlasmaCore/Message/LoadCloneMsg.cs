using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plLoadCloneMsg : plMessage {

        plKey fCloneKey;
        plKey fRequestorKey;
        uint fOriginatingPlayerID, fUserData;
        bool fValidMsg, fIsLoading;
        plMessage fTriggerMsg;

        #region Properties
        public plKey Clone {
            get { return fCloneKey; }
            set { fCloneKey = value; }
        }

        public bool IsLoading {
            get { return fIsLoading; }
            set { fIsLoading = value; }
        }

        public uint PlayerID {
            get { return fOriginatingPlayerID; }
            set { fOriginatingPlayerID = value; }
        }

        public plKey Requestor {
            get { return fRequestorKey; }
            set { fRequestorKey = value; }
        }

        public plMessage Trigger {
            get { return fTriggerMsg; }
            set { fTriggerMsg = value; }
        }

        public uint UserData {
            get { return fUserData; }
            set { fUserData = value; }
        }

        public bool Valid {
            get { return fValidMsg; }
            set { fValidMsg = value; }
        }
        #endregion

        public plLoadCloneMsg() {
            fBCastFlags |= plBCastFlags.kNetPropagate;
        }

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);

            fCloneKey = mgr.ReadKey(s);
            fRequestorKey = mgr.ReadKey(s);
            fOriginatingPlayerID = s.ReadUInt();
            fUserData = s.ReadUInt();
            fValidMsg = s.ReadBool();
            fIsLoading = s.ReadBool();

            plCreatable tMsg = mgr.ReadCreatable(s);
            if (tMsg is plMessage)
                fTriggerMsg = (plMessage)tMsg;
            else if (tMsg != null)
                plDebugLog.GetLog("ResManager").Warn(
                    String.Format("plLoadCloneMsg: TriggerMsg should be a plMessage, but we got a '{0}'",
                    plFactory.ClassName(tMsg)));
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            base.Write(s, mgr);

            mgr.WriteKey(s, fCloneKey);
            mgr.WriteKey(s, fRequestorKey);
            s.WriteUInt(fOriginatingPlayerID);
            s.WriteUInt(fUserData);
            s.WriteBool(fValidMsg);
            s.WriteBool(fIsLoading);
            mgr.WriteCreatable(s, fTriggerMsg);
        }
    }

    public class plLoadAvatarMsg : plLoadCloneMsg {

        bool fIsPlayer;
        plKey fSpawnPoint;
        plAvTask fInitialTask;
        string fUserStr;

        #region Properties
        public bool IsPlayer {
            get { return fIsPlayer; }
            set { fIsPlayer = value; }
        }

        public plKey SpawnPoint {
            get { return fSpawnPoint; }
            set { fSpawnPoint = value; }
        }

        public plAvTask Task {
            get { return fInitialTask; }
            set { fInitialTask = value; }
        }
        #endregion

        public plLoadAvatarMsg() : base() {
            fBCastFlags |= plBCastFlags.kLocalPropagate;
        }

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);

            fIsPlayer = s.ReadBool();
            fSpawnPoint = mgr.ReadKey(s);

            if (s.ReadBool()) {
                plCreatable pCre = mgr.ReadCreatable(s);
                if (pCre is plAvTask)
                    fInitialTask = (plAvTask)pCre;
                else if (pCre != null)
                    plDebugLog.GetLog("ResManager").Warn(
                        String.Format("plLoadAvatarMsg: InitialTask should be a plAvTask, but we got a '{0}'",
                        plFactory.ClassName(pCre)));
            }

            if (s.Version.IsMystOnline)
                fUserStr = s.ReadSafeString();
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            base.Write(s, mgr);

            s.WriteBool(fIsPlayer);
            mgr.WriteKey(s, fSpawnPoint);

            // Cyan is really quite stupid sometimes...
            if (fInitialTask == null)
                s.WriteBool(false);
            else {
                s.WriteBool(true);
                mgr.WriteCreatable(s, fInitialTask);
            }

            if (s.Version.IsMystOnline)
                s.WriteSafeString(fUserStr);
        }
    }
}

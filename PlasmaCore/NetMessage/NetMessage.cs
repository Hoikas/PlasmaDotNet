using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public abstract class plNetMessage : plCreatable {

        [Flags]
        protected enum BitVectorFlags {
            kHasTimeSent = 0x1,
            kHasGameMsgRecvrs = 0x2,
            kEchoBackToSender = 0x4,
            kRequestP2P = 0x8,
            kAllowTimeOut = 0x10,
            kIndirectMember = 0x20,
            kPublicIPClient = 0x40,
            kHasContext = 0x80,
            kAskVaultForGameState = 0x100,
            kHasTransactionID = 0x200,
            kNewSDLState = 0x400,
            kInitialAgeStateRequest = 0x800,
            kHasPlayerID = 0x1000,
            kUseRelevanceRegions = 0x2000,
            kHasAcctUUID = 0x4000,
            kInterAgeRouting = 0x8000,
            kHasVersion = 0x10000,
            kIsSystemMessage = 0x20000,
            kNeedsReliableSend = 0x40000,
            kRouteToAllPlayers = 0x80000
        }

        protected BitVectorFlags fFlags;
        byte fVerMajor = 12, fVerMinor = 6;
        plUnifiedTime fTimeSent;
        uint fContext, fTransID, fPlayerID;
        Guid fAcctUUID;

        #region Flag Boolean Properties
        public bool HasAccount {
            get { return ((fFlags & BitVectorFlags.kHasAcctUUID) != 0); }
        }

        public bool HasContext {
            get { return ((fFlags & BitVectorFlags.kHasContext) != 0); }
            set {
                if (value)
                    fFlags |= BitVectorFlags.kHasContext;
                else
                    fFlags &= ~BitVectorFlags.kHasContext;
            }
        }

        public bool HasPlayerID {
            get { return ((fFlags & BitVectorFlags.kHasPlayerID) != 0); }
            set {
                if (value)
                    fFlags |= BitVectorFlags.kHasPlayerID;
                else
                    fFlags &= ~BitVectorFlags.kHasPlayerID;
            }
        }

        public bool HasTimeSent {
            get { return ((fFlags & BitVectorFlags.kHasTimeSent) != 0); }
        }

        public bool HasTransID {
            get { return ((fFlags & BitVectorFlags.kHasTransactionID) != 0); }
            set {
                if (value)
                    fFlags |= BitVectorFlags.kHasTransactionID;
                else
                    fFlags &= ~BitVectorFlags.kHasTransactionID;
            }
        }

        public bool HasVersion {
            get { return ((fFlags & BitVectorFlags.kHasVersion) != 0); }
            set {
                if (value)
                    fFlags |= BitVectorFlags.kHasVersion;
                else
                    fFlags &= ~BitVectorFlags.kHasVersion;
            }
        }

        public bool InterAgeRouting {
            get { return ((fFlags & BitVectorFlags.kInterAgeRouting) != 0); }
            set {
                if (value)
                    fFlags |= BitVectorFlags.kInterAgeRouting;
                else
                    fFlags &= ~BitVectorFlags.kInterAgeRouting;
            }
        }

        public bool IsSystemMessage {
            get { return ((fFlags & BitVectorFlags.kIsSystemMessage) != 0); }
            set {
                if (value)
                    fFlags |= BitVectorFlags.kIsSystemMessage;
                else
                    fFlags &= ~BitVectorFlags.kIsSystemMessage;
            }
        }

        public bool NeedsReliableSend {
            get { return ((fFlags & BitVectorFlags.kNeedsReliableSend) != 0); }
            set {
                if (value)
                    fFlags |= BitVectorFlags.kNeedsReliableSend;
                else
                    fFlags &= ~BitVectorFlags.kNeedsReliableSend;
            }
        }

        /// <summary>
        /// Gets or sets whether or not this message should be sent to only
        /// to clients in regions relevant to the one I'm in
        /// </summary>
        /// <seealso cref="plRelevanceRegion"/>
        public bool UseRelRegions {
            get { return ((fFlags & BitVectorFlags.kUseRelevanceRegions) != 0); }
            set {
                if (value)
                    fFlags |= BitVectorFlags.kUseRelevanceRegions;
                else
                    fFlags &= ~BitVectorFlags.kUseRelevanceRegions;
            }
        }
        #endregion

        #region Properties
        public Guid Account {
            get { return fAcctUUID; }
            set {
                if (value == Guid.Empty && fAcctUUID != Guid.Empty)
                    fFlags &= ~BitVectorFlags.kHasAcctUUID;
                else if (value != Guid.Empty && fAcctUUID == Guid.Empty)
                    fFlags |= BitVectorFlags.kHasAcctUUID;

                fAcctUUID = value;
            }
        }

        public uint Context {
            get { return fContext; }
            set { 
                fContext = value;
                HasContext = true;
            }
        }

        public uint PlayerID {
            get { return fPlayerID; }
            set {
                fPlayerID = value;
                HasPlayerID = true;
            }
        }

        public plUnifiedTime TimeSent {
            get { return fTimeSent; }
            set {
                if (value == null && fTimeSent != null)
                    fFlags &= ~BitVectorFlags.kHasTimeSent;
                else if (value != null && fTimeSent == null)
                    fFlags |= BitVectorFlags.kHasTimeSent;

                fTimeSent = value;
            }
        }

        public uint TransID {
            get { return fTransID; }
            set {
                fTransID = value;
                HasTransID = true;
            }
        }
        #endregion

        public override void Read(hsStream s, plResManager mgr) {
            fFlags = (BitVectorFlags)s.ReadInt();

            if (HasVersion) {
                fVerMajor = s.ReadByte();
                fVerMinor = s.ReadByte();
            }

            if (HasTimeSent)
                fTimeSent = new plUnifiedTime(s);
            if (HasContext)
                fContext = s.ReadUInt();
            if (HasTransID)
                fTransID = s.ReadUInt();
            if (HasPlayerID)
                fPlayerID = s.ReadUInt();
            if (HasAccount)
                fAcctUUID = new Guid(s.ReadBytes(16));
        }

        public override void Write(hsStream s, plResManager mgr) {
            s.WriteInt((int)fFlags);

            if (HasVersion) {
                s.WriteByte(fVerMajor);
                s.WriteByte(fVerMinor);
            }

            if (HasTimeSent)
                fTimeSent.Write(s);
            if (HasContext)
                s.WriteUInt(fContext);
            if (HasTransID)
                s.WriteUInt(fTransID);
            if (HasPlayerID)
                s.WriteUInt(fPlayerID);
            if (HasAccount)
                s.WriteBytes(fAcctUUID.ToByteArray());
        }
    }
}

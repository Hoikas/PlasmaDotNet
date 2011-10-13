using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public abstract class plMessage : plCreatable {

        [Flags]
        protected enum plBCastFlags {
            kBCastNone = 0x0,
            kBCastByType = 0x1,
            kBCastUNUSED_0 = 0x2,
            kPropagateToChildren = 0x4,
            kBCastByExactType = 0x8,
            kPropagateToModifiers = 0x10,
            kClearAfterBCast = 0x20,
            kNetPropagate = 0x40,
            kNetSent = 0x80,
            kNetUseRelevanceRegions = 0x100,
            kNetForce = 0x200,
            kNetNonLocal = 0x400,
            kLocalPropagate = 0x800,
            kNetNonDeterministic = 0x200,
            kMsgWatch = 0x1000,
            kNetStartCascade = 0x2000,
            kNetAllowInterAge = 0x4000,
            kNetSendUnreliable = 0x8000,
            kCCRSendToAllPlayers = 0x10000,
            kNetCreatedRemotely = 0x20000
        }

        plKey fSender;
        List<plKey> fReceivers = new List<plKey>();
        double fTimeStamp;
        protected plBCastFlags fBCastFlags = plBCastFlags.kLocalPropagate;

        #region Bit Flag Properties
        public bool InterAgeRouting {
            get { return fBCastFlags.HasFlag(plBCastFlags.kNetAllowInterAge); }
            set {
                if (value)
                    fBCastFlags |= plBCastFlags.kNetAllowInterAge;
                else
                    fBCastFlags &= ~plBCastFlags.kNetAllowInterAge;
            }
        }

        public bool NetForce {
            get { return fBCastFlags.HasFlag(plBCastFlags.kNetForce); }
            set {
                if (value)
                    fBCastFlags |= plBCastFlags.kNetForce;
                else
                    fBCastFlags &= ~plBCastFlags.kNetForce;
            }
        }

        public bool NetPropagate {
            get { return fBCastFlags.HasFlag(plBCastFlags.kNetPropagate); }
            set {
                if (value)
                    fBCastFlags |= plBCastFlags.kNetPropagate;
                else
                    fBCastFlags &= ~plBCastFlags.kNetPropagate;
            }
        }

        /// <summary>
        /// Gets or sets whether or not this message should be sent to only
        /// to clients in regions relevant to the one I'm in
        /// </summary>
        /// <seealso cref="plRelevanceRegion"/>
        public bool UseRelRegions {
            get { return fBCastFlags.HasFlag(plBCastFlags.kNetUseRelevanceRegions); }
            set {
                if (value)
                    fBCastFlags |= plBCastFlags.kNetUseRelevanceRegions;
                else
                    fBCastFlags &= ~plBCastFlags.kNetUseRelevanceRegions;
            }
        }
        #endregion

        #region Member Properties
        public plKey Sender {
            get { return fSender; }
            set { fSender = value; }
        }

        public List<plKey> Receivers {
            get { return fReceivers; }
        }

        public double TimeStamp {
            get { return fTimeStamp; }
            set { fTimeStamp = value; }
        }
        #endregion

        public override void Read(hsStream s, hsResMgr mgr) {
            fSender = mgr.ReadKey(s);
            fReceivers.Capacity = s.ReadInt();
            for (int i = 0; i < fReceivers.Capacity; i++)
                fReceivers.Add(mgr.ReadKey(s));
            if (s.Version.IsPlasma20)
                fTimeStamp = s.ReadDouble();
            fBCastFlags = (plBCastFlags)s.ReadInt();
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            mgr.WriteKey(s, fSender);
            s.WriteInt(fReceivers.Count);
            for (int i = 0; i < fReceivers.Count; i++)
                mgr.WriteKey(s, fReceivers[i]);
            if (s.Version.IsPlasma20)
                s.WriteDouble(fTimeStamp);
            s.WriteInt((int)fBCastFlags);
        }
    }
}

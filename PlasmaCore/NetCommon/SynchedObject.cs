using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public abstract class plSynchedObject : hsKeyedObject {

        #region Enumerations
        [Flags]
        enum Flags {
            kDontDirty = 0x1,
            kSendReliably = 0x2,
            kHasConstantNetGroup = 0x4,
            kDontSynchGameMessages = 0x8,
            kExcludePersistentState = 0x10,
            kExcludeAllPersistentState = 0x20,
            kLocalOnly = kExcludeAllPersistentState | kDontSynchGameMessages,
            kHasVolatileState = 0x40,
            kAllStateIsVolatile = 0x80
        }

        enum States {
            Physical,
            AGMaster,
            Responder,
            Clothing,
            Avatar,
            AvatarPhysical,
            Layer,
            Sound,
            XRegion,
            MorphSequence,
            ParticleSystem,
            CloneMessage,
        }
        #endregion

        Flags fSynchFlags;
        List<States> fExcludeStates = new List<States>();
        List<States> fVolatileStates = new List<States>();

        #region Flag Properties
        private bool ExcludeStates {
            get { return fSynchFlags.HasFlag(Flags.kExcludePersistentState); }
            set {
                if (value && !ExcludeStates)
                    fSynchFlags |= Flags.kExcludePersistentState;
                else if (!value && ExcludeStates)
                    fSynchFlags &= ~Flags.kExcludePersistentState;
            }
        }

        private bool VolatileStates {
            get { return fSynchFlags.HasFlag(Flags.kHasVolatileState); }
            set {
                if (value && !VolatileStates)
                    fSynchFlags |= Flags.kHasVolatileState;
                else if (!value && VolatileStates)
                    fSynchFlags &= ~Flags.kHasVolatileState;
            }
        }
        #endregion

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);

            fSynchFlags = (Flags)s.ReadInt();
            if (s.Version.IsPlasma21)
                fSynchFlags &= (Flags)~0x8;

            if (s.Version.IsPlasma21 && (((int)fSynchFlags & 0x06) != 0) ||
               (s.Version.IsPlasma20 && ExcludeStates)) {
                short count = s.ReadShort();
                for (short i = 0; i < count; i++)
                    fExcludeStates.Add((States)Enum.Parse(typeof(States), s.ReadStdString()));
            }

            //Plasma 2.1+ ends here...
            if (s.Version.IsPlasma21) {
                fSynchFlags = 0; // Synch Flags are pretty useless in Plasma21
                return;
            } else if (s.Version.IsPlasma20) {
                if (VolatileStates) {
                    short count = s.ReadShort();
                    for (short i = 0; i < count; i++)
                        fVolatileStates.Add((States)Enum.Parse(typeof(States), s.ReadStdString()));
                }
            }
        }
    }
}

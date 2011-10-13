using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public static class plKeySeed {

        static plKey fAvatarMgr = new plKey(plCreatableID.plAvatarMgr, "kAvatarMgr_KEY");
        static plKey fClient    = new plKey(plCreatableID.plClient, "kClient_KEY");
        static plKey fNetCliMgr = new plKey(plCreatableID.plNetClientMgr, "kNetClientMgr_KEY");

        public static plKey AvatarMgr {
            get { return fAvatarMgr; }
        }

        public static plKey Client {
            get { return fClient; }
        }

        public static plKey NetClientMgr {
            get { return fNetCliMgr; }
        }
    }
}

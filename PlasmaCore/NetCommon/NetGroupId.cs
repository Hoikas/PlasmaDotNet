using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public struct plNetGroupId {

        enum NetGroupConstants {
            kNetGroupConstant = 0x1,
            kNetGroupLocal = 0x2
        }

        plLocation fLocation;
        NetGroupConstants fFlags;

        public void Read(hsStream s) {
            fLocation = new plLocation();
            fLocation.Read(s);
            fFlags = (NetGroupConstants)s.ReadByte();
        }
    }

    public class plNetGroupInfo {

        plNetGroupId fGroupID = new plNetGroupId();
        bool fOwnIt;

        public void Read(hsStream s) {
            fGroupID.Read(s);
            fOwnIt = s.ReadBool();
        }
    }
}

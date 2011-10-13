using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    /// <summary>
    /// Quick-and-dirty Plasma resource manager for network applications
    /// </summary>
    /// <remarks>
    /// Unlike plResManager, pnResManager does not track keys nor even attempt
    /// to ensure that they will ever pass an equality test. We also will not hold
    /// any resources. The purpose of this class is to provide fast key, uoid, and creatable
    /// read/writes without that overhead.
    /// </remarks>
    public class pnResManager : hsResMgr {

        static pnResManager fInstance;

        /// <summary>
        /// Gets a singleton
        /// </summary>
        public static pnResManager Instance {
            get {
                if (fInstance == null)
                    fInstance = new pnResManager();
                return fInstance;
            }
        }
    }
}

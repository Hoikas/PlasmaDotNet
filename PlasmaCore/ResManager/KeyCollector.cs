using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plKeyCollector {

        Dictionary<plLocation, List<plKey>> fDict = new Dictionary<plLocation, List<plKey>>();

        public void AddKey(plKey key) {
            if (!fDict.ContainsKey(key.Location))
                fDict.Add(key.Location, new List<plKey>());
            fDict[key.Location].Add(key);
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

            foreach (plKey key in fDict[uoid.fLocation]) {
                if (key.Uoid.Equals(uoid))
                    return key;
            }

            return null;
        }

        public plKey FindKey(plLocation loc, ushort type, string name) {
            if (!fDict.ContainsKey(loc))
                return null;

            foreach (plKey key in fDict[loc])
                if (key.ClassType == type &&
                    key.ObjectName == name) return key;

            return null;
        }

        public List<plKey> GetKeys(plLocation loc) {
            if (!fDict.ContainsKey(loc)) return new List<plKey>();
            else return fDict[loc];
        }

        public List<plKey> GetKeys(plLocation loc, ushort t) {
            List<plKey> keys = new List<plKey>();
            if (!fDict.ContainsKey(loc)) return keys;
            foreach (plKey key in fDict[loc])
                if (key.ClassType.Equals(t))
                    keys.Add(key);
            return keys;
        }

        /// <summary>
        /// Gets all the types we have keys for
        /// </summary>
        /// <param name="loc">The location to search in</param>
        /// <returns>A list of Managed Types</returns>
        public List<ushort> GetTypes(plLocation loc) {
            List<ushort> types = new List<ushort>();
            if (!fDict.ContainsKey(loc)) return types;
            foreach (plKey key in fDict[loc])
                if (!types.Contains(key.ClassType))
                    types.Add(key.ClassType);
            return types;
        }

        /// <summary>
        /// Increases the capacity of a collector to prevent excessive resizing of internal collections
        /// </summary>
        /// <param name="loc">The Location to reserve space in</param>
        /// <param name="count">The number of Keys to make room for</param>
        public void Reserve(plLocation loc, int count) {
            if (fDict.ContainsKey(loc))
                fDict[loc].Capacity += count;
            else
                fDict.Add(loc, new List<plKey>(count));
        }
    }
}

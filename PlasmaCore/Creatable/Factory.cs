using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public static class plFactory {
        public static plCreatableID ClassIndex(plCreatable pCre) {
            string name = ClassName(pCre);
            if (Enum.IsDefined(typeof(plCreatableID), name))
                return (plCreatableID)Enum.Parse(typeof(plCreatableID), name);
            else
                throw new plFactoryException("plCreatable not in list of types!");
        }

        /// <summary>
        /// Gets the ***version-specific*** plCreatable Type ID for a given Creatable
        /// </summary>
        /// <param name="pCre">The Creatable to get the ID of</param>
        /// <param name="v">The Plasma Version to get the ID for</param>
        /// <returns>Unmanaged Creatable Type</returns>
        public static ushort ClassIndex(plCreatable pCre, plVersion v) {
            string name = ClassName(pCre);
            if (Enum.IsDefined(typeof(plCreatableID), name))
                return plManagedType.ClassIndex(name, v);
            else
                throw new plFactoryException("plCreatable not in list of types!");
        }

        public static string ClassName(plCreatable pCre) {
            return pCre.GetType().ToString().Substring(7);
        }

        /// <summary>
        /// Creates an instance of a plCreatable with the given internal type
        /// </summary>
        /// <param name="type">The internal/universal plCreatableID of the plCreatable to create</param>
        /// <returns>The new plCreatable</returns>
        /// <exception cref="plFacotoryException">The CreatableID (type) is invalid/unknown</exception>
        public static plCreatable Create(plCreatableID type) {
            switch (type) {
                case plCreatableID.plAvatarInputStateMsg:
                    return new plAvatarInputStateMsg();
                case plCreatableID.plClientGuid:
                    return new plClientGuid();
                case plCreatableID.plConsoleMsg:
                    return new plConsoleMsg();
                case plCreatableID.pfKIMsg:
                    return new pfKIMsg();
                case plCreatableID.plLinkEffectsTriggerMsg:
                    return new plLinkEffectsTriggerMsg();
                case plCreatableID.plLinkEffectsTriggerPrepMsg:
                    return new plLinkEffectsTriggerPrepMsg();
                case plCreatableID.plLoadAvatarMsg:
                    return new plLoadAvatarMsg();
                case plCreatableID.plLoadCloneMsg:
                    return new plLoadCloneMsg();
                case plCreatableID.plNetMsgGameMessage:
                    return new plNetMsgGameMessage();
                case plCreatableID.plNetMsgGameMessageDirected:
                    return new plNetMsgGameMessageDirected();
                case plCreatableID.plNetMsgGroupOwner:
                    return new plNetMsgGroupOwner();
                case plCreatableID.plNetMsgLoadClone:
                    return new plNetMsgLoadClone();
                case plCreatableID.plNetMsgMemberInfoHelper:
                    return new plNetMsgMemberInfoHelper();
                case plCreatableID.plNetMsgMembersList:
                    return new plNetMsgMembersList();
                case plCreatableID.plNetMsgPing:
                    return new plNetMsgPing();
                case plCreatableID.plNetMsgSDLState:
                    return new plNetMsgSDLState();
                case plCreatableID.plNetMsgSDLStateBCast:
                    return new plNetMsgSDLStateBCast();
                case plCreatableID.plNetMsgVoice:
                    return new plNetMsgVoice();
                case plCreatableID.plNotifyMsg:
                    return new plNotifyMsg();
                case plCreatableID.plSceneNode:
                    return new plSceneNode();
                case plCreatableID.plSceneObject:
                    return new plSceneObject();
                case plCreatableID.NULL:
                    return null;
                default:
                    if (Enum.IsDefined(typeof(plCreatableID), type)) {
                        string name = plManagedType.ClassName(type);
                        plDebugLog.GetLog("Factory").Warn(
                            String.Format("Couldn't create a(n) {0} because it is not implemented yet",
                            name));
                        throw new NotImplementedException(name);
                    } else
                        throw new plFactoryException("Unknown plCreatableID");
            }
        }
    }
}

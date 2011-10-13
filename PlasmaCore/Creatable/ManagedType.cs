using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public static class plManagedType {

        static Dictionary<plTypeBounds, Type> fTypeMaps;

        public static plCreatableID ClassIndex(string type) {
            try {
                return (plCreatableID)Enum.Parse(typeof(plCreatableID), type);
            } catch (ArgumentException) {
                throw new plFactoryException(String.Format("Internal Type 0x{0} is not defined", type));
            }
        }

        /// <summary>
        /// Translates a given class name to a ***version-specific*** Creatable ID
        /// </summary>
        /// <param name="type">Creatable ID to translate</param>
        /// <param name="v">Plasma Version to translate to</param>
        /// <returns>Translated Creatable ID</returns>
        public static ushort ClassIndex(string type, plVersion v) {
            if (fTypeMaps == null)
                IBuildTypeMaps();

            ushort value = (ushort)plCreatableID.NULL;
            foreach (KeyValuePair<plTypeBounds, Type> kvp in fTypeMaps) {
                if (kvp.Key.UpperBoundary >= v && v >= kvp.Key.LowerBoundary)
                    try {
                        value = (ushort)Enum.Parse(kvp.Value, type);
                    } catch (ArgumentException) {
                        throw new plFactoryException(String.Format("Internal Type 0x{0} is not defined", type));
                    }
            }

            return value;
        }

        public static string ClassName(plCreatableID type) {
            if (Enum.IsDefined(typeof(plCreatableID), type))
                return Enum.GetName(typeof(plCreatableID), type);
            else
                throw new plFactoryException(String.Format("Internal Type 0x{0} is not defined", type.ToString("XXXX")));
        }

        /// <summary>
        /// Gets a friendly, user consumable name for the specified Plasma Type
        /// </summary>
        /// <param name="type">The universal type</param>
        /// <returns>Friendly class name</returns>
        public static string FriendlyClassName(plCreatableID type) {
            switch (type) {
                case plCreatableID.plActivatorConditionalObject:
                    return "Activator Conditional Object";
                case plCreatableID.plAGAnim:
                    return "AG Animation";
                case plCreatableID.plAGMasterMod:
                    return "AG Master Modifier";
                case plCreatableID.plAGModifier:
                    return "AG Modifier";
                case plCreatableID.plATCAnim:
                    return "ATC Animation";
                case plCreatableID.plAudioInterface:
                    return "Audio Interface";
                case plCreatableID.plCameraBrainUru:
                    return "Camera Brain (Uru)";
                case plCreatableID.plCameraBrainUru_Avatar:
                    return "Camera Brain (Avatar)";
                case plCreatableID.plCameraBrainUru_Fixed:
                    return "Camera Brain (Fixed)";
                case plCreatableID.plCameraBrainUru_POA:
                    return "Camera Brain (Point on Avatar)";
                case plCreatableID.plCameraModifier1:
                    return "Camera Modifier";
                case plCreatableID.plCameraRegionDetector:
                    return "Camera Region Detector";
                case plCreatableID.plClusterGroup:
                    return "Cluster Group";
                case plCreatableID.plCoordinateInterface:
                    return "Coordinate Interface";
                case plCreatableID.plCubicEnvironmap:
                    return "Cubic Environment Map";
                case plCreatableID.plDirectShadowMaster:
                    return "Shadow Master (Direct)";
                case plCreatableID.plDirectionalLightInfo:
                    return "Light Info (Directional)";
                case plCreatableID.plDrawableSpans:
                    return "Drawable Spans";
                case plCreatableID.plDrawInterface:
                    return "Draw Interface";
                case plCreatableID.plDynaFootMgr:
                    return "Dynamic Foot Manager";
                case plCreatableID.plDynaRippleMgr:
                    return "Dynamic Ripple Manager";
                case plCreatableID.plDynaRippleVSMgr:
                    return "Dynamic Ripple VS Manager";
                case plCreatableID.plDynamicCamMap:
                    return "Dynamic Camera Map";
                case plCreatableID.plDynamicEnvMap:
                    return "Dynamic Environment Map";
                case plCreatableID.plDynamicTextMap:
                    return "Dynamic Text Map";
                case plCreatableID.plEAXListenerMod:
                    return "EAX Listener Modifier";
                case plCreatableID.plEAXReverbEffect:
                    return "EAX Reverb Effect";
                case plCreatableID.plFacingConditionalObject:
                    return "Facing Conditional Object";
                case plCreatableID.hsGMaterial:
                    return "Material";
                case plCreatableID.plHKPhysical:
                    return "Physical (Havok)";
                case plCreatableID.plInterfaceInfoModifier:
                    return "Interface Info Modifier";
                case plCreatableID.plLadderModifier:
                    return "Ladder Modifier";
                case plCreatableID.plLayerAnimation:
                    return "Layer Animation";
                case plCreatableID.plLineFollowMod:
                    return "Line Follow Modifier";
                case plCreatableID.plLogicModifier:
                    return "Logic Modifier";
                case plCreatableID.plMsgForwarder:
                    return "Message Forwarder";
                case plCreatableID.plMultistageBehMod:
                    return "Multistage Behavior Modifier";
                case plCreatableID.plNPCSpawnMod:
                    return "NPC Spawn Modifier";
                case plCreatableID.plODEPhysical:
                    return "Physical (ODE)";
                case plCreatableID.plObjectInBoxConditionalObject:
                    return "Object in Box Conditional Object";
                case plCreatableID.plObjectInVolumeAndFacingDetector:
                    return "Object in Volume and Facing Detector";
                case plCreatableID.plObjectInVolumeDetector:
                    return "Object in Volume Detector";
                case plCreatableID.plOmniLightInfo:
                    return "Light Info (Omni)";
                case plCreatableID.plOneShotMod:
                    return "One Shot Modifier";
                case plCreatableID.plPanicLinkRegion:
                    return "Panic Link Region";
                case plCreatableID.plParticleFlockEffect:
                    return "Particle Effect (Flock)";
                case plCreatableID.plParticleSystem:
                    return "Particle System";
                case plCreatableID.plPhysicalSndGroup:
                    return "Physical Sound Group";
                case plCreatableID.plPickingDetector:
                    return "Picking Detector";
                case plCreatableID.plPointShadowMaster:
                    return "Shadow Master (Point)";
                case plCreatableID.plPXPhysical:
                    return "Physical (PhysX)";
                case plCreatableID.plPythonFileMod:
                    return "Python File Modifier";
                case plCreatableID.plPythonFileModConditionalObject:
                    return "Python File Conditional Object";
                case plCreatableID.plRailCameraMod:
                    return "Rail Camera Modifier";
                case plCreatableID.plRandomSoundMod:
                    return "Random Sound Modifier";
                case plCreatableID.plRelevanceRegion:
                    return "Relevance Region";
                case plCreatableID.plResponderModifier:
                    return "Responder Modifier";
                case plCreatableID.plSceneNode:
                    return "Scene Node";
                case plCreatableID.plSceneObject:
                    return "Scene Object";
                case plCreatableID.plShadowCaster:
                    return "Shadow Caster";
                case plCreatableID.plSimulationInterface:
                    return "Simulation Interface";
                case plCreatableID.plSoftVolumeComplex:
                    return "Soft Volume (Complex)";
                case plCreatableID.plSoftVolumeIntersect:
                    return "Soft Volume (Intersect)";
                case plCreatableID.plSoftVolumeInvert:
                    return "Soft Volume (Invert)";
                case plCreatableID.plSoftVolumeSimple:
                    return "Soft Volume (Simple)";
                case plCreatableID.plSoftVolumeUnion:
                    return "Soft Volume (Union)";
                case plCreatableID.plSoundBuffer:
                    return "Sound Buffer";
                case plCreatableID.plSpawnMod:
                case plCreatableID.plSpawnModifier:
                    return "Spawn Modifier";
                case plCreatableID.plSpotLightInfo:
                    return "Light Info (Spot)";
                case plCreatableID.plSwimCircularCurrentRegion:
                    return "Swim Current Region (Circular)";
                case plCreatableID.plSwimStraightCurrentRegion:
                    return "Swim Current Region (Circular)";
                case plCreatableID.plSwimDetector:
                    return "Swim Detector";
                case plCreatableID.plSwimRegionInterface:
                    return "Swim Region Interface";
                case plCreatableID.plViewFaceModifier:
                    return "View Face Modifier";
                case plCreatableID.plVisRegion:
                    return "Visibility Region";
                case plCreatableID.plVolumeSensorConditionalObject:
                    return "Volume Sensor Conditional Object";
                case plCreatableID.plVolumeSensorConditionalObjectNoArbitration:
                    return "Volume Sensor Conditional Object w/o Arbitration";
                case plCreatableID.plWaveSet7:
                    return "Wave Set 7";
                case plCreatableID.plWinAudible:
                    return "Win Audible";
                case plCreatableID.plWin32StaticSound:
                    return "Win32 Static Sound";
                case plCreatableID.plWin32StreamingSound:
                    return "Win32 Streaming Sound";
                default:
                    string ugly = ((plCreatableID)type).ToString();
                    plDebugLog.GetLog("Factory").Warn(String.Format("FIXME: {0} has no friendly name!", ugly));
                    return ugly.Substring(2);
            }
        }

        /// <summary>
        /// Reads in a Version-specific type and returns an internal type identifier
        /// </summary>
        /// <param name="s">The stream to read from</param>
        /// <returns>Internal unique type identifier</returns>
        public static plCreatableID Read(hsStream s) {
            if (fTypeMaps == null)
                IBuildTypeMaps();

            ushort data = s.ReadUShort();
            plCreatableID type = plCreatableID.NULL;

            //Note: We must adjust < PotS IDs...
            if (s.Version < plVersion.PathOfTheShell)
                if (data > (ushort)UruTypes.plAvatarSpawnNotifyMsg)
                    data++;
            //End type adjustment

            foreach (KeyValuePair<plTypeBounds, Type> kvp in fTypeMaps) {
                if (kvp.Key.UpperBoundary >= s.Version && s.Version >= kvp.Key.LowerBoundary) {
                    string value = Enum.GetName(kvp.Value, data);
                    type = (plCreatableID)Enum.Parse(typeof(plCreatableID), value);
                }
            }

            return type;
        }

        public static void Write(hsStream s, plCreatableID data) {
            if (fTypeMaps == null)
                IBuildTypeMaps();

            plCreatableID type = (plCreatableID)data;
            ushort end = (ushort)plCreatableID.NULL;

            foreach (KeyValuePair<plTypeBounds, Type> kvp in fTypeMaps) {
                if (kvp.Key.UpperBoundary >= s.Version && s.Version >= kvp.Key.LowerBoundary) {
                    string value = Enum.GetName(typeof(plCreatableID), type);
                    end = (ushort)Enum.Parse(kvp.Value, value);
                }
            }

            //Note: We must adjust < PotS IDs...
            if (s.Version < plVersion.PathOfTheShell)
                if (end > (ushort)UruTypes.plAvatarSpawnNotifyMsg)
                    end--;
            //End type adjustment

            s.WriteUShort(end);
        }

        private static void IBuildTypeMaps() {
            fTypeMaps = new Dictionary<plTypeBounds, Type>();

            plTypeBounds uru  = new plTypeBounds(plVersion.AgesBeyondMyst, plVersion.PathOfTheShell);
            plTypeBounds live = new plTypeBounds(plVersion.GTLiveBeta, plVersion.MystOnline);
            plTypeBounds eoa  = new plTypeBounds(plVersion.EndOfAges, plVersion.EndOfAges);
            plTypeBounds hex  = new plTypeBounds(plVersion.HexIsle, plVersion.HexIsle);

            fTypeMaps.Add(uru, typeof(UruTypes));
            fTypeMaps.Add(live, typeof(LiveTypes));
            fTypeMaps.Add(eoa, typeof(EoaTypes));
            fTypeMaps.Add(hex, typeof(HexTypes));
        }
    }

    public struct plTypeBounds {

        plVersion fLowerBound;
        plVersion fUpperBound;

        public plVersion LowerBoundary {
            get { return fLowerBound; }
        }

        public plVersion UpperBoundary {
            get { return fUpperBound; }
        }

        public plTypeBounds(plVersion low, plVersion high) {
            fLowerBound = low;
            fUpperBound = high;
        }
    }
}

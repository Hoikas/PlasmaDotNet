using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum plCreatableID : ushort {
        plSceneNode,
        plSceneObject,
        hsKeyedObject,
        plBitmap,
        plMipmap,
        plCubicEnvironmap,
        plLayer,
        hsGMaterial,
        plParticleSystem,
        plParticleEffect,
        plParticleCollisionEffectBeat,
        plParticleFadeVolumeEffect,
        plBoundInterface,
        plRenderTarget,
        plCubicRenderTarget,
        plCubicRenderTargetModifier,
        plObjInterface,
        plAudioInterface,
        plAudible,
        plAudibleNull,
        plWinAudible,
        plCoordinateInterface,
        plDrawInterface,
        plDrawable,
        plAutoWalkRegion,
        plDrawableIce,
        plPhysical,
        plCrossfade,
        plSimulationInterface,
        plParticleFadeOutEffect,
        plModifier,
        plSingleModifier,
        plSimpleModifier,
        plWindBoneMod,
        plCameraBrain_NovicePlus,
        plGrassShaderMod,
        plDetectorModifier,
        pfSubtitleMgr,
        plPythonFileModConditionalObject,
        plMultiModifier,
        plSynchedObject,
        plSoundBuffer,
        plPickingDetector,
        plCollisionDetector,
        plLogicModifier,
        plConditionalObject,
        plANDConditionalObject,
        plORConditionalObject,
        plPickedConditionalObject,
        plActivatorConditionalObject,
        plTimerCallbackManager,
        plKeyPressConditionalObject,
        plAnimationEventConditionalObject,
        plControlEventConditionalObject,
        plObjectInBoxConditionalObject,
        plLocalPlayerInBoxConditionalObject,
        plObjectIntersectPlaneConditionalObject,
        plLocalPlayerIntersectPlaneConditionalObject,
        plLayerTransform,
        plBubbleShaderMod,
        plSpawnModifier,
        plFacingConditionalObject,
        plViewFaceModifier,
        plLayerInterface,
        plLayerAnimation,
        plLayerDepth,
        plLayerMovie,
        plLayerBink,
        plLayerAVI,
        plSound,
        plWin32Sound,
        plLayerOr,
        plAudioSystem,
        plDrawableSpans,
        plDrawablePatchSet,
        plInputManager,
        plLogicModBase,
        plFogEnvironment,
        plLineFollowModBase,
        plLightInfo,
        plDirectionalLightInfo,
        plOmniLightInfo,
        plSpotLightInfo,
        plCameraBrain,
        plClientApp,
        plClient,
        pfGUICreditsCtrl,
        pfGUIClickMapCtrl,
        plListener,
        plAvatarAnim,
        plOccluder,
        plMobileOccluder,
        plLayerShadowBase,
        plLimitedDirLightInfo,
        plAGAnim,
        plAGModifier,
        plAGMasterMod,
        plCameraRegionDetector,
        plLineFollowMod,
        plLightModifier,
        plOmniModifier,
        plSpotModifier,
        plLtdDirModifier,
        plSeekPointMod,
        plOneShotMod,
        plRandomCommandMod,
        plRandomSoundMod,
        plPostEffectMod,
        plObjectInVolumeDetector,
        plResponderModifier,
        plAxisAnimModifier,
        plLayerLightBase,
        plFollowMod,
        plTransitionMgr,
        plLinkEffectsMgr,
        plWin32StreamingSound,
        plActivatorActivatorConditionalObject,
        plSoftVolume,
        plSoftVolumeSimple,
        plSoftVolumeComplex,
        plSoftVolumeUnion,
        plSoftVolumeIntersect,
        plSoftVolumeInvert,
        plWin32LinkSound,
        plLayerLinkAnimation,
        plArmatureMod,
        plWin32StaticSound,
        pfGameGUIMgr,
        pfGUIDialogMod,
        plCameraBrainUru,
        plVirtualCamera,
        plCameraModifier,
        plCameraBrainUru_Drive,
        plCameraBrainUru_Follow,
        plCameraBrainUru_Fixed,
        pfGUIButtonMod,
        plPythonFileMod,
        pfGUIControlMod,
        plExcludeRegionModifier,
        pfGUIDraggableMod,
        plVolumeSensorConditionalObject,
        plVolActivatorConditionalObject,
        plMsgForwarder,
        plBlower,
        pfGUIListBoxMod,
        pfGUITextBoxMod,
        pfGUIEditBoxMod,
        plDynamicTextMap,
        pfGUISketchCtrl,
        pfGUIUpDownPairMod,
        pfGUIValueCtrl,
        pfGUIKnobCtrl,
        plLadderModifier,
        plCameraBrainUru_FirstPerson,
        plCloneSpawnModifier,
        pfGUIDragBarCtrl,
        pfGUICheckBoxCtrl,
        pfGUIRadioGroupCtrl,
        pfGUIDynDisplayCtrl,
        plLayerProject,
        plInputInterfaceMgr,
        plRailCameraMod,
        plMultistageBehMod,
        plCameraBrainUru_Circle,
        plParticleWindEffect,
        plAnimEventModifier,
        plAutoProfile,
        pfGUISkin,
        plAVIWriter,
        plParticleCollisionEffect,
        plParticleCollisionEffectDie,
        plParticleCollisionEffectBounce,
        plInterfaceInfoModifier,
        plSharedMesh,
        plArmatureEffectsMgr,
        plVehicleModifier,
        plParticleLocalWind,
        plParticleUniformWind,
        plInstanceDrawInterface,
        plShadowMaster,
        plShadowCaster,
        plPointShadowMaster,
        plDirectShadowMaster,
        plSDLModifier,
        plPhysicalSDLModifier,
        plAGMasterSDLModifier,
        plLayerSDLModifier,
        plAnimTimeConvertSDLModifier,
        plResponderSDLModifier,
        plSoundSDLModifier,
        plResManagerHelper,
        plArmatureEffect,
        plArmatureEffectFootSound,
        plEAXReverbEffect,
        plDynaDecalMgr,
        plObjectInVolumeAndFacingDetector,
        plDynaFootMgr,
        plDynaRippleMgr,
        plDynaBulletMgr,
        plDecalEnableMod,
        plPrintShape,
        plDynaPuddleMgr,
        pfGUIMultiLineEditCtrl,
        plLayerAnimationBase,
        plLayerSDLAnimation,
        plATCAnim,
        plAgeGlobalAnim,
        plAvatarMgr,
        plSpawnMod,
        plActivePrintShape,
        plExcludeRegionSDLModifier,
        plDynaWakeMgr,
        plWaveSet7,
        plPanicLinkRegion,
        plWin32GroupedSound,
        plFilterCoordInterface,
        plStereizer,
        plShader,
        plDynamicEnvMap,
        plSimpleRegionSensor,
        plMorphSequence,
        plCameraBrain_Novice,
        plDynaRippleVSMgr,
        plWaveSet6,
        pfGUIProgressCtrl,
        plMaintainersMarkerModifier,
        plMorphDataSet,
        plHardRegion,
        plHardRegionPlanes,
        plHardRegionComplex,
        plHardRegionUnion,
        plHardRegionIntersect,
        plHardRegionInvert,
        plVisRegion,
        plVisMgr,
        plRegionBase,
        pfGUIPopUpMenu,
        pfGUIMenuItem,
        plRelevanceRegion,
        plRelevanceMgr,
        pfJournalBook,
        plImageLibMod,
        plParticleFlockEffect,
        plParticleSDLMod,
        plAgeLoader,
        plWaveSetBase,
        pfBookData,
        plDynaTorpedoMgr,
        plDynaTorpedoVSMgr,
        plClusterGroup,
        plLODMipmap,
        plSwimDetector,
        plFadeOpacityMod,
        plFadeOpacityLay,
        plDistOpacityMod,
        plArmatureModBase,
        plDirectMusicSound,
        plParticleFollowSystemEffect,
        plClientSessionMgr,
        plODEPhysical,
        plSDLVarChangeNotifier,
        plInterestWellModifier,
        plElevatorModifier,
        plCameraBrain_Expert,
        plPagingRegionModifier,
        plGuidepathModifier,
        pfNodeMgr,
        plEAXEffect,
        plEAXPitchShifter,
        plIKModifier,
        pfObjectFlocker,
        plCameraBrain_M5,
        plAGAnimBink,
        plDynamicCamMap,
        plTreeShader,
        plNodeRegionModifier,
        plPiranhaRegionModifier,
        plAvBrainPirahna,
        plMessage,
        plRefMsg,
        plTimeMsg,
        plAnimCmdMsg,
        plParticleUpdateMsg,
        plCameraMsg,
        plInputEventMsg,
        plKeyEventMsg,
        plAxisEventMsg,
        plEvalMsg,
        plTransformMsg,
        plControlEventMsg,
        plCrossfadeMsg,
        pfSubtitleMsg,
        plDX9Pipeline,
        plSDLStoreMsg,
        plActivatorMsg,
        plDispatch,
        plReceiver,
        plOmniSqApplicator,
        plController,
        plLeafController,
        plCompoundController,
        plCompoundRotController,
        plCompoundPosController,
        plTMController,
        hsFogControl,
        plCorrectionMsg,
        plPickedMsg,
        plCollideMsg,
        plTriggerMsg,
        plInterestingModMsg,
        plMatrixUpdateMsg,
        plTimerCallbackMsg,
        plEventCallbackMsg,
        plSpawnModMsg,
        plSpawnRequestMsg,
        plLoadCloneMsg,
        plEnableMsg,
        plWarpMsg,
        plAttachMsg,
        pfConsole,
        plRenderMsg,
        plAnimTimeConvert,
        plSoundMsg,
        plInterestingPing,
        plNodeCleanupMsg,
        plSpaceTree,
        plAudioSysMsg,
        plDispatchBase,
        plDeviceRecreateMsg,
        plPipeline,
        plMessageWithCallbacks,
        plClientMsg,
        plSimulationMsg,
        plAvatarMsg,
        plAvTaskMsg,
        plAvSeekMsg,
        plAvOneShotMsg,
        plProxyDrawMsg,
        plSelfDestructMsg,
        plSimInfluenceMsg,
        plForceMsg,
        plOffsetForceMsg,
        plTorqueMsg,
        plImpulseMsg,
        plOffsetImpulseMsg,
        plAngularImpulseMsg,
        plDampMsg,
        plShiftMassMsg,
        plSimStateMsg,
        plFreezeMsg,
        plInitialAgeStateLoadedMsg,
        plAvTaskSeekDoneMsg,
        plSDLModifierMsg,
        plRenderRequestMsg,
        plRenderRequestAck,
        plConvexVolume,
        plParticleGenerator,
        plSimpleParticleGenerator,
        plParticleEmitter,
        plAGChannel,
        plMatrixChannel,
        plMatrixTimeScale,
        plMatrixBlend,
        plMatrixControllerChannel,
        plPointChannel,
        plPointConstant,
        plPointBlend,
        plQuatChannel,
        plQuatConstant,
        plQuatBlend,
        plLinkToAgeMsg,
        plPlayerPageMsg,
        plListenerMsg,
        plAnimPath,
        plNotifyMsg,
        plNodeChangeMsg,
        plLinkCallbackMsg,
        plTransitionMsg,
        plConsoleMsg,
        plVolumeIsect,
        plSphereIsect,
        plConeIsect,
        plCylinderIsect,
        plParallelIsect,
        plConvexIsect,
        plComplexIsect,
        plUnionIsect,
        plIntersectionIsect,
        plModulator,
        plLinkEffectsTriggerMsg,
        plLinkEffectBCMsg,
        plResponderEnableMsg,
        plResponderMsg,
        plOneShotMsg,
        plPointTimeScale,
        plPointControllerChannel,
        plQuatTimeScale,
        plAGApplicator,
        plMatrixChannelApplicator,
        plPointChannelApplicator,
        plLightDiffuseApplicator,
        plLightAmbientApplicator,
        plLightSpecularApplicator,
        plOmniApplicator,
        plQuatChannelApplicator,
        plScalarChannel,
        plScalarTimeScale,
        plScalarBlend,
        plScalarControllerChannel,
        plScalarChannelApplicator,
        plSpotInnerApplicator,
        plSpotOuterApplicator,
        plATCEaseCurve,
        plConstAccelEaseCurve,
        plSplineEaseCurve,
        plReplaceGeometryMsg,
        plDniCoordinateInfo,
        plScalarConstant,
        plMatrixConstant,
        plAGCmdMsg,
        plParticleTransferMsg,
        plParticleKillMsg,
        plExcludeRegionMsg,
        plOneTimeParticleGenerator,
        plParticleApplicator,
        plParticleLifeMinApplicator,
        plParticleLifeMaxApplicator,
        plParticlePPSApplicator,
        plParticleAngleApplicator,
        plParticleVelMinApplicator,
        plParticleVelMaxApplicator,
        plParticleScaleMinApplicator,
        plParticleScaleMaxApplicator,
        plDynamicTextMsg,
        plCameraTargetFadeMsg,
        plAgeLoadedMsg,
        plPointControllerCacheChannel,
        plScalarControllerCacheChannel,
        plLinkEffectsTriggerPrepMsg,
        plLinkEffectPrepBCMsg,
        plAgeLoaderMsg,
        plDISpansMsg,
        plDelayedTransformMsg,
        pfGUINotifyMsg,
        plArmatureBrain,
        plAvBrainAvatar,
        plAvBrainDrive,
        plAvBrainGeneric,
        plAvBrainLadder,
        plInputIfaceMgrMsg,
        pfPythonNotifyMsg,
        plMatrixDelayedCorrectionApplicator,
        plAvPushBrainMsg,
        plAvPopBrainMsg,
        plAvTask,
        plAvTaskDumbSeek,
        plAvTaskBrain,
        plAnimStage,
        plCreatableGenericValue,
        plAvBrainGenericMsg,
        plAvTaskSmartSeek,
        plAGInstanceCallbackMsg,
        plArmatureEffectMsg,
        plArmatureEffectStateMsg,
        plShadowCastMsg,
        plBoundsIsect,
        plResMgrHelperMsg,
        plMultistageModMsg,
        plSoundVolumeApplicator,
        plCutter,
        plBulletMsg,
        plDynaDecalEnableMsg,
        plOmniCutoffApplicator,
        plArmatureUpdateMsg,
        plAvatarFootMsg,
        plParticleFlockMsg,
        plAvatarBehaviorNotifyMsg,
        plATCChannel,
        plScalarSDLChannel,
        plLoadAvatarMsg,
        plAvatarSetTypeMsg,
        plRippleShapeMsg,
        plMatrixDifferenceApp,
        plSetListenerMsg,
        plAvatarStealthModeMsg,
        plEventCallbackInterceptMsg,
        plDynamicEnvMapMsg,
        plClimbMsg,
        plIfaceFadeAvatarMsg,
        plSharedMeshBCMsg,
        plSwimMsg,
        plMorphDelta,
        plMatrixControllerCacheChannel,
        plPipeResMakeMsg,
        plPipeRTMakeMsg,
        plPipeGeoMakeMsg,
        plSimSuppressMsg,
        plAgeBeginLoadingMsg,
        plAvatarOpacityCallbackMsg,
        plAGDetachCallbackMsg,
        pfMovieEventMsg,
        plMovieMsg,
        plPipeTexMakeMsg,
        plCaptureRenderMsg,
        pfClimbingWallMsg,
        plClimbEventMsg,
        plSDLGameTimeElapsedVar,
        plLinkEffectsDoneMsg,
        pfGameGUIMsg,
        pfBackdoorMsg,
        plSDLVar,
        plSDLStructVar,
        plSDLBoolVar,
        plSDLCharVar,
        plSDLByteVar,
        plSDLIntVar,
        plSDLUIntVar,
        plSDLFloatVar,
        plSDLDoubleVar,
        plSDLStringVar,
        plSDLTimeVar,
        plSDLUoidVar,
        plSDLVector3Var,
        plSDLPoint3Var,
        plSDLQuaternionVar,
        plSDLMatrix44Var,
        plSDLRGBAVar,
        plSDLAgeTimeOfDayVar,
        plSDLAgeTimeElapsedVar,
        plSDLMetaDoubleVar,
        plSDLFixedArrayStructVar,
        plSDLFixedArrayBoolVar,
        plSDLFixedArrayCharVar,
        plSDLFixedArrayByteVar,
        plSDLFixedArrayIntVar,
        plSDLFixedArrayUIntVar,
        plSDLFixedArrayFloatVar,
        plSDLFixedArrayDoubleVar,
        plSDLFixedArrayStringVar,
        plSDLFixedArrayTimeVar,
        plSDLFixedArrayUoidVar,
        plSDLFixedArrayVector3Var,
        plSDLFixedArrayPoint3Var,
        plSDLFixedArrayQuaternionVar,
        plSDLFixedArrayMatrix44Var,
        plSDLFixedArrayRGBAVar,
        plSDLDynArrayStructVar,
        plSDLDynArrayBoolVar,
        plSDLDynArrayCharVar,
        plSDLDynArrayByteVar,
        plSDLDynArrayIntVar,
        plSDLDynArrayUIntVar,
        plSDLDynArrayFloatVar,
        plSDLDynArrayDoubleVar,
        plSDLDynArrayStringVar,
        plSDLDynArrayTimeVar,
        plSDLDynArrayUoidVar,
        plSDLDynArrayVector3Var,
        plSDLDynArrayPoint3Var,
        plSDLDynArrayQuaternionVar,
        plSDLDynArrayMatrix44Var,
        plSDLDynArrayRGBAVar,
        plSDLArrayVar,
        plSDLVarChangeMsg,
        plAvBrainPath,
        plSDLBufferVar,
        plSDLFixedArrayBufferVar,
        plSDLDynArrayBufferVar,
        plMatrixBorrowedChannel,
        plNodeRegionMsg,
        plEventCallbackSetupMsg,
        plDXPipeline,
        plRelativeMatrixChannelApplicator,
        plPiranhaRegionMsg,
        plFXMaterial,
        plMovableMod,
        plParticleFaceOutEffect,
        plMaterial,
        plEffect,
        plParticleBulletEffect,
        pfGUIProgrssCtrl,
        plCameraBrain_Ground,
        plPirahnaRegionModifier,
        plCameraBrain_Flight,
        plAnimEvalMsg,
        plAvBrainFlight,
        plAvBrainNPC,
        plAvBrainBlimp,
        plAvBrainFlightNPC,
        plParticleBulletHitMsg,
        pfPanicLinkMsg,
        plAvTaskOneShot,
        plAvatarPhysicsEnableCallbackMsg,
        plDrawableMesh,
        plPhysicalMesh,
        pfSecurePreloader,
        plRandomTMModifier,
        plInterestingModifier,
        plSimplePhysicalMesh,
        plCompoundPhysicalMesh,
        plAliasModifier,
        plPortalDrawable,
        plPortalPhysical,
        plPXPhysical,
        plLayerWrapper,
        plNetApp,
        plNetClientMgr,
        pl2WayWinAudible,
        plLightSpace,
        plNetClientApp,
        plNetServerApp,
        plCompoundTMModifier,
        plCameraBrain_Default,
        plCameraBrain_Drive,
        plCameraBrain_Fixed,
        plCameraBrain_FixedPan,
        plAvatarMod,
        plAvatarAnimMgr,
        plCameraBrain_Avatar,
        plCameraBrain_FP,
        plInventoryMod,
        plInventoryObjMod,
        plPythonMod,
        plCameraBrain_Freelook,
        plHavokConstraintsMod,
        plHingeConstraintMod,
        plWheelConstraintMod,
        plStrongSpringConstraintMod,
        plArmatureLODMod,
        plVirtualCam1,
        plCameraModifier1,
        plCameraBrainUru_POA,
        plCameraBrainUru_Avatar,
        plCameraBrainUru_POAFixed,
        plSittingModifier,
        plAvLadderMod,
        plClothingItem,
        plClothingOutfit,
        plClothingBase,
        plClothingMgr,
        pfPlayerBookMod,
        pfMarkerMgr,
        plClothingSDLModifier,
        plAvatarSDLModifier,
        plPythonSDLModifier,
        plAvatarPhysicalSDLModifier,
        plEAXListenerMod,
        plSubworldRegionDetector,
        plNPCSpawnMod,
        plLOSDispatch,
        plSimulationMgr,
        plCCRMgr,
        plCCRSpecialist,
        plCCRSeniorSpecialist,
        plCCRShiftSupervisor,
        plCCRGameOperator,
        plEmoteAnim,
        plMorphSequenceSDLMod,
        plCoopCoordinator,
        plFont,
        plFontCache,
        plLayerTargetContainer,
        plPhysicalSndGroup,
        plGameMarkerModifier,
        plSwimRegionInterface,
        plSwimCircularCurrentRegion,
        plSwimStraightCurrentRegion,
        plRidingAnimatedPhysicalDetector,
        plVolumeSensorConditionalObjectNoArbitration,
        plObjRefMsg,
        plNodeRefMsg,
        plGenRefMsg,
        plLayRefMsg,
        plMatRefMsg,
        plMouseEventMsg,
        plVaultCCRNode,
        plLOSRequestMsg,
        plLOSHitMsg,
        plSingleModMsg,
        plMultiModMsg,
        plMemberUpdateMsg,
        plNetMsgPagingRoom,
        plMeshRefMsg,
        hsGRenderProcs,
        hsSfxAngleFade,
        hsSfxDistFade,
        hsSfxDistShade,
        hsSfxGlobalShade,
        hsSfxIntenseAlpha,
        hsSfxObjDistFade,
        hsSfxObjDistShade,
        hsDynamicValue,
        hsDynamicScalar,
        hsDynamicColorRGBA,
        hsDynamicMatrix33,
        hsDynamicMatrix44,
        plPreResourceMsg,
        plRotController,
        plPosController,
        plScalarController,
        plPoint3Controller,
        plScaleValueController,
        plQuatController,
        plMatrix33Controller,
        plMatrix44Controller,
        plEaseController,
        plSimpleScaleController,
        plSimpleRotController,
        plSimplePosController,
        plIntRefMsg,
        plCollisionReactor,
        plPhysicalModifier,
        plDebugKeyEventMsg,
        plPhysicalProperties,
        plSimplePhys,
        plCondRefMsg,
        plNetMessage,
        plNetMsgJoinReq,
        plNetMsgJoinAck,
        plNetMsgLeave,
        plNetMsgPing,
        plNetMsgRoomsList,
        plNetMsgGroupOwner,
        plNetMsgGameStateRequest,
        plNetMsgSessionReset,
        plNetMsgOmnibus,
        plNetMsgObject,
        plCCRInvisibleMsg,
        plLinkInDoneMsg,
        plNetMsgGameMessage,
        plNetMsgStream,
        plServerReplyMsg,
        plNetMsgStreamHelper,
        plNetMsgObjectHelper,
        plIMouseXEventMsg,
        plIMouseYEventMsg,
        plIMouseBEventMsg,
        plLogicTriggerMsg,
        plNetMsgVoice,
        plLightRefMsg,
        plNetMsgStreamedObject,
        plNetMsgSharedState,
        plNetMsgTestAndSet,
        plNetMsgGetSharedState,
        plSharedStateMsg,
        plNetGenericServerTask,
        plNetClientMgrMsg,
        plLoadAgeMsg,
        plClientRefMsg,
        plNetMsgObjStateRequest,
        plCCRPetitionMsg,
        plVaultCCRInitializationTask,
        plNetServerMsg,
        plNetServerMsgWithContext,
        plNetServerMsgRegisterServer,
        plNetServerMsgUnregisterServer,
        plNetServerMsgStartProcess,
        plNetServerMsgRegisterProcess,
        plNetServerMsgUnregisterProcess,
        plNetServerMsgFindProcess,
        plNetServerMsgProcessFound,
        plNetMsgRoutingInfo,
        plNetServerSessionInfo,
        plSimulationSynchMsg,
        plHKSimulationSynchMsg,
        plSatisfiedMsg,
        plNetMsgObjectListHelper,
        plNetMsgObjectUpdateFilter,
        plEventGroupMsg,
        plSuspendEventMsg,
        plNetMsgMembersListReq,
        plNetMsgMembersList,
        plNetMsgMemberInfoHelper,
        plNetMsgMemberListHelper,
        plNetMsgMemberUpdate,
        plNetMsgServerToClient,
        plNetMsgCreatePlayer,
        plNetMsgAuthenticateHello,
        plNetMsgAuthenticateChallenge,
        plConnectedToVaultMsg,
        plCCRCommunicationMsg,
        plNetMsgInitialAgeStateSent,
        plNetServerMsgFindServerBase,
        plNetServerMsgFindServerReplyBase,
        plNetServerMsgFindAuthServer,
        plNetServerMsgFindAuthServerReply,
        plNetServerMsgFindVaultServer,
        plNetServerMsgFindVaultServerReply,
        plNCAgeJoinerMsg,
        plNetServerMsgVaultTask,
        plNetMsgVaultTask,
        plAgeLinkStruct,
        plVaultAgeInfoNode,
        plNetMsgStreamableHelper,
        plNetMsgReceiversListHelper,
        plNetMsgListenListUpdate,
        plNetServerMsgPing,
        plNetMsgAlive,
        plNetMsgTerminated,
        plNetMsgSDLState,
        plNetServerMsgSessionReset,
        plCCRBanLinkingMsg,
        plCCRSilencePlayerMsg,
        plNetMember,
        plNetGameMember,
        plNetTransportMember,
        plQuatPointCombine,
        plCmdIfaceModMsg,
        plNetServerMsgPlsUpdatePlayer,
        plClothingUpdateBCMsg,
        plFakeOutMsg,
        plCursorChangeMsg,
        plAvEnableMsg,
        plInventoryMsg,
        plNetServerMsgHello,
        plNetServerMsgHelloReply,
        plNetServerMember,
        plVaultAgeInfoListNode,
        plNetServerMsgServerRegistered,
        plNetServerMsgPlsRoutableMsg,
        plPuppetBrainMsg,
        plVaultAgeInfoInitializationTask,
        plNetServerMsgVaultRequestGameState,
        plNetServerMsgVaultGameState,
        plNetServerMsgVaultGameStateSave,
        plNetServerMsgVaultGameStateSaved,
        plNetServerMsgVaultGameStateLoad,
        plNetClientTask,
        plNetMsgSDLStateBCast,
        plNetServerMsgExitProcess,
        plNetServerMsgSaveGameState,
        plNetMsgGameMessageDirected,
        plLinkOutUnloadMsg,
        plAvatarInputStateMsg,
        plAgeInfoStruct,
        plSDLNotificationMsg,
        plNetClientConnectAgeVaultTask,
        plLinkingMgrMsg,
        plVaultNotifyMsg,
        plPlayerInfo,
        plSwapSpansRefMsg,
        pfKI,
        plNetMsgCreatableHelper,
        plCreatableUuid,
        plNetMsgRequestMyVaultPlayerList,
        plSuperVNodeMgrInitTask,
        plElementRefMsg,
        plClothingMsg,
        plEventGroupEnableMsg,
        plAvBrain,
        plAvBrainHuman,
        plAvBrainCritter,
        plAvBrainSample,
        plPreloaderMsg,
        pfKIMsg,
        plRemoteAvatarInfoMsg,
        plRoomLoadNotifyMsg,
        plAvAnimTask,
        plAvSeekTask,
        plNetCommAuthConnectedMsg,
        plAvOneShotTask,
        plAvEnableTask,
        plNetClientMember,
        plNetClientCommTask,
        plNetServerMsgAuthRequest,
        plNetServerMsgAuthReply,
        plNetClientCommAuthTask,
        plClientGuid,
        plNetMsgVaultPlayerList,
        plNetMsgSetMyActivePlayer,
        plNetServerMsgRequestAccountPlayerList,
        plNetServerMsgAccountPlayerList,
        plNetMsgPlayerCreated,
        plNetServerMsgVaultCreatePlayer,
        plNetServerMsgVaultPlayerCreated,
        plNetMsgFindAge,
        plNetMsgFindAgeReply,
        plNetClientConnectPrepTask,
        plNetClientAuthTask,
        plNetClientGetPlayerVaultTask,
        plNetClientSetActivePlayerTask,
        plNetClientFindAgeTask,
        plNetClientLeaveTask,
        plNetClientJoinTask,
        plNetClientCalibrateTask,
        plNetMsgDeletePlayer,
        plNetServerMsgVaultDeletePlayer,
        plNetCoreStatsSummary,
        plCreatableListHelper,
        plCreatableStream,
        plAvTaskSeek,
        plNetCommAuthMsg,
        plNetCommFileListMsg,
        plNetCommFileDownloadMsg,
        plNetCommLinkToAgeMsg,
        plNetCommPlayerListMsg,
        plNetCommActivePlayerMsg,
        plNetCommCreatePlayerMsg,
        plNetCommDeletePlayerMsg,
        plNetCommPublicAgeListMsg,
        plNetCommPublicAgeMsg,
        plNetCommRegisterAgeMsg,
        plVaultAdminInitializationTask,
        plNetOwnershipMsg,
        plNetMsgRelevanceRegions,
        plNetMsgLoadClone,
        plNetMsgPlayerPage,
        plVNodeInitTask,
        plEventManager,
        plVaultNeighborhoodInitializationTask,
        plNetServerMsgAgentRecoveryRequest,
        plNetServerMsgFrontendRecoveryRequest,
        plNetServerMsgBackendRecoveryRequest,
        plNetServerMsgAgentRecoveryData,
        plNetServerMsgFrontendRecoveryData,
        plNetServerMsgBackendRecoveryData,
        plSubWorldMsg,
        plAvatarSpawnNotifyMsg,
        plVaultGameServerInitializationTask,
        plNetClientFindDefaultAgeTask,
        plVaultAgeNode,
        plVaultAgeInitializationTask,
        plVaultSystemNode,
        plAvBrainSwim,
        plNetMsgVault,
        plNetServerMsgVault,
        plVaultTask,
        plVaultConnectTask,
        plVaultNegotiateManifestTask,
        plVaultFetchNodesTask,
        plVaultSaveNodeTask,
        plVaultFindNodeTask,
        plVaultAddNodeRefTask,
        plVaultRemoveNodeRefTask,
        plVaultSendNodeTask,
        plVaultNotifyOperationCallbackTask,
        plVNodeMgrInitializationTask,
        plVaultPlayerInitializationTask,
        plNetVaultServerInitializationTask,
        plCommonNeighborhoodsInitTask,
        plVaultNodeRef,
        plVaultNode,
        plVaultFolderNode,
        plVaultImageNode,
        plVaultTextNoteNode,
        plVaultSDLNode,
        plVaultAgeLinkNode,
        plVaultChronicleNode,
        plVaultPlayerInfoNode,
        plVaultMgrNode,
        plVaultPlayerNode,
        plSynchEnableMsg,
        plNetVaultServerNode,
        plVaultAdminNode,
        plVaultGameServerNode,
        plVaultPlayerInfoListNode,
        plAvBrainClimb,
        plNetVoiceListMsg,
        plVaultMarkerNode,
        pfMarkerMsg,
        plAvCoopMsg,
        plAvBrainCoop,
        plVaultMarkerListNode,
        plAvTaskOrient,
        plSetNetGroupIDMsg,
        plAIMsg,
        plAIBrainCreatedMsg,
        plStateDataRecord,
        plNetClientCommDeletePlayerTask,
        plNetMsgSetTimeout,
        plNetMsgActivePlayerSet,
        plNetClientCommSetTimeoutTask,
        plNetRoutableMsgOmnibus,
        plNetMsgGetPublicAgeList,
        plNetMsgPublicAgeList,
        plNetMsgCreatePublicAge,
        plNetMsgPublicAgeCreated,
        plNetServerMsgEnvelope,
        plNetClientCommGetPublicAgeListTask,
        plNetClientCommCreatePublicAgeTask,
        plNetServerMsgPendingMsgs,
        plNetServerMsgRequestPendingMsgs,
        plDbInterface,
        plDbProxyInterface,
        plDBGenericSQLDB,
        pfGameMgrMsg,
        pfGameCliMsg,
        pfGameCli,
        pfGmTicTacToe,
        pfGmHeek,
        pfGmMarker,
        pfGmBlueSpiral,
        pfGmClimbingWall,
        plAIArrivedAtGoalMsg,
        pfGmVarSync,
        plNetMsgRemovePublicAge,
        plNetMsgPublicAgeRemoved,
        plNetClientCommRemovePublicAgeTask,
        plCCRMessage,
        plAvOneShotLinkTask,
        plNetAuthDatabase,
        plEventLog,
        plDbEventLog,
        plSyslogEventLog,
        plAgeLoaded2Msg,
        plPseudoLinkEffectMsg,
        plPseudoLinkAnimTriggerMsg,
        plPseudoLinkAnimCallbackMsg,
        plAvBrainQuab,
        plAccountUpdateMsg,
        plLinearVelocityMsg,
        plAngularVelocityMsg,
        plRideAnimatedPhysMsg,
        plAvBrainRideAnimatedPhysical,
        plSimpleTMModifier,
        plHKPhysical,
        plHKSubWorld,
        plPlayerMsg,
        plScaleController,
        plDX8Pipeline,
        plNetLookupServerGetAgeInfoFromVaultTask,
        plNetServerMsgFindAdminServer,
        plServerGuid,
        plAvBrainUser,
        plAvBrainPuppet,
        plAvBlendedSeekTask,
        plNetClientCommLeaveTask,
        plNetMsgAuthenticateResponse,
        plNetMsgAccountAuthenticated,
        plNetClientCommSendPeriodicAliveTask,
        plNetClientCommCheckServerSilenceTask,
        plNetClientCommPingTask,
        plNetClientCommFindAgeTask,
        plNetClientCommSetActivePlayerTask,
        plNetClientCommGetPlayerListTask,
        plNetClientCommCreatePlayerTask,
        plNetClientCommJoinAgeTask,
        plNetMsgPython,
        pfPythonMsg,
        plMySqlDB,
        plNetGenericDatabase,
        plNetVaultDatabase,
        plNetServerMsgPlsUpdatePlayerReply,
        plVaultDisconnectTask,
        plNetClientCommSetAgePublicTask,
        plNetClientCommRegisterOwnedAge,
        plNetClientCommUnregisterOwnerAge,
        plNetClientCommRegisterVisitAge,
        plNetClientCommUnregisterVisitAge,


        NULL = 0x8000,
    }

    enum EoaTypes : ushort {
        /* Keyed */
        plSceneNode,                                     // 0000
        plSceneObject,                                   // 0001
        hsKeyedObject,                                   // 0002
        plBitmap,                                        // 0003
        plMipmap,                                        // 0004
        plCubicEnvironmap,                               // 0005
        plLayer,                                         // 0006
        hsGMaterial,                                     // 0007
        plParticleSystem,                                // 0008
        plParticleEffect,                                // 0009
        plParticleCollisionEffectBeat,                   // 000A
        plParticleFadeVolumeEffect,                      // 000B
        plBoundInterface,                                // 000C
        plRenderTarget,                                  // 000D
        plCubicRenderTarget,                             // 000E
        plCubicRenderTargetModifier,                     // 000F
        plObjInterface,                                  // 0010
        plAudioInterface,                                // 0011
        plAudible,                                       // 0012
        plAudibleNull,                                   // 0013
        plWinAudible,                                    // 0014
        plCoordinateInterface,                           // 0015
        plDrawInterface,                                 // 0016
        plDrawable,                                      // 0017
        plAutoWalkRegion,                                // 0018
        plDrawableIce,                                   // 0019
        plPhysical,                                      // 001A
        plCrossfade,                                     // 001B
        plSimulationInterface,                           // 001C
        plParticleFadeOutEffect,                         // 001D
        plModifier,                                      // 001E
        plSingleModifier,                                // 001F
        plSimpleModifier,                                // 0020
        plWindBoneMod,                                   // 0021
        plCameraBrain_NovicePlus,                        // 0022
        plGrassShaderMod,                                // 0023
        plDetectorModifier,                              // 0024
        pfSubtitleMgr,                                   // 0025
        plPythonFileModConditionalObject,                // 0026
        plMultiModifier,                                 // 0027
        plSynchedObject,                                 // 0028
        plSoundBuffer,                                   // 0029
        plPickingDetector,                               // 002A
        plCollisionDetector,                             // 002B
        plLogicModifier,                                 // 002C
        plConditionalObject,                             // 002D
        plANDConditionalObject,                          // 002E
        plORConditionalObject,                           // 002F
        plPickedConditionalObject,                       // 0030
        plActivatorConditionalObject,                    // 0031
        plTimerCallbackManager,                          // 0032
        plKeyPressConditionalObject,                     // 0033
        plAnimationEventConditionalObject,               // 0034
        plControlEventConditionalObject,                 // 0035
        plObjectInBoxConditionalObject,                  // 0036
        plLocalPlayerInBoxConditionalObject,             // 0037
        plObjectIntersectPlaneConditionalObject,         // 0038
        plLocalPlayerIntersectPlaneConditionalObject,    // 0039
        plLayerTransform,                                // 003A
        plBubbleShaderMod,                               // 003B
        plSpawnModifier,                                 // 003C
        plFacingConditionalObject,                       // 003D
        plViewFaceModifier,                              // 003E
        plLayerInterface,                                // 003F
        plLayerAnimation,                                // 0040
        plLayerDepth,                                    // 0041
        plLayerMovie,                                    // 0042
        plLayerBink,                                     // 0043
        plLayerAVI,                                      // 0044
        plSound,                                         // 0045
        plWin32Sound,                                    // 0046
        plLayerOr,                                       // 0047
        plAudioSystem,                                   // 0048
        plDrawableSpans,                                 // 0049
        plDrawablePatchSet,                              // 004A
        plInputManager,                                  // 004B
        plLogicModBase,                                  // 004C
        plFogEnvironment,                                // 004D
        plLineFollowModBase,                             // 004E
        plLightInfo,                                     // 004F
        plDirectionalLightInfo,                          // 0050
        plOmniLightInfo,                                 // 0051
        plSpotLightInfo,                                 // 0052
        plCameraBrain,                                   // 0053
        plClientApp,                                     // 0054
        plClient,                                        // 0055
        pfGUICreditsCtrl,                                // 0056
        pfGUIClickMapCtrl,                               // 0057
        plListener,                                      // 0058
        plAvatarAnim,                                    // 0059
        plOccluder,                                      // 005A
        plMobileOccluder,                                // 005B
        plLayerShadowBase,                               // 005C
        plLimitedDirLightInfo,                           // 005D
        plAGAnim,                                        // 005E
        plAGModifier,                                    // 005F
        plAGMasterMod,                                   // 0060
        plCameraRegionDetector,                          // 0061
        plLineFollowMod,                                 // 0062
        plLightModifier,                                 // 0063
        plOmniModifier,                                  // 0064
        plSpotModifier,                                  // 0065
        plLtdDirModifier,                                // 0066
        plSeekPointMod,                                  // 0067
        plOneShotMod,                                    // 0068
        plRandomCommandMod,                              // 0069
        plRandomSoundMod,                                // 006A
        plPostEffectMod,                                 // 006B
        plObjectInVolumeDetector,                        // 006C
        plResponderModifier,                             // 006D
        plAxisAnimModifier,                              // 006E
        plLayerLightBase,                                // 006F
        plFollowMod,                                     // 0070
        plTransitionMgr,                                 // 0071
        plLinkEffectsMgr,                                // 0072
        plWin32StreamingSound,                           // 0073
        plActivatorActivatorConditionalObject,           // 0074
        plSoftVolume,                                    // 0075
        plSoftVolumeSimple,                              // 0076
        plSoftVolumeComplex,                             // 0077
        plSoftVolumeUnion,                               // 0078
        plSoftVolumeIntersect,                           // 0079
        plSoftVolumeInvert,                              // 007A
        plWin32LinkSound,                                // 007B
        plLayerLinkAnimation,                            // 007C
        plArmatureMod,                                   // 007D
        plWin32StaticSound,                              // 007E
        pfGameGUIMgr,                                    // 007F
        pfGUIDialogMod,                                  // 0080
        plCameraBrainUru,                                // 0081
        plVirtualCamera,                                 // 0082
        plCameraModifier,                                // 0083
        plCameraBrainUru_Drive,                          // 0084
        plCameraBrainUru_Follow,                         // 0085
        plCameraBrainUru_Fixed,                          // 0086
        pfGUIButtonMod,                                  // 0087
        plPythonFileMod,                                 // 0088
        pfGUIControlMod,                                 // 0089
        plExcludeRegionModifier,                         // 008A
        pfGUIDraggableMod,                               // 008B
        plVolumeSensorConditionalObject,                 // 008C
        plVolActivatorConditionalObject,                 // 008D
        plMsgForwarder,                                  // 008E
        plBlower,                                        // 008F
        pfGUIListBoxMod,                                 // 0090
        pfGUITextBoxMod,                                 // 0091
        pfGUIEditBoxMod,                                 // 0092
        plDynamicTextMap,                                // 0093
        pfGUISketchCtrl,                                 // 0094
        pfGUIUpDownPairMod,                              // 0095
        pfGUIValueCtrl,                                  // 0096
        pfGUIKnobCtrl,                                   // 0097
        plLadderModifier,                                // 0098
        plCameraBrainUru_FirstPerson,                    // 0099
        plCloneSpawnModifier,                            // 009A
        pfGUIDragBarCtrl,                                // 009B
        pfGUICheckBoxCtrl,                               // 009C
        pfGUIRadioGroupCtrl,                             // 009D
        pfGUIDynDisplayCtrl,                             // 009E
        plLayerProject,                                  // 009F
        plInputInterfaceMgr,                             // 00A0
        plRailCameraMod,                                 // 00A1
        plMultistageBehMod,                              // 00A2
        plCameraBrainUru_Circle,                         // 00A3
        plParticleWindEffect,                            // 00A4
        plAnimEventModifier,                             // 00A5
        plAutoProfile,                                   // 00A6
        pfGUISkin,                                       // 00A7
        plAVIWriter,                                     // 00A8
        plParticleCollisionEffect,                       // 00A9
        plParticleCollisionEffectDie,                    // 00AA
        plParticleCollisionEffectBounce,                 // 00AB
        plInterfaceInfoModifier,                         // 00AC
        plSharedMesh,                                    // 00AD
        plArmatureEffectsMgr,                            // 00AE
        plVehicleModifier,                               // 00AF
        plParticleLocalWind,                             // 00B0
        plParticleUniformWind,                           // 00B1
        plInstanceDrawInterface,                         // 00B2
        plShadowMaster,                                  // 00B3
        plShadowCaster,                                  // 00B4
        plPointShadowMaster,                             // 00B5
        plDirectShadowMaster,                            // 00B6
        plSDLModifier,                                   // 00B7
        plPhysicalSDLModifier,                           // 00B8
        plAGMasterSDLModifier,                           // 00B9
        plLayerSDLModifier,                              // 00BA
        plAnimTimeConvertSDLModifier,                    // 00BB
        plResponderSDLModifier,                          // 00BC
        plSoundSDLModifier,                              // 00BD
        plResManagerHelper,                              // 00BE
        plArmatureEffect,                                // 00BF
        plArmatureEffectFootSound,                       // 00C0
        plEAXReverbEffect,                               // 00C1
        plDynaDecalMgr,                                  // 00C2
        plObjectInVolumeAndFacingDetector,               // 00C3
        plDynaFootMgr,                                   // 00C4
        plDynaRippleMgr,                                 // 00C5
        plDynaBulletMgr,                                 // 00C6
        plDecalEnableMod,                                // 00C7
        plPrintShape,                                    // 00C8
        plDynaPuddleMgr,                                 // 00C9
        pfGUIMultiLineEditCtrl,                          // 00CA
        plLayerAnimationBase,                            // 00CB
        plLayerSDLAnimation,                             // 00CC
        plATCAnim,                                       // 00CD
        plAgeGlobalAnim,                                 // 00CE
        plAvatarMgr,                                     // 00CF
        plSpawnMod,                                      // 00D0
        plActivePrintShape,                              // 00D1
        plExcludeRegionSDLModifier,                      // 00D2
        plDynaWakeMgr,                                   // 00D3
        plWaveSet7,                                      // 00D4
        plPanicLinkRegion,                               // 00D5
        plWin32GroupedSound,                             // 00D6
        plFilterCoordInterface,                          // 00D7
        plStereizer,                                     // 00D8
        plShader,                                        // 00D9
        plDynamicEnvMap,                                 // 00DA
        plSimpleRegionSensor,                            // 00DB
        plMorphSequence,                                 // 00DC
        plCameraBrain_Novice,                            // 00DD
        plDynaRippleVSMgr,                               // 00DE
        plWaveSet6,                                      // 00DF
        pfGUIProgressCtrl,                               // 00E0
        plMaintainersMarkerModifier,                     // 00E1
        plMorphDataSet,                                  // 00E2
        plHardRegion,                                    // 00E3
        plHardRegionPlanes,                              // 00E4
        plHardRegionComplex,                             // 00E5
        plHardRegionUnion,                               // 00E6
        plHardRegionIntersect,                           // 00E7
        plHardRegionInvert,                              // 00E8
        plVisRegion,                                     // 00E9
        plVisMgr,                                        // 00EA
        plRegionBase,                                    // 00EB
        pfGUIPopUpMenu,                                  // 00EC
        pfGUIMenuItem,                                   // 00ED
        plRelevanceRegion,                               // 00EE
        plRelevanceMgr,                                  // 00EF
        pfJournalBook,                                   // 00F0
        plImageLibMod,                                   // 00F1
        plParticleFlockEffect,                           // 00F2
        plParticleSDLMod,                                // 00F3
        plAgeLoader,                                     // 00F4
        plWaveSetBase,                                   // 00F5
        pfBookData,                                      // 00F6
        plDynaTorpedoMgr,                                // 00F7
        plDynaTorpedoVSMgr,                              // 00F8
        plClusterGroup,                                  // 00F9
        plLODMipmap,                                     // 00FA
        plSwimDetector,                                  // 00FB
        plFadeOpacityMod,                                // 00FC
        plFadeOpacityLay,                                // 00FD
        plDistOpacityMod,                                // 00FE
        plArmatureModBase,                               // 00FF
        plDirectMusicSound,                              // 0100
        plParticleFollowSystemEffect,                    // 0101
        plClientSessionMgr,                              // 0102
        plODEPhysical,                                   // 0103
        plSDLVarChangeNotifier,                          // 0104
        plInterestWellModifier,                          // 0105
        plElevatorModifier,                              // 0106
        plCameraBrain_Expert,                            // 0107
        plPagingRegionModifier,                          // 0108
        plGuidepathModifier,                             // 0109
        pfNodeMgr,                                       // 010A
        plEAXEffect,                                     // 010B
        plEAXPitchShifter,                               // 010C
        plIKModifier,                                    // 010D
        pfObjectFlocker,                                 // 010E
        plCameraBrain_M5,                                // 010F
        plAGAnimBink,                                    // 0110
        plDynamicCamMap,                                 // 0111
        plTreeShader,                                    // 0112
        plNodeRegionModifier,                            // 0113
        plPiranhaRegionModifier,                         // 0114

        /* Non-Keyed */
        plAvBrainPirahna = 0x0200,                       // 0200
        plMessage,                                       // 0201
        plRefMsg,                                        // 0202
        plTimeMsg,                                       // 0203
        plAnimCmdMsg,                                    // 0204
        plParticleUpdateMsg,                             // 0205
        plCameraMsg,                                     // 0206
        plInputEventMsg,                                 // 0207
        plKeyEventMsg,                                   // 0208
        plAxisEventMsg,                                  // 0209
        plEvalMsg,                                       // 020A
        plTransformMsg,                                  // 020B
        plControlEventMsg,                               // 020C
        plCrossfadeMsg,                                  // 020D
        pfSubtitleMsg,                                   // 020E
        plDX9Pipeline,                                   // 020F
        plSDLStoreMsg,                                   // 0210
        plActivatorMsg,                                  // 0211
        plDispatch,                                      // 0212
        plReceiver,                                      // 0213
        plOmniSqApplicator,                              // 0214
        plController = 0x0219,                           // 0219
        plLeafController,                                // 021A
        plCompoundController,                            // 021B
        plCompoundRotController = 0x0227,                // 0227
        plCompoundPosController = 0x0229,                // 0229
        plTMController,                                  // 022A
        hsFogControl,                                    // 022B
        plCorrectionMsg,                                 // 022C
        plPickedMsg,                                     // 022D
        plCollideMsg,                                    // 022E
        plTriggerMsg,                                    // 022F
        plInterestingModMsg,                             // 0230
        plMatrixUpdateMsg,                               // 0231
        plTimerCallbackMsg,                              // 0232
        plEventCallbackMsg,                              // 0233
        plSpawnModMsg,                                   // 0234
        plSpawnRequestMsg,                               // 0235
        plLoadCloneMsg,                                  // 0236
        plEnableMsg,                                     // 0237
        plWarpMsg,                                       // 0238
        plAttachMsg,                                     // 0239
        pfConsole,                                       // 023A
        plRenderMsg,                                     // 023B
        plAnimTimeConvert,                               // 023C
        plSoundMsg,                                      // 023D
        plInterestingPing,                               // 023E
        plNodeCleanupMsg,                                // 023F
        plSpaceTree,                                     // 0240
        plAudioSysMsg,                                   // 0241
        plDispatchBase,                                  // 0242
        plDeviceRecreateMsg,                             // 0243
        plPipeline,                                      // 0244
        plMessageWithCallbacks = 0x0246,                 // 0246
        plClientMsg,                                     // 0247
        plSimulationMsg,                                 // 0248
        plAvatarMsg,                                     // 0249
        plAvTaskMsg,                                     // 024A
        plAvSeekMsg,                                     // 024B
        plAvOneShotMsg,                                  // 024C
        plProxyDrawMsg,                                  // 024D
        plSelfDestructMsg,                               // 024E
        plSimInfluenceMsg,                               // 024F
        plForceMsg,                                      // 0250
        plOffsetForceMsg,                                // 0251
        plTorqueMsg,                                     // 0252
        plImpulseMsg,                                    // 0253
        plOffsetImpulseMsg,                              // 0254
        plAngularImpulseMsg,                             // 0255
        plDampMsg,                                       // 0256
        plShiftMassMsg,                                  // 0257
        plSimStateMsg,                                   // 0258
        plFreezeMsg,                                     // 0259
        plInitialAgeStateLoadedMsg,                      // 025A
        plAvTaskSeekDoneMsg,                             // 025B
        plSDLModifierMsg,                                // 025C
        plRenderRequestMsg,                              // 025D
        plRenderRequestAck,                              // 025E
        plConvexVolume,                                  // 025F
        plParticleGenerator,                             // 0260
        plSimpleParticleGenerator,                       // 0261
        plParticleEmitter,                               // 0262
        plAGChannel,                                     // 0263
        plMatrixChannel,                                 // 0264
        plMatrixTimeScale,                               // 0265
        plMatrixBlend,                                   // 0266
        plMatrixControllerChannel,                       // 0267
        plPointChannel,                                  // 0268
        plPointConstant,                                 // 0269
        plPointBlend,                                    // 026A
        plQuatChannel,                                   // 026B
        plQuatConstant,                                  // 026C
        plQuatBlend,                                     // 026D
        plLinkToAgeMsg,                                  // 026E
        plPlayerPageMsg,                                 // 026F
        plListenerMsg,                                   // 0270
        plAnimPath,                                      // 0271
        plNotifyMsg,                                     // 0272
        plNodeChangeMsg,                                 // 0273
        plLinkCallbackMsg,                               // 0274
        plTransitionMsg,                                 // 0275
        plConsoleMsg,                                    // 0276
        plVolumeIsect,                                   // 0277
        plSphereIsect,                                   // 0278
        plConeIsect,                                     // 0279
        plCylinderIsect,                                 // 027A
        plParallelIsect,                                 // 027B
        plConvexIsect,                                   // 027C
        plComplexIsect,                                  // 027D
        plUnionIsect,                                    // 027E
        plIntersectionIsect,                             // 027F
        plModulator,                                     // 0280
        plLinkEffectsTriggerMsg,                         // 0281
        plLinkEffectBCMsg,                               // 0282
        plResponderEnableMsg,                            // 0283
        plResponderMsg,                                  // 0284
        plOneShotMsg,                                    // 0285
        plPointTimeScale,                                // 0286
        plPointControllerChannel,                        // 0287
        plQuatTimeScale,                                 // 0288
        plAGApplicator,                                  // 0289
        plMatrixChannelApplicator,                       // 028A
        plPointChannelApplicator,                        // 028B
        plLightDiffuseApplicator,                        // 028C
        plLightAmbientApplicator,                        // 028D
        plLightSpecularApplicator,                       // 028E
        plOmniApplicator,                                // 028F
        plQuatChannelApplicator,                         // 0290
        plScalarChannel,                                 // 0291
        plScalarTimeScale,                               // 0292
        plScalarBlend,                                   // 0293
        plScalarControllerChannel,                       // 0294
        plScalarChannelApplicator,                       // 0295
        plSpotInnerApplicator,                           // 0296
        plSpotOuterApplicator,                           // 0297
        plATCEaseCurve,                                  // 0298
        plConstAccelEaseCurve,                           // 0299
        plSplineEaseCurve,                               // 029A
        plReplaceGeometryMsg,                            // 029B
        plDniCoordinateInfo,                             // 029C
        plScalarConstant,                                // 029D
        plMatrixConstant,                                // 029E
        plAGCmdMsg,                                      // 029F
        plParticleTransferMsg,                           // 02A0
        plParticleKillMsg,                               // 02A1
        plExcludeRegionMsg,                              // 02A2
        plOneTimeParticleGenerator,                      // 02A3
        plParticleApplicator,                            // 02A4
        plParticleLifeMinApplicator,                     // 02A5
        plParticleLifeMaxApplicator,                     // 02A6
        plParticlePPSApplicator,                         // 02A7
        plParticleAngleApplicator,                       // 02A8
        plParticleVelMinApplicator,                      // 02A9
        plParticleVelMaxApplicator,                      // 02AA
        plParticleScaleMinApplicator,                    // 02AB
        plParticleScaleMaxApplicator,                    // 02AC
        plDynamicTextMsg,                                // 02AD
        plCameraTargetFadeMsg,                           // 02AE
        plAgeLoadedMsg,                                  // 02AF
        plPointControllerCacheChannel,                   // 02B0
        plScalarControllerCacheChannel,                  // 02B1
        plLinkEffectsTriggerPrepMsg,                     // 02B2
        plLinkEffectPrepBCMsg,                           // 02B3
        plAgeLoaderMsg,                                  // 02B4
        plDISpansMsg,                                    // 02B5
        plDelayedTransformMsg,                           // 02B6
        pfGUINotifyMsg,                                  // 02B7
        plArmatureBrain,                                 // 02B8
        plAvBrainAvatar,                                 // 02B9
        plAvBrainDrive,                                  // 02BA
        plAvBrainGeneric,                                // 02BB
        plAvBrainLadder,                                 // 02BC
        plInputIfaceMgrMsg,                              // 02BD
        pfPythonNotifyMsg,                               // 02BE
        plMatrixDelayedCorrectionApplicator,             // 02BF
        plAvPushBrainMsg,                                // 02C0
        plAvPopBrainMsg,                                 // 02C1
        plAvTask,                                        // 02C2
        plAvTaskDumbSeek,                                // 02C3
        plAvTaskBrain,                                   // 02C4
        plAnimStage,                                     // 02C5
        plCreatableGenericValue,                         // 02C6
        plAvBrainGenericMsg,                             // 02C7
        plAvTaskSmartSeek,                               // 02C8
        plAGInstanceCallbackMsg,                         // 02C9
        plArmatureEffectMsg,                             // 02CA
        plArmatureEffectStateMsg,                        // 02CB
        plShadowCastMsg,                                 // 02CC
        plBoundsIsect,                                   // 02CD
        plResMgrHelperMsg,                               // 02CE
        plMultistageModMsg,                              // 02CF
        plSoundVolumeApplicator,                         // 02D0
        plCutter,                                        // 02D1
        plBulletMsg,                                     // 02D2
        plDynaDecalEnableMsg,                            // 02D3
        plOmniCutoffApplicator,                          // 02D4
        plArmatureUpdateMsg,                             // 02D5
        plAvatarFootMsg,                                 // 02D6
        plParticleFlockMsg,                              // 02D7
        plAvatarBehaviorNotifyMsg,                       // 02D8
        plATCChannel,                                    // 02D9
        plScalarSDLChannel,                              // 02DA
        plLoadAvatarMsg,                                 // 02DB
        plAvatarSetTypeMsg,                              // 02DC
        plRippleShapeMsg,                                // 02DD
        plMatrixDifferenceApp,                           // 02DE
        plSetListenerMsg,                                // 02DF
        plAvatarStealthModeMsg,                          // 02E0
        plEventCallbackInterceptMsg,                     // 02E1
        plDynamicEnvMapMsg,                              // 02E2
        plClimbMsg,                                      // 02E3
        plIfaceFadeAvatarMsg,                            // 02E4
        plSharedMeshBCMsg,                               // 02E5
        plSwimMsg,                                       // 02E6
        plMorphDelta,                                    // 02E7
        plMatrixControllerCacheChannel,                  // 02E8
        plPipeResMakeMsg,                                // 02E9
        plPipeRTMakeMsg,                                 // 02EA
        plPipeGeoMakeMsg,                                // 02EB
        plSimSuppressMsg,                                // 02EC
        plAgeBeginLoadingMsg,                            // 02ED
        plAvatarOpacityCallbackMsg,                      // 02EE
        plAGDetachCallbackMsg,                           // 02EF
        pfMovieEventMsg,                                 // 02F0
        plMovieMsg,                                      // 02F1
        plPipeTexMakeMsg,                                // 02F2
        plCaptureRenderMsg,                              // 02F3
        pfClimbingWallMsg,                               // 02F4
        plClimbEventMsg,                                 // 02F5
        plSDLGameTimeElapsedVar,                         // 02F6
        plLinkEffectsDoneMsg,                            // 02F7
        pfGameGUIMsg,                                    // 02F8
        pfBackdoorMsg,                                   // 02F9
        plSDLVar,                                        // 02FA
        plSDLStructVar,                                  // 02FB
        plSDLBoolVar,                                    // 02FC
        plSDLCharVar,                                    // 02FD
        plSDLByteVar,                                    // 02FE
        plSDLIntVar,                                     // 02FF
        plSDLUIntVar,                                    // 0300
        plSDLFloatVar,                                   // 0301
        plSDLDoubleVar,                                  // 0302
        plSDLStringVar,                                  // 0303
        plSDLTimeVar,                                    // 0304
        plSDLUoidVar,                                    // 0305
        plSDLVector3Var,                                 // 0306
        plSDLPoint3Var,                                  // 0307
        plSDLQuaternionVar,                              // 0308
        plSDLMatrix44Var,                                // 0309
        plSDLRGBAVar,                                    // 030A
        plSDLAgeTimeOfDayVar,                            // 030B
        plSDLAgeTimeElapsedVar,                          // 030C
        plSDLMetaDoubleVar,                              // 030D
        plSDLFixedArrayStructVar,                        // 030E
        plSDLFixedArrayBoolVar,                          // 030F
        plSDLFixedArrayCharVar,                          // 0310
        plSDLFixedArrayByteVar,                          // 0311
        plSDLFixedArrayIntVar,                           // 0312
        plSDLFixedArrayUIntVar,                          // 0313
        plSDLFixedArrayFloatVar,                         // 0314
        plSDLFixedArrayDoubleVar,                        // 0315
        plSDLFixedArrayStringVar,                        // 0316
        plSDLFixedArrayTimeVar,                          // 0317
        plSDLFixedArrayUoidVar,                          // 0318
        plSDLFixedArrayVector3Var,                       // 0319
        plSDLFixedArrayPoint3Var,                        // 031A
        plSDLFixedArrayQuaternionVar,                    // 031B
        plSDLFixedArrayMatrix44Var,                      // 031C
        plSDLFixedArrayRGBAVar,                          // 031D
        plSDLDynArrayStructVar,                          // 031E
        plSDLDynArrayBoolVar,                            // 031F
        plSDLDynArrayCharVar,                            // 0320
        plSDLDynArrayByteVar,                            // 0321
        plSDLDynArrayIntVar,                             // 0322
        plSDLDynArrayUIntVar,                            // 0323
        plSDLDynArrayFloatVar,                           // 0324
        plSDLDynArrayDoubleVar,                          // 0325
        plSDLDynArrayStringVar,                          // 0326
        plSDLDynArrayTimeVar,                            // 0327
        plSDLDynArrayUoidVar,                            // 0328
        plSDLDynArrayVector3Var,                         // 0329
        plSDLDynArrayPoint3Var,                          // 032A
        plSDLDynArrayQuaternionVar,                      // 032B
        plSDLDynArrayMatrix44Var,                        // 032C
        plSDLDynArrayRGBAVar,                            // 032D
        plSDLArrayVar,                                   // 032E
        plSDLVarChangeMsg,                               // 032F
        plAvBrainPath,                                   // 0330
        plSDLBufferVar,                                  // 0331
        plSDLFixedArrayBufferVar,                        // 0332
        plSDLDynArrayBufferVar,                          // 0333
        plMatrixBorrowedChannel,                         // 0334
        plNodeRegionMsg,                                 // 0335
        plEventCallbackSetupMsg,                         // 0336
        plDXPipeline,                                    // 0337
        plRelativeMatrixChannelApplicator,               // 0338
        plPiranhaRegionMsg,                              // 0339

        NULL = 0x8000,
    }

    enum HexTypes : ushort {
        /* Keyed */
        plSceneNode,                                     // 0000
        plSceneObject,                                   // 0001
        hsKeyedObject,                                   // 0002
        plBitmap,                                        // 0003
        plMipmap,                                        // 0004
        plCubicEnvironmap,                               // 0005
        plLayer,                                         // 0006
        hsGMaterial,                                     // 0007
        plParticleSystem,                                // 0008
        plParticleEffect,                                // 0009
        plParticleCollisionEffectBeat,                   // 000A
        plParticleFadeVolumeEffect,                      // 000B
        plBoundInterface,                                // 000C
        plRenderTarget,                                  // 000D
        plCubicRenderTarget,                             // 000E
        plCubicRenderTargetModifier,                     // 000F
        plObjInterface,                                  // 0010
        plAudioInterface,                                // 0011
        plAudible,                                       // 0012
        plAudibleNull,                                   // 0013
        plWinAudible,                                    // 0014
        plCoordinateInterface,                           // 0015
        plDrawInterface,                                 // 0016
        plDrawable,                                      // 0017
        plAutoWalkRegion,                                // 0018
        plFXMaterial,                                    // 0019
        plPhysical,                                      // 001A
        plMovableMod,                                    // 001B
        plSimulationInterface,                           // 001C
        plParticleFaceOutEffect,                         // 001D
        plModifier,                                      // 001E
        plSingleModifier,                                // 001F
        plSimpleModifier,                                // 0020
        plWindBoneMod,                                   // 0021
        plCameraBrain_NovicePlus,                        // 0022
        plGrassShaderMod,                                // 0023
        plDetectorModifier,                              // 0024
        pfSubtitleMgr,                                   // 0025
        plPythonFileModConditionalObject,                // 0026
        plMultiModifier,                                 // 0027
        plSynchedObject,                                 // 0028
        plSoundBuffer,                                   // 0029
        plPickingDetector,                               // 002A
        plCollisionDetector,                             // 002B
        plLogicModifier,                                 // 002C
        plConditionalObject,                             // 002D
        plANDConditionalObject,                          // 002E
        plORConditionalObject,                           // 002F
        plPickedConditionalObject,                       // 0030
        plActivatorConditionalObject,                    // 0031
        plTimerCallbackManager,                          // 0032
        plKeyPressConditionalObject,                     // 0033
        plAnimationEventConditionalObject,               // 0034
        plControlEventConditionalObject,                 // 0035
        plObjectInBoxConditionalObject,                  // 0036
        plLocalPlayerInBoxConditionalObject,             // 0037
        plObjectIntersectPlaneConditionalObject,         // 0038
        plLocalPlayerIntersectPlaneConditionalObject,    // 0039
        plLayerTransform,                                // 003A
        plBubbleShaderMod,                               // 003B
        plSpawnModifier,                                 // 003C
        plFacingConditionalObject,                       // 003D
        plViewFaceModifier,                              // 003E
        plLayerInterface,                                // 003F
        plLayerAnimation,                                // 0040
        plLayerDepth,                                    // 0041
        plLayerMovie,                                    // 0042
        plLayerBink,                                     // 0043
        plLayerAVI,                                      // 0044
        plSound,                                         // 0045
        plWin32Sound,                                    // 0046
        plLayerOr,                                       // 0047
        plAudioSystem,                                   // 0048
        plDrawableSpans,                                 // 0049
        plMaterial,                                      // 004A
        plInputManager,                                  // 004B
        plLogicModBase,                                  // 004C
        plFogEnvironment,                                // 004D
        plLineFollowModBase,                             // 004E
        plLightInfo,                                     // 004F
        plDirectionalLightInfo,                          // 0050
        plOmniLightInfo,                                 // 0051
        plSpotLightInfo,                                 // 0052
        plCameraBrain,                                   // 0053
        plClientApp,                                     // 0054
        plClient,                                        // 0055
        pfGUICreditsCtrl,                                // 0056
        pfGUIClickMapCtrl,                               // 0057
        plListener,                                      // 0058
        plAvatarAnim,                                    // 0059
        plOccluder,                                      // 005A
        plMobileOccluder,                                // 005B
        plLayerShadowBase,                               // 005C
        plLimitedDirLightInfo,                           // 005D
        plAGAnim,                                        // 005E
        plAGModifier,                                    // 005F
        plAGMasterMod,                                   // 0060
        plCameraRegionDetector,                          // 0061
        plLineFollowMod,                                 // 0062
        plLightModifier,                                 // 0063
        plOmniModifier,                                  // 0064
        plSpotModifier,                                  // 0065
        plLtdDirModifier,                                // 0066
        plSeekPointMod,                                  // 0067
        plOneShotMod,                                    // 0068
        plRandomCommandMod,                              // 0069
        plRandomSoundMod,                                // 006A
        plPostEffectMod,                                 // 006B
        plObjectInVolumeDetector,                        // 006C
        plResponderModifier,                             // 006D
        plAxisAnimModifier,                              // 006E
        plLayerLightBase,                                // 006F
        plFollowMod,                                     // 0070
        plTransitionMgr,                                 // 0071
        plLinkEffectsMgr,                                // 0072
        plWin32StreamingSound,                           // 0073
        plActivatorActivatorConditionalObject,           // 0074
        plSoftVolume,                                    // 0075
        plSoftVolumeSimple,                              // 0076
        plSoftVolumeComplex,                             // 0077
        plSoftVolumeUnion,                               // 0078
        plSoftVolumeIntersect,                           // 0079
        plSoftVolumeInvert,                              // 007A
        plWin32LinkSound,                                // 007B
        plLayerLinkAnimation,                            // 007C
        plArmatureMod,                                   // 007D
        plWin32StaticSound,                              // 007E
        pfGameGUIMgr,                                    // 007F
        pfGUIDialogMod,                                  // 0080
        plCameraBrainUru,                                // 0081
        plVirtualCamera,                                 // 0082
        plCameraModifier,                                // 0083
        plCameraBrainUru_Drive,                          // 0084
        plCameraBrainUru_Follow,                         // 0085
        plCameraBrainUru_Fixed,                          // 0086
        pfGUIButtonMod,                                  // 0087
        plPythonFileMod,                                 // 0088
        pfGUIControlMod,                                 // 0089
        plExcludeRegionModifier,                         // 008A
        pfGUIDraggableMod,                               // 008B
        plVolumeSensorConditionalObject,                 // 008C
        plVolActivatorConditionalObject,                 // 008D
        plMsgForwarder,                                  // 008E
        plBlower,                                        // 008F
        pfGUIListBoxMod,                                 // 0090
        pfGUITextBoxMod,                                 // 0091
        pfGUIEditBoxMod,                                 // 0092
        plDynamicTextMap,                                // 0093
        pfGUISketchCtrl,                                 // 0094
        pfGUIUpDownPairMod,                              // 0095
        pfGUIValueCtrl,                                  // 0096
        pfGUIKnobCtrl,                                   // 0097
        plLadderModifier,                                // 0098
        plEffect,                                        // 0099
        plCloneSpawnModifier,                            // 009A
        pfGUIDragBarCtrl,                                // 009B
        pfGUICheckBoxCtrl,                               // 009C
        pfGUIRadioGroupCtrl,                             // 009D
        pfGUIDynDisplayCtrl,                             // 009E
        plLayerProject,                                  // 009F
        plInputInterfaceMgr,                             // 00A0
        plRailCameraMod,                                 // 00A1
        plMultistageBehMod,                              // 00A2
        plCameraBrainUru_Circle,                         // 00A3
        plParticleWindEffect,                            // 00A4
        plAnimEventModifier,                             // 00A5
        plAutoProfile,                                   // 00A6
        pfGUISkin,                                       // 00A7
        plAVIWriter,                                     // 00A8
        plParticleCollisionEffect,                       // 00A9
        plParticleCollisionEffectDie,                    // 00AA
        plParticleCollisionEffectBounce,                 // 00AB
        plInterfaceInfoModifier,                         // 00AC
        plSharedMesh,                                    // 00AD
        plArmatureEffectsMgr,                            // 00AE
        plVehicleModifier,                               // 00AF
        plParticleLocalWind,                             // 00B0
        plParticleUniformWind,                           // 00B1
        plInstanceDrawInterface,                         // 00B2
        plShadowMaster,                                  // 00B3
        plShadowCaster,                                  // 00B4
        plPointShadowMaster,                             // 00B5
        plDirectShadowMaster,                            // 00B6
        plSDLModifier,                                   // 00B7
        plPhysicalSDLModifier,                           // 00B8
        plAGMasterSDLModifier,                           // 00B9
        plLayerSDLModifier,                              // 00BA
        plAnimTimeConvertSDLModifier,                    // 00BB
        plResponderSDLModifier,                          // 00BC
        plSoundSDLModifier,                              // 00BD
        plResManagerHelper,                              // 00BE
        plArmatureEffect,                                // 00BF
        plArmatureEffectFootSound,                       // 00C0
        plEAXReverbEffect,                               // 00C1
        plDynaDecalMgr,                                  // 00C2
        plObjectInVolumeAndFacingDetector,               // 00C3
        plDynaFootMgr,                                   // 00C4
        plDynaRippleMgr,                                 // 00C5
        plDynaBulletMgr,                                 // 00C6
        plDecalEnableMod,                                // 00C7
        plPrintShape,                                    // 00C8
        plDynaPuddleMgr,                                 // 00C9
        pfGUIMultiLineEditCtrl,                          // 00CA
        plLayerAnimationBase,                            // 00CB
        plLayerSDLAnimation,                             // 00CC
        plATCAnim,                                       // 00CD
        plAgeGlobalAnim,                                 // 00CE
        plAvatarMgr,                                     // 00CF
        plSpawnMod,                                      // 00D0
        plActivePrintShape,                              // 00D1
        plExcludeRegionSDLModifier,                      // 00D2
        plDynaWakeMgr,                                   // 00D3
        plWaveSet7,                                      // 00D4
        plPanicLinkRegion,                               // 00D5
        plWin32GroupedSound,                             // 00D6
        plFilterCoordInterface,                          // 00D7
        plStereizer,                                     // 00D8
        plShader,                                        // 00D9
        plDynamicEnvMap,                                 // 00DA
        plSimpleRegionSensor,                            // 00DB
        plMorphSequence,                                 // 00DC
        plCameraBrain_Novice,                            // 00DD
        plDynaRippleVSMgr,                               // 00DE
        plParticleBulletEffect,                          // 00DF
        pfGUIProgrssCtrl,                                // 00E0
        plMaintainersMarkerModifier,                     // 00E1
        plMorphDataSet,                                  // 00E2
        plHardRegion,                                    // 00E3
        plHardRegionPlanes,                              // 00E4
        plHardRegionComplex,                             // 00E5
        plHardRegionUnion,                               // 00E6
        plHardRegionIntersect,                           // 00E7
        plHardRegionInvert,                              // 00E8
        plVisRegion,                                     // 00E9
        plVisMgr,                                        // 00EA
        plRegionBase,                                    // 00EB
        pfGUIPopUpMenu,                                  // 00EC
        pfGUIMenuItem,                                   // 00ED
        plRelevanceRegion,                               // 00EE
        plRelevanceMgr,                                  // 00EF
        pfJournalBook,                                   // 00F0
        plImageLibMod,                                   // 00F1
        plParticleFlockEffect,                           // 00F2
        plParticleSDLMod,                                // 00F3
        plAgeLoader,                                     // 00F4
        plWaveSetBase,                                   // 00F5
        pfBookData,                                      // 00F6
        plDynaTorpedoMgr,                                // 00F7
        plDynaTorpedoVSMgr,                              // 00F8
        plClusterGroup,                                  // 00F9
        plLODMipmap,                                     // 00FA
        plSwimDetector,                                  // 00FB
        plFadeOpacityMod,                                // 00FC
        plFadeOpacityLay,                                // 00FD
        plArmatureModBase = 0x00FF,                      // 00FF
        plParticleFollowSystemEffect = 0x0101,           // 0101
        plClientSessionMgr,                              // 0102
        plODEPhysical,                                   // 0103
        plSDLVarChangeNotifier,                          // 0104
        plInterestWellModifier,                          // 0105
        plElevatorModifier,                              // 0106
        plCameraBrain_Ground,                            // 0107
        plPagingRegionModifier,                          // 0108
        plGuidepathModifier,                             // 0109
        pfNodeMgr,                                       // 010A
        plEAXEffect,                                     // 010B
        plEAXPitchShifter,                               // 010C
        plIKModifier,                                    // 010D
        pfObjectFlocker,                                 // 010E
        plCameraBrain_M5,                                // 010F
        plAGAnimBink,                                    // 0110
        plDynamicCamMap,                                 // 0111
        plTreeShader,                                    // 0112
        plNodeRegionModifier,                            // 0113
        plPirahnaRegionModifier,                         // 0114
        plCameraBrain_Flight,                            // 0115

        /* Non-Keyed */
        plAvBrainPirahna = 0x0200,                       // 0200
        plMessage,                                       // 0201
        plRefMsg,                                        // 0202
        plTimeMsg,                                       // 0203
        plAnimCmdMsg,                                    // 0204
        plParticleUpdateMsg,                             // 0205
        plCameraMsg,                                     // 0206
        plInputEventMsg,                                 // 0207
        plKeyEventMsg,                                   // 0208
        plAxisEventMsg,                                  // 0209
        plEvalMsg,                                       // 020A
        plTransformMsg,                                  // 020B
        plControlEventMsg,                               // 020C
        plCrossfadeMsg,                                  // 020D
        pfSubtitleMsg,                                   // 020E
        plAnimEvalMsg,                                   // 020F
        plSDLStoreMsg,                                   // 0210
        plActivatorMsg,                                  // 0211
        plDispatch,                                      // 0212
        plReceiver,                                      // 0213
        plOmniSqApplicator,                              // 0214
        plAvBrainFlight,                                 // 0215
        plAvBrainNPC,                                    // 0216
        plAvBrainBlimp,                                  // 0217
        plAvBrainFlightNPC,                              // 0218
        plController,                                    // 0219
        plLeafController,                                // 021A
        plCompoundController,                            // 021B
        plParticleBulletHitMsg,                          // 021C
        pfPanicLinkMsg,                                  // 021D
        plCompoundRotController = 0x0227,                // 0227
        plCompoundPosController = 0x0229,                // 0229
        plTMController,                                  // 022A
        hsFogControl,                                    // 022B
        plCorrectionMsg,                                 // 022C
        plPickedMsg,                                     // 022D
        plCollideMsg,                                    // 022E
        plTriggerMsg,                                    // 022F
        plInterestingModMsg,                             // 0230
        plMatrixUpdateMsg,                               // 0231
        plTimerCallbackMsg,                              // 0232
        plEventCallbackMsg,                              // 0233
        plSpawnModMsg,                                   // 0234
        plSpawnRequestMsg,                               // 0235
        plLoadCloneMsg,                                  // 0236
        plEnableMsg,                                     // 0237
        plWarpMsg,                                       // 0238
        plAttachMsg,                                     // 0239
        pfConsole,                                       // 023A
        plRenderMsg,                                     // 023B
        plAnimTimeConvert,                               // 023C
        plSoundMsg,                                      // 023D
        plInterestingPing,                               // 023E
        plNodeCleanupMsg,                                // 023F
        plSpaceTree,                                     // 0240
        plAudioSysMsg,                                   // 0241
        plDispatchBase,                                  // 0242
        plDeviceRecreateMsg,                             // 0243
        plPipeline,                                      // 0244
        plMessageWithCallbacks = 0x0246,                 // 0246
        plClientMsg,                                     // 0247
        plSimulationMsg,                                 // 0248
        plAvatarMsg,                                     // 0249
        plAvTaskMsg,                                     // 024A
        plAvSeekMsg,                                     // 024B
        plAvOneShotMsg,                                  // 024C
        plProxyDrawMsg,                                  // 024D
        plSelfDestructMsg,                               // 024E
        plSimInfluenceMsg,                               // 024F
        plForceMsg,                                      // 0250
        plOffsetForceMsg,                                // 0251
        plTorqueMsg,                                     // 0252
        plImpulseMsg,                                    // 0253
        plOffsetImpulseMsg,                              // 0254
        plAngularImpulseMsg,                             // 0255
        plDampMsg,                                       // 0256
        plShiftMassMsg,                                  // 0257
        plSimStateMsg,                                   // 0258
        plFreezeMsg,                                     // 0259
        plInitialAgeStateLoadedMsg,                      // 025A
        plAvTaskSeekDoneMsg,                             // 025B
        plSDLModifierMsg,                                // 025C
        plRenderRequestMsg,                              // 025D
        plRenderRequestAck,                              // 025E
        plConvexVolume,                                  // 025F
        plParticleGenerator,                             // 0260
        plSimpleParticleGenerator,                       // 0261
        plParticleEmitter,                               // 0262
        plAGChannel,                                     // 0263
        plMatrixChannel,                                 // 0264
        plMatrixTimeScale,                               // 0265
        plMatrixBlend,                                   // 0266
        plMatrixControllerChannel,                       // 0267
        plPointChannel,                                  // 0268
        plPointConstant,                                 // 0269
        plPointBlend,                                    // 026A
        plQuatChannel,                                   // 026B
        plQuatConstant,                                  // 026C
        plQuatBlend,                                     // 026D
        plLinkToAgeMsg,                                  // 026E
        plPlayerPageMsg,                                 // 026F
        plListenerMsg,                                   // 0270
        plAnimPath,                                      // 0271
        plNotifyMsg,                                     // 0272
        plNodeChangeMsg,                                 // 0273
        plLinkCallbackMsg,                               // 0274
        plTransitionMsg,                                 // 0275
        plConsoleMsg,                                    // 0276
        plVolumeIsect,                                   // 0277
        plSphereIsect,                                   // 0278
        plConeIsect,                                     // 0279
        plCylinderIsect,                                 // 027A
        plParallelIsect,                                 // 027B
        plConvexIsect,                                   // 027C
        plComplexIsect,                                  // 027D
        plUnionIsect,                                    // 027E
        plIntersectionIsect,                             // 027F
        plModulator,                                     // 0280
        plLinkEffectsTriggerMsg,                         // 0281
        plLinkEffectBCMsg,                               // 0282
        plResponderEnableMsg,                            // 0283
        plResponderMsg,                                  // 0284
        plOneShotMsg,                                    // 0285
        plPointTimeScale,                                // 0286
        plPointControllerChannel,                        // 0287
        plQuatTimeScale,                                 // 0288
        plAGApplicator,                                  // 0289
        plMatrixChannelApplicator,                       // 028A
        plPointChannelApplicator,                        // 028B
        plLightDiffuseApplicator,                        // 028C
        plLightAmbientApplicator,                        // 028D
        plLightSpecularApplicator,                       // 028E
        plOmniApplicator,                                // 028F
        plQuatChannelApplicator,                         // 0290
        plScalarChannel,                                 // 0291
        plScalarTimeScale,                               // 0292
        plScalarBlend,                                   // 0293
        plScalarControllerChannel,                       // 0294
        plScalarChannelApplicator,                       // 0295
        plSpotInnerApplicator,                           // 0296
        plSpotOuterApplicator,                           // 0297
        plATCEaseCurve,                                  // 0298
        plConstAccelEaseCurve,                           // 0299
        plSplineEaseCurve,                               // 029A
        plReplaceGeometryMsg,                            // 029B
        plDniCoordinateInfo,                             // 029C
        plScalarConstant,                                // 029D
        plMatrixConstant,                                // 029E
        plAGCmdMsg,                                      // 029F
        plParticleTransferMsg,                           // 02A0
        plParticleKillMsg,                               // 02A1
        plExcludeRegionMsg,                              // 02A2
        plOneTimeParticleGenerator,                      // 02A3
        plParticleApplicator,                            // 02A4
        plParticleLifeMinApplicator,                     // 02A5
        plParticleLifeMaxApplicator,                     // 02A6
        plParticlePPSApplicator,                         // 02A7
        plParticleAngleApplicator,                       // 02A8
        plParticleVelMinApplicator,                      // 02A9
        plParticleVelMaxApplicator,                      // 02AA
        plParticleScaleMinApplicator,                    // 02AB
        plParticleScaleMaxApplicator,                    // 02AC
        plDynamicTextMsg,                                // 02AD
        plCameraTargetFadeMsg,                           // 02AE
        plAgeLoadedMsg,                                  // 02AF
        plPointControllerCacheChannel,                   // 02B0
        plScalarControllerCacheChannel,                  // 02B1
        plLinkEffectsTriggerPrepMsg,                     // 02B2
        plLinkEffectPrepBCMsg,                           // 02B3
        plAgeLoaderMsg,                                  // 02B4
        plDISpansMsg,                                    // 02B5
        plDelayedTransformMsg,                           // 02B6
        pfGUINotifyMsg,                                  // 02B7
        plArmatureBrain,                                 // 02B8
        plAvBrainAvatar,                                 // 02B9
        plAvBrainDrive,                                  // 02BA
        plAvBrainGeneric,                                // 02BB
        plAvBrainLadder,                                 // 02BC
        plInputIfaceMgrMsg,                              // 02BD
        pfPythonNotifyMsg,                               // 02BE
        plMatrixDelayedCorrectionApplicator,             // 02BF
        plAvPushBrainMsg,                                // 02C0
        plAvPopBrainMsg,                                 // 02C1
        plAvTask,                                        // 02C2
        plAvTaskDumbSeek,                                // 02C3
        plAvTaskBrain,                                   // 02C4
        plAnimStage,                                     // 02C5
        plCreatableGenericValue,                         // 02C6
        plAvBrainGenericMsg,                             // 02C7
        plAvTaskSmartSeek,                               // 02C8
        plAGInstanceCallbackMsg,                         // 02C9
        plArmatureEffectMsg,                             // 02CA
        plArmatureEffectStateMsg,                        // 02CB
        plShadowCastMsg,                                 // 02CC
        plBoundsIsect,                                   // 02CD
        plResMgrHelperMsg,                               // 02CE
        plMultistageModMsg,                              // 02CF
        plSoundVolumeApplicator,                         // 02D0
        plCutter,                                        // 02D1
        plBulletMsg,                                     // 02D2
        plDynaDecalEnableMsg,                            // 02D3
        plAvTaskOneShot,                                 // 02D4
        plArmatureUpdateMsg,                             // 02D5
        plAvatarFootMsg,                                 // 02D6
        plParticleFlockMsg,                              // 02D7
        plAvatarBehaviorNotifyMsg,                       // 02D8
        plATCChannel,                                    // 02D9
        plScalarSDLChannel,                              // 02DA
        plLoadAvatarMsg,                                 // 02DB
        plAvatarSetTypeMsg,                              // 02DC
        plRippleShapeMsg,                                // 02DD
        plMatrixDifferenceApp,                           // 02DE
        plSetListenerMsg,                                // 02DF
        plAvatarStealthModeMsg,                          // 02E0
        plEventCallbackInterceptMsg,                     // 02E1
        plDynamicEnvMapMsg,                              // 02E2
        plClimbMsg,                                      // 02E3
        plIfaceFadeAvatarMsg,                            // 02E4
        plSharedMeshBCMsg,                               // 02E5
        plSwimMsg,                                       // 02E6
        plMorphDelta,                                    // 02E7
        plMatrixControllerCacheChannel,                  // 02E8
        plPipeResMakeMsg,                                // 02E9
        plPipeRTMakeMsg,                                 // 02EA
        plPipeGeoMakeMsg,                                // 02EB
        plSimSuppressMsg,                                // 02EC
        plAgeBeginLoadingMsg,                            // 02ED
        plAvatarOpacityCallbackMsg,                      // 02EE
        plAGDetachCallbackMsg,                           // 02EF
        pfMovieEventMsg,                                 // 02F0
        plMovieMsg,                                      // 02F1
        plPipeTexMakeMsg,                                // 02F2
        plCaptureRenderMsg,                              // 02F3
        pfClimbingWallMsg,                               // 02F4
        plClimbEventMsg,                                 // 02F5
        plSDLGameTimeElapsedVar,                         // 02F6
        plLinkEffectsDoneMsg,                            // 02F7
        pfGameGUIMsg,                                    // 02F8
        pfBackdoorMsg,                                   // 02F9
        plSDLVar,                                        // 02FA
        plSDLStructVar,                                  // 02FB
        plSDLBoolVar,                                    // 02FC
        plSDLCharVar,                                    // 02FD
        plSDLByteVar,                                    // 02FE
        plSDLIntVar,                                     // 02FF
        plSDLUIntVar,                                    // 0300
        plSDLFloatVar,                                   // 0301
        plSDLDoubleVar,                                  // 0302
        plSDLStringVar,                                  // 0303
        plSDLTimeVar,                                    // 0304
        plSDLUoidVar,                                    // 0305
        plSDLVector3Var,                                 // 0306
        plSDLPoint3Var,                                  // 0307
        plSDLQuaternionVar,                              // 0308
        plSDLMatrix44Var,                                // 0309
        plSDLRGBAVar,                                    // 030A
        plSDLAgeTimeOfDayVar,                            // 030B
        plSDLAgeTimeElapsedVar,                          // 030C
        plSDLMetaDoubleVar,                              // 030D
        plSDLFixedArrayStructVar,                        // 030E
        plSDLFixedArrayBoolVar,                          // 030F
        plSDLFixedArrayCharVar,                          // 0310
        plSDLFixedArrayByteVar,                          // 0311
        plSDLFixedArrayIntVar,                           // 0312
        plSDLFixedArrayUIntVar,                          // 0313
        plSDLFixedArrayFloatVar,                         // 0314
        plSDLFixedArrayDoubleVar,                        // 0315
        plSDLFixedArrayStringVar,                        // 0316
        plSDLFixedArrayTimeVar,                          // 0317
        plSDLFixedArrayUoidVar,                          // 0318
        plSDLFixedArrayVector3Var,                       // 0319
        plSDLFixedArrayPoint3Var,                        // 031A
        plSDLFixedArrayQuaternionVar,                    // 031B
        plSDLFixedArrayMatrix44Var,                      // 031C
        plSDLFixedArrayRGBAVar,                          // 031D
        plSDLDynArrayStructVar,                          // 031E
        plSDLDynArrayBoolVar,                            // 031F
        plSDLDynArrayCharVar,                            // 0320
        plSDLDynArrayByteVar,                            // 0321
        plSDLDynArrayIntVar,                             // 0322
        plSDLDynArrayUIntVar,                            // 0323
        plSDLDynArrayFloatVar,                           // 0324
        plSDLDynArrayDoubleVar,                          // 0325
        plSDLDynArrayStringVar,                          // 0326
        plSDLDynArrayTimeVar,                            // 0327
        plSDLDynArrayUoidVar,                            // 0328
        plSDLDynArrayVector3Var,                         // 0329
        plSDLDynArrayPoint3Var,                          // 032A
        plSDLDynArrayQuaternionVar,                      // 032B
        plSDLDynArrayMatrix44Var,                        // 032C
        plSDLDynArrayRGBAVar,                            // 032D
        plSDLArrayVar,                                   // 032E
        plSDLVarChangeMsg,                               // 032F
        plAvBrainPath,                                   // 0330
        plSDLBufferVar,                                  // 0331
        plSDLFixedArrayBufferVar,                        // 0332
        plSDLDynArrayBufferVar,                          // 0333
        plMatrixBorrowedChannel,                         // 0334
        plNodeRegionMsg,                                 // 0335
        plEventCallbackSetupMsg,                         // 0336
        plDXPipeline,                                    // 0337
        plRelativeMatrixChannelApplicator,               // 0338
        plPiranhaRegionMsg,                              // 0339
        plAvatarPhysicsEnableCallbackMsg,                // 033A

        NULL = 0x8000,
    }

    enum LiveTypes : ushort {
        /* Keyed */
        plSceneNode,                                     // 0000
        plSceneObject,                                   // 0001
        hsKeyedObject,                                   // 0002
        plBitmap,                                        // 0003
        plMipmap,                                        // 0004
        plCubicEnvironmap,                               // 0005
        plLayer,                                         // 0006
        hsGMaterial,                                     // 0007
        plParticleSystem,                                // 0008
        plParticleEffect,                                // 0009
        plParticleCollisionEffectBeat,                   // 000A
        plParticleFadeVolumeEffect,                      // 000B
        plBoundInterface,                                // 000C
        plRenderTarget,                                  // 000D
        plCubicRenderTarget,                             // 000E
        plCubicRenderTargetModifier,                     // 000F
        plObjInterface,                                  // 0010
        plAudioInterface,                                // 0011
        plAudible,                                       // 0012
        plAudibleNull,                                   // 0013
        plWinAudible,                                    // 0014
        plCoordinateInterface,                           // 0015
        plDrawInterface,                                 // 0016
        plDrawable,                                      // 0017
        plDrawableMesh,                                  // 0018
        plDrawableIce,                                   // 0019
        plPhysical,                                      // 001A
        plPhysicalMesh,                                  // 001B
        plSimulationInterface,                           // 001C
        plCameraModifier,                                // 001D
        plModifier,                                      // 001E
        plSingleModifier,                                // 001F
        plSimpleModifier,                                // 0020
        pfSecurePreloader,                               // 0021
        plRandomTMModifier,                              // 0022
        plInterestingModifier,                           // 0023
        plDetectorModifier,                              // 0024
        plSimplePhysicalMesh,                            // 0025
        plCompoundPhysicalMesh,                          // 0026
        plMultiModifier,                                 // 0027
        plSynchedObject,                                 // 0028
        plSoundBuffer,                                   // 0029
        plAliasModifier,                                 // 002A
        plPickingDetector,                               // 002B
        plCollisionDetector,                             // 002C
        plLogicModifier,                                 // 002D
        plConditionalObject,                             // 002E
        plANDConditionalObject,                          // 002F
        plORConditionalObject,                           // 0030
        plPickedConditionalObject,                       // 0031
        plActivatorConditionalObject,                    // 0032
        plTimerCallbackManager,                          // 0033
        plKeyPressConditionalObject,                     // 0034
        plAnimationEventConditionalObject,               // 0035
        plControlEventConditionalObject,                 // 0036
        plObjectInBoxConditionalObject,                  // 0037
        plLocalPlayerInBoxConditionalObject,             // 0038
        plObjectIntersectPlaneConditionalObject,         // 0039
        plLocalPlayerIntersectPlaneConditionalObject,    // 003A
        plPortalDrawable,                                // 003B
        plPortalPhysical,                                // 003C
        plSpawnModifier,                                 // 003D
        plFacingConditionalObject,                       // 003E
        plPXPhysical,                                    // 003F
        plViewFaceModifier,                              // 0040
        plLayerInterface,                                // 0041
        plLayerWrapper,                                  // 0042
        plLayerAnimation,                                // 0043
        plLayerDepth,                                    // 0044
        plLayerMovie,                                    // 0045
        plLayerBink,                                     // 0046
        plLayerAVI,                                      // 0047
        plSound,                                         // 0048
        plWin32Sound,                                    // 0049
        plLayerOr,                                       // 004A
        plAudioSystem,                                   // 004B
        plDrawableSpans,                                 // 004C
        plDrawablePatchSet,                              // 004D
        plInputManager,                                  // 004E
        plLogicModBase,                                  // 004F
        plFogEnvironment,                                // 0050
        plNetApp,                                        // 0051
        plNetClientMgr,                                  // 0052
        pl2WayWinAudible,                                // 0053
        plLightInfo,                                     // 0054
        plDirectionalLightInfo,                          // 0055
        plOmniLightInfo,                                 // 0056
        plSpotLightInfo,                                 // 0057
        plLightSpace,                                    // 0058
        plNetClientApp,                                  // 0059
        plNetServerApp,                                  // 005A
        plClient,                                        // 005B
        plCompoundTMModifier,                            // 005C
        plCameraBrain,                                   // 005D
        plCameraBrain_Default,                           // 005E
        plCameraBrain_Drive,                             // 005F
        plCameraBrain_Fixed,                             // 0060
        plCameraBrain_FixedPan,                          // 0061
        pfGUIClickMapCtrl,                               // 0062
        plListener,                                      // 0063
        plAvatarMod,                                     // 0064
        plAvatarAnim,                                    // 0065
        plAvatarAnimMgr,                                 // 0066
        plOccluder,                                      // 0067
        plMobileOccluder,                                // 0068
        plLayerShadowBase,                               // 0069
        plLimitedDirLightInfo,                           // 006A
        plAGAnim,                                        // 006B
        plAGModifier,                                    // 006C
        plAGMasterMod,                                   // 006D
        plCameraBrain_Avatar,                            // 006E
        plCameraRegionDetector,                          // 006F
        plCameraBrain_FP,                                // 0070
        plLineFollowMod,                                 // 0071
        plLightModifier,                                 // 0072
        plOmniModifier,                                  // 0073
        plSpotModifier,                                  // 0074
        plLtdDirModifier,                                // 0075
        plSeekPointMod,                                  // 0076
        plOneShotMod,                                    // 0077
        plRandomCommandMod,                              // 0078
        plRandomSoundMod,                                // 0079
        plPostEffectMod,                                 // 007A
        plObjectInVolumeDetector,                        // 007B
        plResponderModifier,                             // 007C
        plAxisAnimModifier,                              // 007D
        plLayerLightBase,                                // 007E
        plFollowMod,                                     // 007F
        plTransitionMgr,                                 // 0080
        plInventoryMod,                                  // 0081
        plInventoryObjMod,                               // 0082
        plLinkEffectsMgr,                                // 0083
        plWin32StreamingSound,                           // 0084
        plPythonMod,                                     // 0085
        plActivatorActivatorConditionalObject,           // 0086
        plSoftVolume,                                    // 0087
        plSoftVolumeSimple,                              // 0088
        plSoftVolumeComplex,                             // 0089
        plSoftVolumeUnion,                               // 008A
        plSoftVolumeIntersect,                           // 008B
        plSoftVolumeInvert,                              // 008C
        plWin32LinkSound,                                // 008D
        plLayerLinkAnimation,                            // 008E
        plArmatureMod,                                   // 008F
        plCameraBrain_Freelook,                          // 0090
        plHavokConstraintsMod,                           // 0091
        plHingeConstraintMod,                            // 0092
        plWheelConstraintMod,                            // 0093
        plStrongSpringConstraintMod,                     // 0094
        plArmatureLODMod,                                // 0095
        plWin32StaticSound,                              // 0096
        pfGameGUIMgr,                                    // 0097
        pfGUIDialogMod,                                  // 0098
        plCameraBrainUru,                                // 0099
        plVirtualCam1,                                   // 009A
        plCameraModifier1,                               // 009B
        plCameraBrainUru_Drive,                          // 009C
        plCameraBrainUru_POA,                            // 009D
        plCameraBrainUru_Avatar,                         // 009E
        plCameraBrainUru_Fixed,                          // 009F
        plCameraBrainUru_POAFixed,                       // 00A0
        pfGUIButtonMod,                                  // 00A1
        plPythonFileMod,                                 // 00A2
        pfGUIControlMod,                                 // 00A3
        plExcludeRegionModifier,                         // 00A4
        pfGUIDraggableMod,                               // 00A5
        plVolumeSensorConditionalObject,                 // 00A6
        plVolActivatorConditionalObject,                 // 00A7
        plMsgForwarder,                                  // 00A8
        plBlower,                                        // 00A9
        pfGUIListBoxMod,                                 // 00AA
        pfGUITextBoxMod,                                 // 00AB
        pfGUIEditBoxMod,                                 // 00AC
        plDynamicTextMap,                                // 00AD
        plSittingModifier,                               // 00AE
        pfGUIUpDownPairMod,                              // 00AF
        pfGUIValueCtrl,                                  // 00B0
        pfGUIKnobCtrl,                                   // 00B1
        plAvLadderMod,                                   // 00B2
        plCameraBrainUru_FirstPerson,                    // 00B3
        plCloneSpawnModifier,                            // 00B4
        plClothingItem,                                  // 00B5
        plClothingOutfit,                                // 00B6
        plClothingBase,                                  // 00B7
        plClothingMgr,                                   // 00B8
        pfGUIDragBarCtrl,                                // 00B9
        pfGUICheckBoxCtrl,                               // 00BA
        pfGUIRadioGroupCtrl,                             // 00BB
        pfPlayerBookMod,                                 // 00BC
        pfGUIDynDisplayCtrl,                             // 00BD
        plLayerProject,                                  // 00BE
        plInputInterfaceMgr,                             // 00BF
        plRailCameraMod,                                 // 00C0
        plMultistageBehMod,                              // 00C1
        plCameraBrainUru_Circle,                         // 00C2
        plParticleWindEffect,                            // 00C3
        plAnimEventModifier,                             // 00C4
        plAutoProfile,                                   // 00C5
        pfGUISkin,                                       // 00C6
        plAVIWriter,                                     // 00C7
        plParticleCollisionEffect,                       // 00C8
        plParticleCollisionEffectDie,                    // 00C9
        plParticleCollisionEffectBounce,                 // 00CA
        plInterfaceInfoModifier,                         // 00CB
        plSharedMesh,                                    // 00CC
        plArmatureEffectsMgr,                            // 00CD
        pfMarkerMgr,                                     // 00CE
        plVehicleModifier,                               // 00CF
        plParticleLocalWind,                             // 00D0
        plParticleUniformWind,                           // 00D1
        plInstanceDrawInterface,                         // 00D2
        plShadowMaster,                                  // 00D3
        plShadowCaster,                                  // 00D4
        plPointShadowMaster,                             // 00D5
        plDirectShadowMaster,                            // 00D6
        plSDLModifier,                                   // 00D7
        plPhysicalSDLModifier,                           // 00D8
        plClothingSDLModifier,                           // 00D9
        plAvatarSDLModifier,                             // 00DA
        plAGMasterSDLModifier,                           // 00DB
        plPythonSDLModifier,                             // 00DC
        plLayerSDLModifier,                              // 00DD
        plAnimTimeConvertSDLModifier,                    // 00DE
        plResponderSDLModifier,                          // 00DF
        plSoundSDLModifier,                              // 00E0
        plResManagerHelper,                              // 00E1
        plAvatarPhysicalSDLModifier,                     // 00E2
        plArmatureEffect,                                // 00E3
        plArmatureEffectFootSound,                       // 00E4
        plEAXListenerMod,                                // 00E5
        plDynaDecalMgr,                                  // 00E6
        plObjectInVolumeAndFacingDetector,               // 00E7
        plDynaFootMgr,                                   // 00E8
        plDynaRippleMgr,                                 // 00E9
        plDynaBulletMgr,                                 // 00EA
        plDecalEnableMod,                                // 00EB
        plPrintShape,                                    // 00EC
        plDynaPuddleMgr,                                 // 00ED
        pfGUIMultiLineEditCtrl,                          // 00EE
        plLayerAnimationBase,                            // 00EF
        plLayerSDLAnimation,                             // 00F0
        plATCAnim,                                       // 00F1
        plAgeGlobalAnim,                                 // 00F2
        plSubworldRegionDetector,                        // 00F3
        plAvatarMgr,                                     // 00F4
        plNPCSpawnMod,                                   // 00F5
        plActivePrintShape,                              // 00F6
        plExcludeRegionSDLModifier,                      // 00F7
        plLOSDispatch,                                   // 00F8
        plDynaWakeMgr,                                   // 00F9
        plSimulationMgr,                                 // 00FA
        plWaveSet7,                                      // 00FB
        plPanicLinkRegion,                               // 00FC
        plWin32GroupedSound,                             // 00FD
        plFilterCoordInterface,                          // 00FE
        plStereizer,                                     // 00FF
        plCCRMgr,                                        // 0100
        plCCRSpecialist,                                 // 0101
        plCCRSeniorSpecialist,                           // 0102
        plCCRShiftSupervisor,                            // 0103
        plCCRGameOperator,                               // 0104
        plShader,                                        // 0105
        plDynamicEnvMap,                                 // 0106
        plSimpleRegionSensor,                            // 0107
        plMorphSequence,                                 // 0108
        plEmoteAnim,                                     // 0109
        plDynaRippleVSMgr,                               // 010A
        plWaveSet6,                                      // 010B
        pfGUIProgressCtrl,                               // 010C
        plMaintainersMarkerModifier,                     // 010D
        plMorphSequenceSDLMod,                           // 010E
        plMorphDataSet,                                  // 010F
        plHardRegion,                                    // 0110
        plHardRegionPlanes,                              // 0111
        plHardRegionComplex,                             // 0112
        plHardRegionUnion,                               // 0113
        plHardRegionIntersect,                           // 0114
        plHardRegionInvert,                              // 0115
        plVisRegion,                                     // 0116
        plVisMgr,                                        // 0117
        plRegionBase,                                    // 0118
        pfGUIPopUpMenu,                                  // 0119
        pfGUIMenuItem,                                   // 011A
        plCoopCoordinator,                               // 011B
        plFont,                                          // 011C
        plFontCache,                                     // 011D
        plRelevanceRegion,                               // 011E
        plRelevanceMgr,                                  // 011F
        pfJournalBook,                                   // 0120
        plLayerTargetContainer,                          // 0121
        plImageLibMod,                                   // 0122
        plParticleFlockEffect,                           // 0123
        plParticleSDLMod,                                // 0124
        plAgeLoader,                                     // 0125
        plWaveSetBase,                                   // 0126
        plPhysicalSndGroup,                              // 0127
        pfBookData,                                      // 0128
        plDynaTorpedoMgr,                                // 0129
        plDynaTorpedoVSMgr,                              // 012A
        plClusterGroup,                                  // 012B
        plGameMarkerModifier,                            // 012C
        plLODMipmap,                                     // 012D
        plSwimDetector,                                  // 012E
        plFadeOpacityMod,                                // 012F
        plFadeOpacityLay,                                // 0130
        plDistOpacityMod,                                // 0131
        plArmatureModBase,                               // 0132
        plSwimRegionInterface,                           // 0133
        plSwimCircularCurrentRegion,                     // 0134
        plParticleFollowSystemEffect,                    // 0135
        plSwimStraightCurrentRegion,                     // 0136
        pfObjectFlocker,                                 // 0137
        plGrassShaderMod,                                // 0138
        plDynamicCamMap,                                 // 0139
        plRidingAnimatedPhysicalDetector,                // 013A
        plVolumeSensorConditionalObjectNoArbitration,    // 013B

        /* Non-Keyed */
        plObjRefMsg = 0x0200,                            // 0200
        plNodeRefMsg,                                    // 0201
        plMessage,                                       // 0202
        plRefMsg,                                        // 0203
        plGenRefMsg,                                     // 0204
        plTimeMsg,                                       // 0205
        plAnimCmdMsg,                                    // 0206
        plParticleUpdateMsg,                             // 0207
        plLayRefMsg,                                     // 0208
        plMatRefMsg,                                     // 0209
        plCameraMsg,                                     // 020A
        plInputEventMsg,                                 // 020B
        plKeyEventMsg,                                   // 020C
        plMouseEventMsg,                                 // 020D
        plEvalMsg,                                       // 020E
        plTransformMsg,                                  // 020F
        plControlEventMsg,                               // 0210
        plVaultCCRNode,                                  // 0211
        plLOSRequestMsg,                                 // 0212
        plLOSHitMsg,                                     // 0213
        plSingleModMsg,                                  // 0214
        plMultiModMsg,                                   // 0215
        plAvatarPhysicsEnableCallbackMsg,                // 0216
        plMemberUpdateMsg,                               // 0217
        plNetMsgPagingRoom,                              // 0218
        plActivatorMsg,                                  // 0219
        plDispatch,                                      // 021A
        plReceiver,                                      // 021B
        plMeshRefMsg,                                    // 021C
        hsGRenderProcs,                                  // 021D
        hsSfxAngleFade,                                  // 021E
        hsSfxDistFade,                                   // 021F
        hsSfxDistShade,                                  // 0220
        hsSfxGlobalShade,                                // 0221
        hsSfxIntenseAlpha,                               // 0222
        hsSfxObjDistFade,                                // 0223
        hsSfxObjDistShade,                               // 0224
        hsDynamicValue,                                  // 0225
        hsDynamicScalar,                                 // 0226
        hsDynamicColorRGBA,                              // 0227
        hsDynamicMatrix33,                               // 0228
        hsDynamicMatrix44,                               // 0229
        plOmniSqApplicator,                              // 022A
        plPreResourceMsg,                                // 022B
        // ... DUPES?!?!?! ...
        //hsDynamicColorRGBA,                            // 022C
        //hsDynamicMatrix33,                             // 022D
        //hsDynamicMatrix44,                             // 022E
        plController = 0x022F,                           // 022F
        plLeafController,                                // 0230
        plCompoundController,                            // 0231
        plRotController,                                 // 0232
        plPosController,                                 // 0233
        plScalarController,                              // 0234
        plPoint3Controller,                              // 0235
        plScaleValueController,                          // 0236
        plQuatController,                                // 0237
        plMatrix33Controller,                            // 0238
        plMatrix44Controller,                            // 0239
        plEaseController,                                // 023A
        plSimpleScaleController,                         // 023B
        plSimpleRotController,                           // 023C
        plCompoundRotController,                         // 023D
        plSimplePosController,                           // 023E
        plCompoundPosController,                         // 023F
        plTMController,                                  // 0240
        hsFogControl,                                    // 0241
        plIntRefMsg,                                     // 0242
        plCollisionReactor,                              // 0243
        plCorrectionMsg,                                 // 0244
        plPhysicalModifier,                              // 0245
        plPickedMsg,                                     // 0246
        plCollideMsg,                                    // 0247
        plTriggerMsg,                                    // 0248
        plInterestingModMsg,                             // 0249
        plDebugKeyEventMsg,                              // 024A
        plPhysicalProperties,                            // 024B
        plSimplePhys,                                    // 024C
        plMatrixUpdateMsg,                               // 024D
        plCondRefMsg,                                    // 024E
        plTimerCallbackMsg,                              // 024F
        plEventCallbackMsg,                              // 0250
        plSpawnModMsg,                                   // 0251
        plSpawnRequestMsg,                               // 0252
        plLoadCloneMsg,                                  // 0253
        plEnableMsg,                                     // 0254
        plWarpMsg,                                       // 0255
        plAttachMsg,                                     // 0256
        pfConsole,                                       // 0257
        plRenderMsg,                                     // 0258
        plAnimTimeConvert,                               // 0259
        plSoundMsg,                                      // 025A
        plInterestingPing,                               // 025B
        plNodeCleanupMsg,                                // 025C
        plSpaceTree,                                     // 025D
        plNetMessage,                                    // 025E
        plNetMsgJoinReq,                                 // 025F
        plNetMsgJoinAck,                                 // 0260
        plNetMsgLeave,                                   // 0261
        plNetMsgPing,                                    // 0262
        plNetMsgRoomsList,                               // 0263
        plNetMsgGroupOwner,                              // 0264
        plNetMsgGameStateRequest,                        // 0265
        plNetMsgSessionReset,                            // 0266
        plNetMsgOmnibus,                                 // 0267
        plNetMsgObject,                                  // 0268
        plCCRInvisibleMsg,                               // 0269
        plLinkInDoneMsg,                                 // 026A
        plNetMsgGameMessage,                             // 026B
        plNetMsgStream,                                  // 026C
        plAudioSysMsg,                                   // 026D
        plDispatchBase,                                  // 026E
        plServerReplyMsg,                                // 026F
        plDeviceRecreateMsg,                             // 0270
        plNetMsgStreamHelper,                            // 0271
        plNetMsgObjectHelper,                            // 0272
        plIMouseXEventMsg,                               // 0273
        plIMouseYEventMsg,                               // 0274
        plIMouseBEventMsg,                               // 0275
        plLogicTriggerMsg,                               // 0276
        plPipeline,                                      // 0277
        plDXPipeline,                                    // 0278
        plNetMsgVoice,                                   // 0279
        plLightRefMsg,                                   // 027A
        plNetMsgStreamedObject,                          // 027B
        plNetMsgSharedState,                             // 027C
        plNetMsgTestAndSet,                              // 027D
        plNetMsgGetSharedState,                          // 027E
        plSharedStateMsg,                                // 027F
        plNetGenericServerTask,                          // 0280
        plNetClientMgrMsg,                               // 0281
        plLoadAgeMsg,                                    // 0282
        plMessageWithCallbacks,                          // 0283
        plClientMsg,                                     // 0284
        plClientRefMsg,                                  // 0285
        plNetMsgObjStateRequest,                         // 0286
        plCCRPetitionMsg,                                // 0287
        plVaultCCRInitializationTask,                    // 0288
        plNetServerMsg,                                  // 0289
        plNetServerMsgWithContext,                       // 028A
        plNetServerMsgRegisterServer,                    // 028B
        plNetServerMsgUnregisterServer,                  // 028C
        plNetServerMsgStartProcess,                      // 028D
        plNetServerMsgRegisterProcess,                   // 028E
        plNetServerMsgUnregisterProcess,                 // 028F
        plNetServerMsgFindProcess,                       // 0290
        plNetServerMsgProcessFound,                      // 0291
        plNetMsgRoutingInfo,                             // 0292
        plNetServerSessionInfo,                          // 0293
        plSimulationMsg,                                 // 0294
        plSimulationSynchMsg,                            // 0295
        plHKSimulationSynchMsg,                          // 0296
        plAvatarMsg,                                     // 0297
        plAvTaskMsg,                                     // 0298
        plAvSeekMsg,                                     // 0299
        plAvOneShotMsg,                                  // 029A
        plSatisfiedMsg,                                  // 029B
        plNetMsgObjectListHelper,                        // 029C
        plNetMsgObjectUpdateFilter,                      // 029D
        plProxyDrawMsg,                                  // 029E
        plSelfDestructMsg,                               // 029F
        plSimInfluenceMsg,                               // 02A0
        plForceMsg,                                      // 02A1
        plOffsetForceMsg,                                // 02A2
        plTorqueMsg,                                     // 02A3
        plImpulseMsg,                                    // 02A4
        plOffsetImpulseMsg,                              // 02A5
        plAngularImpulseMsg,                             // 02A6
        plDampMsg,                                       // 02A7
        plShiftMassMsg,                                  // 02A8
        plSimStateMsg,                                   // 02A9
        plFreezeMsg,                                     // 02AA
        plEventGroupMsg,                                 // 02AB
        plSuspendEventMsg,                               // 02AC
        plNetMsgMembersListReq,                          // 02AD
        plNetMsgMembersList,                             // 02AE
        plNetMsgMemberInfoHelper,                        // 02AF
        plNetMsgMemberListHelper,                        // 02B0
        plNetMsgMemberUpdate,                            // 02B1
        plNetMsgServerToClient,                          // 02B2
        plNetMsgCreatePlayer,                            // 02B3
        plNetMsgAuthenticateHello,                       // 02B4
        plNetMsgAuthenticateChallenge,                   // 02B5
        plConnectedToVaultMsg,                           // 02B6
        plCCRCommunicationMsg,                           // 02B7
        plNetMsgInitialAgeStateSent,                     // 02B8
        plInitialAgeStateLoadedMsg,                      // 02B9
        plNetServerMsgFindServerBase,                    // 02BA
        plNetServerMsgFindServerReplyBase,               // 02BB
        plNetServerMsgFindAuthServer,                    // 02BC
        plNetServerMsgFindAuthServerReply,               // 02BD
        plNetServerMsgFindVaultServer,                   // 02BE
        plNetServerMsgFindVaultServerReply,              // 02BF
        plAvTaskSeekDoneMsg,                             // 02C0
        plNCAgeJoinerMsg,                                // 02C1
        plNetServerMsgVaultTask,                         // 02C2
        plNetMsgVaultTask,                               // 02C3
        plAgeLinkStruct,                                 // 02C4
        plVaultAgeInfoNode,                              // 02C5
        plNetMsgStreamableHelper,                        // 02C6
        plNetMsgReceiversListHelper,                     // 02C7
        plNetMsgListenListUpdate,                        // 02C8
        plNetServerMsgPing,                              // 02C9
        plNetMsgAlive,                                   // 02CA
        plNetMsgTerminated,                              // 02CB
        plSDLModifierMsg,                                // 02CC
        plNetMsgSDLState,                                // 02CD
        plNetServerMsgSessionReset,                      // 02CE
        plCCRBanLinkingMsg,                              // 02CF
        plCCRSilencePlayerMsg,                           // 02D0
        plRenderRequestMsg,                              // 02D1
        plRenderRequestAck,                              // 02D2
        plNetMember,                                     // 02D3
        plNetGameMember,                                 // 02D4
        plNetTransportMember,                            // 02D5
        plConvexVolume,                                  // 02D6
        plParticleGenerator,                             // 02D7
        plSimpleParticleGenerator,                       // 02D8
        plParticleEmitter,                               // 02D9
        plAGChannel,                                     // 02DA
        plMatrixChannel,                                 // 02DB
        plMatrixTimeScale,                               // 02DC
        plMatrixBlend,                                   // 02DD
        plMatrixControllerChannel,                       // 02DE
        plQuatPointCombine,                              // 02DF
        plPointChannel,                                  // 02E0
        plPointConstant,                                 // 02E1
        plPointBlend,                                    // 02E2
        plQuatChannel,                                   // 02E3
        plQuatConstant,                                  // 02E4
        plQuatBlend,                                     // 02E5
        plLinkToAgeMsg,                                  // 02E6
        plPlayerPageMsg,                                 // 02E7
        plCmdIfaceModMsg,                                // 02E8
        plNetServerMsgPlsUpdatePlayer,                   // 02E9
        plListenerMsg,                                   // 02EA
        plAnimPath,                                      // 02EB
        plClothingUpdateBCMsg,                           // 02EC
        plNotifyMsg,                                     // 02ED
        plFakeOutMsg,                                    // 02EE
        plCursorChangeMsg,                               // 02EF
        plNodeChangeMsg,                                 // 02F0
        plAvEnableMsg,                                   // 02F1
        plLinkCallbackMsg,                               // 02F2
        plTransitionMsg,                                 // 02F3
        plConsoleMsg,                                    // 02F4
        plVolumeIsect,                                   // 02F5
        plSphereIsect,                                   // 02F6
        plConeIsect,                                     // 02F7
        plCylinderIsect,                                 // 02F8
        plParallelIsect,                                 // 02F9
        plConvexIsect,                                   // 02FA
        plComplexIsect,                                  // 02FB
        plUnionIsect,                                    // 02FC
        plIntersectionIsect,                             // 02FD
        plModulator,                                     // 02FE
        plInventoryMsg,                                  // 02FF
        plLinkEffectsTriggerMsg,                         // 0300
        plLinkEffectBCMsg,                               // 0301
        plResponderEnableMsg,                            // 0302
        plNetServerMsgHello,                             // 0303
        plNetServerMsgHelloReply,                        // 0304
        plNetServerMember,                               // 0305
        plResponderMsg,                                  // 0306
        plOneShotMsg,                                    // 0307
        plVaultAgeInfoListNode,                          // 0308
        plNetServerMsgServerRegistered,                  // 0309
        plPointTimeScale,                                // 030A
        plPointControllerChannel,                        // 030B
        plQuatTimeScale,                                 // 030C
        plAGApplicator,                                  // 030D
        plMatrixChannelApplicator,                       // 030E
        plPointChannelApplicator,                        // 030F
        plLightDiffuseApplicator,                        // 0310
        plLightAmbientApplicator,                        // 0311
        plLightSpecularApplicator,                       // 0312
        plOmniApplicator,                                // 0313
        plQuatChannelApplicator,                         // 0314
        plScalarChannel,                                 // 0315
        plScalarTimeScale,                               // 0316
        plScalarBlend,                                   // 0317
        plScalarControllerChannel,                       // 0318
        plScalarChannelApplicator,                       // 0319
        plSpotInnerApplicator,                           // 031A
        plSpotOuterApplicator,                           // 031B
        plNetServerMsgPlsRoutableMsg,                    // 031C
        plPuppetBrainMsg,                               // 031D
        plATCEaseCurve,                                  // 031E
        plConstAccelEaseCurve,                           // 031F
        plSplineEaseCurve,                               // 0320
        plVaultAgeInfoInitializationTask,                // 0321
        pfGameGUIMsg,                                    // 0322
        plNetServerMsgVaultRequestGameState,             // 0323
        plNetServerMsgVaultGameState,                    // 0324
        plNetServerMsgVaultGameStateSave,                // 0325
        plNetServerMsgVaultGameStateSaved,               // 0326
        plNetServerMsgVaultGameStateLoad,                // 0327
        plNetClientTask,                                 // 0328
        plNetMsgSDLStateBCast,                           // 0329
        plReplaceGeometryMsg,                            // 032A
        plNetServerMsgExitProcess,                       // 032B
        plNetServerMsgSaveGameState,                     // 032C
        plDniCoordinateInfo,                             // 032D
        plNetMsgGameMessageDirected,                     // 032E
        plLinkOutUnloadMsg,                              // 032F
        plScalarConstant,                                // 0330
        plMatrixConstant,                                // 0331
        plAGCmdMsg,                                      // 0332
        plParticleTransferMsg,                           // 0333
        plParticleKillMsg,                               // 0334
        plExcludeRegionMsg,                              // 0335
        plOneTimeParticleGenerator,                      // 0336
        plParticleApplicator,                            // 0337
        plParticleLifeMinApplicator,                     // 0338
        plParticleLifeMaxApplicator,                     // 0339
        plParticlePPSApplicator,                         // 033A
        plParticleAngleApplicator,                       // 033B
        plParticleVelMinApplicator,                      // 033C
        plParticleVelMaxApplicator,                      // 033D
        plParticleScaleMinApplicator,                    // 033E
        plParticleScaleMaxApplicator,                    // 033F
        plDynamicTextMsg,                                // 0340
        plCameraTargetFadeMsg,                           // 0341
        plAgeLoadedMsg,                                  // 0342
        plPointControllerCacheChannel,                   // 0343
        plScalarControllerCacheChannel,                  // 0344
        plLinkEffectsTriggerPrepMsg,                     // 0345
        plLinkEffectPrepBCMsg,                           // 0346
        plAvatarInputStateMsg,                           // 0347
        plAgeInfoStruct,                                 // 0348
        plSDLNotificationMsg,                            // 0349
        plNetClientConnectAgeVaultTask,                  // 034A
        plLinkingMgrMsg,                                 // 034B
        plVaultNotifyMsg,                                // 034C
        plPlayerInfo,                                    // 034D
        plSwapSpansRefMsg,                               // 034E
        pfKI,                                            // 034F
        plDISpansMsg,                                    // 0350
        plNetMsgCreatableHelper,                         // 0351
        plCreatableUuid,                                 // 0352
        plNetMsgRequestMyVaultPlayerList,                // 0353
        plDelayedTransformMsg,                           // 0354
        plSuperVNodeMgrInitTask,                         // 0355
        plElementRefMsg,                                 // 0356
        plClothingMsg,                                   // 0357
        plEventGroupEnableMsg,                           // 0358
        pfGUINotifyMsg,                                  // 0359
        plAvBrain,                                       // 035A
        plArmatureBrain,                                 // 035B
        plAvBrainHuman,                                  // 035C
        plAvBrainCritter,                                // 035D
        plAvBrainDrive,                                  // 035E
        plAvBrainSample,                                 // 035F
        plAvBrainGeneric,                                // 0360
        plPreloaderMsg,                                  // 0361
        plAvBrainLadder,                                 // 0362
        plInputIfaceMgrMsg,                              // 0363
        pfKIMsg,                                         // 0364
        plRemoteAvatarInfoMsg,                           // 0365
        plMatrixDelayedCorrectionApplicator,             // 0366
        plAvPushBrainMsg,                                // 0367
        plAvPopBrainMsg,                                 // 0368
        plRoomLoadNotifyMsg,                             // 0369
        plAvTask,                                        // 036A
        plAvAnimTask,                                    // 036B
        plAvSeekTask,                                    // 036C
        plNetCommAuthConnectedMsg,                       // 036D
        plAvOneShotTask,                                 // 036E
        plAvEnableTask,                                  // 036F
        plAvTaskBrain,                                   // 0370
        plAnimStage,                                     // 0371
        plNetClientMember,                               // 0372
        plNetClientCommTask,                             // 0373
        plNetServerMsgAuthRequest,                       // 0374
        plNetServerMsgAuthReply,                         // 0375
        plNetClientCommAuthTask,                         // 0376
        plClientGuid,                                    // 0377
        plNetMsgVaultPlayerList,                         // 0378
        plNetMsgSetMyActivePlayer,                       // 0379
        plNetServerMsgRequestAccountPlayerList,          // 037A
        plNetServerMsgAccountPlayerList,                 // 037B
        plNetMsgPlayerCreated,                           // 037C
        plNetServerMsgVaultCreatePlayer,                 // 037D
        plNetServerMsgVaultPlayerCreated,                // 037E
        plNetMsgFindAge,                                 // 037F
        plNetMsgFindAgeReply,                            // 0380
        plNetClientConnectPrepTask,                      // 0381
        plNetClientAuthTask,                             // 0382
        plNetClientGetPlayerVaultTask,                   // 0383
        plNetClientSetActivePlayerTask,                  // 0384
        plNetClientFindAgeTask,                          // 0385
        plNetClientLeaveTask,                            // 0386
        plNetClientJoinTask,                             // 0387
        plNetClientCalibrateTask,                        // 0388
        plNetMsgDeletePlayer,                            // 0389
        plNetServerMsgVaultDeletePlayer,                 // 038A
        plNetCoreStatsSummary,                           // 038B
        plCreatableGenericValue,                         // 038C
        plCreatableListHelper,                           // 038D
        plCreatableStream,                               // 038E
        plAvBrainGenericMsg,                             // 038F
        plAvTaskSeek,                                    // 0390
        plAGInstanceCallbackMsg,                         // 0391
        plArmatureEffectMsg,                             // 0392
        plArmatureEffectStateMsg,                        // 0393
        plShadowCastMsg,                                 // 0394
        plBoundsIsect,                                   // 0395
        plResMgrHelperMsg,                               // 0396
        plNetCommAuthMsg,                                // 0397
        plNetCommFileListMsg,                            // 0398
        plNetCommFileDownloadMsg,                        // 0399
        plNetCommLinkToAgeMsg,                           // 039A
        plNetCommPlayerListMsg,                          // 039B
        plNetCommActivePlayerMsg,                        // 039C
        plNetCommCreatePlayerMsg,                        // 039D
        plNetCommDeletePlayerMsg,                        // 039E
        plNetCommPublicAgeListMsg,                       // 039F
        plNetCommPublicAgeMsg,                           // 03A0
        plNetCommRegisterAgeMsg,                         // 03A1
        plVaultAdminInitializationTask,                  // 03A2
        plMultistageModMsg,                              // 03A3
        plSoundVolumeApplicator,                         // 03A4
        plCutter,                                        // 03A5
        plBulletMsg,                                     // 03A6
        plDynaDecalEnableMsg,                            // 03A7
        plOmniCutoffApplicator,                          // 03A8
        plArmatureUpdateMsg,                             // 03A9
        plAvatarFootMsg,                                 // 03AA
        plNetOwnershipMsg,                               // 03AB
        plNetMsgRelevanceRegions,                        // 03AC
        plParticleFlockMsg,                              // 03AD
        plAvatarBehaviorNotifyMsg,                       // 03AE
        plATCChannel,                                    // 03AF
        plScalarSDLChannel,                              // 03B0
        plLoadAvatarMsg,                                 // 03B1
        plAvatarSetTypeMsg,                              // 03B2
        plNetMsgLoadClone,                               // 03B3
        plNetMsgPlayerPage,                              // 03B4
        plVNodeInitTask,                                 // 03B5
        plRippleShapeMsg,                                // 03B6
        plEventManager,                                  // 03B7
        plVaultNeighborhoodInitializationTask,           // 03B8
        plNetServerMsgAgentRecoveryRequest,              // 03B9
        plNetServerMsgFrontendRecoveryRequest,           // 03BA
        plNetServerMsgBackendRecoveryRequest,            // 03BB
        plNetServerMsgAgentRecoveryData,                 // 03BC
        plNetServerMsgFrontendRecoveryData,              // 03BD
        plNetServerMsgBackendRecoveryData,               // 03BE
        plSubWorldMsg,                                   // 03BF
        plMatrixDifferenceApp,                           // 03C0
        plAvatarSpawnNotifyMsg,                          // 03C1

        /* Post-DB */
        plVaultGameServerInitializationTask = 0x0427,    // 0427
        plNetClientFindDefaultAgeTask,                   // 0428
        plVaultAgeNode,                                  // 0429
        plVaultAgeInitializationTask,                    // 042A
        plSetListenerMsg,                                // 042B
        plVaultSystemNode,                               // 042C
        plAvBrainSwim,                                   // 042D
        plNetMsgVault,                                   // 042E
        plNetServerMsgVault,                             // 042F
        plVaultTask,                                     // 0430
        plVaultConnectTask,                              // 0431
        plVaultNegotiateManifestTask,                    // 0432
        plVaultFetchNodesTask,                           // 0433
        plVaultSaveNodeTask,                             // 0434
        plVaultFindNodeTask,                             // 0435
        plVaultAddNodeRefTask,                           // 0436
        plVaultRemoveNodeRefTask,                        // 0437
        plVaultSendNodeTask,                             // 0438
        plVaultNotifyOperationCallbackTask,              // 0439
        plVNodeMgrInitializationTask,                    // 043A
        plVaultPlayerInitializationTask,                 // 043B
        plNetVaultServerInitializationTask,              // 043C
        plCommonNeighborhoodsInitTask,                   // 043D
        plVaultNodeRef,                                  // 043E
        plVaultNode,                                     // 043F
        plVaultFolderNode,                               // 0440
        plVaultImageNode,                                // 0441
        plVaultTextNoteNode,                             // 0442
        plVaultSDLNode,                                  // 0443
        plVaultAgeLinkNode,                              // 0444
        plVaultChronicleNode,                            // 0445
        plVaultPlayerInfoNode,                           // 0446
        plVaultMgrNode,                                  // 0447
        plVaultPlayerNode,                               // 0448
        plSynchEnableMsg,                                // 0449
        plNetVaultServerNode,                            // 044A
        plVaultAdminNode,                                // 044B
        plVaultGameServerNode,                           // 044C
        plVaultPlayerInfoListNode,                       // 044D
        plAvatarStealthModeMsg,                          // 044E
        plEventCallbackInterceptMsg,                     // 044F
        plDynamicEnvMapMsg,                              // 0450
        plClimbMsg,                                      // 0451
        plIfaceFadeAvatarMsg,                            // 0452
        plAvBrainClimb,                                  // 0453
        plSharedMeshBCMsg,                               // 0454
        plNetVoiceListMsg,                               // 0455
        plSwimMsg,                                       // 0456
        plMorphDelta,                                    // 0457
        plMatrixControllerCacheChannel,                  // 0458
        plVaultMarkerNode,                               // 0459
        pfMarkerMsg,                                     // 045A
        plPipeResMakeMsg,                                // 045B
        plPipeRTMakeMsg,                                 // 045C
        plPipeGeoMakeMsg,                                // 045D
        plAvCoopMsg,                                     // 045E
        plAvBrainCoop,                                   // 045F
        plSimSuppressMsg,                                // 0460
        plVaultMarkerListNode,                           // 0461
        plAvTaskOrient,                                  // 0462
        plAgeBeginLoadingMsg,                            // 0463
        plSetNetGroupIDMsg,                              // 0464
        pfBackdoorMsg,                                   // 0465
        plAIMsg,                                         // 0466
        plAIBrainCreatedMsg,                             // 0467
        plStateDataRecord,                               // 0468
        plNetClientCommDeletePlayerTask,                 // 0469
        plNetMsgSetTimeout,                              // 046A
        plNetMsgActivePlayerSet,                         // 046B
        plNetClientCommSetTimeoutTask,                   // 046C
        plNetRoutableMsgOmnibus,                         // 046D
        plNetMsgGetPublicAgeList,                        // 046E
        plNetMsgPublicAgeList,                           // 046F
        plNetMsgCreatePublicAge,                         // 0470
        plNetMsgPublicAgeCreated,                        // 0471
        plNetServerMsgEnvelope,                          // 0472
        plNetClientCommGetPublicAgeListTask,             // 0473
        plNetClientCommCreatePublicAgeTask,              // 0474
        plNetServerMsgPendingMsgs,                       // 0475
        plNetServerMsgRequestPendingMsgs,                // 0476
        plDbInterface,                                   // 0477
        plDbProxyInterface,                              // 0478
        plDBGenericSQLDB,                                // 0479
        pfGameMgrMsg,                                    // 047A
        pfGameCliMsg,                                    // 047B
        pfGameCli,                                       // 047C
        pfGmTicTacToe,                                   // 047D
        pfGmHeek,                                        // 047E
        pfGmMarker,                                      // 047F
        pfGmBlueSpiral,                                  // 0480
        pfGmClimbingWall,                                // 0481
        plAIArrivedAtGoalMsg,                            // 0482
        pfGmVarSync,                                     // 0483
        plNetMsgRemovePublicAge,                         // 0484
        plNetMsgPublicAgeRemoved,                        // 0485
        plNetClientCommRemovePublicAgeTask,              // 0486
        plCCRMessage,                                    // 0487
        plAvOneShotLinkTask,                             // 0488
        plNetAuthDatabase,                               // 0489
        plAvatarOpacityCallbackMsg,                      // 048A
        plAGDetachCallbackMsg,                           // 048B
        pfMovieEventMsg,                                 // 048C
        plMovieMsg,                                      // 048D
        plPipeTexMakeMsg,                                // 048E
        plEventLog,                                      // 048F
        plDbEventLog,                                    // 0490
        plSyslogEventLog,                                // 0491
        plCaptureRenderMsg,                              // 0492
        plAgeLoaded2Msg,                                 // 0493
        plPseudoLinkEffectMsg,                           // 0494
        plPseudoLinkAnimTriggerMsg,                      // 0495
        plPseudoLinkAnimCallbackMsg,                     // 0496
        pfClimbingWallMsg,                               // 0497
        plClimbEventMsg,                                 // 0498
        plAvBrainQuab,                                   // 0499
        plAccountUpdateMsg,                              // 049A
        plLinearVelocityMsg,                             // 049B
        plAngularVelocityMsg,                            // 049C
        plRideAnimatedPhysMsg,                           // 049D
        plAvBrainRideAnimatedPhysical,                   // 049E

        NULL = 0x8000,
    }

    enum UruTypes : ushort {
        /* Keyed */
        plSceneNode,                                     // 0000
        plSceneObject,                                   // 0001
        hsKeyedObject,                                   // 0002
        plBitmap,                                        // 0003
        plMipmap,                                        // 0004
        plCubicEnvironmap,                               // 0005
        plLayer,                                         // 0006
        hsGMaterial,                                     // 0007
        plParticleSystem,                                // 0008
        plParticleEffect,                                // 0009
        plParticleCollisionEffectBeat,                   // 000A
        plParticleFadeVolumeEffect,                      // 000B
        plBoundInterface,                                // 000C
        plRenderTarget,                                  // 000D
        plCubicRenderTarget,                             // 000E
        plCubicRenderTargetModifier,                     // 000F
        plObjInterface,                                  // 0010
        plAudioInterface,                                // 0011
        plAudible,                                       // 0012
        plAudibleNull,                                   // 0013
        plWinAudible,                                    // 0014
        plCoordinateInterface,                           // 0015
        plDrawInterface,                                 // 0016
        plDrawable,                                      // 0017
        plDrawableMesh,                                  // 0018
        plDrawableIce,                                   // 0019
        plPhysical,                                      // 001A
        plPhysicalMesh,                                  // 001B
        plSimulationInterface,                           // 001C
        plCameraModifier,                                // 001D
        plModifier,                                      // 001E
        plSingleModifier,                                // 001F
        plSimpleModifier,                                // 0020
        plSimpleTMModifier,                              // 0021
        plRandomTMModifier,                              // 0022
        plInterestingModifier,                           // 0023
        plDetectorModifier,                              // 0024
        plSimplePhysicalMesh,                            // 0025
        plCompoundPhysicalMesh,                          // 0026
        plMultiModifier,                                 // 0027
        plSynchedObject,                                 // 0028
        plSoundBuffer,                                   // 0029
        plAliasModifier,                                 // 002A
        plPickingDetector,                               // 002B
        plCollisionDetector,                             // 002C
        plLogicModifier,                                 // 002D
        plConditionalObject,                             // 002E
        plANDConditionalObject,                          // 002F
        plORConditionalObject,                           // 0030
        plPickedConditionalObject,                       // 0031
        plActivatorConditionalObject,                    // 0032
        plTimerCallbackManager,                          // 0033
        plKeyPressConditionalObject,                     // 0034
        plAnimationEventConditionalObject,               // 0035
        plControlEventConditionalObject,                 // 0036
        plObjectInBoxConditionalObject,                  // 0037
        plLocalPlayerInBoxConditionalObject,             // 0038
        plObjectIntersectPlaneConditionalObject,         // 0039
        plLocalPlayerIntersectPlaneConditionalObject,    // 003A
        plPortalDrawable,                                // 003B
        plPortalPhysical,                                // 003C
        plSpawnModifier,                                 // 003D
        plFacingConditionalObject,                       // 003E
        plHKPhysical,                                    // 003F
        plViewFaceModifier,                              // 0040
        plLayerInterface,                                // 0041
        plLayerWrapper,                                  // 0042
        plLayerAnimation,                                // 0043
        plLayerDepth,                                    // 0044
        plLayerMovie,                                    // 0045
        plLayerBink,                                     // 0046
        plLayerAVI,                                      // 0047
        plSound,                                         // 0048
        plWin32Sound,                                    // 0049
        plLayerOr,                                       // 004A
        plAudioSystem,                                   // 004B
        plDrawableSpans,                                 // 004C
        plDrawablePatchSet,                              // 004D
        plInputManager,                                  // 004E
        plLogicModBase,                                  // 004F
        plFogEnvironment,                                // 0050
        plNetApp,                                        // 0051
        plNetClientMgr,                                  // 0052
        pl2WayWinAudible,                                // 0053
        plLightInfo,                                     // 0054
        plDirectionalLightInfo,                          // 0055
        plOmniLightInfo,                                 // 0056
        plSpotLightInfo,                                 // 0057
        plLightSpace,                                    // 0058
        plNetClientApp,                                  // 0059
        plNetServerApp,                                  // 005A
        plClient,                                        // 005B
        plCompoundTMModifier,                            // 005C
        plCameraBrain,                                   // 005D
        plCameraBrain_Default,                           // 005E
        plCameraBrain_Drive,                             // 005F
        plCameraBrain_Fixed,                             // 0060
        plCameraBrain_FixedPan,                          // 0061
        pfGUIClickMapCtrl,                               // 0062
        plListener,                                      // 0063
        plAvatarMod,                                     // 0064
        plAvatarAnim,                                    // 0065
        plAvatarAnimMgr,                                 // 0066
        plOccluder,                                      // 0067
        plMobileOccluder,                                // 0068
        plLayerShadowBase,                               // 0069
        plLimitedDirLightInfo,                           // 006A
        plAGAnim,                                        // 006B
        plAGModifier,                                    // 006C
        plAGMasterMod,                                   // 006D
        plCameraBrain_Avatar,                            // 006E
        plCameraRegionDetector,                          // 006F
        plCameraBrain_FP,                                // 0070
        plLineFollowMod,                                 // 0071
        plLightModifier,                                 // 0072
        plOmniModifier,                                  // 0073
        plSpotModifier,                                  // 0074
        plLtdDirModifier,                                // 0075
        plSeekPointMod,                                  // 0076
        plOneShotMod,                                    // 0077
        plRandomCommandMod,                              // 0078
        plRandomSoundMod,                                // 0079
        plPostEffectMod,                                 // 007A
        plObjectInVolumeDetector,                        // 007B
        plResponderModifier,                             // 007C
        plAxisAnimModifier,                              // 007D
        plLayerLightBase,                                // 007E
        plFollowMod,                                     // 007F
        plTransitionMgr,                                 // 0080
        plInventoryMod,                                  // 0081
        plInventoryObjMod,                               // 0082
        plLinkEffectsMgr,                                // 0083
        plWin32StreamingSound,                           // 0084
        plPythonMod,                                     // 0085
        plActivatorActivatorConditionalObject,           // 0086
        plSoftVolume,                                    // 0087
        plSoftVolumeSimple,                              // 0088
        plSoftVolumeComplex,                             // 0089
        plSoftVolumeUnion,                               // 008A
        plSoftVolumeIntersect,                           // 008B
        plSoftVolumeInvert,                              // 008C
        plWin32LinkSound,                                // 008D
        plLayerLinkAnimation,                            // 008E
        plArmatureMod,                                   // 008F
        plCameraBrain_Freelook,                          // 0090
        plHavokConstraintsMod,                           // 0091
        plHingeConstraintMod,                            // 0092
        plWheelConstraintMod,                            // 0093
        plStrongSpringConstraintMod,                     // 0094
        plArmatureLODMod,                                // 0095
        plWin32StaticSound,                              // 0096
        pfGameGUIMgr,                                    // 0097
        pfGUIDialogMod,                                  // 0098
        plCameraBrainUru,                                // 0099
        plVirtualCam1,                                   // 009A
        plCameraModifier1,                               // 009B
        plCameraBrainUru_Drive,                          // 009C
        plCameraBrainUru_POA,                            // 009D
        plCameraBrainUru_Avatar,                         // 009E
        plCameraBrainUru_Fixed,                          // 009F
        plCameraBrainUru_POAFixed,                       // 00A0
        pfGUIButtonMod,                                  // 00A1
        plPythonFileMod,                                 // 00A2
        pfGUIControlMod,                                 // 00A3
        plExcludeRegionModifier,                         // 00A4
        pfGUIDraggableMod,                               // 00A5
        plVolumeSensorConditionalObject,                 // 00A6
        plVolActivatorConditionalObject,                 // 00A7
        plMsgForwarder,                                  // 00A8
        plBlower,                                        // 00A9
        pfGUIListBoxMod,                                 // 00AA
        pfGUITextBoxMod,                                 // 00AB
        pfGUIEditBoxMod,                                 // 00AC
        plDynamicTextMap,                                // 00AD
        plSittingModifier,                               // 00AE
        pfGUIUpDownPairMod,                              // 00AF
        pfGUIValueCtrl,                                  // 00B0
        pfGUIKnobCtrl,                                   // 00B1
        plAvLadderMod,                                   // 00B2
        plCameraBrainUru_FirstPerson,                    // 00B3
        plCloneSpawnModifier,                            // 00B4
        plClothingItem,                                  // 00B5
        plClothingOutfit,                                // 00B6
        plClothingBase,                                  // 00B7
        plClothingMgr,                                   // 00B8
        pfGUIDragBarCtrl,                                // 00B9
        pfGUICheckBoxCtrl,                               // 00BA
        pfGUIRadioGroupCtrl,                             // 00BB
        pfPlayerBookMod,                                 // 00BC
        pfGUIDynDisplayCtrl,                             // 00BD
        plLayerProject,                                  // 00BE
        plInputInterfaceMgr,                             // 00BF
        plRailCameraMod,                                 // 00C0
        plMultistageBehMod,                              // 00C1
        plCameraBrainUru_Circle,                         // 00C2
        plParticleWindEffect,                            // 00C3
        plAnimEventModifier,                             // 00C4
        plAutoProfile,                                   // 00C5
        pfGUISkin,                                       // 00C6
        plAVIWriter,                                     // 00C7
        plParticleCollisionEffect,                       // 00C8
        plParticleCollisionEffectDie,                    // 00C9
        plParticleCollisionEffectBounce,                 // 00CA
        plInterfaceInfoModifier,                         // 00CB
        plSharedMesh,                                    // 00CC
        plArmatureEffectsMgr,                            // 00CD
        pfMarkerMgr,                                     // 00CE
        plVehicleModifier,                               // 00CF
        plParticleLocalWind,                             // 00D0
        plParticleUniformWind,                           // 00D1
        plInstanceDrawInterface,                         // 00D2
        plShadowMaster,                                  // 00D3
        plShadowCaster,                                  // 00D4
        plPointShadowMaster,                             // 00D5
        plDirectShadowMaster,                            // 00D6
        plSDLModifier,                                   // 00D7
        plPhysicalSDLModifier,                           // 00D8
        plClothingSDLModifier,                           // 00D9
        plAvatarSDLModifier,                             // 00DA
        plAGMasterSDLModifier,                           // 00DB
        plPythonSDLModifier,                             // 00DC
        plLayerSDLModifier,                              // 00DD
        plAnimTimeConvertSDLModifier,                    // 00DE
        plResponderSDLModifier,                          // 00DF
        plSoundSDLModifier,                              // 00E0
        plResManagerHelper,                              // 00E1
        plHKSubWorld,                                    // 00E2
        plArmatureEffect,                                // 00E3
        plArmatureEffectFootSound,                       // 00E4
        plEAXListenerMod,                                // 00E5
        plDynaDecalMgr,                                  // 00E6
        plObjectInVolumeAndFacingDetector,               // 00E7
        plDynaFootMgr,                                   // 00E8
        plDynaRippleMgr,                                 // 00E9
        plDynaBulletMgr,                                 // 00EA
        plDecalEnableMod,                                // 00EB
        plPrintShape,                                    // 00EC
        plDynaPuddleMgr,                                 // 00ED
        pfGUIMultiLineEditCtrl,                          // 00EE
        plLayerAnimationBase,                            // 00EF
        plLayerSDLAnimation,                             // 00F0
        plATCAnim,                                       // 00F1
        plAgeGlobalAnim,                                 // 00F2
        plSubworldRegionDetector,                        // 00F3
        plAvatarMgr,                                     // 00F4
        plNPCSpawnMod,                                   // 00F5
        plActivePrintShape,                              // 00F6
        plExcludeRegionSDLModifier,                      // 00F7
        plLOSDispatch,                                   // 00F8
        plDynaWakeMgr,                                   // 00F9
        plSimulationMgr,                                 // 00FA
        plWaveSet7,                                      // 00FB
        plPanicLinkRegion,                               // 00FC
        plWin32GroupedSound,                             // 00FD
        plFilterCoordInterface,                          // 00FE
        plStereizer,                                     // 00FF
        plCCRMgr,                                        // 0100
        plCCRSpecialist,                                 // 0101
        plCCRSeniorSpecialist,                           // 0102
        plCCRShiftSupervisor,                            // 0103
        plCCRGameOperator,                               // 0104
        plShader,                                        // 0105
        plDynamicEnvMap,                                 // 0106
        plSimpleRegionSensor,                            // 0107
        plMorphSequence,                                 // 0108
        plEmoteAnim,                                     // 0109
        plDynaRippleVSMgr,                               // 010A
        plWaveSet6,                                      // 010B
        pfGUIProgressCtrl,                               // 010C
        plMaintainersMarkerModifier,                     // 010D
        plMorphSequenceSDLMod,                           // 010E
        plMorphDataSet,                                  // 010F
        plHardRegion,                                    // 0110
        plHardRegionPlanes,                              // 0111
        plHardRegionComplex,                             // 0112
        plHardRegionUnion,                               // 0113
        plHardRegionIntersect,                           // 0114
        plHardRegionInvert,                              // 0115
        plVisRegion,                                     // 0116
        plVisMgr,                                        // 0117
        plRegionBase,                                    // 0118
        pfGUIPopUpMenu,                                  // 0119
        pfGUIMenuItem,                                   // 011A
        plCoopCoordinator,                               // 011B
        plFont,                                          // 011C
        plFontCache,                                     // 011D
        plRelevanceRegion,                               // 011E
        plRelevanceMgr,                                  // 011F
        pfJournalBook,                                   // 0120
        plLayerTargetContainer,                          // 0121
        plImageLibMod,                                   // 0122
        plParticleFlockEffect,                           // 0123
        plParticleSDLMod,                                // 0124
        plAgeLoader,                                     // 0125
        plWaveSetBase,                                   // 0126
        plPhysicalSndGroup,                              // 0127
        pfBookData,                                      // 0128
        plDynaTorpedoMgr,                                // 0129
        plDynaTorpedoVSMgr,                              // 012A
        plClusterGroup,                                  // 012B
        plGameMarkerModifier,                            // 012C
        plLODMipmap,                                     // 012D
        plSwimDetector,                                  // 012E

        //The following classes were added in PotS
        plFadeOpacityMod,                                // 012F
        plFadeOpacityLay,                                // 0130
        plDistOpacityMod,                                // 0131
        plArmatureModBase,                               // 0132
        plSwimRegionInterface,                           // 0133
        plSwimCircularCurrentRegion,                     // 0134
        plParticleFollowSystemEffect,                    // 0135
        plSwimStraightCurrentRegion,                     // 0136
        //End PotS additions

        /* Non-Keyed */
        plObjRefMsg = 0x0200,                            // 0200
        plNodeRefMsg,                                    // 0201
        plMessage,                                       // 0202
        plRefMsg,                                        // 0203
        plGenRefMsg,                                     // 0204
        plTimeMsg,                                       // 0205
        plAnimCmdMsg,                                    // 0206
        plParticleUpdateMsg,                             // 0207
        plLayRefMsg,                                     // 0208
        plMatRefMsg,                                     // 0209
        plCameraMsg,                                     // 020A
        plInputEventMsg,                                 // 020B
        plKeyEventMsg,                                   // 020C
        plMouseEventMsg,                                 // 020D
        plEvalMsg,                                       // 020E
        plTransformMsg,                                  // 020F
        plControlEventMsg,                               // 0210
        plVaultCCRNode,                                  // 0211
        plLOSRequestMsg,                                 // 0212
        plLOSHitMsg,                                     // 0213
        plSingleModMsg,                                  // 0214
        plMultiModMsg,                                   // 0215
        plPlayerMsg,                                     // 0216
        plMemberUpdateMsg,                               // 0217
        plNetMsgPagingRoom,                              // 0218
        plActivatorMsg,                                  // 0219
        plDispatch,                                      // 021A
        plReceiver,                                      // 021B
        plMeshRefMsg,                                    // 021C
        hsGRenderProcs,                                  // 021D
        hsSfxAngleFade,                                  // 021E
        hsSfxDistFade,                                   // 021F
        hsSfxDistShade,                                  // 0220
        hsSfxGlobalShade,                                // 0221
        hsSfxIntenseAlpha,                               // 0222
        hsSfxObjDistFade,                                // 0223
        hsSfxObjDistShade,                               // 0224
        hsDynamicValue,                                  // 0225
        hsDynamicScalar,                                 // 0226
        hsDynamicColorRGBA,                              // 0227
        hsDynamicMatrix33,                               // 0228
        hsDynamicMatrix44,                               // 0229
        plController,                                    // 022A
        plLeafController,                                // 022B
        plScaleController,                               // 022C
        plRotController,                                 // 022D
        plPosController,                                 // 022E
        plScalarController,                              // 022F
        plPoint3Controller,                              // 0230
        plScaleValueController,                          // 0231
        plQuatController,                                // 0232
        plMatrix33Controller,                            // 0233
        plMatrix44Controller,                            // 0234
        plEaseController,                                // 0235
        plSimpleScaleController,                         // 0236
        plSimpleRotController,                           // 0237
        plCompoundRotController,                         // 0238
        plSimplePosController,                           // 0239
        plCompoundPosController,                         // 023A
        plTMController,                                  // 023B
        hsFogControl,                                    // 023C
        plIntRefMsg,                                     // 023D
        plCollisionReactor,                              // 023E
        plCorrectionMsg,                                 // 023F
        plPhysicalModifier,                              // 0240
        plPickedMsg,                                     // 0241
        plCollideMsg,                                    // 0242
        plTriggerMsg,                                    // 0243
        plInterestingModMsg,                             // 0244
        plDebugKeyEventMsg,                              // 0245
        plPhysicalProperties,                            // 0246
        plSimplePhys,                                    // 0247
        plMatrixUpdateMsg,                               // 0248
        plCondRefMsg,                                    // 0249
        plTimerCallbackMsg,                              // 024A
        plEventCallbackMsg,                              // 024B
        plSpawnModMsg,                                   // 024C
        plSpawnRequestMsg,                               // 024D
        plLoadCloneMsg,                                  // 024E
        plEnableMsg,                                     // 024F
        plWarpMsg,                                       // 0250
        plAttachMsg,                                     // 0251
        pfConsole,                                       // 0252
        plRenderMsg,                                     // 0253
        plAnimTimeConvert,                               // 0254
        plSoundMsg,                                      // 0255
        plInterestingPing,                               // 0256
        plNodeCleanupMsg,                                // 0257
        plSpaceTree,                                     // 0258
        plNetMessage,                                    // 0259
        plNetMsgJoinReq,                                 // 025A
        plNetMsgJoinAck,                                 // 025B
        plNetMsgLeave,                                   // 025C
        plNetMsgPing,                                    // 025D
        plNetMsgRoomsList,                               // 025E
        plNetMsgGroupOwner,                              // 025F
        plNetMsgGameStateRequest,                        // 0260
        plNetMsgSessionReset,                            // 0261
        plNetMsgOmnibus,                                 // 0262
        plNetMsgObject,                                  // 0263
        plCCRInvisibleMsg,                               // 0264
        plLinkInDoneMsg,                                 // 0265
        plNetMsgGameMessage,                             // 0266
        plNetMsgStream,                                  // 0267
        plAudioSysMsg,                                   // 0268
        plDispatchBase,                                  // 0269
        plServerReplyMsg,                                // 026A
        plDeviceRecreateMsg,                             // 026B
        plNetMsgStreamHelper,                            // 026C
        plNetMsgObjectHelper,                            // 026D
        plIMouseXEventMsg,                               // 026E
        plIMouseYEventMsg,                               // 026F
        plIMouseBEventMsg,                               // 0270
        plLogicTriggerMsg,                               // 0271
        plPipeline,                                      // 0272
        plDX8Pipeline,                                   // 0273
        plNetMsgVoice,                                   // 0274
        plLightRefMsg,                                   // 0275
        plNetMsgStreamedObject,                          // 0276
        plNetMsgSharedState,                             // 0277
        plNetMsgTestAndSet,                              // 0278
        plNetMsgGetSharedState,                          // 0279
        plSharedStateMsg,                                // 027A
        plNetGenericServerTask,                          // 027B
        plNetLookupServerGetAgeInfoFromVaultTask,        // 027C
        plLoadAgeMsg,                                    // 027D
        plMessageWithCallbacks,                          // 027E
        plClientMsg,                                     // 027F
        plClientRefMsg,                                  // 0280
        plNetMsgObjStateRequest,                         // 0281
        plCCRPetitionMsg,                                // 0282
        plVaultCCRInitializationTask,                    // 0283
        plNetServerMsg,                                  // 0284
        plNetServerMsgWithContext,                       // 0285
        plNetServerMsgRegisterServer,                    // 0286
        plNetServerMsgUnregisterServer,                  // 0287
        plNetServerMsgStartProcess,                      // 0288
        plNetServerMsgRegisterProcess,                   // 0289
        plNetServerMsgUnregisterProcess,                 // 028A
        plNetServerMsgFindProcess,                       // 028B
        plNetServerMsgProcessFound,                      // 028C
        plNetMsgRoutingInfo,                             // 028D
        plNetServerSessionInfo,                          // 028E
        plSimulationMsg,                                 // 028F
        plSimulationSynchMsg,                            // 0290
        plHKSimulationSynchMsg,                          // 0291
        plAvatarMsg,                                     // 0292
        plAvTaskMsg,                                     // 0293
        plAvSeekMsg,                                     // 0294
        plAvOneShotMsg,                                  // 0295
        plSatisfiedMsg,                                  // 0296
        plNetMsgObjectListHelper,                        // 0297
        plNetMsgObjectUpdateFilter,                      // 0298
        plProxyDrawMsg,                                  // 0299
        plSelfDestructMsg,                               // 029A
        plSimInfluenceMsg,                               // 029B
        plForceMsg,                                      // 029C
        plOffsetForceMsg,                                // 029D
        plTorqueMsg,                                     // 029E
        plImpulseMsg,                                    // 029F
        plOffsetImpulseMsg,                              // 02A0
        plAngularImpulseMsg,                             // 02A1
        plDampMsg,                                       // 02A2
        plShiftMassMsg,                                  // 02A3
        plSimStateMsg,                                   // 02A4
        plFreezeMsg,                                     // 02A5
        plEventGroupMsg,                                 // 02A6
        plSuspendEventMsg,                               // 02A7
        plNetMsgMembersListReq,                          // 02A8
        plNetMsgMembersList,                             // 02A9
        plNetMsgMemberInfoHelper,                        // 02AA
        plNetMsgMemberListHelper,                        // 02AB
        plNetMsgMemberUpdate,                            // 02AC
        plNetMsgServerToClient,                          // 02AD
        plNetMsgCreatePlayer,                            // 02AE
        plNetMsgAuthenticateHello,                       // 02AF
        plNetMsgAuthenticateChallenge,                   // 02B0
        plConnectedToVaultMsg,                           // 02B1
        plCCRCommunicationMsg,                           // 02B2
        plNetMsgInitialAgeStateSent,                     // 02B3
        plInitialAgeStateLoadedMsg,                      // 02B4
        plNetServerMsgFindServerBase,                    // 02B5
        plNetServerMsgFindServerReplyBase,               // 02B6
        plNetServerMsgFindAuthServer,                    // 02B7
        plNetServerMsgFindAuthServerReply,               // 02B8
        plNetServerMsgFindVaultServer,                   // 02B9
        plNetServerMsgFindVaultServerReply,              // 02BA
        plAvTaskSeekDoneMsg,                             // 02BB
        plNetServerMsgFindAdminServer,                  // 02BC
        plNetServerMsgVaultTask,                         // 02BD
        plNetMsgVaultTask,                               // 02BE
        plAgeLinkStruct,                                 // 02BF
        plVaultAgeInfoNode,                              // 02C0
        plNetMsgStreamableHelper,                        // 02C1
        plNetMsgReceiversListHelper,                     // 02C2
        plNetMsgListenListUpdate,                        // 02C3
        plNetServerMsgPing,                              // 02C4
        plNetMsgAlive,                                   // 02C5
        plNetMsgTerminated,                              // 02C6
        plSDLModifierMsg,                                // 02C7
        plNetMsgSDLState,                                // 02C8
        plNetServerMsgSessionReset,                      // 02C9
        plCCRBanLinkingMsg,                              // 02CA
        plCCRSilencePlayerMsg,                           // 02CB
        plRenderRequestMsg,                              // 02CC
        plRenderRequestAck,                              // 02CD
        plNetMember,                                     // 02CE
        plNetGameMember,                                 // 02CF
        plNetTransportMember,                            // 02D0
        plConvexVolume,                                  // 02D1
        plParticleGenerator,                             // 02D2
        plSimpleParticleGenerator,                       // 02D3
        plParticleEmitter,                               // 02D4
        plAGChannel,                                     // 02D5
        plMatrixChannel,                                 // 02D6
        plMatrixTimeScale,                               // 02D7
        plMatrixBlend,                                   // 02D8
        plMatrixControllerChannel,                       // 02D9
        plQuatPointCombine,                              // 02DA
        plPointChannel,                                  // 02DB
        plPointConstant,                                 // 02DC
        plPointBlend,                                    // 02DD
        plQuatChannel,                                   // 02DE
        plQuatConstant,                                  // 02DF
        plQuatBlend,                                     // 02E0
        plLinkToAgeMsg,                                  // 02E1
        plPlayerPageMsg,                                 // 02E2
        plCmdIfaceModMsg,                                // 02E3
        plNetServerMsgPlsUpdatePlayer,                   // 02E4
        plListenerMsg,                                   // 02E5
        plAnimPath,                                      // 02E6
        plClothingUpdateBCMsg,                           // 02E7
        plNotifyMsg,                                     // 02E8
        plFakeOutMsg,                                    // 02E9
        plCursorChangeMsg,                               // 02EA
        plNodeChangeMsg,                                 // 02EB
        plAvEnableMsg,                                   // 02EC
        plLinkCallbackMsg,                               // 02ED
        plTransitionMsg,                                 // 02EE
        plConsoleMsg,                                    // 02EF
        plVolumeIsect,                                   // 02F0
        plSphereIsect,                                   // 02F1
        plConeIsect,                                     // 02F2
        plCylinderIsect,                                 // 02F3
        plParallelIsect,                                 // 02F4
        plConvexIsect,                                   // 02F5
        plComplexIsect,                                  // 02F6
        plUnionIsect,                                    // 02F7
        plIntersectionIsect,                             // 02F8
        plModulator,                                     // 02F9
        plInventoryMsg,                                  // 02FA
        plLinkEffectsTriggerMsg,                         // 02FB
        plLinkEffectBCMsg,                               // 02FC
        plResponderEnableMsg,                            // 02FD
        plNetServerMsgHello,                             // 02FE
        plNetServerMsgHelloReply,                        // 02FF
        plNetServerMember,                               // 0300
        plResponderMsg,                                  // 0301
        plOneShotMsg,                                    // 0302
        plVaultAgeInfoListNode,                          // 0303
        plNetServerMsgServerRegistered,                  // 0304
        plPointTimeScale,                                // 0305
        plPointControllerChannel,                        // 0306
        plQuatTimeScale,                                 // 0307
        plAGApplicator,                                  // 0308
        plMatrixChannelApplicator,                       // 0309
        plPointChannelApplicator,                        // 030A
        plLightDiffuseApplicator,                        // 030B
        plLightAmbientApplicator,                        // 030C
        plLightSpecularApplicator,                       // 030D
        plOmniApplicator,                                // 030E
        plQuatChannelApplicator,                         // 030F
        plScalarChannel,                                 // 0310
        plScalarTimeScale,                               // 0311
        plScalarBlend,                                   // 0312
        plScalarControllerChannel,                       // 0313
        plScalarChannelApplicator,                       // 0314
        plSpotInnerApplicator,                           // 0315
        plSpotOuterApplicator,                           // 0316
        plNetServerMsgPlsRoutableMsg,                    // 0317
        plPuppetBrainMsg,                                // 0318
        plATCEaseCurve,                                  // 0319
        plConstAccelEaseCurve,                           // 031A
        plSplineEaseCurve,                               // 031B
        plVaultAgeInfoInitializationTask,                // 031C
        pfGameGUIMsg,                                    // 031D
        plNetServerMsgVaultRequestGameState,             // 031E
        plNetServerMsgVaultGameState,                    // 031F
        plNetServerMsgVaultGameStateSave,                // 0320
        plNetServerMsgVaultGameStateSaved,               // 0321
        plNetServerMsgVaultGameStateLoad,                // 0322
        plNetClientTask,                                 // 0323
        plNetMsgSDLStateBCast,                           // 0324
        plReplaceGeometryMsg,                            // 0325
        plNetServerMsgExitProcess,                       // 0326
        plNetServerMsgSaveGameState,                     // 0327
        plDniCoordinateInfo,                             // 0328
        plNetMsgGameMessageDirected,                     // 0329
        plLinkOutUnloadMsg,                              // 032A
        plScalarConstant,                                // 032B
        plMatrixConstant,                                // 032C
        plAGCmdMsg,                                      // 032D
        plParticleTransferMsg,                           // 032E
        plParticleKillMsg,                               // 032F
        plExcludeRegionMsg,                              // 0330
        plOneTimeParticleGenerator,                      // 0331
        plParticleApplicator,                            // 0332
        plParticleLifeMinApplicator,                     // 0333
        plParticleLifeMaxApplicator,                     // 0334
        plParticlePPSApplicator,                         // 0335
        plParticleAngleApplicator,                       // 0336
        plParticleVelMinApplicator,                      // 0337
        plParticleVelMaxApplicator,                      // 0338
        plParticleScaleMinApplicator,                    // 0339
        plParticleScaleMaxApplicator,                    // 033A
        plDynamicTextMsg,                                // 033B
        plCameraTargetFadeMsg,                           // 033C
        plAgeLoadedMsg,                                  // 033D
        plPointControllerCacheChannel,                   // 033E
        plScalarControllerCacheChannel,                  // 033F
        plLinkEffectsTriggerPrepMsg,                     // 0340
        plLinkEffectPrepBCMsg,                           // 0341
        plAvatarInputStateMsg,                           // 0342
        plAgeInfoStruct,                                 // 0343
        plSDLNotificationMsg,                            // 0344
        plNetClientConnectAgeVaultTask,                  // 0345
        plLinkingMgrMsg,                                 // 0346
        plVaultNotifyMsg,                                // 0347
        plPlayerInfo,                                    // 0348
        plSwapSpansRefMsg,                               // 0349
        pfKI,                                            // 034A
        plDISpansMsg,                                    // 034B
        plNetMsgCreatableHelper,                         // 034C
        plServerGuid,                                    // 034D
        plNetMsgRequestMyVaultPlayerList,                // 034E
        plDelayedTransformMsg,                           // 034F
        plSuperVNodeMgrInitTask,                         // 0350
        plElementRefMsg,                                 // 0351
        plClothingMsg,                                   // 0352
        plEventGroupEnableMsg,                           // 0353
        pfGUINotifyMsg,                                  // 0354
        plAvBrain,                                       // 0355
        plAvBrainUser,                                   // 0356
        plAvBrainHuman,                                  // 0357
        plAvBrainCritter,                                // 0358
        plAvBrainDrive,                                  // 0359
        plAvBrainSample,                                 // 035A
        plAvBrainGeneric,                                // 035B
        plAvBrainPuppet,                                 // 035C
        plAvBrainLadder,                                 // 035D
        plInputIfaceMgrMsg,                              // 035E
        pfKIMsg,                                         // 035F
        plRemoteAvatarInfoMsg,                           // 0360
        plMatrixDelayedCorrectionApplicator,             // 0361
        plAvPushBrainMsg,                                // 0362
        plAvPopBrainMsg,                                 // 0363
        plRoomLoadNotifyMsg,                             // 0364
        plAvTask,                                        // 0365
        plAvAnimTask,                                    // 0366
        plAvSeekTask,                                    // 0367
        plAvBlendedSeekTask,                             // 0368
        plAvOneShotTask,                                 // 0369
        plAvEnableTask,                                  // 036A
        plAvTaskBrain,                                   // 036B
        plAnimStage,                                     // 036C
        plNetClientMember,                               // 036D
        plNetClientCommTask,                             // 036E
        plNetServerMsgAuthRequest,                       // 036F
        plNetServerMsgAuthReply,                         // 0370
        plNetClientCommAuthTask,                         // 0371
        plClientGuid,                                    // 0372
        plNetMsgVaultPlayerList,                         // 0373
        plNetMsgSetMyActivePlayer,                       // 0374
        plNetServerMsgRequestAccountPlayerList,          // 0375
        plNetServerMsgAccountPlayerList,                 // 0376
        plNetMsgPlayerCreated,                           // 0377
        plNetServerMsgVaultCreatePlayer,                 // 0378
        plNetServerMsgVaultPlayerCreated,                // 0379
        plNetMsgFindAge,                                 // 037A
        plNetMsgFindAgeReply,                            // 037B
        plNetClientConnectPrepTask,                      // 037C
        plNetClientAuthTask,                             // 037D
        plNetClientGetPlayerVaultTask,                   // 037E
        plNetClientSetActivePlayerTask,                  // 037F
        plNetClientFindAgeTask,                          // 0380
        plNetClientLeaveTask,                            // 0381
        plNetClientJoinTask,                             // 0382
        plNetClientCalibrateTask,                        // 0383
        plNetMsgDeletePlayer,                            // 0384
        plNetServerMsgVaultDeletePlayer,                 // 0385
        plNetCoreStatsSummary,                           // 0386
        plCreatableGenericValue,                         // 0387
        plCreatableListHelper,                           // 0388
        plCreatableStream,                               // 0389
        plAvBrainGenericMsg,                             // 038A
        plAvTaskSeek,                                    // 038B
        plAGInstanceCallbackMsg,                         // 038C
        plArmatureEffectMsg,                             // 038D
        plArmatureEffectStateMsg,                        // 038E
        plShadowCastMsg,                                 // 038F
        plBoundsIsect,                                   // 0390
        plNetClientCommLeaveTask,                        // 0391
        plResMgrHelperMsg,                               // 0392
        plNetMsgAuthenticateResponse,                    // 0393
        plNetMsgAccountAuthenticated,                    // 0394
        plNetClientCommSendPeriodicAliveTask,            // 0395
        plNetClientCommCheckServerSilenceTask,           // 0396
        plNetClientCommPingTask,                         // 0397
        plNetClientCommFindAgeTask,                      // 0398
        plNetClientCommSetActivePlayerTask,              // 0399
        plNetClientCommGetPlayerListTask,                // 039A
        plNetClientCommCreatePlayerTask,                 // 039B
        plNetClientCommJoinAgeTask,                      // 039C
        plVaultAdminInitializationTask,                  // 039D
        plMultistageModMsg,                              // 039E
        plSoundVolumeApplicator,                         // 039F
        plCutter,                                        // 03A0
        plBulletMsg,                                     // 03A1
        plDynaDecalEnableMsg,                            // 03A2
        plOmniCutoffApplicator,                          // 03A3
        plArmatureUpdateMsg,                             // 03A4
        plAvatarFootMsg,                                 // 03A5
        plNetOwnershipMsg,                               // 03A6
        plNetMsgRelevanceRegions,                        // 03A7
        plParticleFlockMsg,                              // 03A8
        plAvatarBehaviorNotifyMsg,                       // 03A9
        plATCChannel,                                    // 03AA
        plScalarSDLChannel,                              // 03AB
        plLoadAvatarMsg,                                 // 03AC
        plAvatarSetTypeMsg,                              // 03AD
        plNetMsgLoadClone,                               // 03AE
        plNetMsgPlayerPage,                              // 03AF
        plVNodeInitTask,                                 // 03B0
        plRippleShapeMsg,                                // 03B1
        plEventManager,                                  // 03B2
        plVaultNeighborhoodInitializationTask,           // 03B3
        plNetServerMsgAgentRecoveryRequest,              // 03B4
        plNetServerMsgFrontendRecoveryRequest,           // 03B5
        plNetServerMsgBackendRecoveryRequest,            // 03B6
        plNetServerMsgAgentRecoveryData,                 // 03B7
        plNetServerMsgFrontendRecoveryData,              // 03B8
        plNetServerMsgBackendRecoveryData,               // 03B9
        plSubWorldMsg,                                   // 03BA
        plMatrixDifferenceApp,                           // 03BB
        plAvatarSpawnNotifyMsg,                          // 03BC

        /* Post-DB
         * Note: All pCres here are the PotS IDs
         *       We MUST do ID - 1 to get the < PotS
         *       Class ID. You have been warned!
         */
        plVaultGameServerInitializationTask = 0x0422,    // 0422
        plNetClientFindDefaultAgeTask,                   // 0423
        plVaultAgeNode,                                  // 0424
        plVaultAgeInitializationTask,                    // 0425
        plSetListenerMsg,                                // 0426
        plVaultSystemNode,                               // 0427
        plAvBrainSwim,                                   // 0428
        plNetMsgVault,                                   // 0429
        plNetServerMsgVault,                             // 042A
        plVaultTask,                                     // 042B
        plVaultConnectTask,                              // 042C
        plVaultNegotiateManifestTask,                    // 042D
        plVaultFetchNodesTask,                           // 042E
        plVaultSaveNodeTask,                             // 042F
        plVaultFindNodeTask,                             // 0430
        plVaultAddNodeRefTask,                           // 0431
        plVaultRemoveNodeRefTask,                        // 0432
        plVaultSendNodeTask,                             // 0433
        plVaultNotifyOperationCallbackTask,              // 0434
        plVNodeMgrInitializationTask,                    // 0435
        plVaultPlayerInitializationTask,                 // 0436
        plNetVaultServerInitializationTask,              // 0437
        plCommonNeighborhoodsInitTask,                   // 0438
        plVaultNodeRef,                                  // 0439
        plVaultNode,                                     // 043A
        plVaultFolderNode,                               // 043B
        plVaultImageNode,                                // 043C
        plVaultTextNoteNode,                             // 043D
        plVaultSDLNode,                                  // 043E
        plVaultAgeLinkNode,                              // 043F
        plVaultChronicleNode,                            // 0440
        plVaultPlayerInfoNode,                           // 0441
        plVaultMgrNode,                                  // 0442
        plVaultPlayerNode,                               // 0443
        plSynchEnableMsg,                                // 0444
        plNetVaultServerNode,                            // 0445
        plVaultAdminNode,                                // 0446
        plVaultGameServerNode,                           // 0447
        plVaultPlayerInfoListNode,                       // 0448
        plAvatarStealthModeMsg,                          // 0449
        plEventCallbackInterceptMsg,                     // 044A
        plDynamicEnvMapMsg,                              // 044B
        plClimbMsg,                                      // 044C
        plIfaceFadeAvatarMsg,                            // 044D
        plAvBrainClimb,                                  // 044E
        plSharedMeshBCMsg,                               // 044F
        plNetVoiceListMsg,                               // 0450
        plSwimMsg,                                       // 0451
        plMorphDelta,                                    // 0452
        plMatrixControllerCacheChannel,                  // 0453
        plVaultMarkerNode,                               // 0454
        pfMarkerMsg,                                     // 0455
        plPipeResMakeMsg,                                // 0456
        plPipeRTMakeMsg,                                 // 0457
        plPipeGeoMakeMsg,                                // 0458
        plAvCoopMsg,                                     // 0459
        plAvBrainCoop,                                   // 045A
        plSimSuppressMsg,                                // 045B
        plVaultMarkerListNode,                           // 045C
        plAvTaskOrient,                                  // 045D
        plAgeBeginLoadingMsg,                            // 045E
        plSetNetGroupIDMsg,                              // 045F
        pfBackdoorMsg,                                   // 0460
        plNetMsgPython,                                  // 0461
        pfPythonMsg,                                     // 0462
        plStateDataRecord,                               // 0463
        plNetClientCommDeletePlayerTask,                 // 0464
        plNetMsgSetTimeout,                              // 0465
        plNetMsgActivePlayerSet,                         // 0466
        plNetClientCommSetTimeoutTask,                   // 0467
        plNetRoutableMsgOmnibus,                         // 0468
        plNetMsgGetPublicAgeList,                        // 0469
        plNetMsgPublicAgeList,                           // 046A
        plNetMsgCreatePublicAge,                         // 046B
        plNetMsgPublicAgeCreated,                        // 046C
        plNetServerMsgEnvelope,                          // 046D
        plNetClientCommGetPublicAgeListTask,             // 046E
        plNetClientCommCreatePublicAgeTask,              // 046F
        plNetServerMsgPendingMsgs,                       // 0470
        plNetServerMsgRequestPendingMsgs,                // 0471
        plDbInterface,                                   // 0472
        plDbProxyInterface,                              // 0473
        plDBGenericSQLDB,                                // 0474
        plMySqlDB,                                       // 0475
        plNetGenericDatabase,                            // 0476
        plNetVaultDatabase,                              // 0477
        plNetServerMsgPlsUpdatePlayerReply,              // 0478
        plVaultDisconnectTask,                           // 0479
        plNetClientCommSetAgePublicTask,                 // 047A
        plNetClientCommRegisterOwnedAge,                 // 047B
        plNetClientCommUnregisterOwnerAge,               // 047C
        plNetClientCommRegisterVisitAge,                 // 047D
        plNetClientCommUnregisterVisitAge,               // 047E
        plNetMsgRemovePublicAge,                         // 047F
        plNetMsgPublicAgeRemoved,                        // 0480
        plNetClientCommRemovePublicAgeTask,              // 0481
        plCCRMessage,                                    // 0482
        plAvOneShotLinkTask,                             // 0483
        plNetAuthDatabase,                               // 0484
        plAvatarOpacityCallbackMsg,                      // 0485
        plAGDetachCallbackMsg,                           // 0486
        pfMovieEventMsg,                                 // 0487
        plMovieMsg,                                      // 0488
        plPipeTexMakeMsg,                                // 0489
        plEventLog,                                      // 048A
        plDbEventLog,                                    // 048B
        plSyslogEventLog,                                // 048C
        plCaptureRenderMsg,                              // 048D
        plAgeLoaded2Msg,                                 // 048E
        plPseudoLinkEffectMsg,                           // 048F
        plPseudoLinkAnimTriggerMsg,                      // 0490
        plPseudoLinkAnimCallbackMsg,                     // 0491
        pfClimbingWallMsg,                               // 0492
        plClimbEventMsg,                                 // 0493
        plAvBrainQuab,                                   // 0494

        /* Special */
        NULL = 0x8000,
    }
}

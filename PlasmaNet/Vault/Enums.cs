using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum ENodeType {
        kNodeInvalid, kNodeVNodeMgrLow, kNodePlayer, kNodeAge, kNodeGameServer,
        kNodeAdmin, kNodeVaultServer, kNodeCCR, kNodeVNodeMgrHigh = 21,
        kNodeFolder, kNodePlayerInfo, kNodeSystem, kNodeImage, kNodeTextNote,
        kNodeSDL, kNodeAgeLink, kNodeChronicle, kNodePlayerInfoList,
        kNodeUNUSED, kNodeMarker, kNodeAgeInfo, kNodeAgeInfoList,
        kNodeMarkerList
    }

    public enum EStandardNode {
        kUserDefinedNode, kInboxFolder, kBuddyListFolder, kIgnoreListFolder,
        kPeopleIKnowAboutFolder, kVaultMgrGlobalDataFolder, kChronicleFolder,
        kAvatarOutfitFolder, kAgeTypeJournalFolder, kSubAgesFolder,
        kDeviceInboxFolder, kHoodMembersFolder, kAllPlayersFolder,
        kAgeMembersFolder, kAgeJournalsFolder, kAgeDevicesFolder,
        kAgeInstanceSDLNode, kAgeGlobalSDLNode, kCanVisitFolder, kAgeOwnersFolder,
        kAllAgeGlobalSDLNodesFolder, kPlayerInfoNode, kPublicAgesFolder,
        kAgesIOwnFolder, kAgesICanVisitFolder, kAvatarClosetFolder, kAgeInfoNode,
        kSystemNode, kPlayerInviteFolder, kCCRPlayersFolder, kGlobalInboxFolder,
        kChildAgesFolder, kGameScoresFolder, kLastStandardNode
    }

    public enum ENoteType {
        kNoteGeneric, kNoteCCRPetition, kNoteDevice, kNoteInvite, kNoteVisit,
        kNoteUnVisit
    }

    [Flags]
    public enum pnVaultNodeFields : long {
        NodeIdx = (1 << 0),
        CreateTime = (1 << 1),
        ModifyTime = (1 << 2),
        CreateAgeName = (1 << 3),
        CreateAgeUuid = (1 << 4),
        CreatorUuid = (1 << 5),
        CreatorIdx = (1 << 6),
        NodeType = (1 << 7),
        Int32_1 = (1 << 8),
        Int32_2 = (1 << 9),
        Int32_3 = (1 << 10),
        Int32_4 = (1 << 11),
        UInt32_1 = (1 << 12),
        UInt32_2 = (1 << 13),
        UInt32_3 = (1 << 14),
        UInt32_4 = (1 << 15),
        Uuid_1 = (1 << 16),
        Uuid_2 = (1 << 17),
        Uuid_3 = (1 << 18),
        Uuid_4 = (1 << 19),
        String64_1 = (1 << 20),
        String64_2 = (1 << 21),
        String64_3 = (1 << 22),
        String64_4 = (1 << 23),
        String64_5 = (1 << 24),
        String64_6 = (1 << 25),
        IString64_1 = (1 << 26),
        IString64_2 = (1 << 27),
        Text_1 = (1 << 28),
        Text_2 = (1 << 29),
        Blob_1 = (1 << 30),
        Blob_2 = (1 << 31)
    }
}

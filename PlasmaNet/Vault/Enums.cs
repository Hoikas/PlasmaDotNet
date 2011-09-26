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
        kNodeIdx = (1 << 0),
        kCreateTime = (1 << 1),
        kModifyTime = (1 << 2),
        kCreateAgeName = (1 << 3),
        kCreateAgeUuid = (1 << 4),
        kCreatorUuid = (1 << 5),
        kCreatorIdx = (1 << 6),
        kNodeType = (1 << 7),
        kInt32_1 = (1 << 8),
        kInt32_2 = (1 << 9),
        kInt32_3 = (1 << 10),
        kInt32_4 = (1 << 11),
        kUInt32_1 = (1 << 12),
        kUInt32_2 = (1 << 13),
        kUInt32_3 = (1 << 14),
        kUInt32_4 = (1 << 15),
        kUuid_1 = (1 << 16),
        kUuid_2 = (1 << 17),
        kUuid_3 = (1 << 18),
        kUuid_4 = (1 << 19),
        kString64_1 = (1 << 20),
        kString64_2 = (1 << 21),
        kString64_3 = (1 << 22),
        kString64_4 = (1 << 23),
        kString64_5 = (1 << 24),
        kString64_6 = (1 << 25),
        kIString64_1 = (1 << 26),
        kIString64_2 = (1 << 27),
        kText_1 = (1 << 28),
        kText_2 = (1 << 29),
        kBlob_1 = (1 << 30),
        kBlob_2 = (1 << 31)
    }
}

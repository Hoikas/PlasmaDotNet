using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class pfKIMsg : plMessage {

        public enum Cmd {
            ChatMsg, EnterChatMode, SetChatFadeDelay, SetTextChatAdminMode,
            DisableKIandBB, EnableKIandBB, YesNoDialog, AddPlayerDevice,
            RemovePlayerDevice, UpgradeKILevel, DowngradeKILevel, RateIt,
            SetPrivateChatChannel, UnsetPrivateChatChannel, StartBookAlert,
            MiniBigKIToggle, KIPutAway, ChatAreaPageUp, ChatAreaPageDown,
            ChatAreaGoToBegin, ChatAreaGoToEnd, KITakePicture,
            KICreateJournalNote, KIToggleFade, KIToggleFadeEnable,
            KIChatStatusMsg, KILocalChatStatusMsg, KIUpSizeFont, KIDownSizeFont,
            KIOpenYeehsaBook, KIOpenKI, KIShowCCRHelp, KICreateMarker,
            KICreateMarkerFolder, KILocalChatErrorMsg, KIPhasedAllOn,
            KIPhasedAllOff, KIOKDialog, DisableYeeshaBook, EnableYeeshaBook,
            QuitDialog, TempDisableKIandBB, TempEnableKIandBB,
            DisableEntireYeeshaBook, EnableEntireYeeshaBook, KIOKDialogNoQuit,
            GZUpdated, GZInRange, GZOutRange, UpgradeKIMarkerLevel,
            KIShowMiniKI, GZFlashUpdate, StartJournalAlert, AddJournalBook,
            RemoveJournalBook, KIOpenJournalBook, MGStartCGZGame, MGStopCGZGame,
            KICreateMarkerNode, StartKIAlert, UpdatePelletScore, FriendInviteSent,
            RegisterImager, NoCommand
        }

        [Flags]
        enum Flags {
            kPrivateMsg = 0x1,
            kAdminMsg = 0x2,
            kDead = 0x4,
            kUNUSED1 = 0x8,
            kStatusMsg = 0x10,
            kNeighborMsg = 0x20,
            kChannelMask = 0xFF00
        }

        Cmd fCommand = Cmd.NoCommand;
        string fUser;
        uint fPlayerID;
        string fString = "Anonymous Coward"; //Bad things happen if this is NULL/empty
        Flags fFlags;
        float fDelay;
        int fValue;

        #region Properties
        public Cmd Command {
            get { return fCommand; }
            set { fCommand = value; }
        }

        public string Message {
            get { return fString; }
            set { fString = value; }
        }

        public uint PlayerID {
            get { return fPlayerID; }
            set { fPlayerID = value; }
        }

        public string UserName {
            get { return fUser; }
            set { fUser = value; }
        }
        #endregion

        public pfKIMsg() {
            fBCastFlags |= plBCastFlags.kBCastByExactType;
        }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            fCommand = (Cmd)s.ReadByte();
            fUser = s.ReadSafeString();
            fPlayerID = s.ReadUInt();
            fString = s.ReadSafeWString();
            fFlags = (Flags)s.ReadInt();
            fDelay = s.ReadFloat();
            fValue = s.ReadInt();
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);

            s.WriteByte((byte)fCommand);
            s.WriteSafeString(fUser);
            s.WriteUInt(fPlayerID);
            s.WriteSafeWString(fString);
            s.WriteInt((int)fFlags);
            s.WriteFloat(fDelay);
            s.WriteInt(fValue);
        }
    }
}

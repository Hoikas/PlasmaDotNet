using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plNotifyMsg : plMessage {

        public enum NotificationType {
            Activator, Variable, NotifySelf, 
            ResponderFF, ResponderChangeState
        }

        NotificationType fType;
        float fState;
        int fID;
        List<proEventData> fEvents = new List<proEventData>();

        public NotificationType Notify {
            get { return fType; }
            set { fType = value; }
        }

        public float State {
            get { return fState; }
            set { fState = value; }
        }

        public int ID {
            get { return fID; }
            set { fID = value; }
        }

        public List<proEventData> Events {
            get { return fEvents; }
        }

        public plNotifyMsg() {
            fBCastFlags |= plBCastFlags.kNetPropagate;
        }

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);

            fType = (NotificationType)s.ReadInt();
            fState = s.ReadFloat();
            if (s.Version < plVersion.EndOfAges) 
                fID = s.ReadInt();
            else 
                fID = (int)s.ReadByte();

            fEvents.Capacity = s.ReadInt();
            for (int i = 0; i < fEvents.Capacity; i++)
                fEvents.Add(proEventData.Read(s, mgr));
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            base.Write(s, mgr);

            s.WriteInt((int)fType);
            s.WriteFloat(fState);
            if (s.Version < plVersion.EndOfAges)
                s.WriteInt(fID);
            else
                s.WriteByte((byte)fID);

            s.WriteInt(fEvents.Count);
            foreach (proEventData ped in fEvents)
                ped.Write(s, mgr);
        }
    }

    public abstract class proEventData {

        public enum EventType {
            Collision = 1, Picked, ControlKey, Variable, Facing, Contained,
            Activate, Callback, ResponderState, MultiStage, Spawned,
            ClickDrag, Coop, OfferLinkingBook, Book, ClimbingBlockerHit,
            None
        }

        public enum DataType { 
            Number = 1, Key, Notta }

        private static proEventData ICreateEventDataType(EventType type) {
            switch (type) {
                default:
                    return null;
            }
        }

        public static proEventData Read(hsStream s, hsResMgr mgr) {
            proEventData e = ICreateEventDataType((EventType)s.ReadInt());
            if (e != null)
                e.IRead(s, mgr);
            return e;
        }

        public void Write(hsStream s, hsResMgr mgr) {
            if (this is proActivateEventData)
                s.WriteInt((int)EventType.Activate);
            else if (this is proBookEventData)
                s.WriteInt((int)EventType.Book);
            else if (this is proCallbackEventData)
                s.WriteInt((int)EventType.Callback);
            else if (this is proClickDragEventData)
                s.WriteInt((int)EventType.ClickDrag);
            else if (this is proClimbingBlockerHitEventData)
                s.WriteInt((int)EventType.ClimbingBlockerHit);
            else if (this is proCollisionEventData)
                s.WriteInt((int)EventType.Collision);
            else if (this is proContainedEventData)
                s.WriteInt((int)EventType.Contained);
            else if (this is proControlKeyEventData)
                s.WriteInt((int)EventType.ControlKey);
            else if (this is proCoopEventData)
                s.WriteInt((int)EventType.Coop);
            else if (this is proFacingEventData)
                s.WriteInt((int)EventType.Facing);
            else if (this is proMultiStageEventData)
                s.WriteInt((int)EventType.MultiStage);
            else if (this is proOfferLinkingBookEventData)
                s.WriteInt((int)EventType.OfferLinkingBook);
            else if (this is proPickedEventData)
                s.WriteInt((int)EventType.Picked);
            else if (this is proResponderStateEventData)
                s.WriteInt((int)EventType.ResponderState);
            else if (this is proSpawnedEventData)
                s.WriteInt((int)EventType.Spawned);
            else if (this is proVariableEventData)
                s.WriteInt((int)EventType.Variable);
            else
                s.WriteInt((int)EventType.None);

            IWrite(s, mgr);
        }

        protected abstract void IRead(hsStream s, hsResMgr mgr);
        protected abstract void IWrite(hsStream s, hsResMgr mgr);
    }

    public sealed class proActivateEventData : proEventData {

        bool fActive, fActivate;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fActive = s.ReadBool();
            fActivate = s.ReadBool();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteBool(fActive);
            s.WriteBool(fActivate);
        }
    }

    public sealed class proBookEventData : proEventData {

        uint fEvent, fLinkID;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fEvent = s.ReadUInt();
            fLinkID = s.ReadUInt();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteUInt(fEvent);
            s.WriteUInt(fLinkID);
        }
    }

    public sealed class proCallbackEventData : proEventData {

        EventType fEventType;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fEventType = (EventType)s.ReadInt();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteInt((int)fEventType);
        }
    }

    public sealed class proClickDragEventData : proEventData {
        protected override void IRead(hsStream s, hsResMgr mgr) { }
        protected override void IWrite(hsStream s, hsResMgr mgr) { }
    }

    public sealed class proClimbingBlockerHitEventData : proEventData {

        plKey fBlockerKey;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fBlockerKey = mgr.ReadKey(s);
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            mgr.WriteKey(s, fBlockerKey);
        }
    }

    public sealed class proCollisionEventData : proEventData {

        bool fEnter;
        plKey fHitter, fHittee;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fEnter = s.ReadBool();
            fHitter = mgr.ReadKey(s);
            fHittee = mgr.ReadKey(s);
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteBool(fEnter);
            mgr.WriteKey(s, fHitter);
            mgr.WriteKey(s, fHittee);
        }
    }

    public sealed class proContainedEventData : proEventData {

        plKey fContained, fContainer;
        bool fEntering;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fContained = mgr.ReadKey(s);
            fContainer = mgr.ReadKey(s);
            fEntering = s.ReadBool();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            mgr.WriteKey(s, fContained);
            mgr.WriteKey(s, fContainer);
            s.WriteBool(fEntering);
        }
    }

    public sealed class proControlKeyEventData : proEventData {

        int fControlKey;
        bool fDown;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fControlKey = s.ReadInt();
            fDown = s.ReadBool();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteInt(fControlKey);
            s.WriteBool(fDown);
        }
    }

    public sealed class proCoopEventData : proEventData {

        uint fID;
        ushort fSerial;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fID = s.ReadUInt();
            fSerial = s.ReadUShort();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteUInt(fID);
            s.WriteUShort(fSerial);
        }
    }

    public sealed class proFacingEventData : proEventData {

        plKey fFacer, fFacee;
        float fDot;
        bool fEnabled;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fFacer = mgr.ReadKey(s);
            fFacee = mgr.ReadKey(s);
            fDot = s.ReadFloat();
            fEnabled = s.ReadBool();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            mgr.WriteKey(s, fFacer);
            mgr.WriteKey(s, fFacee);
            s.WriteFloat(fDot);
            s.WriteBool(fEnabled);
        }
    }

    public sealed class proMultiStageEventData : proEventData {

        int fStage, fEvent;
        plKey fAvatar;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fStage = s.ReadInt();
            fEvent = s.ReadInt();
            fAvatar = mgr.ReadKey(s);
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteInt(fStage);
            s.WriteInt(fEvent);
            mgr.WriteKey(s, fAvatar);
        }
    }

    public sealed class proOfferLinkingBookEventData : proEventData {

        plKey fOfferer;
        int fTargetAge, fOfferee;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fOfferer = mgr.ReadKey(s);
            fTargetAge = s.ReadInt();
            fOfferee = s.ReadInt();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            mgr.WriteKey(s, fOfferer);
            s.WriteInt(fTargetAge);
            s.WriteInt(fOfferee);
        }
    }

    public sealed class proPickedEventData : proEventData {

        plKey fPicker, fPicked;
        bool fEnabled;
        hsPoint3 fHitPoint = new hsPoint3();

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fPicker = mgr.ReadKey(s);
            fPicked = mgr.ReadKey(s);
            fEnabled = s.ReadBool();
            fHitPoint.Read(s);
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            mgr.WriteKey(s, fPicker);
            mgr.WriteKey(s, fPicked);
            s.WriteBool(fEnabled);
            fHitPoint.Write(s);
        }
    }

    public sealed class proResponderStateEventData : proEventData {

        int fState;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fState = s.ReadInt();
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteInt(fState);
        }
    }

    public sealed class proSpawnedEventData : proEventData {

        plKey fSpawner, fSpawnee;

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fSpawner = mgr.ReadKey(s);
            fSpawnee = mgr.ReadKey(s);
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            mgr.WriteKey(s, fSpawner);
            mgr.WriteKey(s, fSpawnee);
        }
    }

    public sealed class proVariableEventData : proEventData {

        string fName;
        DataType fDataType = DataType.Notta;
        float fNumber;
        plKey fKey;

        public string Name {
            get { return fName; }
            set { fName = value; }
        }

        public DataType DataType {
            get { return fDataType; }
            set { fDataType = value; }
        }

        public float Number {
            get { return fNumber; }
            set { fNumber = value; }
        }

        public plKey Key {
            get { return fKey; }
            set { fKey = value; }
        }

        protected override void IRead(hsStream s, hsResMgr mgr) {
            fName = s.ReadSafeString();
            fDataType = (DataType)s.ReadInt();
            fNumber = s.ReadFloat();
            fKey = mgr.ReadKey(s);
        }

        protected override void IWrite(hsStream s, hsResMgr mgr) {
            s.WriteSafeString(fName);
            s.WriteInt((int)fDataType);
            s.WriteFloat(fNumber);
            mgr.WriteKey(s, fKey);
        }
    }
}

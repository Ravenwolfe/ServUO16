using System;

namespace Server.Items
{
    [FlipableAttribute(0x26BC, 0x26C6)]
    public class Scepter : BaseBashing
    {
        [Constructable]
        public Scepter()
            : base(0x26BC)
        {
            this.Weight = 8.0;
        }

        public Scepter(Serial serial)
            : base(serial)
        {
        }

        public override int OldStrengthReq
        {
            get
            {
                return 40;
            }
        }
        public override int OldMinDamage
        {
            get
            {
                return 14;
            }
        }
        public override int OldMaxDamage
        {
            get
            {
                return 17;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 30;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 31;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 110;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
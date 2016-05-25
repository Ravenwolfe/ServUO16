using System;

namespace Server.Items
{
    [FlipableAttribute(0x143B, 0x143A)]
    public class Maul : BaseBashing
    {
        [Constructable]
        public Maul()
            : base(0x143B)
        {
            this.Weight = 10.0;
        }

        public Maul(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility
        {
            get
            {
                return WeaponAbility.DoubleStrike;
            }
        }
        public override WeaponAbility SecondaryAbility
        {
            get
            {
                return WeaponAbility.ConcussionBlow;
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
                return 70;
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

            if (this.Weight == 14.0)
                this.Weight = 10.0;
        }
    }
}
using System;

namespace Server.Items
{
    [FlipableAttribute(0xF62, 0xF63)]
    public class TribalSpear : BaseSpear
    {
        [Constructable]
        public TribalSpear()
            : base(0xF62)
        {
            this.Weight = 7.0;
            this.Hue = 837;
        }

        public TribalSpear(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility
        {
            get
            {
                return WeaponAbility.ArmorIgnore;
            }
        }
        public override WeaponAbility SecondaryAbility
        {
            get
            {
                return WeaponAbility.ParalyzingBlow;
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
                return 80;
            }
        }
        public override int VirtualDamageBonus
        {
            get
            {
                return 25;
            }
        }
        public override string DefaultName
        {
            get
            {
                return "a tribal spear";
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
using System;
using Server.Mobiles;

namespace Server.Items
{
    /// <summary>
    /// Perfect for the foot-soldier, the Dismount special attack can unseat a mounted opponent.
    /// The fighter using this ability must be on his own two feet and not in the saddle of a steed
    /// (with one exception: players may use a lance to dismount other players while mounted).
    /// If it works, the target will be knocked off his own mount and will take some extra damage from the fall!
    /// </summary>
    public class Dismount : WeaponAbility
    {
        public static readonly TimeSpan DefenderRemountDelay = TimeSpan.FromSeconds(10.0);// TODO: Taken from bola script, needs to be verified
        public static readonly TimeSpan AttackerRemountDelay = TimeSpan.FromSeconds(3.0);
        public Dismount()
        {
        }

        public override int BaseMana
        {
            get
            {
                return 20;
            }
        }
        public override bool Validate(Mobile from)
        {
            if (!base.Validate(from))
                return false;

            if ( (from.Mounted) && !(from.Weapon is Lance) )
            {
                from.SendLocalizedMessage(1061283); // You cannot perform that attack while mounted or flying!
                return false;
            }

            return true;
        }

        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!this.Validate(attacker))
                return;

            if (defender is ChaosDragoon || defender is ChaosDragoonElite)
                return;

            if ((attacker.Mounted || attacker.Flying) && (!(attacker.Weapon is Lance) && !(defender.Weapon is Lance))) // TODO: Should there be a message here?
                return;

            ClearCurrentAbility(attacker);

            IMount mount = defender.Mount;

            if (mount == null)
            {
                attacker.SendLocalizedMessage(1060848); // This attack only works on mounted or flying targets
                return;
            }

            if (!this.CheckMana(attacker, true))
                return;

            attacker.SendLocalizedMessage(1060082); // The force of your attack has dislodged them from their mount!

            if (defender.Flying)
                defender.SendLocalizedMessage(1113590, attacker.Name); // You have been grounded by ~1_NAME~!
			else if ( attacker.Mounted )
				defender.SendLocalizedMessage( 1062315 ); // You fall off your mount!
			else
				defender.SendLocalizedMessage( 1060083 ); // You fall off of your mount and take damage!

            defender.PlaySound(0x140);
            defender.FixedParticles(0x3728, 10, 15, 9955, EffectLayer.Waist);

            if (defender is PlayerMobile)
            {
                if (defender.Mounted)
                {
                    defender.SendLocalizedMessage(1040023); // You have been knocked off of your mount!
                }

                (defender as PlayerMobile).SetMountBlock(BlockMountType.Dazed, TimeSpan.FromSeconds(10), true);
            }
            else if (mount != null)
            {
                mount.Rider = null;
            }

            if (attacker is PlayerMobile)
            {
                (attacker as PlayerMobile).SetMountBlock(BlockMountType.DismountRecovery, TimeSpan.FromSeconds(10), false);
            }
            else if (Core.ML && attacker is BaseCreature)
            {
                BaseCreature bc = attacker as BaseCreature;

                if (bc.ControlMaster is PlayerMobile)
                {
                    PlayerMobile pm = bc.ControlMaster as PlayerMobile;

                    pm.SetMountBlock(BlockMountType.DismountRecovery, TimeSpan.FromSeconds(10), false);
                }
            }
				
            if (!attacker.Mounted)
                defender.Damage(Utility.RandomMinMax(15, 25), attacker);
        }
    }
}
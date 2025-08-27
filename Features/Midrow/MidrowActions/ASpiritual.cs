using FSPRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features.Midrow.MidrowActions
{
    public class ASpiritual : CardAction
    {
        bool returning;

        public bool targetPlayer;

        public int outgoingDamage;

        public bool Return;

        public bool Fired;

        public int worldX;

        public Status? status;

        public int statusAmount = 1;

        public bool weaken;
        public override bool CanSkipTimerIfLastEvent()
        {
            return false;
        }
        
        public override void Update(G g, State s, Combat c)
        {
            c.stuff.TryGetValue(worldX, out StuffBase value);

            if (!(value is SpiritualWeapon spiritualWeapon))
            {
                timer -= g.dt;
                return;
            }

            Ship ship = (targetPlayer ? s.ship : c.otherShip);
            if (ship == null)
            {
                return;
            }

            RaycastResult raycastResult = CombatUtils.RaycastGlobal(c, ship, fromDrone: true, worldX);
            bool flag = false;
            if (raycastResult.hitShip)
            {
                Part? partAtWorldX = ship.GetPartAtWorldX(raycastResult.worldX);
                if (partAtWorldX == null || partAtWorldX.type != PType.empty)
                {
                    flag = true;
                }
            }


            if (!spiritualWeapon.isHitting && returning == false)
            {
                Audio.Play(flag ? Event.Drones_MissileIncoming : Event.Drones_MissileMiss);
                spiritualWeapon.isHitting = true;
            }

            if (!(spiritualWeapon.yAnimation >= 3.5) && returning == false)
            {
                return;
            }

            if (flag && Fired == false)
            {
                Fired = true;
                int num = outgoingDamage;
                foreach (Artifact item in s.EnumerateAllArtifacts())
                {
                    num += item.ModifyBaseMissileDamage(s, s.route as Combat, targetPlayer);
                }

                if (num < 0)
                {
                    num = 0;
                }

                DamageDone dmg = ship.NormalDamage(s, c, num, raycastResult.worldX);
                EffectSpawner.NonCannonHit(g, targetPlayer, raycastResult, dmg);
                Part? partAtWorldX2 = ship.GetPartAtWorldX(raycastResult.worldX);
                if (partAtWorldX2 != null && partAtWorldX2.stunModifier == PStunMod.stunnable)
                {
                    c.QueueImmediate(new AStunPart
                    {
                        worldX = raycastResult.worldX
                    });
                }


                if (ship.Get(Status.payback) > 0 || ship.Get(Status.tempPayback) > 0)
                {
                    c.QueueImmediate(new AAttack
                    {
                        damage = Card.GetActualDamage(s, ship.Get(Status.payback) + ship.Get(Status.tempPayback), !targetPlayer),
                        targetPlayer = !targetPlayer,
                        fast = true
                    });
                }
            }


            if (!(raycastResult.hitDrone || flag))
            {
                //spiritualWeapon.returning = false;
                c.stuff.Remove(worldX);
                c.stuffOutro.Add(spiritualWeapon);
            }
            else
            {
                
                //spiritualWeapon.returning = true;

                /*if ((!(spiritualWeapon.yAnimation <= 10) || !(spiritualWeapon.yAnimation >= 4)) && returning == false)
                {
                    return;
                }
                else */ if ((returning == true && spiritualWeapon.yAnimation <= 2) || !(spiritualWeapon.yAnimation <= 14))
                {
                    spiritualWeapon.isHitting = false;
                    timer = 0;
                }
                else
                {
                    returning = true;
                    //spiritualWeapon.isHitting = true;
                    spiritualWeapon.yAnimation = (spiritualWeapon.yAnimation - 0.4);
                    return;
                }
                //c.stuff.Remove(worldX);

            }
            //spiritualWeapon.isHitting = false;
            timer = 0;
        }
    }
}

using FSPRO;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Angder.EchoesOfTheFuture.Features
{
    public class AShatter : AHurt
    {
        public override List<Tooltip> GetTooltips(State s)
        {
            string key;
            string name;
            string description;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Shatter::Normal";
            name = ModEntry.Instance.Localizations.Localize(["action", "Shatter", "name"]);
            description = ModEntry.Instance.Localizations.Localize(["action", "Shatter", "description"]);


            List<Tooltip> tooltips = [
            new GlossaryTooltip(key)
            {

                    Icon = ModEntry.Instance.ShatterIcon.Sprite,
                    TitleColor = Colors.action,
                    Title = name,
                    Description = description
            }

            ];
            return tooltips;
        }

        public override Icon? GetIcon(State s)
        {
            return new(ModEntry.Instance.ShatterIcon.Sprite, hurtAmount, Colors.hurt);
        }
        public override void Begin(G g, State s, Combat c)
        {
            //STANDARD AHURT BOLT NONSENSE
            Ship ship = (targetPlayer ? s.ship : c.otherShip);
            if (ship == null)
            {
                return;
            }

            if (cannotKillYou)
            {
                if (hurtShieldsFirst)
                {
                    hurtAmount = Math.Min(hurtAmount, ship.hull - 1 + ship.Get(Status.shield) + ship.Get(Status.tempShield));
                }
                else
                {
                    hurtAmount = Math.Min(hurtAmount, ship.hull - 1);
                }
            }
            for (int i = 0; i < hurtAmount * 15; i++)
            {
                PFX.combatAdd.Add(new Particle
                {
                    pos = ship.GetWorldPos(g.state, c) + new Vec(Mutil.NextRand() * (double)ship.parts.Count * 16.0, (double)(ship.isPlayerShip ? 1 : (-1)) * (-10.0 + Mutil.NextRand() * 20.0)),
                    vel = Mutil.RandVel() * 30.0 + new Vec(20),
                    color = new Color(1, 0.5, 0.0).gain(0.8),
                    size = 2.0 + Mutil.NextRand(),
                    dragCoef = 4.0 + Mutil.NextRand(),
                    lifetime = 1.2 + Mutil.NextRand() * 1.0
                });
            }

            if (!hurtShieldsFirst)
            {
                ship.DirectHullDamage(s, c, hurtAmount);
            }
            else
            {
                ship.NormalDamage(s, c, hurtAmount, null);
            }
            Audio.Play(Event.Hits_HitHurt);


            //AATTACK stuff

            if (!targetPlayer)
            {
                AAttack Copyattack = new AAttack()
                {
                    damage = hurtAmount,
                    piercing = true,
                };
                c.QueueImmediate(new AJupiterShoot
                {
                    attackCopy = Copyattack
                });
            }
            if (ship.Get(Status.payback) > 0 || ship.Get(Status.tempPayback) > 0)
            {
                c.QueueImmediate(new AAttack
                {
                    damage = Card.GetActualDamage(s, ship.Get(Status.payback) + ship.Get(Status.tempPayback), !targetPlayer),
                    targetPlayer = !targetPlayer,
                    fast = true,
                    storyFromPayback = true
                });
            }
            foreach (Artifact item4 in s.EnumerateAllArtifacts())
            {
                item4.OnPlayerAttack(s, c);
            }
        }
    }
}

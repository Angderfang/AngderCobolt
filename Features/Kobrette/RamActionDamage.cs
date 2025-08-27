using Angder.EchoesOfTheFuture.Features.Kobrette;
using FSPRO;
using Nickel;
using System;
using System.Collections.Generic;
using System.IO;

namespace Angder.EchoesOfTheFuture;

public class ARamdam : CardAction
{
    //public int Truehull;
    public bool Piercing;
    //public int ModifiedDmg;
    int pain;
    int Attacking;

    public override void Begin(G g, State s, Combat c)
    {
        if (s.ship == null)
        {
            return;
        }
        else if (c.otherShip == null)
        {
            return;
        }
        else if (s.ship.x >= c.otherShip.x + c.otherShip.parts.Count || s.ship.x + s.ship.parts.Count <= c.otherShip.x)
        {
            return;
        }
        /* Wait, this is just two AHurt instances in a trenchcoat!! */

        if (Piercing)
        {
            Attacking = s.ship.hull;
            pain = s.ship.hull / 2;
            c.otherShip.DirectHullDamage(s, c, Attacking);
            s.ship.DirectHullDamage(s, c, pain);
        }
        else
        {
            Attacking = s.ship.hull;
            pain = s.ship.hull / 2;
            c.otherShip.NormalDamage(s, c, Attacking, null);
            s.ship.NormalDamage(s, c, pain, null);
            
        }
        Audio.Play(Event.Hits_HitHurt);
    }
}
using Nickel;
using System.Collections.Generic;

namespace Angder.EchoesOfTheFuture;

public class ARam : CardAction
{
    public int Truehull;
    public bool Piercing;
    public int ModifiedDmg;
    public override List<Tooltip> GetTooltips(State s)
    {
        string key;
        string name;
        string description;
        ISpriteEntry icon;
        icon = ModEntry.Instance.Ram;
        key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Ram::Normal";
        if (Piercing == true)
        {
            name = ModEntry.Instance.Localizations.Localize(["action", "Ramaction", "name", "Piercing"]);
            description = ModEntry.Instance.Localizations.Localize(["action", "Ramaction", "description", "Piercing"]);
        }
        else
        {
            name = ModEntry.Instance.Localizations.Localize(["action", "Ramaction", "name", "normal"]);
            description = ModEntry.Instance.Localizations.Localize(["action", "Ramaction", "description", "Damage"]);
        }
        List<Tooltip> tooltips = [
        new GlossaryTooltip(key)
        {
                    Icon = icon.Sprite,
                    TitleColor = Colors.action,
                    Title = name,
                    Description = description
        }
        ];
        return tooltips;
    }
    public override Icon? GetIcon(State s)
    {
        if (Piercing == true)
        {
            return new(ModEntry.Instance.RamPierce.Sprite, s.ship.hull+ModifiedDmg, Colors.hurt);
        }
        else
        {
            return new(ModEntry.Instance.Ram.Sprite, s.ship.hull+ModifiedDmg, Colors.hurt);
        }
    }



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

        /* Wait, this is just two AHurt instances in a trenchcoat!! */

        if (Piercing)
        {
            if (c.otherShip.Get(Status.perfectShield) > 0)
                c.QueueImmediate(new AHurt()
                {
                    targetPlayer = true,
                    hurtShieldsFirst = false,
                    hurtAmount = c.otherShip.hull, //Ramming an invulnerable enemy is a bad idea
                    omitFromTooltips = true
                });
            else
            {
                c.QueueImmediate(new AHurt()
                {
                    targetPlayer = true,
                    hurtShieldsFirst = false,
                    hurtAmount = c.otherShip.hull - s.ship.hull, //Yes, this works... unless the enemy has some kind of shield up I guess? eh. If the player decides to use it against an enemy which is 100% resistant to damage, it's probably better they don't explode completely.
                    omitFromTooltips = true
                });
                //B is the spiciest upgrade

                c.QueueImmediate(new AHurt()
                {
                    targetPlayer = false,
                    hurtShieldsFirst = false,
                    hurtAmount = s.ship.hull + ModifiedDmg,
                });
            }
        }
        else
        {
            if (Truehull < 0)
                Truehull = 0; // Fixes railcannon giving you tons of temp shield

            c.QueueImmediate(new AHurt() //Why Queue Immediate and not just Queue? Eh, Don't want to break what clearly works.
            {
                targetPlayer = true,
                hurtShieldsFirst = true,
                hurtAmount = Truehull,
                omitFromTooltips = true
            });

            c.QueueImmediate(new AHurt()
            {
                targetPlayer = false,
                hurtShieldsFirst = true,
                hurtAmount = s.ship.hull + ModifiedDmg,
            });
        }
    }
}
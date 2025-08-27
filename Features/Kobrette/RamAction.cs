using Angder.EchoesOfTheFuture.Features.Kobrette;
using Nickel;
using System.Collections.Generic;
using System.IO;

namespace Angder.EchoesOfTheFuture;

public class ARam : CardAction
{
    //public int Truehull;
    public bool Piercing;
    //public int ModifiedDmg;
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
            return new(ModEntry.Instance.RamPierce.Sprite, s.ship.hull, Colors.hurt);
        }
        else
        {
            return new(ModEntry.Instance.Ram.Sprite, s.ship.hull, Colors.hurt);
        }
        
    }
    
    

    public override void Begin(G g, State s, Combat c)
    {
        //Vec start = new Vec(s.ship.x, 0.0);
        //Vec Ram = new Vec(s.ship.x, 100.0);
        //Vec Return = new Vec(s.ship.x, 7.0);
        c.QueueImmediate(new ARamdam()
        {
            Piercing = Piercing
        });
        c.fx.Add(new RamFX()
        {
            age = -0.7
        });
    }
}
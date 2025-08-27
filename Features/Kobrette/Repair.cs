using FSPRO;
using Microsoft.Xna.Framework.Input;
using Nanoray.Pintail;
using Nickel;
using Shockah.Kokoro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features.Kobrette;

internal class ARepairMidrow : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        foreach (StuffBase item in c.stuff.Values.ToList())
        {
            

            if (item is FloatingShield)
            {
                c.stuff.Remove(item.x);
                
                FloatingShield value = new FloatingShield
                {
                    x = item.x,
                    xLerped = item.xLerped,
                    bubbleShield = item.bubbleShield,
                    targetPlayer = false,
                    age = item.age,
                    yAnimation = 0.0
                }; 
                c.stuff[item.x] = value;
            }
        }
        Audio.Play(Event.Status_PowerUp);
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        string key;
        string name;
        string description;
        key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::ARepairMidrow";
        name = ModEntry.Instance.Localizations.Localize(["action", "ARepairMidrow", "name"]);
        description = ModEntry.Instance.Localizations.Localize(["action", "ARepairMidrow", "description"]);
        List<Tooltip> tooltips = [
        new GlossaryTooltip(key)
        {

            Icon = ModEntry.Instance.ShieldIcon.Sprite,
            TitleColor = Colors.action,
            Title = name,
            Description = description
        }   
        ];
        return tooltips;
    }
    public override Icon? GetIcon(State s)
    {
        return new Icon(ModEntry.Instance.ShieldbashIcon.Sprite, null, Colors.textMain);
    }
}



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

internal class AShieldbash : CardAction
{

    public override void Begin(G g, State s, Combat c)
    {
        foreach (StuffBase item in c.stuff.Values.ToList())
        {
            

            if (item is FloatingShield)
            {
                c.stuff.Remove(item.x);
                Missile value = new Missile
                {
                    missileType = MissileType.heavy,
                    skin = "sword",
                    x = item.x,
                    xLerped = item.xLerped,
                    bubbleShield = item.bubbleShield,
                    targetPlayer = false,
                    age = item.age
                };
                c.stuff[item.x] = value;
            }
        }

        Audio.Play(Event.Status_PowerDown);
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        string key;
        string name;
        string description;
        key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::AShieldbash";
        name = ModEntry.Instance.Localizations.Localize(["action", "AShieldbash", "name"]);
        description = ModEntry.Instance.Localizations.Localize(["action", "AShieldbash", "description"]);
        List<Tooltip> tooltips = [
        new GlossaryTooltip(key)
        {

            Icon = ModEntry.Instance.ShieldbashIcon.Sprite,
            TitleColor = Colors.action,
            Title = name,
            Description = description
        },


        ];
        tooltips.Add(new TTGlossary("midrow.missile_heavy", 3));
        return tooltips;
    }
    public override Icon? GetIcon(State s)
    {
        return new Icon(ModEntry.Instance.ShieldbashIcon.Sprite, null, Colors.textMain);
    }
}



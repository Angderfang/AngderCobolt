using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features.Grunan
{
    public class AFlipdisable : CardAction
    {
        public Card? Flippingcard;
        // RAM tooltips lol. Nobody should ever see these.
        public override List<Tooltip> GetTooltips(State s)
        {
            string key;
            string name;
            string description;
            ISpriteEntry icon;
            icon = ModEntry.Instance.Ram;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Ram::Normal";
            name = ModEntry.Instance.Localizations.Localize(["action", "Ramaction", "name", "normal"]);
            description = ModEntry.Instance.Localizations.Localize(["action", "Ramaction", "description", "Damage"]);
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
            return new(ModEntry.Instance.RamPierce.Sprite, s.ship.hull, Colors.hurt);
        }



        public override void Begin(G g, State s, Combat c)
        {
            if (Flippingcard != null)
            {
                Flippingcard.flipped = true;
            }
        }
    }
}
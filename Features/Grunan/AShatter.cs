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
    }
}

using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features
{
    public class AVariableHintExhaustTen : AVariableHint
    {
        public override List<Tooltip> GetTooltips(State s)
        {
            string key;
            string name;
            string description;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::AVariableHintExhaustTen::Normal";
            name = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintExhaustTen", "name"]);
            
            description = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintExhaustTen", "description"]);

            List<Tooltip> tooltips = [
            new GlossaryTooltip(key)
            {
                    TitleColor = Colors.action,
                    Title = name,
                    Description = description
            }

            ];
            return tooltips;
            }

    }
}

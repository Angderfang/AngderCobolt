using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features
{
    public class AVariableHintExhaustFive : AVariableHint
    {
        public override List<Tooltip> GetTooltips(State s)
        {
            string key;
            string name;
            string description;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::AVariableHintExhaustFive::Normal";
            name = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintExhaustFive", "name"]);
            
            description = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintExhaustFive", "description"]);

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
    public class AVariableHintExhaustSix : AVariableHint
    {
        public override List<Tooltip> GetTooltips(State s)
        {
            string key;
            string name;
            string description;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::AVariableHintExhaustSix::Normal";
            name = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintExhaustSix", "name"]);

            description = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintExhaustSix", "description"]);

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
    public class AVariableHintExhaustThree : AVariableHint
    {
        public override List<Tooltip> GetTooltips(State s)
        {
            string key;
            string name;
            string description;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::AVariableHintExhaustThree::Normal";
            name = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintExhaustThree", "name"]);

            description = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintExhaustThree", "description"]);

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
    public class AVariableHintSingleusehand : AVariableHint
    {
        public override List<Tooltip> GetTooltips(State s)
        {
            string key;
            string name;
            string description;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::AVariableHintSingleusehand::Normal";
            name = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintSingleusehand", "name"]);

            description = ModEntry.Instance.Localizations.Localize(["action", "AVariableHintSingleusehand", "description"]);

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

using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features;
public class Dontplaycardsearch : ACardSelect
{

    public override Icon? GetIcon(State s)
    {
        Spr getsprite;
        getsprite = browseSource switch
        {
            CardBrowse.Source.Hand => ModEntry.Instance.HandExhaustone.Sprite,
            CardBrowse.Source.Deck => ModEntry.Instance.DeckExhaustone.Sprite,
            CardBrowse.Source.Codex => ModEntry.Instance.DeckExhaustone.Sprite,
            CardBrowse.Source.DrawPile => ModEntry.Instance.DrawExhaustone.Sprite,
            CardBrowse.Source.DiscardPile => ModEntry.Instance.DiscardExhaustone.Sprite,

            //Unused!
            CardBrowse.Source.DrawOrDiscardPile => ModEntry.Instance.DrawExhaustone.Sprite,
            CardBrowse.Source.ExhaustPile => ModEntry.Instance.DiscardExhaustone.Sprite,
        };
        return new Icon(getsprite, null, Colors.textMain);

    }
    public override List<Tooltip> GetTooltips(State s)
    {
        string key;
        string name;
        string description;
        ISpriteEntry icon;
        icon = browseSource switch
        {
            CardBrowse.Source.Hand => ModEntry.Instance.HandExhaustone,
            CardBrowse.Source.Deck => ModEntry.Instance.DeckExhaustone,
            CardBrowse.Source.Codex => ModEntry.Instance.DeckExhaustone,
            CardBrowse.Source.DrawPile => ModEntry.Instance.DrawExhaustone,
            CardBrowse.Source.DiscardPile => ModEntry.Instance.DiscardExhaustone,

            //UNUSED
            CardBrowse.Source.DrawOrDiscardPile => ModEntry.Instance.DrawExhaustone,
            CardBrowse.Source.ExhaustPile => ModEntry.Instance.DrawExhaustone,
        };
        key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Dontplaycardsearch::Normal";
        name = ModEntry.Instance.Localizations.Localize(["action", "Dontplaycardsearch", "name", "normal"]);
        description = ModEntry.Instance.Localizations.Localize(["action", "Dontplaycardsearch", "description", "Draw"]);
        switch (browseSource)
        {
            case CardBrowse.Source.Hand:
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Dontplaycardsearch::Hand";
                description = ModEntry.Instance.Localizations.Localize(["action", "Dontplaycardsearch", "description", "Hand"]);
            break;
            case CardBrowse.Source.Deck:
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Dontplaycardsearch::Deck";
                description = ModEntry.Instance.Localizations.Localize(["action", "Dontplaycardsearch", "description", "Deck"]);
            break;
            case CardBrowse.Source.DiscardPile:
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Dontplaycardsearch::Discard";
                description = ModEntry.Instance.Localizations.Localize(["action", "Dontplaycardsearch", "description", "Discard"]);
            break;
            case CardBrowse.Source.DrawPile:
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Dontplaycardsearch::Draw";
                description = ModEntry.Instance.Localizations.Localize(["action", "Dontplaycardsearch", "description", "Draw"]);
            break;
        };
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
}

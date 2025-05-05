using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features;
using Angder.EchoesOfTheFuture.Features.Grunan;
using Nickel;
using OneOf.Types;
using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;
using Angder.EchoesOfTheFuture.Cards;

namespace Angder.EchoesOfTheFuture.Cards;

//NEEDS HARMONY PATCH FOR UPGRADING ONLY SINGLE USE CARDS


internal sealed class Empower : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Empower", new()
        {


            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.common,
                
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Empower", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "Empower", "description", upgrade.ToString()]),
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = upgrade == Upgrade.A ? 0 : 1,
            exhaust = true
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                new ACardSelectImproved
                    {
                    browseAction = new UpgradeCardBrowseEmpower(),
                    browseSource = CardBrowse.Source.Hand,
                    filterUpgrade = Upgrade.None,
                    filterUUID = uuid
                    }.ApplyModData(CardBrowseFilterManager.FilterOnlySingleUseKey, true),
                new ACardSelectImproved
                    {
                    browseAction = new UpgradeCardBrowseEmpower(),
                    browseSource = CardBrowse.Source.Hand,
                    filterUpgrade = Upgrade.None,
                    filterUUID = uuid
                    }.ApplyModData(CardBrowseFilterManager.FilterOnlySingleUseKey, true),
                };
        break;
            case Upgrade.A:
                actions = new()
                {
                    new ACardSelectImproved
                    {
                    browseAction = new UpgradeCardBrowseEmpower(),
                    browseSource = CardBrowse.Source.Hand,
                    filterUpgrade = Upgrade.None,
                    filterUUID = uuid
                    }.ApplyModData(CardBrowseFilterManager.FilterOnlySingleUseKey, true),
                new ACardSelectImproved
                    {
                    browseAction = new UpgradeCardBrowseEmpower(),
                    browseSource = CardBrowse.Source.Hand,
                    filterUpgrade = Upgrade.None,
                    filterUUID = uuid
                    }.ApplyModData(CardBrowseFilterManager.FilterOnlySingleUseKey, true),
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                new ACardSelectImproved
                    {
                    browseAction = new UpgradeCardBrowseEmpower(),
                    browseSource = CardBrowse.Source.Deck,
                    filterUpgrade = Upgrade.None,
                    filterUUID = uuid
                    }.ApplyModData(CardBrowseFilterManager.FilterOnlySingleUseKey, true),
                };
        break;
        }
        return actions;
    }
}

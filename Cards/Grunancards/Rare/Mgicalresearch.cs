using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features.Grunan;
using FMOD;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class EndlessResearch : Card, IAngderCard //IHasCustomCardTraits
{
    //public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state) => new HashSet<ICardTraitEntry>() { ModEntry.Instance.RemoteControl };
    //public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state) => new HashSet<ICardTraitEntry>() { ModEntry.Instance.Consuming };
    //bool Used = false;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("EndlessResearch", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.rare,
                
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "EndlessResearch", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "EndlessResearch", "description", upgrade.ToString(), flipped.ToString()]),
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = 0,
            exhaust = true,
            floppable = false,

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
                new ACardOffering
                    {
                    amount = 3,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //disabled = flipped
                    },
                };
                //flipped = true;
            break;
            case Upgrade.A:
                actions = new()
                {
                new ACardOffering
                    {
                    amount = 4,
                    limitDeck = ModEntry.Instance.GrunanDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                   // disabled = flipped
                    },
                };
                //flipped = true;
                break;
            case Upgrade.B:
                actions = new()
                {
                new ACardOffering
                    {
                    amount = 5,
                    //limitDeck = ModEntry.Instance.AngderDeck.Deck,
                    makeAllCardsTemporary = false,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    //disabled = flipped
                    },
                };
                break;
        }
        
        return actions;
    }
}

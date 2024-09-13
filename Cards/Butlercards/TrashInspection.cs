using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardPlayExhaust : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("PlayExhaust", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PlayExhaust", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "PlayExhaust", "description", upgrade.ToString()]),
            //art = ModEntry.Instance.Angder_Enraged.Sprite,
            cost = upgrade == Upgrade.B ? 3 : upgrade == Upgrade.A ? 1 : 2,
            //retain = upgrade == Upgrade.A ? true : false,
            exhaust = upgrade == Upgrade.B ? false : true,
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
                new ACardSelect
                {
                    browseAction = new PlayTrash(),
                    browseSource = CardBrowse.Source.ExhaustPile,
                    filterUUID = uuid
                },
        };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                new ACardSelect
                {
                    browseAction = new ChooseCardToPutInHand(),
                    browseSource = CardBrowse.Source.ExhaustPile,
                    filterUUID = uuid
                },
                new APlayOtherCard
                {
                    handPosition = c.hand.Count -1,
                    timer = 0.5,
                    exhaustThisCardAfterwards = false
                }

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                new ACardSelect
                {
                    browseAction = new ChooseCardToPutInHand(),
                    browseSource = CardBrowse.Source.ExhaustPile,
                    filterUUID = uuid
                },
                new APlayOtherCard
                {
                    handPosition = c.hand.Count -1,
                    timer = 0.5,
                    exhaustThisCardAfterwards = false
                }

                };
                break;
        }
        return actions;
    }
}

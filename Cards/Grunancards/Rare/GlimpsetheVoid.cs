using Angder.EchoesOfTheFuture.Features;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class GlimpseTheVoid : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("GlimpseTheVoid", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "GlimpseTheVoid", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Eldrich.Sprite,
            cost = 1,
            exhaust = true,
            retain = upgrade == Upgrade.A ? true : false,
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

                    new AStatus()
                    {
                        status = ModEntry.Instance.Voidsight.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = ModEntry.Instance.Voidsight.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.Voidsight.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },


                };
                break;
        }
        return actions;
    }
}

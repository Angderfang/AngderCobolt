using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.Angdermod.Cards;

internal sealed class CardAnxiety : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Anxiety", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Anxiety", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 1,
            art = ModEntry.Instance.Angder_Crates.Sprite,
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
                        status = Status.shield,
                        targetPlayer = true,
                        statusAmount = 1
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Disrupt.Status,
                        statusAmount = 2,
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
                        status = Status.shield,
                        targetPlayer = true,
                        statusAmount = 2
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Disrupt.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },


                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AStatus()
                    {
                        status = Status.tempShield,
                        targetPlayer = true,
                        statusAmount = 3
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Disrupt.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}

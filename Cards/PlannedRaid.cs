using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.Angdermod.Cards;

internal sealed class CardPlannedRaid : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Plannedraid", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Plannedraid", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Crates.Sprite,
            cost = 1,
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
                    
                    new AStatus()
                    {
                        status = ModEntry.Instance.Theft.Status,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                    //Heist!

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = ModEntry.Instance.Theft.Status,
                        statusAmount = 5,
                        targetPlayer = true
                    },


                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.Theft.Status,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.FuelSiphon.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}

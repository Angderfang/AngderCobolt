using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.Angdermod.Cards;

internal sealed class CardSiphonFuel : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SiphonFuel", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SiphonFuel", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Crates.Sprite,
            cost = upgrade == Upgrade.B ? 2 : 1,
            exhaust = upgrade == Upgrade.B ? true : false,
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
                        status = ModEntry.Instance.FuelSiphon.Status,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                    //Stealing fuel!

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.FuelSiphon.Status,
                        statusAmount = 6,
                        targetPlayer = true
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.FuelDiscard.Status,
                        statusAmount = 3,
                        targetPlayer = true
                    },

                };
                break;
        }
        return actions;
    }
}

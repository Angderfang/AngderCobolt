using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.Angdermod.Cards;

internal sealed class CardEnrage : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Enrage", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Enrage", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Enraged.Sprite,
            cost = upgrade == Upgrade.A ? 3 : 4,
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
                    
                    new AStatus()
                    {
                        status = ModEntry.Instance.Fury.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    //A bit too good. might bring it to 4 cost base. -- YUP

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = ModEntry.Instance.Fury.Status,
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
                        status = ModEntry.Instance.Fury.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },

                };
                break;
        }
        return actions;
    }
}

using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardLootPowercore : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Powercore", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderstrashDeck.Deck,
                upgradesTo = [Upgrade.A, Upgrade.B],
                rarity = Rarity.common,

            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Powercore", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Crates.Sprite,
            cost = 0,
            exhaust = true,
            temporary = true,
            retain = upgrade == Upgrade.B ? true : false,

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
                    new AEnergy()
                    {
                        changeAmount = 1,
                    }

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AEnergy()
                    {
                        changeAmount = 2,
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AEnergy()
                    {
                        changeAmount = 1,
                    }
                };
                break;
        }
        return actions;
    }
}

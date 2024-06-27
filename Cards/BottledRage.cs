using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardBottledRage : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("BottledRage", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BottledRage", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        RemoteManager.SetRemoteUnending(this, state, true);
        CardData data = new CardData()
        {
            retain = true,
            cost = 1,
            exhaust = true,
            art = ModEntry.Instance.Angder_Bottled.Sprite
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
                    new AAddCard()
                    {
                        card = new CardDistantYelling(),
                        destination = CardDestination.Deck,
                        amount = 2,
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAddCard()
                    {
                        card = new CardDistantYelling(),
                        destination = CardDestination.Deck,
                        amount = 4
                    },

                };
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AAddCard()
                    {
                        card = new CardDistantYelling
                        {
                            upgrade = Upgrade.A
                        },
                        destination = CardDestination.Deck,
                        amount = 2
                    },
                };
                break;
        }
        return actions;
    }
}

using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardCodepurge : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Codepurge", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Codepurge", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Chute.Sprite,
            cost = 0,
            //exhaust = true
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
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 1
                    },

                    new ASpawn
                    {
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        }
                    }
        };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.shield,
                        statusAmount = 2
                    },
                    new ASpawn
                    {
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        }
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        targetPlayer = true,
                        status = Status.tempShield,
                        statusAmount = 4
                    },
                    new ASpawn
                    {
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        }
                    }
                };
                break;
        }
        return actions;
    }
}

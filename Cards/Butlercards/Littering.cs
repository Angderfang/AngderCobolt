using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardLittering : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Littering", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Littering", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Littering.Sprite,
            cost = upgrade == Upgrade.A ? 0 : 1,
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
                    new ASpawn
                    {
                        offset = -1,
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        }
                    },
                    new ASpawn
                    {
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        },
                        omitFromTooltips = true
                    },
                    new ASpawn
                    {
                        offset = 1,
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        },
                        omitFromTooltips = true
                    }
        };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn
                    {
                        offset = -1,
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        }
                    },
                    new ASpawn
                    {
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        },
                        omitFromTooltips = true
                    },
                    new ASpawn
                    {
                        offset = 1,
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        },
                        omitFromTooltips = true
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn
                    {
                        offset = -1,
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        }
                    },
                    new ASpawn
                    {
                        thing = new SpaceMine
                        {
                            yAnimation = 0.0
                        }
                    },
                    new ASpawn
                    {
                        offset = 1,
                        thing = new Trashbag
                        {
                            yAnimation = 0.0
                        },
                        omitFromTooltips = true
                    }
                };
                break;
        }
        return actions;
    }
}

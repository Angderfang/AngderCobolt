using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardCoolRocket : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CoolRocket", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderstrashDeck.Deck,
                upgradesTo = [Upgrade.A, Upgrade.B],
                rarity = Rarity.common,

            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "CoolRocket", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Crates.Sprite,
            cost = 0,
            exhaust = true,
            temporary = true
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

                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                        yAnimation = 0.0,
                        missileType = MissileType.normal
                        }
                    },

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                        yAnimation = 0.0,
                        missileType = MissileType.normal
                        }
                    },
                    new ASpawn()
                    {
                        offset = 1,
                        thing = new Missile()
                        {
                        yAnimation = 0.0,
                        
                        missileType = MissileType.normal
                        }
                        
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                        yAnimation = 0.0,
                        missileType = MissileType.heavy
                        }
                    },
                };
                break;
        }
        return actions;
    }
}

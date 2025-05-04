using Angder.EchoesOfTheFuture.Features;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardMagicMissile : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CardMagicMissile", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "CardMagicMissile", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            //art = ModEntry.Instance.Maid_Chute.Sprite,
            cost = upgrade == Upgrade.B ? 1 : 0,
            singleUse = true,
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
                    thing = new Missile
                    {
                        yAnimation = 0.0,
                        missileType = MissileType.heavy,
                        targetPlayer = false
                    }

                    },
        };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn
                    {
                    thing = new Missile
                    {
                        yAnimation = 0.0,
                        missileType = MissileType.heavy,
                        targetPlayer = false
                    }
                    },
                    new ASpawn
                    {
                        offset = -2,
                    thing = new Missile
                    {
                        yAnimation = 0.0,
                        missileType = MissileType.seeker,
                        targetPlayer = false,
                        
                    }
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {

                    new ASpawn
                    {
                    thing = new Missile
                    {
                        yAnimation = 0.0,
                        missileType = MissileType.corrode,
                        targetPlayer = false
                    }
                    }


                };
                break;
        }
        return actions;
    }
}

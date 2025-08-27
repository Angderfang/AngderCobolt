using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class FaceMe : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FaceMe", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.KobretteDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FaceMe", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            //art = ModEntry.Instance.Kobrette_CardBackground_Lightning.Sprite,
            cost = 1,
            flippable = true
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
                    new AMove()
                    {
                       dir = 2,
                    },
                    new AAttack
                    {
                       damage = GetDmg(s, 1),
                       //stunEnemy = true,
                    }
                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                       dir = 2,
                    },
                    new AAttack
                    {
                       damage = GetDmg(s, 2),
                       //stunEnemy = true,
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                       dir = 3,
                    },
                    new AAttack
                    {
                       damage = GetDmg(s, 0),
                       stunEnemy = true,
                    }
                };
                break;
        }
        return actions;
    }
}

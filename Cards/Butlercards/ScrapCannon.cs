using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardScrapCannon : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ScrapCannon", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ScrapCannon", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Ramcard.Sprite,
            cost = upgrade == Upgrade.B ? 2 : 1,
            //exhaust = true
            
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        int Exhaustcount = c.exhausted.Count;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            

            case Upgrade.None:
                actions = new()
                {
                    new AVariableHintExhaustThree
                    {
                        status = ModEntry.Instance.Exhaustover3.Status
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, 1),
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, Exhaustcount/3),
                       xHint = 1
                    },
        };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new AVariableHintExhaustThree
                    {
                        status = ModEntry.Instance.Exhaustover3.Status
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, 2),
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, Exhaustcount/3),
                       xHint = 1
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AVariableHintExhaustThree
                    {
                        status = ModEntry.Instance.Exhaustover3.Status
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, Exhaustcount/3),
                       xHint = 1
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, Exhaustcount/3),
                       xHint = 1
                    },
                };
                break;
        }
        return actions;
    }
}

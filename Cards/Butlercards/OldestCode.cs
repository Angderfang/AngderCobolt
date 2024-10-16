using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardOldestCode : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("OldestCode", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "OldestCode", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "OldestCode", "description", upgrade.ToString()], new { damage3 = GetDmg(state, 7), damage2 = GetDmg(state, 7), damage1 = GetDmg(state, 7) }),
            art = ModEntry.Instance.Maid_Origin.Sprite,
            cost = upgrade == Upgrade.B ? 0 : 2,
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
                    new AAddCard
                    {
                        card = new CardGarbage()
                            {
                            singleUseOverride = true
                            },
                        amount = 2,
                        destination = CardDestination.Exhaust
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, 7),
                    },
        };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new TrashAnnoyance()
                            {
                            },
                        amount = 2,
                        destination = CardDestination.Exhaust

                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, 7),
                       //piercing = true
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new NonTempTrash()
                            {
                            temporaryOverride = false,
                            //singleUseOverride = true
                            },
                        amount = 1,
                        destination = CardDestination.Exhaust
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, 7),
                    },
                };
                break;
        }
        return actions;
    }
}

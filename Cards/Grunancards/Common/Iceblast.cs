using Angder.EchoesOfTheFuture;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Iceblast : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Iceblast", new()
        {
            

            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.common,
               
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Iceblast", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Icebolt.Sprite,
            //description = ColorlessLoc.GetDesc(state, upgrade == Upgrade.B ? 3 : 2, (Deck)ModEntry.Instance.AngderDeck.Deck),
            cost = 1,
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
                    new AAttack()
                    {
                       damage = GetDmg(s, 2),
                        status = Status.lockdown,
                        statusAmount = 1
                    },
                };

        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                       damage = GetDmg(s, 4),
                        status = Status.lockdown,
                        statusAmount = 1
                    },
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {
                       damage = GetDmg(s, 1),
                        status = Status.lockdown,
                        statusAmount = 3
                    },
                };
        break;
        }
        return actions;
    }
}

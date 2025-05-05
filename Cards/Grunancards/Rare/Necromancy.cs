using Angder.EchoesOfTheFuture.Features;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Necromancy : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Necromancy", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Necromancy", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            description = ModEntry.Instance.Localizations.Localize(["card", "Necromancy", "description", upgrade.ToString()], new { damage = GetInjuredTotal(state) }),
            //art = ModEntry.Instance.Maid_Chute.Sprite,
            cost = upgrade == Upgrade.A ? 1 : 2,
            exhaust = true,
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
                        damage = GetDmg(s, GetInjuredTotal(s))
                    }

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, GetInjuredTotal(s))
                    }

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AShatter()
                    {
                        hurtAmount = GetDmg(s, GetInjuredTotal(s))
                    }

                };
                break;
        }
        return actions;
    }
    public int GetInjuredTotal(State s)
    {
        int num = 0;
        if (s.route is Combat combat)
        {
            num = combat.otherShip.hullMax - combat.otherShip.hull;
        }

        return num;
    }
}

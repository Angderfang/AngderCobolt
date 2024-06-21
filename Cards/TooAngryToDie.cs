using Angder.Angdermod;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardTooAngryToDie : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("TooAngryToDie", new()
        {
            

            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "TooAngryToDie", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Shield.Sprite,
            cost = upgrade == Upgrade.A ? 1 : 2,

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

                    new AVariableHint
                    {
                        status = ModEntry.Instance.Rampage.Status
                    },

                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = GetRampageAmt(s),
                        xHint = 1,
                        targetPlayer = true
                    },

                    /* Rampage into tempShields. Not good. But not bad either */
                    //The fuck you on about! this thing is amazing!

                };
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AVariableHint
                    {
                        status = ModEntry.Instance.Rampage.Status
                    },

                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = GetRampageAmt(s),
                        xHint = 1,
                        targetPlayer = true
                    },

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Rampage.Status
                    },

                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = GetRampageAmt(s),
                        targetPlayer = true,
                        xHint = 1,
                    },
                };
                break;
        }
        return actions;
    }
    private int GetRampageAmt(State s)
    {
        int result = 0;
        if (s.route is Combat)
        {
            result = s.ship.Get(ModEntry.Instance.Rampage.Status);
        }

        return result;
    }
}

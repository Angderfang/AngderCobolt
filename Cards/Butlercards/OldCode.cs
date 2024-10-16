using Angder.EchoesOfTheFuture.Features;
using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards;


internal sealed class CardOldCode : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("OldCode", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.ButlerDeck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "OldCode", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        //bool tablecheck = false;
        //if (state.ship.Get(Status.tableFlip) > 0)
            //tablecheck = true;
        CardData data = new CardData()
        {
            flippable = true,
            //description = ModEntry.Instance.Localizations.Localize(["card", "PlayExhaust", "description", upgrade.ToString()]),
            art = ModEntry.Instance.Maid_Awaken.Sprite,
            cost = 2,
            exhaust = true,
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        int right = 1;
        int Exhaustcount = c.exhausted.Count;
        if (flipped == true)
        {
            right = -1;
        }
        List<CardAction> actions = new();
        switch (upgrade)
        {
            
            
            case Upgrade.None:
                actions = new()
                {

                    new CleaveAction()
                    {
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 1), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 1,
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Warmode.Status,
                        statusAmount = 2,
                        targetPlayer = true,
                    },

                };
        /* Remember to always break it up! */
        break;
            case Upgrade.A:
                actions = new()
                {
                    new CleaveAction()
                    {
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 2), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 2,
                    },

                    new AStatus()
                    {
                        status = ModEntry.Instance.Warmode.Status,
                        statusAmount = 2,
                        targetPlayer = true,
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
                    new CleaveAction()
                    {
                        Length = 2,
                        Thiscard = this,
                        Direction = right,
                        Ignoresoverdrive = true,
                        DamageAlt = GetDmg(s, 2), // If your going to complain about me having two different damage things. Blame Max. Right click forced me to grab "Getdmg" BEFORE I actually reached the bit of code which uses it.
                        Damage = 2,
                        //xHint = Exhaustcount/5,
                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.Warmode.Status,
                        statusAmount = Exhaustcount/3,
                        targetPlayer = true,
                        xHint = 1,
                    },
                };
                break;
        }
        return actions;
    }
}

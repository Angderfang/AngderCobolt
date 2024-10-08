﻿using Angder.EchoesOfTheFuture;
using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardSeeingRed : Card, IAngderCard
{
    /*
    public bool IsRemotecontrolled(State state, Combat? combat)
                => false;
    */
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SeeingRed", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SeeingRed", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_Red.Sprite,
            cost = upgrade == Upgrade.B ? 2 : 1,
            exhaust = upgrade == Upgrade.A ? false : true,
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

                    new AAttack()
                    {
                       damage = GetDmg(s, GetRampageAmt(s)),
                       xHint = 1
                    },


                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AVariableHint
                    {
                        status = ModEntry.Instance.Rampage.Status
                    },

                    new AAttack()
                    {
                       damage = GetDmg(s, GetRampageAmt(s)),
                       xHint = 1
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
                    new AAttack()
                    {
                       damage = GetDmg(s, GetRampageAmt(s)),
                       xHint = 1
                    },
                    new AAttack()
                    {
                       damage = GetDmg(s, GetRampageAmt(s)),
                       xHint = 1
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

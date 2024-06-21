using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardAutoblastleft : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Autoblastleft", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {

                deck = ModEntry.Instance.AngderstrashDeck.Deck,

                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]


            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Autoshoot", "name"]).Localize,
        });
    }
    public override CardData GetData(State state)
    {
        int Damagedealing = Angderjustcleavethings.AngderCleaveDmg(state, 1, this, true, GetDmg(state,1), null);

        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_ManyBulletMuchwow.Sprite,
            unplayable = true,
            cost = 0,
            exhaust = true,
            temporary = true,
            description = ModEntry.Instance.Localizations.Localize(["card", "Autoshoot", "description", upgrade.ToString()], new { damage = Damagedealing })
        };
        return data;
    }
    public override void OnDraw(State s, Combat c)
    {
        int Randomshootcard = (int)s.rngActions.NextInt() % 2;

        if (upgrade != Upgrade.B)
        {
            switch (Randomshootcard)
            {
                case 0:
                    c.QueueImmediate(new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 1),
                        Length = 2,
                        Thiscard = this,
                        Direction = 1,
                        Ignoresoverdrive = true,
                        Damage = 1, //Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                    });
                    break;
                case 1:
                    c.QueueImmediate(new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 1),
                        Length = 2,
                        Direction = -1,
                        Thiscard = this,
                        Ignoresoverdrive = true,
                        Damage = 1, // Angderjustcleavethings.AngderCleaveDmg(s, 1, this, true),
                    });
                    break;
            }

        }
        if (upgrade == Upgrade.A)
        {
            c.Queue(new ADrawCard()
            {
                count = 1,
            });
        }

        if (upgrade == Upgrade.B)
        {

                    c.QueueImmediate(new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 1),
                        Length = 4,
                        Direction = 0,
                        Thiscard = this,
                        Ignoresoverdrive = true,
                        Damage = 1,
                    });
                    c.QueueImmediate(new CleaveAction()
                    {
                        DamageAlt = GetDmg(s, 1),
                        Length = 4,
                        Thiscard = this,
                        Direction = 0,
                        Ignoresoverdrive = true,
                        Damage = 1,
                    });
        }
        c.Queue(new ASelfExhaust()
        {
            CardID = uuid
        });
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new CleaveAction()
                {
                    DamageAlt = GetDmg(s, 1),
                    Length = 2,
                    Direction = 0,
                    Thiscard = this,
                    Ignoresoverdrive = true,
                    Damage = 1,
                }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new CleaveAction()
                {
                    DamageAlt = GetDmg(s, 1),
                    Length = 2,
                    Direction = 0,
                    Thiscard = this,
                    Ignoresoverdrive = true,
                    Damage = 1,
                }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new CleaveAction()
                {
                    DamageAlt = GetDmg(s, 1),
                    Length = 4,
                    Direction = 0,
                    Thiscard = this,
                    Ignoresoverdrive = true,
                    Damage = 1,
                }
                };
                break;
        }
        return actions;
    }
}

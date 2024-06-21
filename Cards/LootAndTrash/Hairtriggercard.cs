using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.Angdermod.Cards;

internal sealed class CardHairTrigger : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("HairTrigger", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderstrashDeck.Deck,
                upgradesTo = [Upgrade.A, Upgrade.B],
                rarity = Rarity.common,


            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "HairTrigger", "name"]).Localize,
        });
    }
    public override CardData GetData(State state)
    {
        int Damagedealing = GetDmg(state, 2);
        int Damagedealing2 = Damagedealing + 1;
        int Damagedealing3 = Damagedealing - 1;
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Angder_ManyBulletMuchwow.Sprite,
            unplayable = true,
            cost = 0,
            exhaust = true,
            temporary = true,
            description = ModEntry.Instance.Localizations.Localize(["card", "HairTrigger", "description", upgrade.ToString()], new { damage = Damagedealing, damage2 = Damagedealing2, damage3 = Damagedealing3 })
        };
        return data;
    }
    public override void OnDraw(State s, Combat c)
    {
        switch (upgrade)
        {
            case Upgrade.None:
                c.Queue(new AAttack()
                {
                    damage = GetDmg(s, 2)
                });

                c.Queue(new ADrawCard()
                {
                    count = 1,
                });

                c.Queue(new ASelfExhaust()
                {
                    CardID = uuid
                });
                break;
            case Upgrade.A:
                c.Queue(new AAttack()
                {
                    damage = GetDmg(s, 3)
                });

                c.Queue(new ADrawCard()
                {
                    count = 1,
                });

                c.Queue(new ASelfExhaust()
                {
                    CardID = uuid
                });
                break;
            case Upgrade.B:
                c.Queue(new AAttack()
                {
                    damage = GetDmg(s, 1)
                });

                c.Queue(new ADrawCard()
                {
                    count = 2,
                });

                c.Queue(new ASelfExhaust()
                {
                    CardID = uuid
                });
                break;
        }
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

                    damage = GetDmg(s, 2)
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {

                    damage = GetDmg(s, 3)
                    }
                };

                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {

                    damage = GetDmg(s, 1)
                    }
                };
                break;

        }
        return actions;
    }
}

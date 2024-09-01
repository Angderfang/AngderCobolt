using Nickel;
//using Shockah.Kokoro;
using System.Collections.Generic;
using System.Reflection;


namespace Angder.EchoesOfTheFuture.Cards.Angdercards;

internal sealed class CardExtractionflare : Card, IAngderCard, IHasCustomCardTraits
{
    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state) => new HashSet<ICardTraitEntry>() { ModEntry.Instance.RemoteControl };
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Explosive", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Extractionflare", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            //art = ModEntry.Instance.Angder_Enraged.Sprite,
            cost = 2,
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

                ModEntry.Instance.KokoroApi.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApi.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApi.ActionCosts.StatusResource(
                            status: ModEntry.Instance.Angdermissing.Status,
                            costUnsatisfiedIcon: ModEntry.Instance.Angdermissingun.Sprite,
                            costSatisfiedIcon: ModEntry.Instance.Angdermissingin.Sprite
                        ),
                        amount: 1
                    ),
                    new AAttack()
                    {
                        damage = GetDmg(s, 4)
                    }


                )

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                ModEntry.Instance.KokoroApi.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApi.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApi.ActionCosts.StatusResource(
                            status: ModEntry.Instance.Angdermissing.Status,
                            //target: IKokoroApi.IActionCostApi.StatusResourceTarget.EnemyWithOutgoingArrow,
                            costUnsatisfiedIcon: ModEntry.Instance.Angdermissingun.Sprite,
                            costSatisfiedIcon: ModEntry.Instance.Angdermissingin.Sprite
                        ),
                        amount: 1
                    ),
                    new AAttack()
                    {
                        damage = GetDmg(s, 6)
                    }


                )

                };
                break;
            case Upgrade.B:
                actions = new()
                {

                ModEntry.Instance.KokoroApi.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApi.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApi.ActionCosts.StatusResource(
                            status: ModEntry.Instance.Angdermissing.Status,
                            //target: IKokoroApi.IActionCostApi.StatusResourceTarget.EnemyWithOutgoingArrow,
                            costUnsatisfiedIcon: ModEntry.Instance.Angdermissingun.Sprite,
                            costSatisfiedIcon: ModEntry.Instance.Angdermissingin.Sprite
                        ),
                        amount: 1
                    ),
                    new AAttack()
                    {
                        damage = GetDmg(s, 3)
                    }
                ),
                ModEntry.Instance.KokoroApi.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApi.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApi.ActionCosts.StatusResource(
                            status: ModEntry.Instance.Angdermissing.Status,
                            //target: IKokoroApi.IActionCostApi.StatusResourceTarget.EnemyWithOutgoingArrow,
                            costUnsatisfiedIcon: ModEntry.Instance.Angdermissingun.Sprite,
                            costSatisfiedIcon: ModEntry.Instance.Angdermissingin.Sprite
                        ),
                        amount: 1
                    ),
                    new AAttack()
                    {
                        damage = GetDmg(s, 3)
                    }
                )

                };
                break;
        }
        return actions;
    }
}

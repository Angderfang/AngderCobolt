using Angder.EchoesOfTheFuture;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardCreateMap : Card, IAngderCard, IHasCustomCardTraits
{
    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state) => new HashSet<ICardTraitEntry>() { ModEntry.Instance.RemoteControl };
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("CreateMap", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "CreateMap", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            
            exhaust = true,
            cost = 0,
            art = ModEntry.Instance.Angder_Crates.Sprite,
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

                ModEntry.Instance.KokoroApiold.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApiold.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApiold.ActionCosts.StatusResource(
                            status: ModEntry.Instance.Angdermissing.Status,
                            //target: IKokoroApi.IActionCostApi.StatusResourceTarget.EnemyWithOutgoingArrow,
                            costUnsatisfiedIcon: ModEntry.Instance.Angdermissingun.Sprite,
                            costSatisfiedIcon: ModEntry.Instance.Angdermissingin.Sprite
                        ),
                        amount: 1
                    ),
                    new AStatus()
                    {
                        status = ModEntry.Instance.Theft.Status,
                        targetPlayer = true,
                        statusAmount = 1
                    }


                )

                };
                break;
            case Upgrade.A:
                actions = new()
                {

                ModEntry.Instance.KokoroApiold.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApiold.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApiold.ActionCosts.StatusResource(
                            status: ModEntry.Instance.Angdermissing.Status,
                            //target: IKokoroApi.IActionCostApi.StatusResourceTarget.EnemyWithOutgoingArrow,
                            costUnsatisfiedIcon: ModEntry.Instance.Angdermissingun.Sprite,
                            costSatisfiedIcon: ModEntry.Instance.Angdermissingin.Sprite
                        ),
                        amount: 1
                    ),
                    new AStatus()
                    {
                        status = ModEntry.Instance.Theft.Status,
                        targetPlayer = true,
                        statusAmount = 2
                    }


                )

                };
                break;
            case Upgrade.B:
                actions = new()
                {

                ModEntry.Instance.KokoroApiold.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApiold.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApiold.ActionCosts.StatusResource(
                            status: ModEntry.Instance.Angdermissing.Status,
                            //target: IKokoroApi.IActionCostApi.StatusResourceTarget.EnemyWithOutgoingArrow,
                            costUnsatisfiedIcon: ModEntry.Instance.Angdermissingun.Sprite,
                            costSatisfiedIcon: ModEntry.Instance.Angdermissingin.Sprite
                        ),
                        amount: 1
                    ),
                    new AStatus()
                    {
                        status = ModEntry.Instance.Theft.Status,
                        targetPlayer = true,
                        statusAmount = 1
                    }
                ),
                 ModEntry.Instance.KokoroApiold.ActionCosts.Make(
                    cost: ModEntry.Instance.KokoroApiold.ActionCosts.Cost(
                        resource: ModEntry.Instance.KokoroApiold.ActionCosts.StatusResource(
                            status: ModEntry.Instance.Angdermissing.Status,
                            //target: IKokoroApi.IActionCostApi.StatusResourceTarget.EnemyWithOutgoingArrow,
                            costUnsatisfiedIcon: ModEntry.Instance.Angdermissingun.Sprite,
                            costSatisfiedIcon: ModEntry.Instance.Angdermissingin.Sprite
                        ),
                        amount: 1
                    ),
                    new AStatus()
                    {
                        status = ModEntry.Instance.FuelSiphon.Status,
                        targetPlayer = true,
                        statusAmount = 1
                    }

                
                )
                };
                break;
        }
        return actions;
    }
}

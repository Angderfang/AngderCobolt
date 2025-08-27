using Angder.EchoesOfTheFuture.Artifacts;
using Angder.EchoesOfTheFuture.Cards;
using Angder.EchoesOfTheFuture.Features;
using FMOD;
using System;
using System.Linq;

namespace Angder.EchoesOfTheFuture;
internal sealed class VoidManager : IStatusLogicHook
{
    bool triggered = false;
    //Card newcard;
    public static ModEntry Instance => ModEntry.Instance;
    public VoidManager()
    {

        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);

        ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook(nameof(Artifact.OnPlayerRecieveCardMidCombat), (State state, Combat combat, Card card) =>
        {
            int count = state.ship.Get(ModEntry.Instance.Voidsight.Status);
            //Console.WriteLine(triggered);
        if (triggered == false) //triggered < state.ship.Get(ModEntry.Instance.Voidsight.Status))
            {
                //triggered = true;
                if (count > 0 && GrunanTraitManager.IsVoid(card, state) != true && card.GetMeta().deck != Deck.trash) //triggered < state.ship.Get(ModEntry.Instance.Voidsight.Status))
                {
                    GrunanTraitManager.SetVoid(card, state, true);

                    combat.Queue(new AAddCard
                    {
                        card = card.CopyWithNewId(),
                        amount = state.ship.Get(ModEntry.Instance.Voidsight.Status),
                        destination = CardDestination.Discard,
                    });
                    GrunanTraitManager.SetVoid(card, state, false);
                    /*
                    combat.Queue(new AStatus()
                    {
                        status = ModEntry.Instance.Voidsight.Status,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
                        targetPlayer = true
                    }); */
                };
                if (0 < state.ship.Get(ModEntry.Instance.Memory.Status) && GrunanTraitManager.IsVoid(card, state) != true)
                {
                    combat.Queue(new Refreshnotes()
                    {

                    });
                }
            }
        });
        

    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != Instance.Voidsight.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnEnd)
            return false;
        //triggered = false;
        if (state.ship.Get(Status.timeStop) == 0 && state.ship.Get(Instance.Voidsight.Status) > 0) 
        {
            combat.Queue(new AStatus()
            {
                status = ModEntry.Instance.Voidsight.Status,
                statusAmount = 0,
                targetPlayer = true,
                mode = AStatusMode.Set
            });
        }
        return false;
    }
    
}
using Angder.EchoesOfTheFuture.Artifacts;
using Angder.EchoesOfTheFuture.Cards;
using System;
using System.Linq;
using System.Threading;

namespace Angder.EchoesOfTheFuture;
internal sealed class CrewManager : IStatusLogicHook
{

    Status Crew = Instance.StatusConstructioncrew.Status;
    //The count lasttime the artifact was called.
    public static ModEntry Instance => ModEntry.Instance;
    public CrewManager()
    {
        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);
        ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook(nameof(Artifact.AfterPlayerStatusAction), (State state, Combat combat, Status status, AStatusMode mode, int statusAmount) =>
        {
            int Oldcount = ModEntry.Instance.Helper.ModData.GetModDataOrDefault<int>(state, key: "Oldcount", defaultValue: 0);
            int changeamount = Math.Min(statusAmount, 2 - Oldcount);
            if (status == Crew && changeamount > 0)
            {
                Oldcount = Oldcount + changeamount;
                ModEntry.Instance.Helper.ModData.SetModData<int>(state, key: "Oldcount", Oldcount);
                state.GetCurrentQueue().QueueImmediate(new AHullMax()
                {
                    targetPlayer = true,
                    amount = changeamount,
                });
            }
        });

    }

    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;
        if (status == Instance.StatusConstructioncrew.Status)
        {
            ModEntry.Instance.Helper.ModData.SetModData<int>(state, key: "Oldcount", state.ship.Get(Instance.StatusConstructioncrew.Status));
        }
        return false;
    }
}
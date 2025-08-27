using Angder.EchoesOfTheFuture.Artifacts;
using Angder.EchoesOfTheFuture.Cards;
using Angder.EchoesOfTheFuture.Features;
using FMOD;
using System;
using System.Linq;

namespace Angder.EchoesOfTheFuture;
internal sealed class FortressManager : IStatusLogicHook
{
    bool triggered = false;
    //Card newcard;
    public static ModEntry Instance => ModEntry.Instance;
    public FortressManager()
    {

        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);

    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;
        if (status == Instance.Fortress.Status && amount > 0)
        {
            combat.Queue(new AStatus()
            {
                status = Status.tempShield,
                statusAmount = 3,
                targetPlayer = true,
                mode = AStatusMode.Add
            });
        }
        return false;
    }
    
}
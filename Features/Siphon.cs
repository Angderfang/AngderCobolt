using Angder.Angdermod.Artifacts;
using Angder.Angdermod.Cards;
using System.Linq;

namespace Angder.Angdermod;
internal sealed class SiphonManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public SiphonManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.RegisterStatusLogicHook(this, 1);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != Instance.FuelSiphon.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;



        if (ship.Get(ModEntry.Instance.FuelSiphon.Status) > 0 && ship.Get(ModEntry.Instance.Angdermissing.Status) > 0)
        {
            combat.Queue(new AStatus()
            {
                status = Status.evade,
                statusAmount = 1,
                targetPlayer = true
            });
                combat.Queue(new AStatus()
                {
                    status = ModEntry.Instance.FuelSiphon.Status,
                    statusAmount = -1,
                    targetPlayer = true
                });

        }
        return false;
    }
}

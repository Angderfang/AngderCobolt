using Angder.Angdermod.Artifacts;
using Angder.Angdermod.Cards;
using System.Linq;

namespace Angder.Angdermod;
internal sealed class FuelDumpManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public FuelDumpManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.RegisterStatusLogicHook(this, 1);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != Instance.FuelDiscard.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnEnd)
            return false;


        /* Theft */


        if (ship.Get(ModEntry.Instance.FuelDiscard.Status) > 0 && ship.Get(ModEntry.Instance.Angdermissing.Status) > 0)
        {
            combat.Queue(new AStatus()
            {
                status = Status.engineStall,
                statusAmount = 1,
                targetPlayer = false
            });
                combat.Queue(new AStatus()
                {
                    status = ModEntry.Instance.FuelDiscard.Status,
                    statusAmount = -1,
                    targetPlayer = true
                });

        }
        return false;
    }
}

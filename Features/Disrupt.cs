using Angder.Angdermod.Artifacts;
using Angder.Angdermod.Cards;
using System.Linq;

namespace Angder.Angdermod;
internal sealed class DisruptManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public DisruptManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.RegisterStatusLogicHook(this, 1);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != Instance.Disrupt.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;


        /* Theft */


        if (ship.Get(ModEntry.Instance.Disrupt.Status) > 0 && ship.Get(ModEntry.Instance.Angdermissing.Status) > 0)
        {
            combat.Queue(new AStatus()
            {
                status = Status.tempShield,
                statusAmount = ship.Get(ModEntry.Instance.Disrupt.Status),
                targetPlayer = true
            });
            if (ship.Get(ModEntry.Instance.Disrupt.Status) > 5)
            {
                combat.Queue(new AStatus()
                {
                    status = ModEntry.Instance.Disrupt.Status,
                    statusAmount = -3,
                    targetPlayer = true
                });
            }
            else if (ship.Get(ModEntry.Instance.Disrupt.Status) > 5)
            {
                combat.Queue(new AStatus()
                {
                    status = ModEntry.Instance.Disrupt.Status,
                    statusAmount = -2,
                    targetPlayer = true
                });
            }
            else
            {
                combat.Queue(new AStatus()
                {
                    status = ModEntry.Instance.Disrupt.Status,
                    statusAmount = -1,
                    targetPlayer = true
                });
            }

        }
        return false;
    }
}

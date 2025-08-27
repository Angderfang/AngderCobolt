using Angder.EchoesOfTheFuture.Artifacts.AngderArtifacts;
using Angder.EchoesOfTheFuture.Cards;
using System.Linq;

namespace Angder.EchoesOfTheFuture;
internal sealed class DisruptManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public DisruptManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != Instance.Disrupt.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnEnd)
            return false;

        /* Theft */


        if (ship.Get(ModEntry.Instance.Disrupt.Status) > 0 && ship.Get(ModEntry.Instance.Angdermissing.Status) > 0) //&& ship.Get(ModEntry.Instance.Rampage.Status) > 0)
        {
            var AggressiveSiphon = state.EnumerateAllArtifacts().OfType<AggressiveSiphon>().FirstOrDefault();

            if (AggressiveSiphon != null)
            {
                combat.Queue(new AStatus()
                {
                    status = Status.shield,
                    statusAmount = -2,
                    targetPlayer = false
                });
            }

            combat.Queue(new AStatus()
            {
                status = Status.tempShield,
                statusAmount = ship.Get(ModEntry.Instance.Disrupt.Status),
                targetPlayer = true
            });

            /*
            if (ship.Get(ModEntry.Instance.Disrupt.Status) > 10)
            {
                combat.Queue(new AStatus()
                {
                    status = ModEntry.Instance.Disrupt.Status,
                    statusAmount = -3,
                    targetPlayer = true
                });
            }
            else */ if (ship.Get(ModEntry.Instance.Disrupt.Status) > 5)
            {
                combat.Queue(new AStatus()
                {
                    status = ModEntry.Instance.Disrupt.Status,
                    statusAmount = 5,
                    mode = AStatusMode.Set,
                    targetPlayer = true
                }) ;
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

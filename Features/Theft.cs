using Angder.Angdermod.Artifacts;
using Angder.Angdermod.Cards;
using System.Linq;

namespace Angder.Angdermod;
internal sealed class TheftManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public TheftManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.RegisterStatusLogicHook(this, 1);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != Instance.Theft.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;


        /* Theft */


        if (ship.Get(ModEntry.Instance.Theft.Status) > 0 && ship.Get(ModEntry.Instance.Angdermissing.Status) > 0)
        {
            int Lootcount = 1;
            var artifact = state.EnumerateAllArtifacts().OfType<ShipsManifest>().FirstOrDefault();

            if (artifact != null)
                Lootcount++;

            while (Lootcount > 0)
            {
                Lootcount--;
                int Randomlootcard = (int)state.rngActions.NextInt() % 3;
                switch (Randomlootcard)
                {
                    case 0:
                        combat.Queue(new AAddCard()
                        {
                            card = new CardLootPowercore(),
                            destination = CardDestination.Hand,
                            amount = 1,
                        });
                        break;
                    case 1:
                        combat.Queue(new AAddCard()
                        {
                            card = new CardCoolRocket(),
                            destination = CardDestination.Hand,
                            amount = 1,
                        });
                        break;
                    case 2:
                        combat.Queue(new AAddCard()
                        {
                            card = new CardStolenMunitions(),
                            destination = CardDestination.Hand,
                            amount = 1,
                        });
                        break;

                    //case 3:
                        //Bossdrops(state, combat)

                    //Kinda ambitious to add drops for each and every boss... Also doesn't scale into future updates.
                    //Have to think about this one.

                    /*case 3:
                        combat.Queue(new AAddCard()
                        {
                            card = new CardExposedport(),
                            destination = CardDestination.Hand,
                            amount = 1,
                        });
                        break; */
                    //Situational. Not Angdery enough, not worth keeping in the drop table.
                }
            }

            combat.Queue(new AStatus()
            {
                status = ModEntry.Instance.Theft.Status,
                statusAmount = -1,
                targetPlayer = true
            });

        }
        return false;
    }
}

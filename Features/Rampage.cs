using Angder.Angdermod.Artifacts;
using Angder.Angdermod.Cards;
using System.Linq;

namespace Angder.Angdermod;
internal sealed class RampageManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public RampageManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.RegisterStatusLogicHook(this, 1);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        /*if (status != Instance.Rampage.Status)
            return false; */
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;


        if (amount > 0)
        {
            if (ship.Get(ModEntry.Instance.Angdermissing.Status) > 0)
            {
                combat.Queue(new AHurt()
                {
                    targetPlayer = !ship.isPlayerShip,
                    hurtAmount = amount,
                });
                amount = amount - 2;
            };
        }

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
        /* Theft */
        /* should probably be seperate TBH. But eh. */
        return false;
    }
}


//Put fury in with Rampage, it's part of the same design.
internal sealed class FuryManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public FuryManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.RegisterStatusLogicHook(this, 1);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != Instance.Fury.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
        {
                combat.Queue(new AStatus()
                {
                    status = ModEntry.Instance.Rampage.Status,
                    statusAmount = amount,
                    targetPlayer = true

                });;
        }
        return false;
    }
}
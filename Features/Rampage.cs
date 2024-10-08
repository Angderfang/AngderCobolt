﻿using Angder.EchoesOfTheFuture.Artifacts;
using Angder.EchoesOfTheFuture.Cards;
using System;
using System.Linq;

namespace Angder.EchoesOfTheFuture;
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
        if (status != Instance.Rampage.Status)
            return false;
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
                amount = ((amount + 1) / 2);
                //amount = (int)double.Ceiling ((double)amount / 2);
            };
        }
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
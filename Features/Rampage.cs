using Angder.EchoesOfTheFuture.Artifacts;
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
        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);
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
        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);
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

/*
Combat combat = g.state.route as Combat;
        if (combat != null)
        {
            int x = combat.otherShip.x;
            foreach (KeyValuePair<int, StuffBase> item in combat.stuff)
            {
                if (item.Value is Football)
                {
                    x = item.Value.x;
                }
            }

            for (int i = 1; i < 8; i++)
{
    if (SashaCanStandHere(g.state, combat, x - i))
    {
        num = x - i;
        num2 = 1;
        break;
    }

    if (SashaCanStandHere(g.state, combat, x + i))
    {
        num = x + i;
        num2 = -1;
        break;
    }
}
        }

        if (!destinationXLerp.HasValue)
{
    destinationXLerp = num;
}
else if (combat != null && combat.PlayerCanAct(g.state))
{
    destinationXLerp = Mutil.MoveTowards(destinationXLerp.Value, num, g.dt * 6.0);
}

bool num3 = Math.Abs(destinationXLerp.Value - (double)num) > 0.1;
if (num3)
{
    num2 = Math.Sign((double)num - destinationXLerp.Value);
}

Vec vec = new Vec(offset.x) + new Vec(247.0 + (destinationXLerp ?? ((double)num)) * 16.0, 90.0);
bool flag = combat != null && combat.stuff.Values.Count((StuffBase s) => s is FootballGoal) < 2;
bool flag2 = false;
if (!flag && combat?.routeOverride is Dialogue && combat != null && combat.turn > 0)
{
    flag2 = true;
}

bool flag3 = !num3 && combat != null && combat.turn > 0;
bool flag4 = !flag3 || flag2 || flag;
Spr? id = Spr.bg_sasha_sasha_shadow;
double x2 = vec.x + (double)((num2 > 0) ? 1 : 0);
double y = vec.y + 1.0;
Vec? originRel = new Vec(0.5, 1.0);
Color? color = new Color(0.0, 0.0, 0.0, 0.3);
Draw.Sprite(id, x2, y, flipX: false, flipY: false, 0.0, null, originRel, null, null, color);
Spr? id2 = (flag2 ? Spr.bg_sasha_sasha_penalty : (flag3 ? Spr.bg_sasha_sasha_watching : Spr.bg_sasha_sasha_excited));
double x3 = vec.x;
double y2 = vec.y - Math.Floor(flag4 ? (Mutil.Parabola(Mutil.Mod(t * 4.0, 1.0)) * 2.0) : 0.0);
originRel = new Vec(0.5, 1.0);
Draw.Sprite(id2, x3, y2, num2 > 0, flipY: false, 0.0, null, originRel);
BGComponents.RegularGlowMono(g, offset, g.state.map.GetVanillaGlowColor());
    }

*/
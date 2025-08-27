using Angder.EchoesOfTheFuture.Artifacts;
using Angder.EchoesOfTheFuture.Cards;
using Angder.EchoesOfTheFuture.Features;
using FMOD;
using FSPRO;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Angder.EchoesOfTheFuture;
internal class AWeatherTheStorm : CardAction
{
    //private static ModEntry Instance => ModEntry.Instance;
    public static int StormNumber;
    public static bool Stormactive;
    public override void Begin(G g, State s, Combat c)
    {
        ModEntry.Instance.Helper.ModData.SetModData(s, key: "StormNumber", StormNumber = s.ship.hull);
        ModEntry.Instance.Helper.ModData.SetModData(s, key: "Stormactive", Stormactive = true);
        Audio.Play(Event.Status_PowerDown);
        for (int i = 0; i < StormNumber * 7; i++)
        {
            PFX.combatAdd.Add(new Particle
            {
                pos = s.ship.GetWorldPos(g.state, c) + new Vec(Mutil.NextRand() * s.ship.parts.Count * 16.0, (double)(s.ship.isPlayerShip ? 1 : (-1)) * (-10.0 + Mutil.NextRand() * 20.0)),
                vel = Mutil.RandVel() * 30.0 + new Vec(20),
                color = new Color(1, 0.5, 0.0).gain(0.8),
                size = 2.0 + Mutil.NextRand(),
                dragCoef = 4.0 + Mutil.NextRand(),
                lifetime = 1.2 + Mutil.NextRand() * 1.0
            });
        }
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        string key;
        string name;
        string description;
        key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::AWeatherTheStorm";
        name = ModEntry.Instance.Localizations.Localize(["action", "AWeatherTheStorm", "name"]);
        description = ModEntry.Instance.Localizations.Localize(["action", "AWeatherTheStorm", "description"]);
        List<Tooltip> tooltips = [
        new GlossaryTooltip(key)
        {

            Icon = ModEntry.Instance.ShieldbashIcon.Sprite,
            TitleColor = Colors.action,
            Title = name,
            Description = description
        }
        ];
        return tooltips;
    }
    public override Icon? GetIcon(State s)
    {
        return new Icon(ModEntry.Instance.ShieldbashIcon.Sprite, null, Colors.textMain);
    }
}

internal sealed class StormManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public StormManager()
    {
        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);

    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        
        bool StormActive = ModEntry.Instance.Helper.ModData.GetModDataOrDefault<bool>(state, key: "Stormactive", defaultValue: false);
        int StormNumber = ModEntry.Instance.Helper.ModData.GetModDataOrDefault<int>(state, key: "StormNumber", defaultValue: 0);
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;
        if (StormActive == true && combat.isPlayerTurn == true)
        {
            ModEntry.Instance.Helper.ModData.SetModData(state, key: "Stormactive", false);
            int trueheal = StormNumber - ship.hull;
            if (trueheal > 0)
            {
                combat.Queue(new AHeal()
                {
                    healAmount = trueheal,
                    targetPlayer = true,
                
                });
            }
        }
        return false;
    }
}
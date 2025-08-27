using JetBrains.Annotations;
using Nickel;
using Shockah.Kokoro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace Angder.EchoesOfTheFuture;

    internal sealed class StatusRenderManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
    {
       
        private static ModEntry Instance => ModEntry.Instance;

        internal StatusRenderManager()
        {
            Instance.KokoroApi.StatusRendering.RegisterHook(this, double.MinValue);
        }
        
        Status Crew = Instance.StatusConstructioncrew.Status;
    IEnumerable<(Status Status, double Priority)> GetExtraStatusesToShow(State state, Combat combat, Ship ship)
    {
        if (ship.Get(Crew) > 0)
        {
            yield return (Instance.StatusConstructioncrew.Status, Priority: 10);
        }

    }

    public bool? ShouldOverrideStatusRenderingAsBars(State state, Combat combat, Ship ship, Status status, int amount)
        => status == Crew ? true : null;
    
    public (IReadOnlyList<Color> Colors, int? BarSegmentWidth)? OverrideStatusRenderingAsBars(IKokoroApi.IV2.IStatusRenderingApi.IHook.IOverrideStatusRenderingAsBarsArgs args)
    {
        int crewmax = 2;
        if (args.Status != Crew)
            return null;

        var Crewcount = Math.Min(args.Amount, crewmax);

        var badColor = Colors.downside;
        var goodColor = Colors.cheevoGold;
        var neutralColor = Colors.white;
        //var barCount = crewmax;
        //var barCount = crewmax;
        //var colors = new Color[barCount];
        return (
            Colors: Enumerable.Range(0, Crewcount).Select(_ => Instance.KokoroApi.StatusRendering.DefaultActiveStatusBarColor)
            .Concat(Enumerable.Range(0, crewmax - Crewcount).Select(_ => Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor))
            .ToList(),
            BarTickWidth: null
           ); 
    }
    /*
    public bool? ShouldOverrideStatusRenderingAsBars(State state, Combat combat, Ship ship, Status status, int amount)
    => status == Crew ? true : null;

    public (IReadOnlyList<Color> Colors, int? BarTickWidth) OverrideStatusRendering(State state, Combat combat, Ship ship, Status status, int amount)
    {
        if (status != Instance.StatusConstructioncrew.Status) { return ([], null); }
        var Crewvar = Math.Min(ship.Get(Crew), amount);
        return (
            Colors: Enumerable.Range(0, crewmax).Select(_ => Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor)
            .Concat(Enumerable.Range(0, amount - crewmax).Select(_ => Instance.KokoroApi.StatusRendering.DefaultActiveStatusBarColor))
            .ToList(),
            BarTickWidth: null
           );
    }
    */


}


    


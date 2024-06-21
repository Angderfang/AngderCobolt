using Angder.Angdermod.Cards;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.Angdermod.Artifacts;

internal class BurntoutFireRegulator : Artifact, IAngderArtifact
{
    public int count = 0;
    public override int? GetDisplayNumber(State s)
    {
        return count;
    }
    
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("BurntoutFireRegulator", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.AngderDeck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/BurntoutFireRegulator.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "BurntoutFireRegulator", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "BurntoutFireRegulator", "description"]).Localize
        });
    }
    public override void OnTurnStart(State s, Combat c)
    {
        count++;
        if (count >= 2)
        {
            Pulse();
            c.QueueImmediate(new AEnergy
            {
                changeAmount = 1,
            });
            count = 0;
        }
    }

}

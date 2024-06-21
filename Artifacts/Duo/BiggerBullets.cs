using Angder.Angdermod.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.Angdermod.Artifacts.Duo;

internal sealed class Biggerbullet : Artifact, IAngderArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Biggerbullets", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.AngderDeck.Deck,
                pools = [ArtifactPool.Unreleased]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/Biggerbullet.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Biggerbullets", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Biggerbullets", "description"]).Localize
        });
    }
    public override void OnPlayerDestroyDrone(State state, Combat combat)
    {
        combat.QueueImmediate(new AHurt
        {
            hurtAmount = 2,
            artifactPulse = Key(),
            hurtShieldsFirst = true,
            targetPlayer = false
        });
    }
}

//Angder-Goat shared artifact?

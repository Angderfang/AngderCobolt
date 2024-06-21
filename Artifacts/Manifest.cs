using Angder.Angdermod.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.Angdermod.Artifacts;

internal sealed class ShipsManifest : Artifact, IAngderArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("ShipsManifest", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.AngderDeck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/ShipsManifest.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ShipsManifest", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ShipsManifest", "description"]).Localize
        });
    }
    public override void OnCombatStart(State s, Combat c)
    {
        c.Queue(new AStatus
        {
            status = ModEntry.Instance.Theft.Status,
            statusAmount = 3,
            targetPlayer = true
        });
    }
}

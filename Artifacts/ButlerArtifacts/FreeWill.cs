using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.ButlerArtifacts;

internal sealed class FreeWill : Artifact, IAngderArtifact
{

    public override List<Tooltip>? GetExtraTooltips()
    => StatusMeta.GetTooltips(ModEntry.Instance.Warmode.Status, 1);

    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("FreeWill", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.ButlerDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/FreeWill.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "FreeWill", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "FreeWill", "description"]).Localize
        });
    }
    public override void OnPlayerLoseHull(State state, Combat combat, int amount)
    {
        Pulse();
        combat.QueueImmediate(new AStatus()
        {
            status = ModEntry.Instance.Warmode.Status,
            statusAmount = amount,
            targetPlayer = true,
        });
    }
}

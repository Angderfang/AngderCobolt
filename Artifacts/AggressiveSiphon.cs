using Angder.Angdermod.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.Angdermod.Artifacts;

internal sealed class AggressiveSiphon : Artifact, IAngderArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("AggressiveSiphon", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.AngderDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/AggressiveSiphon.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "AggressiveSiphon", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "AggressiveSiphon", "description"]).Localize
        });
    }
    
    public override List<Tooltip>? GetExtraTooltips()
    => StatusMeta.GetTooltips(ModEntry.Instance.Disrupt.Status, 5);
    

public override void OnCombatStart(State s, Combat c)
    {
        Pulse();
        c.Queue(new AStatus
        {
            status = ModEntry.Instance.Disrupt.Status,
            statusAmount = 2,
            targetPlayer = true
        });
    }
}

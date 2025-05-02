using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.GrunanArtifacts;

internal sealed class Ritualofair : Artifact, IAngderArtifact
{
    public bool Readup; 
    public static void Register(IModHelper helper)
    {


        helper.Content.Artifacts.RegisterArtifact("Ritualofair", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GrunanDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/Ritualofair.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Ritualofair", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Ritualofair", "description"]).Localize
        });
    }
    public override List<Tooltip>? GetExtraTooltips()
    {
        return new List<Tooltip>
        {
            new TTCard
            {
                card = new Trails()
            },
        };
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        combat.QueueImmediate(new AAddCard
        {
            card = new Trails(),
            amount = 1,
            destination = CardDestination.Deck,
        });
    }
}

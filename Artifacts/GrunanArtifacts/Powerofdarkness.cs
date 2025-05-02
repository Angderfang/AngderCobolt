using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.GrunanArtifacts;

internal sealed class Darkness : Artifact, IAngderArtifact
{
    public bool Readup; 
    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("Darkness", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GrunanDeck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/Darkness.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Darkness", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Darkness", "description"]).Localize
        });
    }
    public override List<Tooltip>? GetExtraTooltips()
    {
        return new List<Tooltip>
        {
            new TTCard
            {
                card = new AbyssalVisions{ temporaryOverride = false }
            },
        };
    }

    public override void OnReceiveArtifact(State state)
    {
        state.ship.baseEnergy++;

        state.GetCurrentQueue().QueueImmediate(new AAddCard()
        {
            
            card = new AbyssalVisions{ temporaryOverride = false },
            //destination = CardDestination.Hand,
            amount = 3,
        });
    }

    public override void OnRemoveArtifact(State state)
    {
        state.ship.baseEnergy--;
    }
}

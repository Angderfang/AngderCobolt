using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.ButlerArtifacts;

internal sealed class LeakyPipe : Artifact, IAngderArtifact
{

    public int count;
    public int cappedamount;

    public override int? GetDisplayNumber(State s)
    {
        return count;
    }


    public override List<Tooltip>? GetExtraTooltips()
    {
        return new List<Tooltip>
        {
            new TTCard
            {
                card = new TrashFumes()
            },
        };
    }

    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("LeakyPipe", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.ButlerDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/LeakyPipe.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "LeakyPipe", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "LeakyPipe", "description"]).Localize
        });
    }
    public override void OnTurnStart(State s, Combat c)
    {
        count ++;
        if (count == 2)
        {
            count = 0;
            Pulse();
            c.QueueImmediate(new AAddCard()
            {
                card = new TrashFumes(),
                destination = CardDestination.Hand,
                amount = 1,
            });
        }
    }
}

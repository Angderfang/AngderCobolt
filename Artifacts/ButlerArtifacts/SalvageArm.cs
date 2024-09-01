using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.ButlerArtifacts;

internal sealed class ScrapArm : Artifact, IAngderArtifact
{

    public int count;

    public override int? GetDisplayNumber(State s)
    {
        return count;
    }

    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("SalvageArm", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.ButlerDeck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/SalvageArm.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "SalvageArm", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "SalvageArm", "description"]).Localize
        });
    }
    public override void OnCombatStart(State s, Combat c)
    {
        count = 0;
    }
    public override void OnTurnStart(State s, Combat c)
    {
        count++;
        if (count == 3)
        {
            count = 0;
            Pulse();
            c.QueueImmediate(new ACardSelect
            {
                browseAction = new ChooseCardToPutInHand(),
                browseSource = CardBrowse.Source.ExhaustPile,
            });
        }
    }
}

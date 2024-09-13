using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.ButlerArtifacts;

internal sealed class VacuumCleaner : Artifact, IAngderArtifact
{


    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("VacuumCleaner", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.ButlerDeck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/VacuumCleaner.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "VacuumCleaner", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "VacuumCleaner", "description"]).Localize
        });
    }
    public override void OnPlayerRecieveCardMidCombat(State state, Combat combat, Card card)
    {
        Pulse();
        combat.QueueImmediate(new ASelfExhaust()
        {
            FX = false,
            CardID = card.uuid
        });
    }
}

using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.GrunanArtifacts;

internal sealed class BookofReading : Artifact, IAngderArtifact
{
    public bool Readup; 
    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("BookofReading", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GrunanDeck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/BookofReading.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "BookofReading", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "BookofReading", "description"]).Localize
        });
    }
    public override void OnCombatStart(State s, Combat c)
    {
        Readup = false;
    }
    public override void OnTurnStart(State s, Combat c)
    {
        Readup = false;
    }
    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
        CardData data = card.GetData(state);

        if (data.description != null && Readup != true)
        {
            Pulse();
            combat.QueueImmediate(new AEnergy()
            {
                changeAmount = 1
            });
            combat.QueueImmediate(new ADrawCard()
            {
                count = 1
            });
            Readup = true;
        }
    }
}

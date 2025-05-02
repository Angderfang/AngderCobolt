using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.GrunanArtifacts;

internal sealed class BurningEffigy : Artifact, IAngderArtifact
{
    public int Countup;
    public override int? GetDisplayNumber(State s)
    {
        return Countup;
    }
    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("BurningEffigy", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GrunanDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/BurningEffigy.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "BurningEffigy", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "BurningEffigy", "description"]).Localize
        });
    }
    public override void OnCombatStart(State s, Combat c)
    {
        //Countup = 0;
    }

    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
        CardData data = card.GetData(state);

        if (data.singleUse == true || GrunanTraitManager.IsUntrashable(card, state))
        {
            Countup = Countup + 1;
        }
        if (Countup == 3)
        {
            Pulse();
            Countup = 0;
            combat.QueueImmediate(new AStatus()
            {
                status = Status.overdrive,
                statusAmount = 1,
                targetPlayer = true,
            });
        }
    }
}

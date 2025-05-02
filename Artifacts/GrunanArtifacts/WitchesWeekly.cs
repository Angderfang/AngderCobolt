using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.GrunanArtifacts;

internal sealed class WitchesWeekly : Artifact, IAngderArtifact
{
    //public bool Readup; 
    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("WitchesWeekly", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GrunanDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/WitchesWeekly.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "WitchesWeekly", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "WitchesWeekly", "description"]).Localize
        });
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        combat.QueueImmediate(new ACardOffering
        {
            amount = 3,
            limitDeck = ModEntry.Instance.GrunanDeck.Deck,
            makeAllCardsTemporary = false,
            overrideUpgradeChances = false,
            canSkip = true,
            inCombat = true,
            rarityOverride = Rarity.common
        });
    }
}

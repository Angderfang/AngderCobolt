using Angder.Angdermod.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.Angdermod.Artifacts;

internal sealed class PersonalJetpack : Artifact, IAngderArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("PersonalJetpack", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.AngderDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/PersonalJetpack.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PersonalJetpack", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PersonalJetpack", "description"]).Localize
        });
    }
    public override List<Tooltip>? GetExtraTooltips()
=> StatusMeta.GetTooltips(ModEntry.Instance.Fury.Status, 1);
    public override void OnTurnStart(State s, Combat c)
    {
        if (!c.isPlayerTurn)
            return;
        if (s.ship.Get(ModEntry.Instance.Rampage.Status) < 3 && s.ship.Get(ModEntry.Instance.FuelSiphon.Status) < 2 && s.ship.Get(ModEntry.Instance.FuelDiscard.Status) < 1 && s.ship.Get(ModEntry.Instance.Disrupt.Status) < 1 && s.ship.Get(ModEntry.Instance.Angdermissing.Status) > 0 && s.ship.Get(ModEntry.Instance.Fury.Status) == 0 && s.ship.Get(ModEntry.Instance.Theft.Status) <= 1)
        {
            c.Queue([
                        new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = -1,
                        artifactPulse = Key()
                    },
            ]);

        }
    }
}

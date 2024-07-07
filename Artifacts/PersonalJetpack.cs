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
                pools = [ArtifactPool.Boss]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/PersonalJetpack.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PersonalJetpack", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "PersonalJetpack", "description"]).Localize
        });
    }
    public override List<Tooltip>? GetExtraTooltips()
=> StatusMeta.GetTooltips(ModEntry.Instance.Angdermissing.Status, 1);
    public override void OnTurnStart(State s, Combat c)
    {
        if (!c.isPlayerTurn)
            return;
        if (s.ship.Get(ModEntry.Instance.Angdermissing.Status) < 1)
        {
            Pulse();
            c.Queue([
                    new AStatus()
                    {
                        status = ModEntry.Instance.Angdermissing.Status,
                        targetPlayer = true,
                        statusAmount = 1,
                        artifactPulse = Key()
                    },
            ]);

        }
    }
    public override void OnReceiveArtifact(State state)
    {
        state.ship.baseEnergy++;
    }

    public override void OnRemoveArtifact(State state)
    {
        state.ship.baseEnergy--;
    }
}

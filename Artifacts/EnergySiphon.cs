using Angder.Angdermod.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.Angdermod.Artifacts;

internal sealed class EnergySiphon : Artifact, IAngderArtifact
{
    bool stateset;
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("EnergySiphon", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.AngderDeck.Deck,
                pools = [ArtifactPool.Boss]
            },
                Sprite = ModEntry.Instance.EnergySiphon2.Sprite,
        Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EnergySiphon", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EnergySiphon", "description"]).Localize
        });
    }

    public override Spr GetSprite()
    {
        if (stateset == true)
        {

            return ModEntry.Instance.EnergySiphon3.Sprite;
        }
        else
        {
            return ModEntry.Instance.EnergySiphon2.Sprite;
        }
    }
    public override List<Tooltip>? GetExtraTooltips()
    => StatusMeta.GetTooltips(ModEntry.Instance.Angdermissing.Status, 3);

    public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    {
        if (status == ModEntry.Instance.Angdermissing.Status && mode == AStatusMode.Add && statusAmount > 0 && stateset == true)
        {
            stateset = false;
            Pulse();
            combat.QueueImmediate(new AEnergy
            {
                changeAmount = 1
            });
        }
    }
    public override void OnTurnStart(State s, Combat c)
    {
        stateset = true;
    }
    public override void OnCombatStart(State s, Combat c)
    {
        stateset = true;
        /*
        Pulse();
        c.Queue(new AStatus
        {
            status = ModEntry.Instance.Angdermissing.Status,
            statusAmount = 3,
            targetPlayer = true
        });
        */
    }
}

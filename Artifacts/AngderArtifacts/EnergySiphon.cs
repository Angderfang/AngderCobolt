using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.AngderArtifacts;

internal sealed class EnergySiphon : Artifact, IAngderArtifact
{
    bool stateset;
    int count;
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
            Sprite = ModEntry.Instance.EnergySiphon3.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EnergySiphon", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "EnergySiphon", "description"]).Localize
        });
    }

    public override Spr GetSprite()
    {
        if (stateset == false)
        {

            return ModEntry.Instance.EnergySiphon3.Sprite;
        }
        else
        {
            return ModEntry.Instance.EnergySiphon2.Sprite;
        }
    }
    public override List<Tooltip>? GetExtraTooltips()
    => StatusMeta.GetTooltips(ModEntry.Instance.Angdermissing.Status, 1); //.Concat(StatusMeta.GetTooltips(ModEntry.Instance.Rampage.Status, 1));
    
    public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    {
        if (status == ModEntry.Instance.Angdermissing.Status && mode == AStatusMode.Add && statusAmount > 0 && stateset == false)
        {
            count++;
            //stateset = true;
            Pulse();
            combat.QueueImmediate(new AEnergy
            {
                changeAmount = 1
            });
            combat.QueueImmediate(new AStatus
            {
                status = ModEntry.Instance.Rampage.Status,
                statusAmount = 1,
                targetPlayer = true
            });
            if (count == 2)
                stateset = true;
        }
    }
    public override void OnTurnStart(State s, Combat c)
    {
        count = 0;
        stateset = false;
    }
    public override void OnCombatStart(State s, Combat c)
    {
        stateset = false;
        /*
        c.Queue(new AStatus
        {
            status = ModEntry.Instance.Angdermissing.Status,
            statusAmount = 3,
            targetPlayer = true
        });
        */
    }
}

using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.AngderArtifacts;

internal sealed class ChainAxe : Artifact, IAngderArtifact
{

    public int count;
    public int cappedamount;

    public override int? GetDisplayNumber(State s)
    {
        return count;
    }


    public override List<Tooltip>? GetExtraTooltips()
    => StatusMeta.GetTooltips(ModEntry.Instance.Rampage.Status, 1);
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("ChainAxe", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.AngderDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = ModEntry.Instance.ChainAxe2.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ChainAxe", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ChainAxe", "description"]).Localize
        });
    }

    public override Spr GetSprite()
    {
        if (cappedamount == 1)
        {

            return ModEntry.Instance.ChainAxe2.Sprite;
        }
        else
        {
            return ModEntry.Instance.ChainAxe1.Sprite;
        }
    }

    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
        if (deck != ModEntry.Instance.AngderDeck.Deck && deck != ModEntry.Instance.AngderstrashDeck.Deck && cappedamount < 1)
        {
            count++;
        }
        else
        {
            count = 0;
        }

        if (count >= 3)
        {
            Pulse();
            combat.QueueImmediate(new AStatus
            {
                status = ModEntry.Instance.Rampage.Status,
                statusAmount = 1,
                targetPlayer = true
            });
            cappedamount++;
            count = 0;
        }
    }
    public override void OnTurnStart(State s, Combat c)
    {
        cappedamount = 0;
        count = 0;
    }
}

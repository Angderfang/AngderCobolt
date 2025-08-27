using Angder.EchoesOfTheFuture.Artifacts;
using Angder.EchoesOfTheFuture.Cards;
using Angder.EchoesOfTheFuture.Features;
using System.Linq;

namespace Angder.EchoesOfTheFuture;
internal sealed class PlaycardManager : IStatusLogicHook
{
    public static ModEntry Instance => ModEntry.Instance;
    public PlaycardManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);

        ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook(nameof(Artifact.OnPlayerPlayCard), (int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount) =>
        {
            //Console.WriteLine(__state);
            Status Disposalprocess = Instance.Disposalprocess.Status;
            Status warmode = Instance.Warmode.Status;
            Status Written = Instance.Memory.Status;
            Ship ship = state.ship;
            CardMeta Metacards = card.GetMeta();
            CardData Datacards = card.GetData(state);
            if (Metacards.deck == Deck.trash && Datacards.unplayable != true)
            {
                if (ship.Get(Disposalprocess) > 0)
                {
                    combat.QueueImmediate(new ADrawCard
                    {
                        count = ship.Get(Disposalprocess),
                    });
                }
                if (ship.Get(warmode) > 0)
                {
                    combat.QueueImmediate(new AAttack
                    {
                        damage = card.GetDmg(state, ship.Get(warmode)),
                    });
                }
            }

            //WELCOME TO JANK; POPULATION GRUNAN
            if (ship.Get(Written) > 0 && Datacards.unplayable != true)
            {

                if (GrunanTraitManager.IsUntrashable(card, state) == true)
                {
                    combat.QueueImmediate(new AStatus
                    {
                        status = ModEntry.Instance.Memory.Status,
                        statusAmount = -1,
                        targetPlayer = true
                    });
                }
                combat.Queue(new Refreshnotes { });


            }
        });
    }
}

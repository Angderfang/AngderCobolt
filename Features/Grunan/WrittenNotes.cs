using Angder.EchoesOfTheFuture.Artifacts;
using Angder.EchoesOfTheFuture.Cards;
using Angder.EchoesOfTheFuture.Features;
using System;
using System.Linq;

namespace Angder.EchoesOfTheFuture;
internal sealed class WrittenManager : IStatusLogicHook
{
    
    public static ModEntry Instance => ModEntry.Instance;
    public WrittenManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApiold.RegisterStatusLogicHook(this, 1);

        
        ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook(nameof(Artifact.OnCombatEnd), (State state) =>
        {
            //Console.WriteLine("functioning");
            foreach (Card item in state.deck)
            {
                if (item.GetData(state).singleUse == true)
                {
                    Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.SingleUseCardTrait, null, false);
                    Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
                    //GrunanTraitManager.SetUntrashable(item, state, false);
                }
            }
        }
        
        );
        ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook(nameof(Artifact.OnPlayerRecieveCardMidCombat), (State state, Combat combat, Card card) =>
        {
        }
        );
    }



}
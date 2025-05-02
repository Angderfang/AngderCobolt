using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using Nickel.Models.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Angder.EchoesOfTheFuture.Artifacts.ButlerArtifacts;

internal sealed class VacuumCleaner : Artifact, IAngderArtifact
{
        private static ModEntry Instance => ModEntry.Instance;

    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("VacuumCleaner", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.ButlerDeck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/VacuumCleaner.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "VacuumCleaner", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "VacuumCleaner", "description"]).Localize
        });
    }


    public override void OnTurnStart(State state, Combat combat)
    {
        foreach (Card item in state.deck)
        {
            Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
        }
        foreach (Card item in combat.discard)
        {
            Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
        }
        foreach (Card item in combat.hand)
        {
            Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
        }
    }
    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
        foreach (Card item in state.deck)
        {
            Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
        }
        foreach (Card item in combat.discard)
        {
            Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
        }
        foreach (Card item in combat.hand)
        {
            Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
        }
        foreach (Card item in combat.exhausted)
        {
            Instance.Helper.Content.Cards.SetCardTraitOverride(state, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
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

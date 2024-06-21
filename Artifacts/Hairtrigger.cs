﻿using Angder.Angdermod.Cards;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.Angdermod.Artifacts;

    internal class HairTrigger : Artifact, IAngderArtifact
    {
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("HairTrigger", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModEntry.Instance.AngderDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/HairTrigger.png")).Sprite,
                Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "HairTrigger", "name"]).Localize,
                Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "HairTrigger", "description"]).Localize
            });
        }

    public override void OnCombatStart(State s, Combat c)
    {
        int decksize = s.deck.Count;
        int Computerproblems = 1;
        switch (decksize)

        {
            case < 10:
                Computerproblems = 1;
                break;
            case < 20:
                Computerproblems = 2;
                break;
            case < 25:
                Computerproblems = 3;
                break;
            case > 24:
                Computerproblems = 4;
                break;
        }

        c.Queue(new AAddCard
        {
            amount = Computerproblems,
            card = new CardHairTrigger()
            {
            },
            destination = CardDestination.Discard
        });
    }
        public override List<Tooltip>? GetExtraTooltips()
        {
            return new List<Tooltip>
        {
            new TTCard
            {
                card = new CardHairTrigger()
            },
        };
        }
    }

    

using Angder.EchoesOfTheFuture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Dialog;
internal static class RiggsAngder
{
    private static ModEntry Instance => ModEntry.Instance;
    internal static void Inject()
    {
        string angder = ModEntry.Instance.AngderDeck.Deck.Key();
        string kobrette = ModEntry.Instance.KobretteDeck.Deck.Key();
        string Grunan = ModEntry.Instance.GrunanDeck.Deck.Key();
        string D26 = ModEntry.Instance.ButlerDeck.Deck.Key();


        DB.story.all["angder_Intro_Riggs"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder, Deck.riggs.Key() },
            once = true,
            bg = "BGRunStart",
            requiredScenes = ["angder_Intro_1"],
            hasArtifacts = ["CargoHold"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "And I am awake again...",
                    loopTag = "grumpy"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "This cockpit... it's familiar.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I mean, the tech is all different, but the seats? the furniture? This stain?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "Maybe your ship was of the same design?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Down to the same stains?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "...Maybe it's just very stainable?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Yea, that makes sense.",
                    loopTag = "neutral"
                },
            }
        };

        DB.story.all["angder_Advance_Riggs"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { angder, Deck.riggs.Key() },
            once = true,
            bg = "BGVanilla",
            requiredScenes = ["angder_Intro_Riggs"],
            hasArtifacts = ["CargoHold"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "I have... another theory about this ship",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I think it is my ship",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "...No?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Think about it, we are like, looping in some kind of micro-dimension? So like, maybe I am from a different other dimension?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "And in that Dimension, this was my ship, and you had a different ship.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "But why would you have stained it in the same place?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I don't know.",
                    loopTag = "serious"
                },
            }

        };
        DB.story.all["angder_Final_Set_Riggs"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "after_pirateBoss" },
            allPresent = new() { angder, Deck.riggs.Key() },
            once = true,
            bg = "BGRedGiant",
            requiredScenes = ["angder_Intro_eunice"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Riggs, I didn't know fighting you would be so fun!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Is everyone I work with going to have an evil duplicate at some point? Like I know about Drake, but is there an evil CAT out there?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Oh oh, what about an evil me!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I hope there is! I can't wait to fight myself!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "I'm afraid it's just me.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "And possibly Isaac",
                    loopTag = "neutral"
                },
            }
        };
    }
}
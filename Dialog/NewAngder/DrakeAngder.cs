using Angder.EchoesOfTheFuture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Dialog;
internal static class DrakeAngder
{
    private static ModEntry Instance => ModEntry.Instance;
    internal static void Inject()
    {
        string angder = ModEntry.Instance.AngderDeck.Deck.Key();
        string kobrette = ModEntry.Instance.KobretteDeck.Deck.Key();
        string Grunan = ModEntry.Instance.GrunanDeck.Deck.Key();
        string D26 = ModEntry.Instance.ButlerDeck.Deck.Key();


        DB.story.all["angder_Intro_eunice"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            requiredScenes = ["angder_Intro_1"],
            allPresent = new() { angder, Deck.eunice.Key() },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Drake? Aren't you supposed to be against us.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "Sometimes.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "Other times I end up here.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...So when you said that this ships entire crew were clowns...",
                    loopTag = "nervous"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Did that include you?",
                    loopTag = "smug"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "...",
                    loopTag = "reallymad"
                },
                /*
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "honk",
                    loopTag = "sadEyesClosed"
                },
                */
            }
        };

        DB.story.all["angder_Advance_eunice"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { angder, Deck.eunice.Key() },
            once = true,
            bg = "BGVanilla",
            requiredScenes = ["angder_Intro_eunice"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "We make quite a team!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "I have had worse",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "It's funny, you... aren't famous or anything are you?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "Why are you asking?",
                    loopTag = "blush"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Just... I think I have seen you before somewhere. Before the loops I mean.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "I am a pretty slick pirate.",
                    loopTag = "sly"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "Prehaps you have seen the wanted posters?",
                    loopTag = "sly"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...That could be it",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I hope that's it.",
                    loopTag = "nervous"
                },
            }
        };
    }
}

using System.Linq;

namespace Angder.EchoesOfTheFuture.Dialog.Grunan;

internal static class GrunanStory
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string angder = ModEntry.Instance.AngderDeck.Deck.Key();
        string grunan = ModEntry.Instance.GrunanDeck.Deck.Key();
        string D26 = ModEntry.Instance.ButlerDeck.Deck.Key();
        //Intros, Not plots

        DB.story.all["Grunan_Intro_1"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { grunan },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = "comp",
                    Text = "Rise and shine...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = "comp",
                    Text = "...",
                    loopTag = "worried"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = "comp",
                    Text = "Maam, are you OK?",
                    loopTag = "worried"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...A suprisingly measured response from seeing my horrific burns.",
                    loopTag = "flipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "I am fine the burns are old, very old actually.",
                    loopTag = "Flippedrelax"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...",
                    loopTag = "flipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "I'm going to go do some research. Poke me if and when you need me to break the laws of physics, and not a moment before.",
                    loopTag = "flipped"
                },

            }
        };
        DB.story.all["Grunan_Intro_2"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { grunan },
            requiredScenes = ["Grunan_Intro_1"],
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "...So CAT. we have infinite time right? The loops just keep going.",
                    loopTag = "neutral"
                },

                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "That appears to be the case.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = grunan,
                    Text = "So when we leave, will we be returning to a point in the far future?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Time is looping. you will end up in the same place that you left.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = grunan,
                    Text = "...Right.",
                    loopTag = "panic"
                },
            }
        };
    }

}



using System.Linq;

namespace Angder.EchoesOfTheFuture.Dialog.Grunan;

internal static class GrunanMaxStory
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string angder = ModEntry.Instance.AngderDeck.Deck.Key();
        string grunan = ModEntry.Instance.GrunanDeck.Deck.Key();
        string D26 = ModEntry.Instance.ButlerDeck.Deck.Key();
        //plots

        DB.story.all["Grunan_Max_Intro"] = new()
        {
            requiredScenes = ["Grunan_Intro_1"],
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { grunan, Deck.hacker.Key() },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "So you are a wizard.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "I prefer the term necromancer.",
                    loopTag = "flipped"
                },

                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "Or witch.",
                    loopTag = "Flippedrelax"
                },
                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "Heh. I considered becoming a witch myself once, maybe you should teach me.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "Fool I know your type. You are a computer wiz, but that won't help you understand the true secrets of the universe.",
                    loopTag = "flippedannoyed"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "Magic draws connections beyond your comprehension, seemingly unrelated things feed into eachother endlessly.",
                    loopTag = "RealAngerflipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "For example, your science looks at the ocean, and the sky, and does not recognize the fact they are both the same",
                    loopTag = "RealAngerflipped"
                },

                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "Oh so like, in magical terms, they are both in the same object class?",
                    loopTag = "intense"
                },
                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "Just with a variable switched from 'water' to 'air'.",
                    loopTag = "gloves"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...",
                    loopTag = "RealAngerflipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "what?",
                    loopTag = "wut"
                },

            }

        };

        DB.story.all["Grunan_Max_advance"] = new()
        {
            requiredScenes = ["Grunan_Max_Intro"],
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { grunan, Deck.hacker.Key() },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "Do you mind if I borrow your notes?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "My notes are my own. I need them.",
                    loopTag = "flippedannoyed"
                },

                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "It's fine, I can just clone your documents.",
                    loopTag = "neutral"
                },

                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "I'm hoping to find a way to link magic and code So I can literally hack reality.",
                    loopTag = "smile"
                },

                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "Or at least this bubble we currently exist in.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "You can't just... hack reality.",
                    loopTag = "flipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "The laws are complex, even a minor mistake in spelling or pronounciation will stop a spell from working.",
                    loopTag = "flippedannoyed"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "Even spells that work as written can have unpredictable effects.",
                    loopTag = "wut"
                },
                new CustomSay()
                {
                    who = Deck.hacker.Key(),
                    Text = "So it's just like coding then.",
                    loopTag = "smile"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...No.",
                    loopTag = "Flippedpain"
                },

            }

        };
    }

}


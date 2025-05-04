
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
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = "comp",
                    Text = "Are you OK?",
                    loopTag = "worried"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...I just woke up in a ship I have never seen before, don't remember my own name, and I am being bothered by some cute little cat emoticon.",
                    loopTag = "flippedannoyed"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "Also, I have a note here that reads \"timeloop\" in my own handwriting.",
                    loopTag = "flippedannoyed"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = "comp",
                    Text = "I was more concerned about the fact half your face is missing.",
                    loopTag = "worried"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...",
                    loopTag = "flippedannoyed"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...Of course...I remember now.",
                    loopTag = "flipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...I'm a space wizard. Of course I have contingencies for if I get trapped in a timeloop.",
                    loopTag = "Flippedrelax"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "I must have written this note so it persists between loops..",
                    loopTag = "flippedsmile"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = "comp",
                    Text = "We are about to be attacked. If you have magical powers, we could use your help.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "I have more important things to worry about.",
                    loopTag = "flipped"
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
                    flipped = true,
                    who = grunan,
                    Text = "Fine, I will see what I can do.",
                    loopTag = "flippedannoyed"
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
                    Text = "So when we leave, will time have passed outside this little bubble?",
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
                    Text = "...Right. That is definitely not concerning at all.",
                    loopTag = "Panic"
                },
            }
        };
    }

}


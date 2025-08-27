
using System.Linq;

namespace Angder.EchoesOfTheFuture.Dialog.Grunan;

internal static class GrunanAngderStory
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string angder = ModEntry.Instance.AngderDeck.Deck.Key();
        string grunan = ModEntry.Instance.GrunanDeck.Deck.Key();
        string D26 = ModEntry.Instance.ButlerDeck.Deck.Key();
        //Intros, Not plots

        DB.story.all["Grunan_Angder_Intro"] = new()
        {
            requiredScenes = ["Grunan_Intro_1"],
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { grunan, angder },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = grunan,
                    Text = "So that confirms it then...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Confirms what?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = grunan,
                    Text = "Our ship was sucked into this. All four of us are here.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = grunan,
                    Text = "More alarmingly, I don't actually know how to get us out.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "No grunan. You are smarter than that.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "You know exactly how to fix this mess.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "You just haven't worked it out yet.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "...Well, I guess I should get to work then.",
                    loopTag = "slightsmile"
                },

            }

        };
        DB.story.all["Grunan_Advance_Angder"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { angder, grunan },
            once = true,
            bg = "BGVanilla",
            requiredScenes = ["Grunan_Angder_Intro", "Grunan_Intro_2"],
            lines = new()
            {
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "Angder... you are hovering.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "...I'm not hovering.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "I mean you are standing there watching me with a concerned expression.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "You have a question. Ask it.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "...",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Do you think we can trust the rest of the crew?",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "...I think so. They are honest about why they are here, and I don't think they mean us harm.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "The real question is, can they trust us?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "When we get out, we will be returned to the exact point we left, at least according to CAT.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "I don't know if you remember how we got here, but I remember enough that I am not sure we actually want that.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "I don't want to betray these people.",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "...",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "But if we are betraying them, can I go detonate the warp core?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = grunan,
                    Text = "Just keep playing along for now. Hopefully I can find a solution that won't involve keeping the rest of them trapped here.",
                    loopTag = "slightsmile"
                },
            }
        };


        DB.story.all["Grunan_Angder_Theplan"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_three" },
            allPresent = new() { angder, grunan },
            once = true,
            bg = "BGRunStart",
            requiredScenes = ["Grunan_Advance_Angder"],
            lines = new()
            {
                new CustomSay()
                {
                    //flipped = true,
                    who = angder,
                    Text = "Won't be long until we reach the Cobalt.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = angder,
                    Text = "If we are going to turn traitor we should start...",
                    loopTag = "serious"
                },

                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "Do nothing of the sort.",
                    loopTag = "flipped"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = angder,
                    Text = "...",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = angder,
                    Text = "Wow, you are objecting to the plan?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = angder,
                    Text = "Not like you to care about ethics more than results.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "...Actually that's why I am objecting. The plan won't work. In fact it is utterly irrelevent.",
                    loopTag = "flippedannoyed"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "These people are already free. Nothing we do will slow down the completion of the loops, and if we were somehow successful, the universe would probably end.",
                    loopTag = "flippedannoyed"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "This timeloop extends in more than the expected number of ways. It also loops in a non-linear fashion. Event's happen in the wrong order. It holds literally infinite possibilities, yet clearly isn't eternal.",
                    loopTag = "flippedannoyed"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = angder,
                    Text = "...",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    //flipped = true,
                    who = angder,
                    Text = "...Let just pretend I understand that. What do we do instead?",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "Just... keep following the rules of the loop for now.",
                    loopTag = "flipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "I believe killing the Cobalt will help us find a better angle we can use.",
                    loopTag = "flipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "Destroying the Cobalt should help restore our memories and minds, and I know I am more than smart enough for this problem.",
                    loopTag = "flipped"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = grunan,
                    Text = "My current failure is only because I am, quite literally, missing pieces.",
                    loopTag = "flippedannoyed"
                },
            }

        };
    }

}


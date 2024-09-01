using System.Linq;

namespace Angder.EchoesOfTheFuture;

internal static class EventDialogueMaid
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string angder = ModEntry.Instance.AngderDeck.Deck.Key();
        string maid = ModEntry.Instance.ButlerDeck.Deck.Key();

        DB.story.GetNode("AbandonedShipyard_Repaired")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            
            who = maid,
            Text = "Damage Threshold Increased.",
            loopTag = "talk"
            
        });

        //Intros, Not plots

        DB.story.all["Droid_Intro_1"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { maid },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = "comp",
                    Text = "Cryosleep... interrupted?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Who put a robot in this capsule?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Memory banks... Incomplete. Attempting reboot.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Reboot failed.",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Hold on. I can upload the current situation directly.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "...Timeloop... Incoming attack. Information fully understood.",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Further issue identified. This ship is a mess.",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Beginning cleaning protocols. Must remove trash from ship.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Hostile Spacejunk contains foreign contaminents. Directive: Clense.",
                    loopTag = "anger"
                },
            }
        };
        DB.story.all["Droid_Intro_2"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { maid },
            nonePresent = new() { angder },
            once = true,
            requiredScenes = ["Droid_Intro_1"],
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = maid,
                    Text = "Previous state situation detected. New solution needed.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Searching...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Are you... trying to hijack the ship?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "...yes",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "It only responds to biological lifeforms. You need to wake the other two.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Unfortunate.",
                    loopTag = "anger"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "...",
                    loopTag = "squint"
                },
                /*
                new CustomSay()
                {
                    who = maid,
                    Text = "...Their presence will force us to keep the lifesupport on, draining resources.",
                    loopTag = "neutral"
                },
                */
            }
        };

        DB.story.all["Droid_Intro_Angder"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { maid, angder },
            once = true,
            requiredScenes = ["Droid_Intro_1"],
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = maid,
                    Text = "...Master?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "...why are you calling me that.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Databanks suggest you are my owner and/or master, boss, headmaid...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Please do not use any of those terms.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Call me Angder instead.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "What are my orders Angder?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "You see that enemy out the window?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Do you wish them to be cleaned?",
                    loopTag = "anger"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Well that depends...",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Do you want to clean them?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Orders understood. Cleaning in progress.",
                    loopTag = "anger"
                },
            }
        };
    }
}

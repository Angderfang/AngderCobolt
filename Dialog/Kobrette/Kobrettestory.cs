
using System.Linq;

namespace Angder.EchoesOfTheFuture.Dialog.Grunan;

internal static class KobretteStory
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string angder = ModEntry.Instance.AngderDeck.Deck.Key();
        string grunan = ModEntry.Instance.GrunanDeck.Deck.Key();
        string D26 = ModEntry.Instance.ButlerDeck.Deck.Key();
        string kobrette = ModEntry.Instance.KobretteDeck.Deck.Key();
        //Intros, Not plots

        DB.story.all["Kobrette_Intro_1"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { kobrette },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {

                new CustomSay()
                {
                    flipped = false,
                    who = kobrette,
                    Text = "I don't remember getting on this ship.",
                    loopTag = "confused"
                },
                new CustomSay()
                {
                    flipped = false,
                    who = kobrette,
                    Text = "Seems a bit old-fashioned, but is sturdy enough.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "No time for sightseeing, whoever you are, we need you on the bridge.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = false,
                    who = kobrette,
                    Text = "Are we in danger, Cat-computer?",
                    loopTag = "confused"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Well, we are about to be attacked, and will likely end up fighting half the sector.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = false,
                    who = kobrette,
                    Text = "Ah, that explains why I am here, you need someone to protect and shield the ship.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "If you know your way around the power systems, some help with the shielding would be appreciated.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = false,
                    who = kobrette,
                    Text = "...",
                    loopTag = "confused"
                },
                new CustomSay()
                {
                    flipped = false,
                    who = kobrette,
                    Text = "I think we have different definitions of the word \"shield\".",
                    loopTag = "shield"
                },

            }
        };
        DB.story.all["Kobrette_Intro_2"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { kobrette },
            requiredScenes = ["Kobrette_Intro_1"],
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = kobrette,
                    Text = "I can't help but feel bad for all the people we are hurting as we head from zone to zone.",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "If it makes you feel better, they reset just like we do.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = kobrette,
                    Text = "So we get to kill them violently repeatedly.",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "...because they keep trying to kill us.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = kobrette,
                    Text = "...",
                    loopTag = "confused"
                },
                new CustomSay()
                {
                    who = kobrette,
                    Text = "When you put it like that, feeling bad is a bit silly isn't it.",
                    loopTag = "squint"
                },
            }
        };
        DB.story.all["Kobrette_Intro_3"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { kobrette },
            requiredScenes = ["Kobrette_Intro_1"],
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = kobrette,
                    Text = "There is an old question people asked back when I was training.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = kobrette,
                    Text = "\"What would you do if your actions had no consequences.\"",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = kobrette,
                    Text = "...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = kobrette,
                    Text = "Turns out not much changes. After all, we are still the same people.",
                    loopTag = "neutral"
                },
            }
        };
    }

}


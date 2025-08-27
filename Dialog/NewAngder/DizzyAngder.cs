using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Dialog;
internal static class DizzyAngder
    {
        private static ModEntry Instance => ModEntry.Instance;

        internal static void Inject()
        {
            string angder = ModEntry.Instance.AngderDeck.Deck.Key();
            string kobrette = ModEntry.Instance.KobretteDeck.Deck.Key();
            string Grunan = ModEntry.Instance.GrunanDeck.Deck.Key();
            string D26 = ModEntry.Instance.ButlerDeck.Deck.Key();

        //DIZZY ANGDER Dialog

        DB.story.all["angder_Advance_Dizzy"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { angder, Deck.dizzy.Key() },
            once = true,
            bg = "BGVanilla",
            //requiredScenes = ["angder_Intro_Dizzy"],
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "Angder, where did you get that suit, it's lighter than any I have seen before.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Oh this, it's a DRA-5 or something.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "How do you breath while wearing it in space? I never see you wearing a helmet",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "A helmet? Why would I need one, the Personal forcefield keeps the oxygen inside.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "right. And where is the oxygen stored?",
                    loopTag = "frown"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "In that little disc on the back.",
                    loopTag = "neutral" // NEED IMAGE OF ANGDER showing thing
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "You know, quantum compression, Transdimensional storage.",
                    loopTag = "serious" // NEED IMAGE OF ANGDER showing thing
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "It's high tech but you should be familiar with it, right?",
                    loopTag = "serious" // Serious
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "Nothing you just said is possible with modern technology.",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "Mind if I take a closer look at some point?",
                    loopTag = "neutral"
                },
            }
        };


        DB.story.all["angder_Final_Set_Dizzy"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_three" },
            allPresent = new() { angder, Deck.dizzy.Key() },
            once = true,
            bg = "BGRunStart",
            requiredScenes = ["angder_Advance_Dizzy"],
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "CAT, where is Angder?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "In the back, grabbing some food.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "He left his suit here.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "Let me take a closer look at this...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "Why have I never heard of this sort of suit before?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "D.R.A, it's an acronym.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...Huh, the manual is still in the pocket",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "According to this...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...",
                    loopTag = "serious" //Shocked?
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "Oh.",
                    loopTag = "serious" //Shocked?
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...I designed this?",
                    loopTag = "serious" //Shocked?
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...",
                    loopTag = "serious" //Shocked?
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...Well may as well take some notes.",
                    loopTag = "neutral" //Shocked?
                },
            }
        };


       

    }
}

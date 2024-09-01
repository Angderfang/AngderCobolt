using Angder.EchoesOfTheFuture;
using FMOD;
using System;
using System.Linq;

namespace Angder.EchoesOfTheFuture;

internal static class EnemyDialogue
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string Angder = Instance.AngderDeck.Deck.Key();
        //Jumbo?
        DB.story.all["Miner_Angder"] = new()
        {
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "miner" }, //Seriously CC devs, why is JUMBO the miner, and not Duncan, the actual miner? No wonder he had to work so hard for his promotion.
            lines = new()
            {

                new CustomSay()
                {
                    who = Angder,
                    Text = "...you mind surrendering so I can have your ship?",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    delay = 1,
                    who = "miner",
                    Text = "Set your autodestruct, and I might consider it.",
                    loopTag = "neutral"
                },
            }
        };
        
        DB.story.all["Miner_Angder_2"] = new()
        {
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "miner" },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 8,
            lines = new()
            {

                new CustomSay()
                {
                    delay = 1,
                    who = Angder,
                    Text = "Aww man. I think we need more cannons.",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    who = "miner",
                    Text = "Let me lend you some more ammunition.",
                    loopTag = "neutral"
                },
            }
        };
        DB.story.all["Miner_Angder_3"] = new()
        {
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "miner" },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 4,
            lines = new()
            {

                new CustomSay()
                {
                    who = Angder,
                    Text = "You know, maybe we should collect your bounty instead.",
                    loopTag = "neutral"
                },
                /*
                new CustomSay()
                {
                    who = "miner",
                    Text = "I don't have a bounty",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "miner",
                    Text = "Also I am not surrendering",
                    loopTag = "neutral"
                },
                */
            }
        };

        DB.story.all["drakebot_Angder_3"] = new()
        {
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "drakebot", Deck.eunice.Key(), },
            lines = new()
            {

                new CustomSay()
                {
                    who = Angder,
                    Text = "It's OK if I cut this things head off right?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    delay = 1.5,
                    who = Deck.eunice.Key(),
                    Text = "We are going to blow up the whole ship, I don't really care about the robot.",
                    loopTag = "neutral"
                },
            }
        };
        DB.story.all["pirate_Angder_3"] = new()
        {
            oncePerCombatTags = new() { "AngderDrake" },
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "pirate" },
            minTurnsThisCombat = 4,
            lines = new()
            {

                new CustomSay()
                {
                    who = Angder,
                    Text = "Hey on the off chance I am on your ship when we explode~", //can I join your crew?",  //She cut me off during playtesting, and it was honestly funnier.
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    delay = 1,
                    who = "pirate",
                    Text = "No. You can't join my crew.",
                    loopTag = "neutral"
                },
            }
        };

        DB.story.all["pirate_Angder_4"] = new()
        {
            oncePerCombatTags = new() { "AngderDrake" },
            requiredScenes = ["angder_Intro_eunice"],
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "pirate" },
            minTurnsThisCombat = 4,
            lines = new()
            {

                new CustomSay()
                {
                    who = Angder,
                    Text = "This fight isn't going to stop us being friends right?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    delay = 1,
                    who = "pirate",
                    Text = "...",
                    loopTag = "sad"
                },
            }
        };


        DB.story.all["pirate_Angder_5"] = new()
        {
            oncePerCombatTags = new() { "AngderDrake" },
            //requiredScenes = ["angder_Intro_eunice"],
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "pirate" },
            minTurnsThisCombat = 4,
            lines = new()
            {

                new CustomSay()
                {
                    who = Angder,
                    Text = "Honestly, I quite enjoy fighting you.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    delay = 1,
                    who = "pirate",
                    Text = "Thanks... I guess.",
                    loopTag = "neutral"
                },
            }
        };


        DB.story.all["Drill_Angder_1"] = new()
        {
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "skunk" },
            minTurnsThisCombat = 2,
            lines = new()
            {

                new CustomSay()
                {
                    who = Angder,
                    Text = "Stop trying to drill us! this ship is not a rock!", 
                    loopTag = "grumpy"
                },
            }
        };

        DB.story.all["Tentacle_Angder_1"] = new()
        {
            oncePerCombat = true,
            type = NodeType.combat,
            allPresent = new() { Angder, "tentacle" },
            minTurnsThisCombat = 2,
            lines = new()
            {

                new CustomSay()
                {
                    who = Angder,
                    Text = "Always love fighting a genuine monster.", //Wow, rude Angder. If Grunan was here, she would respond with something like : "ANGDER! Don't call random creatures monsters (Though to be fair this one does eat people)"
                    loopTag = "neutral"
                },
            }
        };

    }
}

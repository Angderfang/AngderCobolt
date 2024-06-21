using Angder.Angdermod;
using Angder.Angdermod.Artifacts;
using System;
using System.Linq;

namespace Angder.Angdermod;

internal static class CombatDialogue
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        /*
        foreach (var artifactType in ModEntry.Angder_AllArtifact_Types)
        {
            if (Activator.CreateInstance(artifactType) is not IAngderArtifact artifact)
                continue;
            artifact.InjectDialogue();
        }
        */

        //Angder Specific
        string Angder = Instance.AngderDeck.Deck.Key();
        DB.story.all["Theft_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { },
            oncePerRun = true,
            lastTurnPlayerStatuses = new() { ModEntry.Instance.Theft.Status },
            priority = true,
            doesNotHaveArtifacts = ["Angder.Angdermod::ShipsManifest"],
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "With plans like this, you might someday be a real pirate.",
                    loopTag = "neutral"
                },
            }
        };

        DB.story.all["AngderAirlock_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { },
            //oncePerCombatTags = new() { "angderWentMissing" },
            lastTurnPlayerStatuses = new() { ModEntry.Instance.Angdermissing.Status },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Deck.dizzy.Key(),
                            Text = "And off he goes!",
                            loopTag = "neutral"
                        },
                        /*
                        new CustomSay()
                        {
                            who = Deck.hacker.Key(),
                            Text = "Not my kind of hacking, but it works.",
                            loopTag = "neutral"
                        },
                        */
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Yes, spacewalk to the ship we are blowing up. Very smart move.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.eunice.Key(),
                            Text = "Try not to die.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "He should be wearing a helmet, right?",
                            loopTag = "Panic"
                        },
                        new CustomSay()
                        {
                            who = Deck.goat.Key(),
                            Text = "Is he gone?",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Someone just used our airlock.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.peri.Key(),
                            Text = "Angder just left...",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.shard.Key(),
                            Text = "Have fun!",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Deck.riggs.Key(),
                            Text = "Good luck!",
                            loopTag = "neutral"
                        },

                    }
                }
            }

        };
        //About to die
        DB.story.all["aboutToDie_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            maxHull = 2,
            oncePerCombatTags = new() { "aboutToDie" },
            lines = new()
            {
                new CustomSay()
                {
                    who = Angder,
                    Text = "This ship is starting to look even more familiar; It's full of holes!",
                    loopTag = "talk"
                },
            }
        };
        //Hits
        DB.story.all["Verybighit_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 6,
            lines = new()
            {
                new CustomSay()
                {
                    who = Angder,
                    Text = "Pretty sure we just lost something important.",
                    loopTag = "sad"
                },
            }
        };
        DB.story.all["bighit_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 3,
            lines = new()
            {
                new CustomSay()
                {
                    who = Angder,
                    Text = "I think we might be in trouble.",
                    loopTag = "nervous"
                },
            }
        };
        DB.story.all["payback_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            minDamageDealtToPlayerThisTurn = 2,
            playerShotJustHit = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "We hit them, they hit us. It's a good day.",
                            loopTag = "talk"
                        },
                    }
                }
            }
        };
        DB.story.all["scratch_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 1,
            minDamageDealtToPlayerThisTurn = 1,
            oncePerCombatTags = new() { "scratch" },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "Wouldn't be a proper dogfight without a few bruises.",
                            loopTag = "talk"
                        },
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "Another 20 of those, and we might be in trouble.",
                            loopTag = "talk"
                        },
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "Fighting is only fun if it's a little challenging.",
                            loopTag = "talk"
                        },
                    }
                }
            }
        };
        DB.story.all["nosell_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 0,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "...Was that supposed to hurt us?",
                            loopTag = "talk"
                        },
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "Their weapons seem pretty useless.",
                            loopTag = "talk"
                        },
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "I hope we can hit harder than they can.",
                            loopTag = "talk"
                        },
                    }
                }
            }
        };
        //Hitthem

        DB.story.all["Sports_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            playerJustShotASoccerBall = true,
            oncePerCombat = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Angder,
                    Text = "BALL!",
                    loopTag = "talk"
                },
            }
        };
        DB.story.all["Normalhits_outgoing_Angder_2"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            playerShotJustHit = true,
            whoDidThat = Instance.AngderDeck.Deck,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "I hit!",
                        loopTag = "talk"
                        },

                        new CustomSay()
                        {
                        who = Angder,
                        Text = "I wish I had more bullets.",
                        loopTag = "talk"
                        },

                        new CustomSay()
                        {
                        who = Deck.peri.Key(),
                        Text = "You know punching the button doesn't make the attack stronger, right?",
                        loopTag = "neutral"
                        },

                    }
                },
            }
        };

        DB.story.all["Bighits_outgoing_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 6,
            whoDidThat = Instance.AngderDeck.Deck,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Just warming up!",
                        loopTag = "talk"
                        },

                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Sadly I don't think they will survive all the volleys I want to fire at them.",
                        loopTag = "talk"
                        },

                        new CustomSay()
                        {
                        who = Deck.dizzy.Key(),
                        Text = "Our cannons definitely can't handle this abuse.",
                        loopTag = "neutral"
                        },

                    }
                },
            }
        };

        DB.story.all["Normalhits_outgoing_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            playerShotJustHit = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "We want another couple of hits like that!",
                        loopTag = "talk"
                        },
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "That's a hit!",
                        loopTag = "talk"
                        },
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Keep the pressure on them.",
                        loopTag = "talk"
                        },
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Yes! Destroy them.",
                        loopTag = "talk"
                        },
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Their ship is still intact, shoot them again.",
                        loopTag = "neutral"
                        },

                    }
                },
            }
        };

        //Missing

        DB.story.all["MissingHitting_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            minDamageDealtToEnemyThisTurn = 1,
            playerShotJustMissed = true,
            whoDidThat = Instance.AngderDeck.Deck,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "If I fill the air with bullets, I can't actually miss.",
                        loopTag = "talk"
                        },

                        new CustomSay()
                        {
                        who = Angder,
                        Text = "The advantages of blanketting an area...",
                        loopTag = "talk"
                        },

                        new CustomSay()
                        {
                        who = Deck.eunice.Key(),
                        Text = "Careful, I think you might accidentally be hitting them.",
                        loopTag = "neutral"
                        },

                    }
                },
            }
        };
        DB.story.all["Miss_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            playerShotJustMissed = true,
            whoDidThat = Instance.AngderDeck.Deck,
            doesNotHaveArtifacts = ["Recalibrator"],
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "That could have been better.",
                        loopTag = "sad"
                        },

                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Stay still dratt it!",
                        loopTag = "grumpy"
                        },

                        new CustomSay()
                        {
                        who = Deck.peri.Key(),
                        Text = "With how you use our Cannons, I am worried we will run out of ammunition",
                        loopTag = "squint"
                        },

                    }
                },
            }
        };

        // Statuses

        DB.story.all["Corrode_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            lastTurnEnemyStatuses = new() { Status.corrode },
            oncePerRun = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "I don't like Corrode damage, It makes raiding the enemy ship a bit scarier.",
                        loopTag = "sad"
                        },

                    }
                },
            }
        };
        /*
        DB.story.all["EnemyAutododge_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            lastTurnEnemyStatuses = new() { Status.autododgeLeft },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Shooting them is going to be a pain.",
                        loopTag = "talk"
                        },
                    }
                },
            }
        };
        DB.story.all["EnemyAutododge2_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            lastTurnEnemyStatuses = new() { Status.autododgeRight },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "I hate these things.",
                        loopTag = "sad"
                        },

                    }
                },
            }
        };
        */ //Kinda annoying, so I cut it

        //Fleeing
        DB.story.all["Fleeing1_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            shipsDontOverlapAtAll = true,
            turnStart = false,
            oncePerCombat = true,
            oncePerCombatTags = new() { "Angderflee" },
            lines = new()
            {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Why are they getting smaller?",
                        loopTag = "sad"
                        },
            }
        };
        DB.story.all["Fleeing2_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            shipsDontOverlapAtAll = true,
            turnStart = false,
            oncePerCombat = true,
            oncePerCombatTags = new() { "Angderflee" },
            lines = new()
            {
                new CustomSay()
                {
                who = Angder,
                Text = "Shouldn't we move towards the enemy?",
                loopTag = "sad"
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        delay = 1,
                        who = Deck.riggs.Key(),
                        Text = "And get shot more?",
                        loopTag = "squint"
                        },
                        new CustomSay()
                        {
                        delay = 1,
                        who = Deck.dizzy.Key(),
                        Text = "I would rather build up our shields.",
                        loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                        delay = 1,
                        who = Deck.goat.Key(),
                        Text = "Is that a good idea?",
                        loopTag = "squint"
                        },
                        new CustomSay()
                        {
                        delay = 1,
                        who = Deck.peri.Key(),
                        Text = "Only once we have an opening...",
                        loopTag = "neutral"
                        },
                        
                        new CustomSay()
                        {
                        delay = 1,
                        who = Deck.eunice.Key(),
                        Text = "I'm with Angder on this one.",
                        loopTag = "sly"
                        },
                        
                    }
                }
            }
        };

        //Status hits
        DB.story.all["Corrode_self_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            lastTurnPlayerStatuses = new() { Status.corrode },
            oncePerCombat = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "Is the floor supposed to be all melty.",
                        loopTag = "sad"
                        },
                    }
                },
            }
        };

        DB.story.all["endlessMagazine_self_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            lastTurnPlayerStatuses = new() { Status.endlessMagazine },
            oncePerRun = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = "So... we have limitless Ammo now?",
                        loopTag = "talk"
                        },
                    }
                },
            }
        };
        /*
        DB.story.all["endlessMagazine_self_Angder"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            lastTurnPlayerStatuses = new() { Status.ace },
            oncePerCombat = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Angder,
                        Text = ".",
                        loopTag = "talk"
                        },
                    }
                },
            }
        };
        */
   

    }
}

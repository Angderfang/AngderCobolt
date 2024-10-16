namespace Angder.EchoesOfTheFuture;

internal static class CombatDialogueMaid
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
        string Maid = Instance.ButlerDeck.Deck.Key();

        //About to die
        DB.story.all["aboutToDie_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            maxHull = 2,
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 1,
            oncePerCombatTags = new() { "aboutToDieMaid" },
            lines = new()
            {
                new CustomSay()
                {
                    who = Maid,
                    Text = "Our hull requires repairs.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Maid,
                    Text = "Our ship designation is now: coffin. Fix it.",
                    loopTag = "neutral"
                },
            }
        };
        //Hits
        DB.story.all["Verybighit_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 6,
            lines = new()
            {
                new CustomSay()
                {
                    who = Maid,
                    Text = "At least we no longer need to clean that part of the ship.",
                    loopTag = "neutral"
                },
            }
        };
        DB.story.all["bighit_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 3,
            lines = new()
            {
                new CustomSay()
                {
                    who = Maid,
                    Text = "I request that you try to keep me safe.",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = Maid,
                    Text = "Shall I repurpose the engine as a crematorium?",
                    loopTag = "squint"
                },
            }
        };
        DB.story.all["scratch_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 1,
            minDamageDealtToPlayerThisTurn = 1,
            oncePerCombatTags = new() { "scratch_Maid" },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Maid,
                            Text = "Damage is Negligable.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Maid,
                            Text = "Minor spills detected.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Maid,
                            Text = "We can [Shoot] better than that.",
                            loopTag = "anger"
                        },
                    }
                }
            }
        };
        DB.story.all["nosell_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
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
                            who = Maid,
                            Text = "Hull integrity: mildly dented.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Maid,
                            Text = "I don't think they know how to [Attack].",
                            loopTag = "neutral"
                        },
                    }
                }
            }
        };
        //Hitthem


        DB.story.all["Normalhits_outgoing_Maid_2"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            playerShotJustHit = true,
            whoDidThat = Instance.ButlerDeck.Deck,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Maid,
                        Text = "My programming does not allow for [Violence].",
                        loopTag = "glitch"
                        },

                        new CustomSay()
                        {
                        who = Maid,
                        Text = "I am not responsible for any violence that takes place.",
                        loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                        who = Maid,
                        Text = "Cleaning pattern in effect.",
                        loopTag = "neutral"
                        },

                        new CustomSay()
                        {
                        who = Maid,
                        Text = "The enemy ship is classified as scrap.",
                        loopTag = "neutral"
                        },

                        new CustomSay()
                        {
                        who = Maid,
                        Text = "I will send extra lasers to aid in their repairs.",
                        loopTag = "anger"
                        },

                    }
                },
            }
        };

        
        DB.story.all["Bighits_outgoing_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 6,
            whoDidThat = Instance.ButlerDeck.Deck,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Maid,
                        Text = "Excess matter sighted. Purging.",
                        loopTag = "anger"
                        },

                        new CustomSay()
                        {
                        who = Maid,
                        Text = "Please stay away from the cleaning lasers to avoid bodily harm.",
                        loopTag = "neutral"
                        },

                        new CustomSay()
                        {
                        who = Maid,
                        Text = "Please approach the cleaning laser to recieve bodily harm..",
                        loopTag = "anger"
                        },

                    }
                },
            }
        };
        
        //No more big hits...

        //Missing

        DB.story.all["MissingHitting_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            minDamageDealtToEnemyThisTurn = 1,
            playerShotJustMissed = true,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Maid,
                        Text = "Package only partially delivered.",
                        loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                        who = Maid,
                        Text = "Short controlled laserblasts.",
                        loopTag = "neutral"
                        },
                    }
                },
            }
        };
        DB.story.all["Miss_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            playerShotJustMissed = true,
            maxDamageDealtToEnemyThisAction = 0,
            whoDidThat = Instance.ButlerDeck.Deck,
            doesNotHaveArtifacts = ["Recalibrator", "Grazerbeam"],
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Maid,
                        Text = "Repositioning.",
                        loopTag = "anger"
                        },

                        new CustomSay()
                        {
                        who = Maid,
                        Text = "It's fortunate I do not feel irritation.",
                        loopTag = "neutral"
                        },



                    }
                },
            }
        };

        // Statuses

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
                        loopTag = "neutral"
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
        DB.story.all["Fleeing1_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
            shipsDontOverlapAtAll = true,
            turnStart = false,
            oncePerCombat = true,
            oncePerCombatTags = new() { "Maidflee" },
            lines = new()
            {
                        new CustomSay()
                        {
                        who = Maid,
                        Text = "I can't [PURGE ORGANICS] from this far away.",
                        loopTag = "anger"
                        },
                        new CustomSay()
                        {
                        who = Maid,
                        Text = "Avoiding violence is always the better option. [TAKE US BACK].",
                        loopTag = "anger"
                        },
            }
        };

        //Status hits
        DB.story.all["Corrode_self_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Maid },
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
                        who = Maid,
                        Text = "I can't clean that.",
                        loopTag = "neutral"
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
                        loopTag = "neutral"
                        },
                    }
                },
            }
        };
        */


    }
}

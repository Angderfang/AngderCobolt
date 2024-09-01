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
                    Text = "Our hull badly requires repairs.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Maid,
                    Text = "Ship designation is now: coffin.",
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
                    Text = "We no longer need to clean that part of the ship.",
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
                    Text = "...More damage...",
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
                            Text = "Negligable damage.",
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
                            Text = "We can clean better than that.",
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
                            who = Angder,
                            Text = "...I am going to need to scrub the hull.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "I think they are actually doing my job for me.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Angder,
                            Text = "Don't scratch the paint.",
                            loopTag = "anger"
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
                        Text = "Cleaning pattern delta.",
                        loopTag = "neutral"
                        },

                        new CustomSay()
                        {
                        who = Maid,
                        Text = "A hole has been detected.",
                        loopTag = "neutral"
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
                        Text = "Excess matter. Purging.",
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

                    }
                },
            }
        };
        DB.story.all["Miss_Maid"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Angder },
            playerShotJustMissed = true,
            whoDidThat = Instance.ButlerDeck.Deck,
            doesNotHaveArtifacts = ["Recalibrator"],
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
                        Text = "Dangerous projectile will impact innocent ship in; [UNKNOWN TIMEFRAME].",
                        loopTag = "neutral"
                        },
                        /*
                        new CustomSay()
                        {
                        who = Deck.peri.Key(),
                        Text = "With how you use our Cannons, I am worried we will run out of ammunition",
                        loopTag = "squint"
                        },
                        */

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
                        Text = "I can't [PURGE GRIME] from this far away.",
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

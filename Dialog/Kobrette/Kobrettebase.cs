namespace Angder.EchoesOfTheFuture;

internal static class KobretteBase
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string Maid = Instance.ButlerDeck.Deck.Key();
        string Angder = Instance.AngderDeck.Deck.Key();
        string Grunan = Instance.GrunanDeck.Deck.Key();
        string Kobrette = Instance.KobretteDeck.Deck.Key();

        DB.story.all["Verybighited_Kobrette"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 6,
            lines = new()
            {
                new CustomSay()
                {
                    who = Kobrette,
                    Text = "...We have survived worse.",
                    loopTag = "sad"
                },
            }
        };

        DB.story.all["Kobrette_Die"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            oncePerRun = false,
            oncePerCombat = true,
            maxHull = 1,
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 2,
            priority = false,
            lines = new()
            {
                new CustomSay()
                {
                    who = Kobrette,
                    Text = "We just need to believe.",
                    loopTag = "shield"
                },
            }
        };

        DB.story.all["Kobrette_unharmed"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            oncePerRun = false,
            oncePerCombat = true,
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 0,
            priority = false,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "The hull will hold.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "Good thing I upgraded the front of the ship.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "You cannot harm us, villain",
                            loopTag = "neutral"
                        },
                    }
                }
            }
        };

        DB.story.all["Kobrette_nearly_harmed"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            oncePerRun = false,
            oncePerCombat = true,
            enemyShotJustHit = true,
            minHullPercent = 50,
            minDamageDealtToPlayerThisTurn = 1,
            maxDamageDealtToPlayerThisTurn = 1,
            priority = false,
            
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "We can fix that later.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "We are still in good shape.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "We still have the advantage.",
                            loopTag = "neutral"
                        },
                    }
                }
            }
        };

        DB.story.all["Kobrette_ouch"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            oncePerRun = false,
            oncePerCombat = true,
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 2,
            
            priority = false,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "Bit more than a scratch that time.",
                            loopTag = "sad"
                        },
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "That wasn't a critical system.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Kobrette,
                            Text = "That was a nice shot. Next is our turn..",
                            loopTag = "manic"
                        },
                    }
                }
            }
        };

        DB.story.all["Kobrette_BigOuch"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            oncePerRun = false,
            oncePerCombat = true,
            maxHull = 3,
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 4,
            priority = false,
            lines = new()
            {
                new CustomSay()
                {
                    who = Kobrette,
                    Text = "Would surrender be honourable?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Kobrette,
                    Text = "We still have the moral highground.",
                    loopTag = "neutral"
                },
            }
        };



        DB.story.all["Miss_Kobrette"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            playerShotJustMissed = true,
            maxDamageDealtToEnemyThisAction = 0,
            whoDidThat = Instance.KobretteDeck.Deck,
            doesNotHaveArtifacts = ["Recalibrator", "Grazerbeam"],
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Kobrette,
                        Text = "Cowards. Face my cannon bravely.",
                        loopTag = "squint"
                        },

                        new CustomSay()
                        {
                        who = Kobrette,
                        Text = "The cannon is confirmed to be operational.",
                        loopTag = "neutral"
                        },



                    }
                },
            }
        };
        DB.story.all["bighit_Kobrette"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 3,
            whoDidThat = Instance.KobretteDeck.Deck,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Kobrette,
                        Text = "We do accept surrender you know.",
                        loopTag = "neutral"
                        },

                        new CustomSay()
                        {
                        who = Kobrette,
                        Text = "Justice!",
                        loopTag = "manic"
                        },

                        new CustomSay()
                        {
                        who = Kobrette,
                        Text = "Repent, while there is still time!",
                        loopTag = "neutral"
                        },


                    }
                },
            }
        };

        DB.story.all["hugehit_Kobrette"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 6,
            whoDidThat = Instance.KobretteDeck.Deck,
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                        who = Kobrette,
                        Text = "Even if we fall, your not going to last long with damage like that.",
                        loopTag = "manic"
                        },


                    }
                },
            }
        };


        DB.story.all["Fleeing1_Kobrette"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { Kobrette },
            shipsDontOverlapAtAll = true,
            
            turnStart = false,
            oncePerCombat = true,
            oncePerCombatTags = new() { "Kobretteflee" },
            lines = new()
            {
                        new CustomSay()
                        {
                        who = Kobrette,
                        Text = "Our cowardice shames us all...",
                        loopTag = "sad"
                        },
                        new CustomSay()
                        {
                        who = Kobrette,
                        Text = "I will pretend this is a tactical retreat.",
                        loopTag = "sad"
                        },
            }
        };


    }
}

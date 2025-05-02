namespace Angder.EchoesOfTheFuture;

internal static class GrunanBase
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        //NEED TO RWRITE EVERTHING
        //Damage taken
        string Maid = Instance.ButlerDeck.Deck.Key();
        string Angder = Instance.AngderDeck.Deck.Key();
        string Grunan = Instance.GrunanDeck.Deck.Key();
        DB.story.all["Grunan_Die"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { },
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
                    who = Grunan,
                    Text = "Can we try not to blow up the ship? I am working on something important.",
                    loopTag = "facepalm"
                },
            }
        };

        DB.story.all["Grunan_unharmed"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { },
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
                            who = Grunan,
                            Text = "Pathetic.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Grunan,
                            Text = "...I just lost my page.",
                            loopTag = "book"
                        },
                    }
                }
            }
        };

        DB.story.all["Grunan_unharmed"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { },
            oncePerRun = false,
            oncePerCombat = true,
            enemyShotJustHit = true,
            minHullPercent = 50,
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
                            who = Grunan,
                            Text = "Nothing to worry about.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Grunan,
                            Text = "You can handle this.",
                            loopTag = "book"
                        },
                    }
                }
            }
        };

        DB.story.all["Grunan_unharmed"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { },
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
                            who = Grunan,
                            Text = "...You should probably stop them from doing that.",
                            loopTag = "neutral"
                        },
                        new CustomSay()
                        {
                            who = Grunan,
                            Text = "You had one job.",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = Grunan,
                            Text = "Damn, I might actually have to do something about that.",
                            loopTag = "squint"
                        },
                    }
                }
            }
        };

        DB.story.all["Grunan_Ouch"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { },
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
                    who = Grunan,
                    Text = "Can we try not to blow up the ship? I am working on something important.",
                    loopTag = "panic"
                },
            }
        };


    }
}

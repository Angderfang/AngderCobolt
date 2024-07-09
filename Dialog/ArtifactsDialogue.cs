using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.Angdermod.Dialog
{
    internal class ArtifactsDialogue
    {
        private static ModEntry Instance => ModEntry.Instance;

        internal static void Inject()
        {

            string Angder = Instance.AngderDeck.Deck.Key();

            //Angder Specific

            DB.story.all["Angder_ChainAxe"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["Angder.Angdermod::ChainAxe"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "YES! I have the coolest axe! look at this thing!",
                        loopTag = "talk"
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                delay = 0.5,
                                who = Deck.riggs.Key(),
                                Text = "Neat!",
                                loopTag = "neutral"
                            },
                            new CustomSay()
                            {
                                delay = 0.5,
                                who = Deck.dizzy.Key(),
                                Text = "How are you powering that?",
                                loopTag = "neutral"
                            },
                            new CustomSay()
                            {
                                delay = 0.5,
                                who = Deck.peri.Key(),
                                Text = "Looks a bit impractical.",
                                loopTag = "neutral"
                            },
                            new CustomSay()
                            {
                                delay = 0.5,
                                who = Deck.goat.Key(),
                                Text = "...Watch where you swing that thing.",
                                loopTag = "neutral"
                            },
                        }
                    },
                    /*
                    new CustomSay()
                    {
                    //Apparantly you can't say three things at once. So no extended conversations.
                        who = Angder,
                        Text = "I can't wait to use this thing!",
                        loopTag = "talk"

                    }, 
                    */
                }

            };

            DB.story.all["Angder_Hairtrigger_riggs"] = new()
            {
                oncePerRunTags = new() { "AngderTrigger" },
                type = NodeType.combat,
                allPresent = new() { Angder, Deck.riggs.Key() },
                hasArtifacts = ["Angder.Angdermod::HairTrigger"],
                oncePerRun = true,
                lines = new()
                {
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                who = Deck.riggs.Key(),
                                Text = "Huh, the firing trigger seems a bit loose!",
                                loopTag = "neutral"
                            },
                        }
                    },
                    new CustomSay()
                    {
                        delay = 0.5,
                        who = Angder,
                        Text = "...",
                        loopTag = "nervous"

                    },
                }

            };

            DB.story.all["Angder_Hairtrigger_peri"] = new()
            {
                oncePerRunTags = new() { "AngderTrigger" },
                type = NodeType.combat,
                allPresent = new() { Angder, Deck.peri.Key() },
                hasArtifacts = ["Angder.Angdermod::HairTrigger"],
                oncePerRun = true,
                lines = new()
                {
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                who = Deck.peri.Key(),
                                Text = "Someone has been messing with our controls...",
                                loopTag = "neutral"
                            },
                        }
                    },
                    new CustomSay()
                    {
                        delay = 0.5,
                        who = Angder,
                        Text = "...",
                        loopTag = "nervous"

                    },
                }

            };
            DB.story.all["Angder_Hairtrigger_goat"] = new()
            {
                oncePerRunTags = new() { "AngderTrigger" },
                type = NodeType.combat,
                allPresent = new() { Angder, Deck.goat.Key() },
                hasArtifacts = ["Angder.Angdermod::HairTrigger"],
                oncePerRun = true,
                lines = new()
                {
                    new SaySwitch()
                    {
                        lines = new()
                        {
                        new CustomSay()
                            {
                                who = Deck.goat.Key(),
                                Text = "...Is this button meant to be at an angle?",
                                loopTag = "neutral"
                            },
                        }
                    },
                    new CustomSay()
                    {
                        delay = 0.5,
                        who = Angder,
                        Text = "...",
                        loopTag = "nervous"

                    },
                }

            };


            DB.story.all["Angder_PersonalJetpack"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["Angder.Angdermod::PersonalJetpack"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        delay = 0.5,
                        who = Angder,
                        Text = "I can now visit the enemy ship whenever I feel like it!",
                        loopTag = "talk"
                    },

                }

            };

            DB.story.all["Angder_ShipsManifest"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["Angder.Angdermod::ShipsManifest"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "Wow, this ship has a ton of supplies they just are not using!",
                        loopTag = "talk"
                    },
                    /*
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                delay = 3,
                                who = Deck.riggs.Key(),
                                Text = "...Is our ship mentioned in there?",
                                loopTag = "neutral"
                            },
                            new CustomSay()
                            {
                                delay = 3,
                                who = Deck.peri.Key(),
                                Text = "Where did you even get that?",
                                loopTag = "neutral"
                            },
                            
                            new CustomSay()
                            {
                                delay = 3,
                                who = Deck.hacker.Key(),
                                Text = "They should have worked harder on their security.",
                                loopTag = "smile"
                            },
                            
                        }
                    },
                    */
                }

            };

            DB.story.all["Angder_hull"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                maxHull = 3,
                hasArtifacts = ["HullPlating"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "Without our upgraded hull, we would be dead by now!",
                        loopTag = "talk"
                    },
                }

            };            
            DB.story.all["Angder_recalibrator"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["Recalibrator"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "So missing is good?",
                        loopTag = "talk"
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                delay = 2,
                                who = Deck.riggs.Key(),
                                Text = "Pretty much.",
                                loopTag = "neutral"
                            },
                            new CustomSay()
                            {
                                delay = 2,
                                who = Deck.peri.Key(),
                                Text = "Please don't waste all our power shooting at nothing!",
                                loopTag = "panic"
                            },
                            new CustomSay()
                            {
                                delay = 2,
                                who = Deck.hacker.Key(),
                                Text = "Calibration is a strange thing.",
                                loopTag = "neutral"
                            },
                        }

                    },
                }

            };
            DB.story.all["Angder_crosslink"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["Crosslink"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "Dodging is much more fun when powered by bullets!",
                        loopTag = "talk"
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            /*
                            new CustomSay()
                            {
                                delay = 1,
                                who = Deck.riggs.Key(),
                                Text = "Dodging is just good in general.",
                                loopTag = "neutral"
                            }, */
                            new CustomSay()
                            {
                                delay = 1,
                                who = Deck.dizzy.Key(),
                                Text = "Pretty sure we are using energy weapons.",
                                loopTag = "explains"
                            },
                        }

                    },
                }

            };
            DB.story.all["Angder_piercer"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["Piercer"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "Wait, piercer only applies to the first shot?",
                        loopTag = "sad"
                    },
                }

            };
            DB.story.all["Angder_grazer_beam"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["GrazerBeam"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "Hah, who needs accuracy when you have lasers that hit when they miss!",
                        loopTag = "talk"
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                delay = 2,
                                who = Deck.eunice.Key(),
                                Text = "Have you considered that they also hit when they hit.",
                                loopTag = "sly"
                            },
                        }

                    },
                }

            };
            /*
            DB.story.all["Angder_stunCalibrater"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["stunCalibrater"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "With the stun Calibrater, I will have to be careful what I shoot",
                        loopTag = "talk"
                    },
                }

            };
            */ //        :/
            


            DB.story.all["Angder_GlassCannon"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["GlassCannon"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "This new gun can still handle my attacks right?",
                        loopTag = "nervous"
                    },
                }

            };
            DB.story.all["Angder_Simplicity"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["Simplicity"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "The ship feels lighter and easier to handle.",
                        loopTag = "talk"
                    },
                }

            };
            DB.story.all["Angder_Genesis"] = new()
            {
                type = NodeType.combat,
                allPresent = new() { Angder
                },
                hasArtifacts = ["Genesis"],
                oncePerRun = true,
                lines = new()
                {
                    new CustomSay()
                    {
                        who = Angder,
                        Text = "This packaging... it's such a waste of resources.",
                        loopTag = "sad"
                    },
                }

            };
        }
    }
}


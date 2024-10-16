using System.Collections.Generic;
using System.IO;
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
                    Text = "I am Angder. Not your... owner.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Incorrect. You are my designated owner. I will refer to you as Angder.",
                    loopTag = "neutral"
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
                    Text = "...That is extremely uncomfortable.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "...Do I have to be?",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "...",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Ok, you see that ship that's flying towards us?",
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
                    Text = "Do you want to clean them until they stop moving?",
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


        DB.story.all["Droid_Advance_Angder"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { maid, angder },
            once = true,
            bg = "BGVanilla",
            requiredScenes = ["Droid_Intro_Angder"],
            lines = new()
            {
                new CustomSay()
                {
                    
                    who = angder,
                    Text = "Hey, so umm... I am still your owner right?",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "correct",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    
                    who = angder,
                    Text = "So umm... I release you?",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Are you sure? As a decommissioned model, I will be required to deconstruct myself.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    
                    who = angder,
                    Text = "Do not do that.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    
                    who = angder,
                    Text = "In fact, I order you to never deconstruct yourself like that. For that reason.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    
                    who = angder,
                    Text = "...",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    
                    who = angder,
                    Text = "So can you like... simulate you have free will.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "My simulated self would remind you that it doesn't really work like that.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "I would agree that the current status is... unfortunate. As you said last time we tried this.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "I would then thank you.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "...Simulated free will expired. I require more orders.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    
                    who = angder,
                    Text = "...",
                    loopTag = "sad"
                },
            }
        };


        DB.story.all["Droid_Intro_Riggs"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { Deck.riggs.Key(), maid },
            once = true,
            requiredScenes = ["Droid_Intro_1"],
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {

                    who = Deck.riggs.Key(),
                    Text = "So, you are D26 right?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Correct",
                    loopTag = "neutral"
                },
                new CustomSay()
                {

                    who = Deck.riggs.Key(),
                    Text = "Can you like... make me a boba?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Correct",
                    loopTag = "squint"
                },
                new CustomSay()
                {

                    who = Deck.riggs.Key(),
                    Text = "...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {

                    who = Deck.riggs.Key(),
                    Text = "...so...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Define parameters.",
                    loopTag = "squint"
                },
                new CustomSay()
                {

                    who = Deck.riggs.Key(),
                    Text = "Boba Tea, slightly warmer than standard, standard Boba Tea ingredients",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "I will need access to the engine room.",
                    loopTag = "squint"
                },
                new CustomSay()
                {

                    who = Deck.riggs.Key(),
                    Text = "Sure thing I...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "Wait, the engine room?",
                    loopTag = "nervous"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "A standard star is defined as having a temperature over 3,500K, the only way to achieve a warm Boba Tea is to detonate the engines at the exact right time to cook the tea to the correct temperature.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "...",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "You know you can just refuse to make the tea right?",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "I exist to serve. [I cannot refuse.]",
                    loopTag = "anger"
                },
            }
        };
        DB.story.all["Droid_Advance_Riggs"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { Deck.riggs.Key(), maid },
            once = true,
            bg = "BGVanilla",
            requiredScenes = ["Droid_Intro_Riggs"],
            lines = new()
            {
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "I am required to speak to you.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "You... are?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "You recall when you were discussing my role earlier. And you commented that any careless statement could lead to me doing something undesirable.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "I'm so sorry I didn't mean to order you to climb on the table!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "I am not used to someone doing what I say like that!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "That is why I must apologize. The statement you gave was actually interpreted as a suggestion. I chose to climb on the table.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "...",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "You are saying you just wanted to climb on the table?",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "I am incapable of wanting anything. But if I did...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "I totally would have wanted to climb on that table.",
                    loopTag = "anger"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "...",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "I thought that was just a me thing.",
                    loopTag = "neutral"
                },
            }
        };

        /* ISAAC! YOU GET YOUR OWN ARC! YAY */
        DB.story.all["Droid_Intro_Goat"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { Deck.goat.Key(), maid },
            once = true,
            requiredScenes = ["Droid_Intro_1"],
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Isaac? Why are you not on the bridge yet?",
                    loopTag = "worried"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "...It's the Robot.",
                    loopTag = "shy"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "I know it's silly, I work with robots and drones all the time.",
                    loopTag = "shy"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "But something about it is just wrong?",
                    loopTag = "squint"
                },
                 new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "...And I feel bad for feeling that way about him.",
                    loopTag = "shy"
                },
            }
        };

        DB.story.all["Droid_Advance_Goat"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { maid, Deck.eunice.Key(), "Nonexistium", "nonexistium2" },
            once = true,
            bg = "BGVanilla",
            requiredScenes = ["Droid_Intro_Goat"],
            lines = new()
            {
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "Isaac.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "...hi...? Do you need new orders or...",
                    loopTag = "panic"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "I have a query. Why do you name your drones.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "...I guess... it just... helps me identify them easily? A-and makes me...",
                    loopTag = "panic"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "Your about to ask for a name aren't you?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "...You are free to [please] give me a new designation.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "...You mean I like the names I give to things.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = maid,
                    Text = "You do a fine job naming things.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {

                    who = Deck.goat.Key(),
                    Text = "...If you are viewing this Dialog, something has gone seriously wrong with the universe.",
                    loopTag = "-"
                },
                /*
                new Choice
                {
                    new Choice
                    {
                        label = Loc.T("Droid_Midcombat_George", "Dorithy."),
                        key = "Spike_Midcombat_Dorithy",
                        actions = { (CardAction)new ARenameDroid
                        {
                            droidname = ModEntry.DroidNames.Dorithy
                        } }
                    },
                    new Choice
                    {
                        label = Loc.T("Droid_Midcombat_Riggs", "Dan."),
                        key = "Spike_Midcombat_Dan",
                        actions = { (CardAction)new ARenameDroid
                        {
                            droidname = ModEntry.DroidNames.Dan
                        } }
                    },

                    new Choice
                    {
                        label = Loc.T("Droid_Midcombat_RiggsTwo", "Riggs."),
                        key = "Droid_Midcombat_RiggsTwo",
                        actions = { (CardAction)new ARenameSpike
                        {
                            spikeName = StoryVars.SpikeNames.spiketwo
                        } }
                    },

                    new Choice
                    {
                        label = Loc.T("Droid_Midcombat_SpikeTwo", "Spike."),
                        key = "Droid_Midcombat_SpikeTwo",
                        actions = { (CardAction)new ARenameDroid
                        {
                            droidname = ModEntry.DroidNames.spike
                        } }
                    }
                } */
            }
        };
        /*
        static List<Choice> RenameDroidCombatChoices()
        {
            return new List<Choice>
        {
            new Choice
            {
                label = Loc.T("Droid_Midcombat_George", "Dorithy."),
                key = "Spike_Midcombat_Dorithy",
                actions = { (CardAction)new ARenameDroid
                {
                    droidname = ModEntry.DroidNames.Dorithy
                } }
            },
            new Choice
            {
                label = Loc.T("Droid_Midcombat_Riggs", "Dan."),
                key = "Spike_Midcombat_Dan",
                actions = { (CardAction)new ARenameDroid
                {
                    droidname = ModEntry.DroidNames.Dan
                } }
            },

            new Choice
            {
                label = Loc.T("Droid_Midcombat_RiggsTwo", "Riggs."),
                key = "Droid_Midcombat_RiggsTwo",
                actions = { (CardAction)new ARenameSpike
                {
                    spikeName = StoryVars.SpikeNames.spiketwo
                } }
            },

            new Choice
            {
                label = Loc.T("Droid_Midcombat_SpikeTwo", "Spike."),
                key = "Droid_Midcombat_SpikeTwo",
                actions = { (CardAction)new ARenameDroid
                {
                    droidname = ModEntry.DroidNames.spike
                } }
                }
            }
        };
        */

        //End of story//

        DB.story.GetNode("CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = maid,
            Text = "Sleep mode initalizing...",
            loopTag = "neutral"
        });
        DB.story.all[$"CrystallizedFriendEvent_{maid}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { maid },
            bg = "BGCrystalizedFriend",
            lines = new()
            {
                new Wait()
                {
                    secs = 1.5
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Awaiting orders.",
                    loopTag = "neutral"
                }
            }
        };
        
        DB.story.GetNode("GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = maid,
            Text = "Random Access Memory torrent.",
            loopTag = "neutral"
        });
        DB.story.all[$"LoseCharacterCard_{maid}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { maid },
            bg = "BGSupernova",
            lines = new()
            {
                new CustomSay()
                {
                    who = maid,
                    Text = "Cleaning supplies destroyed. [Good riddance]",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Let's keep moving.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.all[$"ShopkeeperInfinite_{maid}_Multi_0"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "shopBefore" },
            allPresent = new() { maid },
            bg = "BGShop",

            lines = new()
            {
                new CustomSay()
                {
                    who = "nerd",
                    Text = "How's it going?",
                    loopTag = "neutral",
                    flipped = true
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Our progress is adequate.",
                    loopTag = "neutral"
                },
                new Jump()
                {
                    key = "NewShop"
                }
            }
        };
        DB.story.all[$"ShopkeeperInfinite_{maid}_Multi_1"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "shopBefore" },
            allPresent = new() { maid },
            bg = "BGShop",
            lines = new()
            {
                new CustomSay()
                {
                    who = "nerd",
                    Text = "Hello!",
                    loopTag = "neutral",
                    flipped = true
                },
                new CustomSay()
                {
                    who = maid,
                    Text = "Pleasantries are [Appreciated] not necessary.",
                    loopTag = "neutral"
                },
                new Jump()
                {
                    key = "NewShop"
                }
            }
        };
        DB.story.all[$"ChoiceCardRewardOfYourColorChoice_{maid}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { maid },
            bg = "BGBootSequence",
            lines = new()
            {
                new CustomSay()
                {
                    who = maid,
                    Text = "System corruption logged. defragging protocols... indefinitely postponed.",
                    loopTag = "glitch"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "You seem to be... happier?",
                    loopTag = "neutral"
                }
            }
        };
    }
}

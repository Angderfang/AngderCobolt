using System.Linq;

namespace Angder.EchoesOfTheFuture;

internal static class EventDialogue
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string angder = ModEntry.Instance.AngderDeck.Deck.Key();

        DB.story.GetNode("AbandonedShipyard_Repaired")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            
            who = angder,
            Text = "Now we can take even more damage.",
            loopTag = "talk"
            
        });

        //Intros, Not plots

        DB.story.all["angder_Intro_1"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "...what is going on? Where am I?",
                    loopTag = "grumpy"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "You just came out of Cryosleep.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "What are you doing on my ship? If I had my axe I would~",
                    loopTag = "grumpy"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Wait... Is this my ship?",
                    loopTag = "nervous"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "I suppose that depends...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Do you want it to be?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Yes?",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Then how about helping us defend it, before we all explode again.",
                    loopTag = "neutral"
                },
                /*
                new CustomSay()
                {
                    who = angder,
                    Text = "Wait, what do you mean by 'Again'?",
                    loopTag = "nervous"
                },
                */
            }
        };


        DB.story.all["angder_Intro_2"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder },
            once = true,
            bg = "BGRunStart",
            requiredScenes = ["angder_Intro_1"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Back here again?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Yes. we are in a timeloop.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I thought we were on a ship.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Just go to the bridge already.",
                    loopTag = "squint"
                },
            }
        };

        DB.story.all["angder_Intro_4"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder },
            once = true,
            bg = "BGRunStart",
            requiredScenes = ["angder_Intro_1"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "I think I am starting to get used to this cycle of waking up here, fighting, and then exploding.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "It actually took less time for you than most.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Though you also took considerably longer to arrive for the first time.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...Like I arrived late to the party.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = "comp",
                    Text = "Which is suprising as that implies the passage of external time.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I would rather just blow some people up rather than think about what that implies.",
                    loopTag = "neutral"
                },
            }
        };

        /*
        DB.story.all["angder_Intro_3"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder },
            once = true,
            bg = "BGRunStart",
            requiredScenes = ["angder_Intro_1"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Another day... or is it the same day?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Pretty sure time doesn't move much within the loop.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...Then how come things change?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "The timeloop is unstable, which is fortunate, otherwise it would probably be impossible to escape from.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...And we do that by fighting?",
                    loopTag = "neutral"
                },
            }
        };
        */

        //Dizzy Story


        DB.story.all["angder_Intro_Dizzy"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder, Deck.dizzy.Key() },
            requiredScenes = ["angder_Intro_1"],
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Aghh. What...?",
                    loopTag = "grumpy"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.dizzy.Key(),
                    Text = "You are awake.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Oh, of course. the loop. Which means we are about to be attacked.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Which is closer, the Bridge or the airlock?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.dizzy.Key(),
                    Text = "Definitely the bridge, and please don't check the map.",
                    loopTag = "serious"
                },
            }
        };
        
        //Angder grumpy intros should be kept to a minimum.


        
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
                    Text = "A helmet? Why would I need one, the Personal forcefield. keeps it all inside.",
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

        /*
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
                    Text = "CAT where is Angder?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "In the back, grabbing some food.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...",
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
                    Text = "Oh. .",
                    loopTag = "serious" //Shocked?
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "The D stands for Dizzy's.",
                    loopTag = "serious" //Shocked?
                },
            }
        };
        */
        //Not happy with how Dizzy sounds here.

        //Riggs

        /*
        DB.story.all["angder_Intro_Riggs"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder, Deck.riggs.Key() },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "Does Cryosleep ever stop causing headaches.",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I prefer it to waking up normally.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "Oh hey! A new person!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "actually. I think I remember meeting you before.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I also think I know you, even before the loops... Your voice is familiar.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "...",
                    loopTag = "squint"
                },
            }
        };
        */

        DB.story.all["angder_Intro_Riggs"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder, Deck.riggs.Key() },
            once = true,
            bg = "BGRunStart",
            requiredScenes = ["angder_Intro_1"],
            hasArtifacts = ["CargoHold"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "And I am awake again...",
                    loopTag = "grumpy"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "This cockpit... it's familiar.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I mean, the tech is all different, but the seats? the furniture? This stain?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "Maybe your ship was of the same design?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Down to the same stains?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "...Maybe it's just very stainable?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Yea, that makes sense.",
                    loopTag = "neutral"
                },
            }
        };

        DB.story.all["angder_Advance_Riggs"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { angder, Deck.riggs.Key() },
            once = true,
            bg = "BGVanilla",
            requiredScenes = ["angder_Intro_Riggs"],
            hasArtifacts = ["CargoHold"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "I have... another theory about this ship",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I think it is my ship",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "...No?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Think about it, we are like, looping in some kind of micro-dimension? So like, maybe I am from a different other dimension?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "And in that Dimension, this was like... my ship, and you had a different ship.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.riggs.Key(),
                    Text = "But why would you have stained it in the same place?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I don't know.",
                    loopTag = "serious"
                },
            }
        };

        //Eunice

        DB.story.all["angder_Intro_eunice"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            requiredScenes = ["angder_Intro_1"],
            allPresent = new() { angder, Deck.eunice.Key() },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Drake? Aren't you supposed to be against us.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "Sometimes.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "Other times I end up here.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...So when you said that this ships entire crew were clowns...",
                    loopTag = "nervous"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Did that include you?",
                    loopTag = "smug"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "...",
                    loopTag = "reallymad"
                },
                /*
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "honk",
                    loopTag = "sadEyesClosed"
                },
                */
            }
        };

        DB.story.all["angder_Advance_eunice"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { angder, Deck.eunice.Key() },
            once = true,
            bg = "BGVanilla",
            requiredScenes = ["angder_Intro_eunice"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "We make quite a team!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "I have had worse",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "It's funny, you... aren't famous or anything are you?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "Why are you asking?",
                    loopTag = "blush"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Just... I think I have seen you before somewhere. Before the loops I mean.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "I am a pretty well known pirate.",
                    loopTag = "sly"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = Deck.eunice.Key(),
                    Text = "Prehaps you have seen the wanted posters?",
                    loopTag = "sly"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...That could be it",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I hope that's it.",
                    loopTag = "nervous"
                },
            }
        };

        /*
        DB.story.all["angder_Final_eunice_postRiggsscene"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_three" },
            allPresent = new() { angder, Deck.eunice.Key() },
            once = true,
            bg = "BGRunStart",
            requiredScenes = ["angder_Advance_eunice", "angder_Advance_Riggs" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Hey, Drake! I remember now, where I saw your name.",
                    loopTag = "nervous"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "And like, for the good of time or something...",
                    loopTag = "nervous"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Just forget that I said anything!",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "...",
                    loopTag = "mad"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "That would be far easier if you hadn't turned up and specifically told me to do that.",
                    loopTag = "mad"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "But... like... I don't want to~",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "I'm dead in the future, aren't I.",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "Riggs told me where you are from, and that reaction only makes sense if I am dead or something similar. So as I already know... tell me exactly what happened.",
                    loopTag = "mad"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Umm guys? Isn't that...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "A really bad idea?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "Since when has that ever stopped me?",
                    loopTag = "sly"
                },
            }
        };
        */

        /* peri */


        
        DB.story.all["angder_Annoys_peri"] = new()
        {
            type = NodeType.@event,
            requiredScenes = ["angder_Intro_1"],
            lookup = new() { "zone_lawless" },
            allPresent = new() { angder, Deck.peri.Key() },
            once = true,
            bg = "BGVanilla",
            lines = new()
            {
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "Angder. We need to talk.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "what is it?",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "I need to ask you to take things a little more seriously.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "What are you talking about? I am always taking this seriously.",
                    loopTag = "serious"
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "I don't think you are.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "I have been watching you in combat, you are constantly grinning from ear to ear, and taking stupid risks without a single concern for the safety of this ship.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "Even when WE are the ones being injured, you are too busy enjoying yourself to actually help.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "I mean yea. I enjoy fighting. Is that really a problem? I do plenty of damage!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "At the slightest opportunity, you dive to the enemy ship, often when we need you at the controls.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    flipped = true,
                    who = angder,
                    Text = "Yea, that's how I fight. Up close, personal. blood rushing through my veins.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "...",
                    loopTag = "nap"
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "Some of us actually want the loops to end Angder.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.peri.Key(),
                    Text = "Not all of us enjoy fighting as much as you do.",
                    loopTag = "neutral"
                },
            }
        };


        //No more Story
        DB.story.GetNode("CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = angder,
            Text = "I suppose it might be for the best...",
            loopTag = "neutral"
        });
        DB.story.all[$"CrystallizedFriendEvent_{angder}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { angder },
            bg = "BGCrystalizedFriend",
            lines = new()
            {
                new Wait()
                {
                    secs = 1.5
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Isn't anyone else worried about the implications that our pods are just floating in space? No? Ok then.",
                    loopTag = "neutral"
                }
            }
        };
        DB.story.GetNode("GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = angder,
            Text = "Food?",
            loopTag = "neutral"
        });
        DB.story.all[$"LoseCharacterCard_{angder}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { angder },
            bg = "BGSupernova",
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Aww. I needed that",
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
        DB.story.all[$"ShopkeeperInfinite_{angder}_Multi_0"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "shopBefore" },
            allPresent = new() { angder },
            bg = "BGShop",
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Hey, a shop.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "nerd",
                    Text = "Leave your weapons on your ship please.",
                    loopTag = "neutral",
                    flipped = true
                },
                new Jump()
                {
                    key = "NewShop"
                }
            }
        };
        DB.story.all[$"ShopkeeperInfinite_{angder}_Multi_1"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "shopBefore" },
            allPresent = new() { angder },
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
                    who = angder,
                    Text = "Amazing! I have never been attacked by so many people!",
                    loopTag = "neutral"
                },
                new Jump()
                {
                    key = "NewShop"
                }
            }
        };
        DB.story.all[$"ShopkeeperInfinite_{angder}_Multi_2"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "shopBefore" },
            allPresent = new() { angder },
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
                    who = angder,
                    Text = "HI!",
                    loopTag = "neutral"
                },
                new Jump()
                {
                    key = "NewShop"
                }
            }
        };
        DB.story.all[$"ChoiceCardRewardOfYourColorChoice_{angder}"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = new() { angder },
            bg = "BGBootSequence",
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "I think I have a cool new idea!",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Let's keep moving.",
                    loopTag = "squint"
                }
            }
        };
        /*
        DB.story.GetNode("Sasha_2_multi_2")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = angder,
            Text = "BALL!",
            loopTag = "neutral"
        });
        */

    }
}

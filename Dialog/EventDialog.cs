using System.Linq;

namespace Angder.Angdermod;

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
                    Text = "Aghh. What... what is going on? Where am I?",
                    loopTag = "talk"
                },
                new CustomSay()
                {
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
                    who = "comp",
                    Text = "I suppose that depends...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Do you want it to be?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Yes?",
                    loopTag = "grumpy"
                },
                new CustomSay()
                {
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
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Yes. we are in a timeloop.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I thought we were on a ship.",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...",
                    loopTag = "talk"
                },
                new CustomSay()
                {
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
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "It actually took less time for you than most.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "...Though you also took considerably longer to arrive for the first time.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...Like I arrived late to the party.",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "...Which is odd as that implies the passage of external time.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...I would rather just blow some people up rather than think about what that implies.",
                    loopTag = "talk"
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
                    loopTag = "talk"
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
                    loopTag = "talk"
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
                    loopTag = "talk"
                },
            }
        };
        */

        //Dizzy Story

        /*
        DB.story.all["angder_Intro_Dizzy"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder, Deck.dizzy.Key() },
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
                    who = Deck.dizzy.Key(),
                    Text = "You are awake.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "What ship is this? it looks familiar...",
                    loopTag = "grumpy"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Wait, this IS my ship. It's just... someone cleaned it?",
                    loopTag = "sad"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "You even replaced some of the newer parts with... vintage replacements?",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "This ship is definitely not yours, and as for being in good condition...",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...Thats not going to last according to the proximity sensor.",
                    loopTag = "neutral"
                },
            }
        };
        */
        //Angder grumpy intros should be kept to a minimum.


        /*
        DB.story.all["angder_Advance_Dizzy"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_lawless" },
            allPresent = new() { angder, Deck.dizzy.Key() },
            once = true,
            bg = "BGRunStart",
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
                    who = angder,
                    Text = "Oh this, it's a DrH-36 or something.",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "How do you breath while wearing it in space?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Without a helmet? That's easy. Personal forcefield.",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "riiight. And where is the oxygen stored?",
                    loopTag = "frown"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "In this thing.",
                    loopTag = "talk" // NEED IMAGE OF ANGDER showing thing
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "...",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "Nothing you just said is possible with modern technology.",
                    loopTag = "squint"
                },
            }
        };

        DB.story.all["angder_Final_Dizzy"] = new()
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
                    Text = "In the back, grabbing some food I think.",
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
                    Text = "DrH-36... What does that... Why have I never heard of this sort of suit before?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = Deck.dizzy.Key(),
                    Text = "What does DrH even stand fo~",
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
                    Text = "The D stands for Dizzy.",
                    loopTag = "serious" //Shocked?
                },
            }
        };
        */ //Not happy with how Dizzy sounds here.

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
                    loopTag = "talk"
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
                    loopTag = "talk"
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
                    loopTag = "grumpy"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I mean, the tech is all different, but the seats? the furniture? This stain?",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "Maybe your ship was of the same design?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Down to the same stains?",
                    loopTag = "talk"
                },
                new CustomSay()
                {
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
            bg = "BGRunStart",
            requiredScenes = ["angder_Intro_Riggs"],
            hasArtifacts = ["CargoHold"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "I have... another theory about this ship",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I think it is my ship",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "...No?",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Think about it, we are like, looping in some kind of micro-dimension? So like, maybe I am from a different other dimension?",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "And in that Dimension, this was like... my ship, and you had a different ship.",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.riggs.Key(),
                    Text = "But why would you have stained it in the same place?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "I don't know.",
                    loopTag = "nervous"
                },
            }
        };

        //Max

        DB.story.all["angder_Intro_eunice"] = new()
        {
            type = NodeType.@event,
            lookup = new() { "zone_first" },
            allPresent = new() { angder, Deck.eunice.Key() },
            once = true,
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "Drake? Aren't you supposed to be against us.",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "Sometimes.",
                    loopTag = "neutral"
                },
                new CustomSay()
                {
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
                    Text = "Were you include yourself in that assessment?",
                    loopTag = "smug"
                },
                new CustomSay()
                {
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
                new CustomSay()
                {
                    who = "comp",
                    Text = "We have an incoming hostile vessel.",
                    loopTag = "neutral"
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
            bg = "BGRunStart",
            requiredScenes = ["angder_Intro_eunice"],
            lines = new()
            {
                new CustomSay()
                {
                    who = angder,
                    Text = "We make quite a team!",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "I have had worse team-mates",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "It's funny, you... aren't famous or anything are you?",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "Why are you asking?",
                    loopTag = "blush"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "Just... I think I have seen you before somewhere...",
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "I am a pretty well known pirate.",
                    loopTag = "sly"
                },
                new CustomSay()
                {
                    who = Deck.eunice.Key(),
                    Text = "Prehaps you have seen the wanted posters?",
                    loopTag = "sly"
                },
                new CustomSay()
                {
                    who = angder,
                    Text = "...That could be it",
                    loopTag = "talk"
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

        //No more Story
        DB.story.GetNode("CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = angder,
            Text = "I suppose it might be for the best...",
            loopTag = "talk"
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
                    loopTag = "talk"
                }
            }
        };
        DB.story.GetNode("GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = angder,
            Text = "Food?",
            loopTag = "talk"
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
                    loopTag = "talk"
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
                    loopTag = "talk"
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
                    loopTag = "talk"
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
                    loopTag = "talk"
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
                    loopTag = "talk"
                },
                new CustomSay()
                {
                    who = "comp",
                    Text = "Let's keep moving.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.GetNode("Sasha_2_multi_2")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = angder,
            Text = "BALL!",
            loopTag = "talk"
        });

    }
}

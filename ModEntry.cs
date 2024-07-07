using Nickel;
using HarmonyLib;
using Nanoray.PluginManager;
using Angder.Angdermod.Cards;
using Angder.Angdermod.Artifacts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Nickel.Common;


namespace Angder.Angdermod;

/* Icons for cleave provided by Soggoru Waffle! Thanks Waffle */
public sealed class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal IKokoroApi KokoroApi { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    internal readonly Harmony Harmony;


    // AngdersDeck and Face Images.
    internal ISpriteEntry Angder_Character_CardBackground { get; }
    internal ISpriteEntry Angder_Trash_CardFrame { get; }
    internal ISpriteEntry Angder_Character_CardFrame { get; }
    internal ISpriteEntry Angder_Character_Panel { get; }
    internal ISpriteEntry Angder_Mini_0 { get; }
    internal ISpriteEntry Angder_talk { get; }
    internal ISpriteEntry Angder_bigmouth { get; } //Do I actually want to use this frame?
    internal ISpriteEntry Angder_Neutral { get; }
    internal ISpriteEntry Angder_Droop { get; }
    internal ISpriteEntry Angder_Droop_talk { get; }
    internal ISpriteEntry Angder_Serious { get; }
    internal ISpriteEntry Angder_Serious_talk { get; }

    internal ISpriteEntry Angder_Nervous { get; }
    internal ISpriteEntry Angder_Nervoustalk1 { get; }
    internal ISpriteEntry Angder_Nervoustalk2 { get; }
    internal ISpriteEntry Angder_grumpy { get; }
    internal ISpriteEntry Angder_grumpytalk { get; }

    internal ISpriteEntry Angder_smug { get; }
    internal ISpriteEntry Angder_smugtalk { get; }
    internal ISpriteEntry Angder_squinttalk { get; }
    internal ISpriteEntry Angder_squint { get; }


    //Angder Card arts

    internal ISpriteEntry Angder_CleaveArt { get; }
    internal ISpriteEntry Angder_RemoteUplink { get; }
    internal ISpriteEntry Angder_Airlock { get; }
    internal ISpriteEntry Angder_Shatter { get; }
    internal ISpriteEntry Angder_Gatling { get; }
    internal ISpriteEntry Angder_Minimap { get; }
    internal ISpriteEntry Angder_Red { get; }
    internal ISpriteEntry Angder_Punch { get; }
    internal ISpriteEntry Angder_Anticipate { get; }
    internal ISpriteEntry Angder_Evadecard { get; }
    internal ISpriteEntry Angder_Bottled { get; }
    internal ISpriteEntry Angder_Deepbreath { get; }
    internal ISpriteEntry Angder_Entrypod { get; }
    internal ISpriteEntry Angder_Shield { get; }
    internal ISpriteEntry Angder_Enraged { get; }
    internal ISpriteEntry Angder_ManyBulletMuchwow { get; }
    internal ISpriteEntry Angder_Crates { get; }
    internal ISpriteEntry Angder_ShiftShot { get; }
    internal ISpriteEntry Angder_Ramcard { get; }
    internal ISpriteEntry Angder_Instinct { get; }


    //traitstuff
    internal ICardTraitEntry RemoteControl { get; private set; } = null!;
    internal ISpriteEntry RemoteControlSprite { get; private set; } = null!;
    internal ISpriteEntry RemoteControlIcon { get; private set; } = null!;

    //Cleave images

    internal ISpriteEntry EnergySiphon3 { get; }
    internal ISpriteEntry EnergySiphon2 { get; }
    internal ISpriteEntry ChainAxe1 { get; }
    internal ISpriteEntry ChainAxe2 { get; }
    internal ISpriteEntry Cleaveshortleft { get; }
    internal ISpriteEntry Cleavelongleft { get; }
    internal ISpriteEntry Cleaveshortright { get; }
    internal ISpriteEntry Cleavelongright { get; }
    internal ISpriteEntry MoveenemyLeft { get; }
    internal ISpriteEntry MoveenemyRight { get; }
    internal ISpriteEntry StunSmallIcon { get; }
    internal ISpriteEntry Overdriveno { get; }
    internal ISpriteEntry Angdermissingin { get; }
    internal ISpriteEntry Angdermissingun { get; } //YES I AM MISSING MY GUN. I USED TO FIRE 10 SHOTS A CARD DARN IT!
    //Ram
    internal ISpriteEntry Ram { get; }
    internal ISpriteEntry RamPierce { get; }

    //The decks
    internal IDeckEntry AngderDeck { get; }
    internal IDeckEntry AngderstrashDeck { get; }
    internal ICharacterEntry Angderchar { get; }

    //Status entries
    //internal ISpriteEntry MoveEnemyLeft { get; }
    //internal ISpriteEntry MoveEnemyRight { get; }
    internal IStatusEntry Rampage { get; }
    internal IStatusEntry Theft { get; }
    internal IStatusEntry Disrupt { get; }
    internal IStatusEntry FuelSiphon { get; }
    internal IStatusEntry FuelDiscard { get; }
    internal IStatusEntry Fury { get; }
    internal IStatusEntry Angdermissing { get; }
    internal static IReadOnlyList<Type> Angder_StarterCard_Types { get; } = [
        typeof(CardBoard),
        typeof(CardEntrypod),
        typeof(CardAnticipation),
        typeof(CardAnxiety),
        typeof(CardEscapePod),
    ];
    internal static IReadOnlyList<Type> Angder_CommonCard_Types { get; } = [
         typeof(CardBoardmanuvour),
         typeof(CardDeepBreaths),
         typeof(CardPunch),
         typeof(CardInstinct),
         typeof(CardCreateMap),
         //typeof(CardRemotecontrol) /*cut for being bad */
    ];

    /* common cards */
    internal static IReadOnlyList<Type> Angder_UnCommonCard_Types { get; } = [
         typeof(CardFastReturn),
         typeof(CardExtractionflare),
         //typeof(CardBottledRage),
         typeof(CardTooAngryToDie),
         typeof(CardFasterCannons),
         typeof(CardDiagnostic),
         typeof(CardSiphonFuel),
         typeof(CardSeeingRed),
         /* typeof(CardIgnition) /*What even is this card supposed to be again?
          * 
          */
    ];
    internal static IReadOnlyList<Type> Angder_RareCard_Types { get; } = [
         typeof(CardPorts),
         typeof(CardRam),
         typeof(CardDeepraid),
         typeof(CardPlannedRaid),
         typeof(CardEnrage),
         typeof(CardDistractiongambit),
        //typeof(CardPowertransfer) //SYMBOL WON'T WORK
 /* Thats the rares done?*/
    ];

    /*
    internal static IReadOnlyList<Type> CATEXE { get; } = [
     typeof(CardRam),
         typeof(AngderEXE),
    ];
    */

    internal static IReadOnlyList<Type> Angder_Trash_Types { get; } = [
     //typeof(CardDistantYelling),
     typeof(CardLootPowercore),
     typeof(CardStolenMunitions),
     typeof(CardCoolRocket),
     typeof(CardDiagnosticComplete),
     typeof(CardExposedport),
     typeof(CardAutoblastleft),
     typeof(CardHairTrigger),
    ];
    internal static IReadOnlyList<Type> Angder_EXE_Types { get; } = [
     typeof(CardAngderBot),
     typeof(AngderEXE)
    ];
    internal static IEnumerable<Type> AngderMod_AllCard_Types
        => Angder_StarterCard_Types
        .Concat(Angder_CommonCard_Types)
        .Concat(Angder_UnCommonCard_Types)
        .Concat(Angder_RareCard_Types)
        .Concat(Angder_Trash_Types)
        .Concat(Angder_EXE_Types);

    /* Going to need to rethink these */
    internal static IReadOnlyList<Type> Angder_CommonArtifact_Types { get; } = [
        typeof(ChainAxe), //OPish? On paper kinda wild.
        //typeof(Biggerbullet), //BAD, Find something better. Now part of duo artifact future planning. // Nope, just cut entirely, Angder isn't cleave anymore.
        typeof(HairTrigger), //Balancing nightmare.
        typeof(AggressiveSiphon)
    ];
    internal static IReadOnlyList<Type> Angder_BossArtifact_Types { get; } = [
        typeof(ShipsManifest),
        typeof(PersonalJetpack),
        //typeof(EnergySiphon),
        ];
    internal static IEnumerable<Type> Angder_AllArtifact_Types
        => Angder_CommonArtifact_Types.Concat(Angder_BossArtifact_Types);


    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;
        Harmony = new(package.Manifest.UniqueName);
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;

        
        
        /* These localizations lists help us organize our mod's text and messages by language.
         * For general use, prefer AnyLocalizations, as that will provide an easier time to potential localization submods that are made for your mod 
         * IMPORTANT: These localizations are found in the i18n folder (short for internationalization). The Demo Mod comes with a barebones en.json localization file that you might want to check out before continuing 
         * Whenever you add a card, artifact, character, ship, pretty much whatever, you will want to update your locale file in i18n with the necessary information
         * Example: You added your own character, you will want to create an appropiate entry in the i18n file. 
         * If you would rather use simple strings whenever possible, that's also an option -you do you. */

        //Keeping this here; no way I would remember how this works.

        AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );

        //Oh hey, I found his face.
        Angder_Trash_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_Trashcardframe.png"));
        Angder_Character_CardBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_cardbackground.png"));
        Angder_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_cardframe.png"));
        Angder_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_panel.png"));
        //Base an nuetral

        Angder_Mini_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_mini_0.png"));
        Angder_talk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angdertalk.png"));
        Angder_Neutral = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angder_Neutral.png"));
        Angder_bigmouth = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angdertalkbigmouth.png"));
        Angder_Droop = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/AngderDroop.png"));
        Angder_Droop_talk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/AngderDrooptalk.png"));

        Angder_Nervous = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angdernervous.png"));
        Angder_Nervoustalk1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angdernervoustalk1.png"));
        Angder_Nervoustalk2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angdernervoustalk2.png"));

        Angder_grumpy = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angdergrumpy.png"));
        Angder_grumpytalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angdergrumpytalk.png"));

        Angder_smug = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angder_Smug.png"));
        Angder_smugtalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angder_Smugtalk.png"));


        Angder_squint = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angder_Squint.png"));
        Angder_squinttalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angder_Squint_talk.png"));

        Angder_Serious = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angder_Serious.png"));
        Angder_Serious_talk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angder_Serious_talk.png"));



        //Artifact art
        EnergySiphon3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/EnergySiphon3.png"));
        EnergySiphon2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/EnergySiphon2.png"));

        ChainAxe1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/ChainAxe.png"));
        ChainAxe2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/ChainAxeExhaust.png"));


        //Angder Card Art
        Angder_CleaveArt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Cleave.png"));
        Angder_RemoteUplink = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Remotecontrol.png"));
        Angder_Airlock = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Airlock.png"));
        Angder_Shatter = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Shatterthesky.png"));
        Angder_Minimap = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Raidship.png"));
        Angder_Gatling = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Gatling.png"));
        Angder_Red = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Seeingred.png"));
        Angder_Punch = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Punchit.png"));
        Angder_Anticipate = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Anticipation.png"));
        Angder_Bottled = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/BottledRage.png"));
        Angder_Evadecard = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/HOWDOYOUSPELLMANOUVOUR.png"));
        Angder_Deepbreath = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/DeepBreath.png"));
        Angder_Entrypod = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/EntryPod.png"));
        Angder_Shield = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/TooAngryToDie.png"));
        Angder_Enraged = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Enraged.png"));
        Angder_ManyBulletMuchwow = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Toomanybullets.png"));
        Angder_Crates = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Crates.png"));
        Angder_ShiftShot = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/ShiftShot.png"));
        Angder_Ramcard = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Ram.png"));
        Angder_Instinct = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Instinct.png"));

        //Angder deck
        AngderDeck = helper.Content.Decks.RegisterDeck("AngderDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("3A4999"),
                titleColor = new Color("D8FFFF")
            },

            DefaultCardArt = Angder_Character_CardBackground.Sprite,
            BorderSprite = Angder_Character_CardFrame.Sprite,
            Name = this.AnyLocalizations.Bind(["character", "Angder", "name"]).Localize,
        });

        helper.ModRegistry.GetApi<IMoreDifficultiesApi>("TheJazMaster.MoreDifficulties", new SemanticVersion(1, 3, 0))?.RegisterAltStarters(
        deck: AngderDeck.Deck,
        starterDeck: new StarterDeck
        {
        cards = [
                new CardEscapePod(),
                new CardBoard(),
                new CardAnxiety()
                ]
        }  
        );
        //Trash deck
        AngderstrashDeck = helper.Content.Decks.RegisterDeck("Angders loot", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("3A4999"),
                titleColor = new Color("000000")
            },
            /* We give it a default art and border some Sprite types by adding '.Sprite' at the end of the ISpriteEntry definitions we made above. */
            DefaultCardArt = Angder_Character_CardBackground.Sprite,
            BorderSprite = Angder_Trash_CardFrame.Sprite,

            Name = this.AnyLocalizations.Bind(["character", "Angder", "Trash"]).Localize,
        });

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,

            LoopTag = "neutral",

            /* The game doesn't use frames properly when there are only 2 or 3 frames. If you want a proper animation, avoid only adding 2 or 3 frames to it */
            Frames = new[]
            {
                Angder_Neutral.Sprite,
                Angder_talk.Sprite,
                Angder_Neutral.Sprite,
                Angder_talk.Sprite,
                Angder_Neutral.Sprite
            }
        });
        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "mini",
            Frames = new[]
            {
                /* Mini only needs one sprite. We call it animation just because we add it the same way as other expressions. */
                Angder_Mini_0.Sprite
            }
        });

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "sad",
            Frames = new[]
{
                Angder_Droop.Sprite,
                Angder_Droop_talk.Sprite,
                Angder_Droop.Sprite,
                Angder_Droop_talk.Sprite,
                Angder_Droop.Sprite,
            }
        });

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "squint",
            Frames = new[]
            {
                Angder_squint.Sprite,
                Angder_squinttalk.Sprite,
                Angder_squint.Sprite,
                Angder_squinttalk.Sprite,
                Angder_squint.Sprite,
                Angder_squinttalk.Sprite,
                Angder_squint.Sprite,
            }
        });
        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "talk",
            Frames = new[]
            {
                Angder_Neutral.Sprite,
                Angder_talk.Sprite,
                Angder_Neutral.Sprite,
                Angder_talk.Sprite,
                Angder_Neutral.Sprite,
                Angder_talk.Sprite,
                Angder_Neutral.Sprite,
            }
        });
        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "nervous",
            Frames = new[]
    {
                Angder_Nervous.Sprite,
                Angder_Nervoustalk1.Sprite,
                Angder_Nervous.Sprite,
                Angder_Nervoustalk2.Sprite,
                Angder_Nervous.Sprite,
            }
        });
        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "grumpy",
            Frames = new[]
            {
                Angder_grumpy.Sprite,
                Angder_grumpytalk.Sprite,
                Angder_grumpy.Sprite,
                Angder_grumpytalk.Sprite,
                Angder_grumpy.Sprite,
            }
        });

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "serious",
            Frames = new[]
    {
                Angder_Serious.Sprite,
                Angder_Serious_talk.Sprite,
                Angder_Serious.Sprite,
                Angder_Serious_talk.Sprite,
                Angder_Serious.Sprite,
            }
        });

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "smug",
            Frames = new[]
{
                Angder_smug.Sprite,
                Angder_smugtalk.Sprite,
                Angder_smug.Sprite,
                Angder_smugtalk.Sprite,
                Angder_smug.Sprite,
            }
        });

        helper.Content.Characters.RegisterCharacterAnimation(new CharacterAnimationConfiguration()
        {
            Deck = AngderDeck.Deck,
            LoopTag = "gameover",
            Frames = new[]
            {
                Angder_Droop.Sprite,
                Angder_Droop_talk.Sprite,
                Angder_Droop.Sprite,
                Angder_Droop_talk.Sprite,
                Angder_Droop.Sprite,
            }
        });






        Angderchar = helper.Content.Characters.RegisterCharacter("Angder", new()
        {
            Deck = AngderDeck.Deck,
            Starters = new StarterDeck
            {
                cards = [new CardEntrypod(), 
                    new CardAnticipation(), 
                    new CardEscapePod()
                    ],
            },
            ExeCardType = typeof(AngderEXE),
            BorderSprite = Angder_Character_Panel.Sprite,
            Description = AnyLocalizations.Bind(["character", "Angder", "description"]).Localize,

        });


        foreach (var cardType in AngderMod_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IAngderCard.Register))?.Invoke(null, [helper]);

        /* 2. ARTIFACTS */

        foreach (var artifactType in Angder_AllArtifact_Types)
            AccessTools.DeclaredMethod(artifactType, nameof(IAngderArtifact.Register))?.Invoke(null, [helper]);

        /* 4. STATUSES */

        Rampage = helper.Content.Statuses.RegisterStatus("Rampage", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Rampage.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Rampage", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Rampage", "description"]).Localize
        });

        Theft = helper.Content.Statuses.RegisterStatus("Theft", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Theft.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Theft", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Theft", "description"]).Localize
        });

        Fury = helper.Content.Statuses.RegisterStatus("Fury", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Fury.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Fury", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Fury", "description"]).Localize
        });

        Disrupt = helper.Content.Statuses.RegisterStatus("Disrupt", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Disrupt.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Disrupt", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Disrupt", "description"]).Localize
        });

        FuelSiphon = helper.Content.Statuses.RegisterStatus("FuelSiphon", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/FuelSiphon.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "FuelSiphon", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "FuelSiphon", "description"]).Localize
        });

        FuelDiscard = helper.Content.Statuses.RegisterStatus("FuelDump", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/FuelDump.png")).Sprite,
                color = new("b500be"),
                isGood = true

            },
            Name = AnyLocalizations.Bind(["status", "FuelDump", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "FuelDump", "description"]).Localize,
           

    });

        //Registering the trait sprites seperately for... some reason?

        RemoteControlSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Remotecontrol.png"));
        RemoteControlIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/RemotecontrolIcon.png"));

        /* Register trait. Making this work sucked! */
        RemoteControl = helper.Content.Cards.RegisterTrait("Remotecontrol", new()
        {
            Icon = (_, _) => RemoteControlSprite.Sprite,
            Name = AnyLocalizations.Bind(["trait", "Remotecontrol", "name"]).Localize,
            Tooltips = (_, _) => [
                new GlossaryTooltip($"cardtrait.{Package.Manifest.UniqueName}::Remotecontrol")
                    {
                        Icon = RemoteControlSprite.Sprite,
                        TitleColor = Colors.cardtrait,
                        Title = Localizations.Localize(["trait", "Remotecontrol", "name"]),
                        Description = Localizations.Localize(["trait", "Remotecontrol", "description"])
                    }
            ]
        });


        Angdermissing = Angderchar.MissingStatus;

        StunSmallIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/stunShipsmallIcon.png"));
        Angdermissingin = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Angdermissingin.png"));
        Angdermissingun = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Angdermissingun.png"));

        MoveenemyLeft = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/moveLeftEnemy.png"));
        MoveenemyRight = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/moveRightEnemy.png"));

        /* Cleave symbols */
        Cleaveshortleft = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/CleaveLeft.png"));

        Cleavelongleft = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/LongCleaveLeft.png"));
        Cleaveshortright = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/CleaveRight.png"));
        Cleavelongright = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/LongCleaveRight.png"));

        Overdriveno = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/overdriveNo.png"));
        
        
        /* RAM symbol */

        Ram = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Ram.png"));
        RamPierce = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/RamPierce.png"));
        MGPatches.Apply(Harmony);


        /* Check this out in Features/Rampage.cs */
        _ = new RampageManager();
        _ = new FuryManager();
        _ = new TheftManager();
        _ = new DisruptManager();
        _ = new FuelDumpManager();
        _ = new SiphonManager();

        _ = new CleaveManager();
        _ = new RemoteManager();
        /* */
        // icons_moveRightEnemyassign = Spr.icons_moveRightEnemy;
    }
}

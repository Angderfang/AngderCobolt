using Nickel;
using HarmonyLib;
using Nanoray.PluginManager;
using Angder.EchoesOfTheFuture.Cards;
using Angder.EchoesOfTheFuture.Artifacts.AngderArtifacts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Nickel.Common;
using Angder.EchoesOfTheFuture.Cards.Angdercards;
using System.Xml;
using Angder.EchoesOfTheFuture.Patches;
using Angder.EchoesOfTheFuture.Cards.Butlercards;
using Angder.EchoesOfTheFuture.Artifacts.ButlerArtifacts;
using Angder.EchoesOfTheFuture.Artifacts.GrunanArtifacts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Angder.EchoesOfTheFuture.Cards.Grunancards.Common;
using Angder.EchoesOfTheFuture.Features.Grunan;
using FMOD;

namespace Angder.EchoesOfTheFuture;

/* Icons for cleave provided by Soggoru Waffle! Thanks Waffle */
public sealed class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal IKokoroApi KokoroApi { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    internal readonly Harmony Harmony;

    #region AngdersBasicstuff
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


    //GRUNAN CARD ARTS

    internal ISpriteEntry Icebolt { get; }
    internal ISpriteEntry Fireball { get; }
    internal ISpriteEntry Candle { get; }
    internal ISpriteEntry Decay { get; }
    internal ISpriteEntry Bookscard { get; }
    internal ISpriteEntry RitualBase { get; }
    internal ISpriteEntry Eldrich { get; }

    //BUTLER Card arts
    internal ISpriteEntry Maid_Trashfire { get; }
    internal ISpriteEntry Maid_Dusting { get; }
    internal ISpriteEntry Maid_Littering { get; }
    internal ISpriteEntry Maid_Chute { get; }
    internal ISpriteEntry Maid_Awaken { get; }
    internal ISpriteEntry Maid_Origin { get; }
    internal ISpriteEntry Maid_Quickclean { get; }
    internal ISpriteEntry Maid_Reset { get; }
    internal ISpriteEntry Maid_Field { get; }
    internal ISpriteEntry Maid_Scrap { get; }
    internal ISpriteEntry Powerdiversion { get; }
    internal ISpriteEntry Replace { get; }
    internal ISpriteEntry ScarpCannon { get; }

    //ANGDER Card arts
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
    internal ISpriteEntry Angder_flare { get; }


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
    internal ISpriteEntry Overdriveno { get; }
    internal ISpriteEntry Angdermissingin { get; }
    internal ISpriteEntry Angdermissingun { get; } //YES I AM MISSING MY GUN. I USED TO FIRE 10 SHOTS A CARD DARN IT!
    //Ram
    internal ISpriteEntry Ram { get; }
    internal ISpriteEntry RamPierce { get; }

    //The decks
    internal IDeckEntry AngderDeck { get; }
    internal IDeckEntry AngderstrashDeck { get; }
    internal IPlayableCharacterEntryV2 Angderchar { get; }

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
         //typeof(CardRam),
         typeof(CardDeepraid),
         typeof(CardPlannedRaid),
         typeof(CardEnrage),
         typeof(CardDistractiongambit),
        //typeof(CardPowertransfer) //SYMBOL WON'T WORK
 /* Thats the rares done?*/
    ];

    internal static IReadOnlyList<Type> Angder_Trash_Types { get; } = [
     typeof(CardLootPowercore),
     typeof(CardStolenMunitions),
     typeof(CardCoolRocket),
     typeof(CardDiagnosticComplete),
     typeof(CardExposedport),
     typeof(CardAutoblastleft),
     typeof(CardHairTrigger),
     typeof(CardGarbage),
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
    internal static IReadOnlyList<Type> Angder_CommonArtifact_Types { get; } = [
        typeof(ChainAxe),
        typeof(HairTrigger),
        typeof(AggressiveSiphon)
    ];
    internal static IReadOnlyList<Type> Angder_BossArtifact_Types { get; } = [
        typeof(ShipsManifest),
        typeof(PersonalJetpack),
        //typeof(EnergySiphon),
        ];
    internal static IEnumerable<Type> Angder_AllArtifact_Types
        => Angder_CommonArtifact_Types.Concat(Angder_BossArtifact_Types);


    #endregion

    #region ButlerBasicStuff

    internal ISpriteEntry StunSmallIcon { get; }

    internal ISpriteEntry HandExhaustone { get; }
    internal ISpriteEntry DeckExhaustone { get; }
    internal ISpriteEntry DiscardExhaustone { get; }
    internal ISpriteEntry DrawExhaustone { get; }
    internal ISpriteEntry TrashbagMidrow { get; }
    internal ISpriteEntry TrashbagIcon { get; }

    internal ISpriteEntry Claw1 { get; }
    internal ISpriteEntry Claw2 { get; }

    internal ISpriteEntry ShieldIcon { get; }
    internal ISpriteEntry ShieldNormalShine { get; }
    internal ISpriteEntry ShieldNormal { get; }
    internal ISpriteEntry ShieldNormalCracked1 { get; }
    internal ISpriteEntry ShieldNormalCracked2 { get; }
    internal ISpriteEntry ShieldNormalCracked3 { get; }
    internal ISpriteEntry ShieldNormalCracked4 { get; }
    internal ISpriteEntry Butler_Character_CardBackground { get; }
    internal ISpriteEntry Butler_Trash_CardFrame { get; }
    internal ISpriteEntry Butler_Character_CardFrame { get; }
    internal ISpriteEntry Butler_Character_Panel { get; }
    internal ISpriteEntry Butler_Mini_0 { get; }

    internal ISpriteEntry ButlerAnger { get; }

    internal ISpriteEntry ButlerAngerMid { get; }

    internal ISpriteEntry ButlerAngerBright { get; }
    internal ISpriteEntry ButlerNeutral { get; }

    internal ISpriteEntry ButlerNeutralMid { get; }

    internal ISpriteEntry ButlerNeutralBright { get; }
    internal ISpriteEntry ButlerSad { get; }
    internal ISpriteEntry ButlerSquint { get; }

    internal ISpriteEntry ButlerSquintMid { get; }

    internal ISpriteEntry ButlerSquintBright { get; }


    internal ISpriteEntry ButlerGlitch { get; }
    internal ISpriteEntry ButlerGlitch2 { get; }
    internal ISpriteEntry ButlerGlitch3 { get; }
    internal ISpriteEntry ButlerGlitch4 { get; }
    internal IDeckEntry ButlerDeck { get; }
    internal IDeckEntry ButlerstrashDeck { get; }
    internal ICharacterEntryV2 Butlerchar { get; }

    //internal ISpriteEntry Warmodesprite { get; }
    internal ISpriteEntry Malfunctionin { get; }
    internal ISpriteEntry Malfunctionout { get; }
    
    internal IStatusEntry Warmode { get; }
    internal IStatusEntry Disposalprocess { get; }
    //internal ISpriteEntry Malfunctionsprite { get; }
    internal IStatusEntry Exhaustover10 { get; }
    internal IStatusEntry Exhaustover5 { get; }
    internal IStatusEntry Exhaustover3 { get; }
    internal IStatusEntry Exhaustover6 { get; }
    internal static IReadOnlyList<Type> Butler_StarterCard_Types { get; } = [
        typeof(CardQuickClean),
        typeof(CardCleave),
        typeof(CardBusywork),
        typeof(CardScrapCannon),
    ];
    internal static IReadOnlyList<Type> Butler_CommonCard_Types { get; } = [
         typeof(CardShiftingShot),
        typeof(CardCodepurge),
        typeof(CardReplace),
        typeof(CardResetShields),
        typeof(CardAlightdusting),
        //typeof(CardShieldtest),
    ];

    /* common cards */
    internal static IReadOnlyList<Type> Butler_UnCommonCard_Types { get; } = [
        typeof(CardAmmoDump),
        typeof(CardOrginization),
        typeof(CardFieldOfFumes),
        typeof(CardDisposal),
        typeof(CardBioEngine),
        typeof(CardLittering),
        typeof(CardDisposalChute),
         /* 
          */
    ];
    internal static IReadOnlyList<Type> Butler_RareCard_Types { get; } = [
        typeof(CardSystemCleanout),
        typeof(CardShatterTheSky),
        typeof(CardOldCode),
        typeof(CardOldestCode),
        typeof(CardPlayExhaust),
    ];
    internal static IReadOnlyList<Type> Butler_Trash_Types { get; } = [
    ];

    internal static IEnumerable<Type> Butler_AllCard_Types
    => Butler_StarterCard_Types
    .Concat(Butler_CommonCard_Types)
    .Concat(Butler_UnCommonCard_Types)
    .Concat(Butler_RareCard_Types)
    .Concat(Butler_Trash_Types);
    //.Concat(Butler_EXE_Types);

    internal static IReadOnlyList<Type> Butler_CommonArtifact_Types { get; } = [
        typeof(LeakyPipe),
        typeof(FreeWill),
        typeof(Accellerant),
];
    internal static IReadOnlyList<Type> Butler_BossArtifact_Types { get; } = [
        typeof(ScrapArm),
        typeof(VacuumCleaner),
        //typeof(EnergySiphon),
        ];
    internal static IEnumerable<Type> Butler_AllArtifact_Types
        => Butler_CommonArtifact_Types.Concat(Butler_BossArtifact_Types);

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DroidNames
    {
        D26,
        Dan,
        Dorithy,
        spike
    }

    #endregion

    #region GrunanBasicStuff
    internal ISpriteEntry Grunan_CardFrame { get; }
    internal ISpriteEntry Grunan_Character_CardBackground { get; }
    internal ISpriteEntry Grunan_Character_Panel { get; }
    internal ISpriteEntry GrunanSquint { get; }
    internal ISpriteEntry GrunanSquinttalk { get; }
    internal ISpriteEntry GrunanNeutral { get; }
    internal ISpriteEntry GrunanMini { get; }
    internal ISpriteEntry GrunanNeutralTalk { get; }
    internal ISpriteEntry GrunanNeutralTalk2 { get; }

    internal ISpriteEntry Grunanhand { get; }
    internal ISpriteEntry Grunanhandtalk { get; }
    internal ISpriteEntry GrunanBook { get; }
    internal ISpriteEntry GrunanBookTalk { get; }
    internal ISpriteEntry GrunanFacepalm { get; }
    internal ISpriteEntry GrunanFacepalmTalk { get; }
    internal ISpriteEntry GrunanSmile { get; }
    internal ISpriteEntry GrunanSmileTalk { get; }
    internal ISpriteEntry GrunanFlipped { get; }
    internal ISpriteEntry GrunanFlippedTalk { get; }
    internal ISpriteEntry GrunanFlippedSmile { get; }
    internal ISpriteEntry GrunanPanic { get; }
    internal ISpriteEntry GrunanPanicTalk { get; }
    internal ISpriteEntry GrunanFlippedPain { get; }
    internal ISpriteEntry GrunanFlippedPainTalk { get; }
    internal ISpriteEntry GrunanFlippedRelax { get; }
    internal ISpriteEntry GrunanFlippedRelaxTalk { get; }

    internal ISpriteEntry GrunanFlippedAnnoyed { get; }
    internal ISpriteEntry GrunanFlippedAnnoyedTalk { get; }
    //traitstuff
    internal ICardTraitEntry Consuming { get; private set; } = null!;
    internal ISpriteEntry ConsumingSprite { get; private set; } = null!;
    internal ISpriteEntry ConsumingIcon { get; private set; } = null!;


    internal ICardTraitEntry Untrashable { get; private set; } = null!;
    internal ISpriteEntry UntrashableSprite { get; private set; } = null!;
    internal ISpriteEntry UntrashableIcon { get; private set; } = null!;

    internal ISpriteEntry ShatterIcon { get; }


    //Stutus stuff

    //internal ISpriteEntry MemoryIcon { get; }
    internal IStatusEntry Memory { get; }
    internal IStatusEntry Voidsight { get; }

    internal IStatusEntry EldrichAttention { get; }

    internal IDeckEntry GrunanDeck { get; }
    internal ICharacterEntryV2 Grunanchar { get; }

    internal static IReadOnlyList<Type> Grunan_StarterCard_Types { get; } = [
        typeof(EndlessResearch),
        typeof(Fireball),
    ];
    
    internal static IReadOnlyList<Type> Grunan_CommonCard_Types { get; } = [
        typeof(Empower),
        typeof(CardMagicShield),
        typeof(Greed),
        typeof(Trails),
        typeof(Sigil),
        typeof(Shatter),
        typeof(Salve)
    ];

    /* common cards */
    
    internal static IReadOnlyList<Type> Grunan_UnCommonCard_Types { get; } = [
        typeof(Teleport),
        typeof(Flow),
        typeof(Iceblast),
        typeof(SpellBook),
        typeof(Ritual),
        typeof(RitualDec),
        typeof(CardMagicMissile),
    ];
    internal static IReadOnlyList<Type> Grunan_RareCard_Types { get; } = [
        typeof(CardMemory),
        typeof(ConsumeSoul),
        typeof(Bloodritual),
        typeof(GlimpseTheVoid),
        typeof(Necromancy),
    ];
    internal static IEnumerable<Type> Grunan_AllCard_Types
    => Grunan_StarterCard_Types
    .Concat(Grunan_CommonCard_Types)
    .Concat(Grunan_UnCommonCard_Types)
    .Concat(Grunan_RareCard_Types);
    //.Concat(Grunan_EXE_Types);

    internal static IReadOnlyList<Type> Grunan_CommonArtifact_Types { get; } = [
        typeof(Ritualofair),
        typeof(WitchesWeekly),
        typeof(BurningEffigy),
];
    internal static IReadOnlyList<Type> Grunan_BossArtifact_Types { get; } = [

        typeof(BookofReading),
        typeof(Darkness),
        ];
    internal static IEnumerable<Type> Grunan_AllArtifact_Types
        => Grunan_CommonArtifact_Types.Concat(Grunan_BossArtifact_Types);

    #endregion

    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;
        Harmony = new(package.Manifest.UniqueName);
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
        _ = new CardBrowseFilterManager();

        AnyLocalizations = new JsonLocalizationProvider(
        tokenExtractor: new SimpleLocalizationTokenExtractor(),
        localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );

        #region AngderStuff
        /* These localizations lists help us organize our mod's text and messages by language.
         * For general use, prefer AnyLocalizations, as that will provide an easier time to potential localization submods that are made for your mod 
         * IMPORTANT: These localizations are found in the i18n folder (short for internationalization). The Demo Mod comes with a barebones en.json localization file that you might want to check out before continuing 
         * Whenever you add a card, artifact, character, ship, pretty much whatever, you will want to update your locale file in i18n with the necessary information
         * Example: You added your own character, you will want to create an appropiate entry in the i18n file. 
         * If you would rather use simple strings whenever possible, that's also an option -you do you. */

        //Keeping this here; no way I would remember how this works.

            //Oh hey, I found his face.
            Angder_Trash_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_Trashcardframe.png"));
            Angder_Character_CardBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_cardbackground.png"));
            Angder_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_cardframe.png"));
            Angder_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Angder_character_panel.png"));
            //Base an nuetral

            Angder_Mini_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/ANgder4/Angder_character_mini_0.png"));
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
        Angder_flare = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Flare.png"));

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
                    new CardBoard(),
                    new CardEscapePod(),
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

        helper.Content.Characters.V2.RegisterCharacterAnimation("Angderneutral", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",

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
        helper.Content.Characters.V2.RegisterCharacterAnimation("Angdermini", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
            LoopTag = "mini",
                Frames = new[]
                {
                /* Mini only needs one sprite. We call it animation just because we add it the same way as other expressions. */
                Angder_Mini_0.Sprite
            }
            });

        helper.Content.Characters.V2.RegisterCharacterAnimation("Angdersad", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
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

        helper.Content.Characters.V2.RegisterCharacterAnimation("Angdersquint", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
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
        helper.Content.Characters.V2.RegisterCharacterAnimation("Angdertalk", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
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
            helper.Content.Characters.V2.RegisterCharacterAnimation("Angdernervous", new CharacterAnimationConfigurationV2()
            {
                CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
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
            helper.Content.Characters.V2.RegisterCharacterAnimation("Angdergrumpy", new CharacterAnimationConfigurationV2()
            {
                CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
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

            helper.Content.Characters.V2.RegisterCharacterAnimation("Angderserious", new CharacterAnimationConfigurationV2()
            {
                CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
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

            helper.Content.Characters.V2.RegisterCharacterAnimation("Angdersmug", new CharacterAnimationConfigurationV2()
            {
                CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
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

            helper.Content.Characters.V2.RegisterCharacterAnimation("AngderGameover", new CharacterAnimationConfigurationV2()
            {
                CharacterType = "Angder.EchoesOfTheFuture::AngderDeck",
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


            Angderchar = helper.Content.Characters.V2.RegisterPlayableCharacter("AngderDeck", new PlayableCharacterConfigurationV2()
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
            });;
        //Console.WriteLine("USE THIS:" + EnumExtensions.Key(AngderDeck.Deck));

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

        #endregion
        #region butlerstuff

        Maid_Scrap = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/scrapCannon.png"));
        Maid_Trashfire = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Trashfire.png"));
        Maid_Littering = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Littering.png"));
        Maid_Dusting = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Lightdusting.png"));
        Maid_Chute = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Chute.png"));
        Maid_Origin = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/ErrorOrigin.png"));
        Maid_Awaken = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/ErrorAwaken.png"));
        Maid_Quickclean = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Quickclean.png"));
        Maid_Reset = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Reset.png"));
        Maid_Field = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Interferencefield.png"));
        Powerdiversion = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Powerdiversion.png"));
        Replace = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Powerdiversion.png"));
        ScarpCannon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Maid/Scrapcannon2.png"));

        HandExhaustone = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/ExhaustFromhand.png"));
        DeckExhaustone = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/ExhaustDraw.png"));
        DiscardExhaustone = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/ExhaustDiscard.png"));
        DrawExhaustone = DeckExhaustone;

        ShieldIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/FlyingShield.png"));
        ShieldNormalShine = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Midrow/ShieldShine.png"));
        ShieldNormal = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Midrow/ShieldFull.png"));
        ShieldNormalCracked1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Midrow/ShieldCracked.png"));
        ShieldNormalCracked2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Midrow/ShieldCrackedTwo.png"));
        ShieldNormalCracked3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Midrow/ShieldCrackedThree.png"));
        ShieldNormalCracked4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Midrow/ShieldCrackedAlot.png"));

        TrashbagMidrow = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Midrow/trashbag.png"));
        TrashbagIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/trashbag.png"));

        //No, I am not calling it "buttStuff" I am a professional.
        Butler_Trash_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler_character_Trashcardframe.png"));
            Butler_Character_CardBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler_character_cardbackground.png"));
            Butler_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler_character_cardframe.png"));
            Butler_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler_character_panel.png"));
            Butler_Mini_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/Butler_character_mini_0.png"));

        ButlerNeutral = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidNeutral.png"));
        ButlerNeutralMid = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidMid.png"));
        ButlerNeutralBright = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidBright.png"));
        ButlerSquint = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidSquint.png"));
        ButlerSquintMid = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidSquintMid.png"));
        ButlerSquintBright = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidSquintBright.png"));
        ButlerSad = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidSad.png"));

        ButlerAnger = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidAnger.png"));
        ButlerAngerMid = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidAngerMid.png"));
        ButlerAngerBright = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/MaidAngerBright.png"));
        //Malfunctionsprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Malfunction.png"));
        ButlerGlitch = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/Glitch.png"));
        ButlerGlitch2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/Glitch2.png"));
        ButlerGlitch3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/Glitch3.png"));
        ButlerGlitch4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Butler/Glitch4.png"));

        //claw images

        Claw1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/SalvageArm.png"));
        Claw2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/artifacts/Brokenclaw.png"));


        ButlerDeck = helper.Content.Decks.RegisterDeck("ButlerDeck", new DeckConfiguration()
            {
                Definition = new DeckDef()
                {
                    color = new Color("808080"),
                    titleColor = new Color("000000")
                },
                
                DefaultCardArt = Butler_Character_CardBackground.Sprite,
                BorderSprite = Butler_Character_CardFrame.Sprite,
                Name = this.AnyLocalizations.Bind(["character", "Butler", "name"]).Localize,
            });
        ButlerstrashDeck = helper.Content.Decks.RegisterDeck("Computermalfunctions", new DeckConfiguration()
            {
                Definition = new DeckDef()
                {
                    color = new Color("808080"),
                    titleColor = new Color("000000")
                },
                /* We give it a default art and border some Sprite types by adding '.Sprite' at the end of the ISpriteEntry definitions we made above. */
                DefaultCardArt = Butler_Character_CardBackground.Sprite,
                BorderSprite = Butler_Trash_CardFrame.Sprite,

                Name = this.AnyLocalizations.Bind(["character", "Computermalfunctions", "Trash"]).Localize,
            });

        ACombatPatches.Apply();
        helper.ModRegistry.GetApi<IMoreDifficultiesApi>("TheJazMaster.MoreDifficulties", new SemanticVersion(1, 3, 0))?.RegisterAltStarters(
            deck: ButlerDeck.Deck,
            starterDeck: new StarterDeck
            {
                cards = [
                    new CardCodepurge(),
                    new CardCleave(),
                    ]
            }
        );
        helper.Content.Characters.V2.RegisterCharacterAnimation("Butlergameover", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::ButlerDeck",
            LoopTag = "gameover",
            Frames = new[]
            {
                ButlerSad.Sprite,
                ButlerSad.Sprite,
                ButlerSad.Sprite,
                ButlerSad.Sprite,
                ButlerSad.Sprite,
            }
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation("Butleranger", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::ButlerDeck",
            LoopTag = "anger",
            Frames = new[]
            {
                ButlerAnger.Sprite,
                ButlerAngerMid.Sprite,
                ButlerAngerBright.Sprite,
                ButlerAngerMid.Sprite,
                ButlerAnger.Sprite,
            }
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation("Butlersquint", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::ButlerDeck",
            LoopTag = "squint",
            Frames = new[]
            {
                ButlerSquint.Sprite,
                ButlerSquintMid.Sprite,
                ButlerSquint.Sprite,
                ButlerSquintBright.Sprite,
                ButlerSquint.Sprite,
            }
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation("Butlerglitch", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::ButlerDeck",
            LoopTag = "glitch",
            Frames = new[]
            {
                ButlerNeutral.Sprite,
                ButlerGlitch.Sprite,
                ButlerGlitch2.Sprite,
                ButlerGlitch4.Sprite,
                ButlerGlitch3.Sprite,
                ButlerGlitch.Sprite,
                ButlerGlitch4.Sprite,
                ButlerGlitch3.Sprite,
                ButlerGlitch2.Sprite,
                ButlerGlitch.Sprite,
                ButlerGlitch3.Sprite,
                ButlerGlitch2.Sprite,
                ButlerGlitch4.Sprite,
                ButlerNeutral.Sprite,
            }
        });
        Butlerchar = helper.Content.Characters.V2.RegisterPlayableCharacter("Butler", new PlayableCharacterConfigurationV2()
                {
                Deck = ButlerDeck.Deck,
                Starters = new StarterDeck
                {
                    cards = [new CardScrapCannon(),
                    new CardQuickClean()
                        ],
                }, 
                //ExeCardType = typeof(AngderEXE),
                BorderSprite = Butler_Character_Panel.Sprite,
                Description = AnyLocalizations.Bind(["character", "Butler", "description"]).Localize,
                NeutralAnimation = new CharacterAnimationConfigurationV2()
                {
                    CharacterType = "Angder.EchoesOfTheFuture::ButlerDeck",
                    LoopTag = "neutral",
                    Frames = new[]
                    {
                        ButlerNeutral.Sprite,
                        ButlerNeutralMid.Sprite,
                        ButlerNeutralBright.Sprite,
                        ButlerNeutralMid.Sprite,
                        ButlerNeutral.Sprite
                    }
                },
                MiniAnimation = new CharacterAnimationConfigurationV2()
                {
                    CharacterType = "Angder.EchoesOfTheFuture::ButlerDeck",
                    LoopTag = "mini",

                    Frames = new[]
                    {
                        Butler_Mini_0.Sprite,
                    }
                }
        });
        //Console.WriteLine(EnumExtensions.Key(ButlerDeck.Deck));
        foreach (var cardType in Butler_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IAngderCard.Register))?.Invoke(null, [helper]);

        Disposalprocess = helper.Content.Statuses.RegisterStatus("DisposalProcess", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Malfunction.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "DisposalProcess", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "DisposalProcess", "description"]).Localize
        });

        Warmode = helper.Content.Statuses.RegisterStatus("Warmode", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Warmode.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Warmode", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Warmode", "description"]).Localize
        });


        foreach (var artifactType in Butler_AllArtifact_Types)
            AccessTools.DeclaredMethod(artifactType, nameof(IAngderArtifact.Register))?.Invoke(null, [helper]);

        //Both fake Statuses, purely for X hints
        Exhaustover10 = helper.Content.Statuses.RegisterStatus("Exhaustover10", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Exhaustover10.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Exhaustover10", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Exhaustover10", "description"]).Localize
        });
        Exhaustover5 = helper.Content.Statuses.RegisterStatus("Exhaustover5", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Exhaustover5.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Exhaustover5", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Exhaustover5", "description"]).Localize
        });
        Exhaustover3 = helper.Content.Statuses.RegisterStatus("Exhaustover3", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Exhaustover3.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Exhaustover3", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Exhaustover3", "description"]).Localize
        });
        Exhaustover6 = helper.Content.Statuses.RegisterStatus("Exhaustover6", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Exhaustover6.png")).Sprite,
                color = new("b500be"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Exhaustover6", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Exhaustover6", "description"]).Localize
        });
        #endregion
        
        #region GrunanStuff

        Grunan_Character_CardBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan_character_cardbackground.png"));
        Grunan_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan_character_cardframe.png"));
        Grunan_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan_character_panel.png"));

        Icebolt = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Grunan/Iceball.png"));
        Fireball = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Grunan/Fireball.png"));
        Candle = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Grunan/Ritualfire.png"));
        Decay = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Grunan/Decay.png"));
        RitualBase = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Grunan/Ritualbase.png"));
        Bookscard = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Grunan/Books.png"));
        Eldrich = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/CardArt/Grunan/Eldrich.png"));

        GrunanMini = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_character_mini_0.png"));
        GrunanNeutral = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Neutral.png"));
        GrunanNeutralTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Neutral_talk.png"));
        GrunanNeutralTalk2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Neutral_talk2.png"));
        GrunanSquint = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Squint.png"));
        GrunanSquinttalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Squint_talk.png"));
        GrunanSmile = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Smile.png"));
        GrunanSmileTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Smiletalk.png"));
        GrunanBook = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/GrunanBook.png"));
        GrunanBookTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/GrunanBookbob.png"));
        Grunanhand = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/GrunanHand.png"));
        Grunanhandtalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/GrunanHandtalk.png"));
        GrunanFacepalm = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Facepalm.png"));
        GrunanFacepalmTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Facepalm_talk.png"));
        GrunanFlipped = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped.png"));
        GrunanFlippedTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped_talk.png"));
        GrunanFlippedSmile = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped_smile.png"));
        GrunanPanic = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Panic.png"));
        GrunanPanicTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Panic_talk.png"));

        GrunanFlippedPain = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped_Pain.png"));
        GrunanFlippedPainTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped_Pain_talk.png"));

        GrunanFlippedRelax = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped_relaxed.png"));
        GrunanFlippedRelaxTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped_relaxed_talk.png"));

        GrunanFlippedAnnoyed = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped_annoyed.png"));
        GrunanFlippedAnnoyedTalk = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Grunan/Grunan_Flipped_annoyed_talk.png"));



        GrunanDeck = helper.Content.Decks.RegisterDeck("GrunanDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("597D35"),
                titleColor = new Color("000000")
            },
            
            DefaultCardArt = RitualBase.Sprite,
            BorderSprite = Grunan_CardFrame.Sprite,
            Name = this.AnyLocalizations.Bind(["character", "Grunan", "name"]).Localize,
        });
        
        helper.ModRegistry.GetApi<IMoreDifficultiesApi>("TheJazMaster.MoreDifficulties", new SemanticVersion(1, 3, 0))?.RegisterAltStarters(
            deck: GrunanDeck.Deck,
            starterDeck: new StarterDeck
            {
                cards = [
                    new CardMagicShield(),
                    new Bloodritual(),
                ]
            }
        );
        
        

        helper.Content.Characters.V2.RegisterCharacterAnimation("Grunanmini", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "mini",
            Frames = new[]
        {
                /* Mini only needs one sprite. We call it animation just because we add it the same way as other expressions. */
                GrunanMini.Sprite
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("GrunanNeutral", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "neutral",
            Frames = new[]
            {
                GrunanNeutral.Sprite,
                GrunanNeutralTalk.Sprite,
                GrunanNeutral.Sprite,
                GrunanNeutralTalk.Sprite,
                GrunanNeutral.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("GrunanFlippedAnnoyed", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "flippedannoyed",
            Frames = new[]
            {
                GrunanFlippedAnnoyed.Sprite,
                GrunanFlippedAnnoyedTalk.Sprite,
                GrunanFlippedAnnoyed.Sprite,
                GrunanFlippedAnnoyedTalk.Sprite,
                GrunanFlippedAnnoyed.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("Grunanbook", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "book",
            Frames = new[]
            {
                GrunanBook.Sprite,
                GrunanBookTalk.Sprite,
                GrunanBook.Sprite,
                GrunanBookTalk.Sprite,
                GrunanBook.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("GrunanGameover", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "gameover",
            Frames = new[]
    {
                GrunanFacepalm.Sprite,
                GrunanFacepalmTalk.Sprite,
                GrunanFacepalm.Sprite,
                GrunanFacepalmTalk.Sprite,
                GrunanFacepalm.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("Grunanfacepalm", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "facepalm",
            Frames = new[]
            {
                GrunanFacepalm.Sprite,
                GrunanFacepalmTalk.Sprite,
                GrunanFacepalm.Sprite,
                GrunanFacepalmTalk.Sprite,
                GrunanFacepalm.Sprite,
            }
        });
        
        helper.Content.Characters.V2.RegisterCharacterAnimation("Grunansquint", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "squint",
            Frames = new[]
                {
                GrunanSquint.Sprite,
                GrunanSquinttalk.Sprite,
                GrunanSquint.Sprite,
                GrunanSquinttalk.Sprite,
                GrunanSquint.Sprite,
                GrunanSquinttalk.Sprite,
                GrunanSquint.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("GrunanFlippedpain", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "Flippedpain",
            Frames = new[]
            {
                GrunanFlippedPain.Sprite,
                GrunanFlippedPainTalk.Sprite,
                GrunanFlippedPain.Sprite,
                GrunanFlippedPainTalk.Sprite,
                GrunanFlippedPain.Sprite,

            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("GrunanFlippedRelax", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "Flippedrelax",
            Frames = new[]
    {
                GrunanFlippedRelax.Sprite,
                GrunanFlippedRelaxTalk.Sprite,
                GrunanFlippedRelax.Sprite,
                GrunanFlippedRelaxTalk.Sprite,
                GrunanFlippedRelax.Sprite,

            }
        });


        helper.Content.Characters.V2.RegisterCharacterAnimation("Grunanflipped", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "flipped",
            Frames = new[]
            {
                GrunanFlipped.Sprite,
                GrunanFlippedTalk.Sprite,
                GrunanFlipped.Sprite,
                GrunanFlippedTalk.Sprite,
                GrunanFlipped.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("Grunanflippedsmile", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "flippedsmile",
            Frames = new[]
            {
                GrunanFlippedSmile.Sprite,
                GrunanFlippedTalk.Sprite,
                GrunanFlippedSmile.Sprite,
                GrunanFlippedTalk.Sprite,
                GrunanFlippedSmile.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("Grunanhand", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "hand",
            Frames = new[]
            {
                Grunanhand.Sprite,
                Grunanhandtalk.Sprite,
                Grunanhand.Sprite,
                Grunanhandtalk.Sprite,
                Grunanhand.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation("Grunanpanic", new CharacterAnimationConfigurationV2()
        {
            CharacterType = "Angder.EchoesOfTheFuture::GrunanDeck",
            LoopTag = "Panic",
            Frames = new[]
            {
                GrunanPanic.Sprite,
                GrunanPanicTalk.Sprite,
                GrunanPanic.Sprite,
                GrunanPanicTalk.Sprite,
                GrunanPanic.Sprite,
            }
        });
        
        Grunanchar = helper.Content.Characters.V2.RegisterPlayableCharacter("Grunan", new PlayableCharacterConfigurationV2()
        {
            Deck = GrunanDeck.Deck,
            Starters = new StarterDeck
            {
                cards = [new EndlessResearch(),
                    new Fireball{},
                    ],
            },
            //ExeCardType = typeof(AngderEXE),
            BorderSprite = Grunan_Character_Panel.Sprite,
            Description = AnyLocalizations.Bind(["character", "Grunan", "description"]).Localize,
        }); ;

        foreach (var cardType in Grunan_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IAngderCard.Register))?.Invoke(null, [helper]);


        foreach (var artifactType in Grunan_AllArtifact_Types)
            AccessTools.DeclaredMethod(artifactType, nameof(IAngderArtifact.Register))?.Invoke(null, [helper]);


        UntrashableSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Untrashable.png"));
        UntrashableIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/UntrashableIcon.png"));
        /* Register trait. Making this work sucked! */
        Untrashable = helper.Content.Cards.RegisterTrait("Untrashable", new()
        {
            Icon = (_, _) => UntrashableSprite.Sprite,
            Name = AnyLocalizations.Bind(["trait", "Untrashable", "name"]).Localize,
            Tooltips = (_, _) => [
                new GlossaryTooltip($"cardtrait.{Package.Manifest.UniqueName}::Untrashable")
                    {
                        Icon = UntrashableSprite.Sprite,
                        TitleColor = Colors.cardtrait,
                        Title = Localizations.Localize(["trait", "Untrashable", "name"]),
                        Description = Localizations.Localize(["trait", "Untrashable", "description"])
                    }
            ]
        });


        //NOTE: CONSUMING IS MISNAMED: IT'S ACTUALLY "ABYSSAL".
        ConsumingSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Consuming.png"));
        ConsumingIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/ConsumingIcon.png"));
        /* Register trait. Making this work sucked! */
        Consuming = helper.Content.Cards.RegisterTrait("Consuming", new()
        {
            Icon = (_, _) => ConsumingSprite.Sprite,
            Name = AnyLocalizations.Bind(["trait", "Consuming", "name"]).Localize,
            Tooltips = (_, _) => [
                new GlossaryTooltip($"cardtrait.{Package.Manifest.UniqueName}::Consuming")
                    {
                        Icon = ConsumingSprite.Sprite,
                        TitleColor = Colors.cardtrait,
                        Title = Localizations.Localize(["trait", "Consuming", "name"]),
                        Description = Localizations.Localize(["trait", "Consuming", "description"])
                    }
            ]
        });


        //memory

        Memory = helper.Content.Statuses.RegisterStatus("Memory", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Memory.png")).Sprite,
                color = new("06402B"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Memory", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Memory", "description"]).Localize
        });

        Voidsight = helper.Content.Statuses.RegisterStatus("Voidsight", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Voidsight.png")).Sprite,
                color = new("06402B"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Voidsight", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Voidsight", "description"]).Localize,
        });
        EldrichAttention = helper.Content.Statuses.RegisterStatus("EldrichAttention", new()
        {
            Definition = new()
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/EldrichAttention.png")).Sprite,
                color = new("06402B"),
                isGood = true,
            },
            Name = AnyLocalizations.Bind(["status", "EldrichAttention", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "EldrichAttention", "description"]).Localize,
            
            });



        #endregion

        StunSmallIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/stunShipsmallIcon.png"));
        Angdermissingin = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Angdermissingin.png"));
        Angdermissingun = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/Angdermissingun.png"));
        Malfunctionin = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/MalfunctioningIN.png"));
        Malfunctionout = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/MalfunctioningOUT.png"));

        ShatterIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/shatter.png"));
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
        _ = new WrittenManager();

        _ = new CleaveManager();
        _ = new RemoteManager();
        _ = new VoidManager();
        /* */
        // icons_moveRightEnemyassign = Spr.icons_moveRightEnemy;
    }
}

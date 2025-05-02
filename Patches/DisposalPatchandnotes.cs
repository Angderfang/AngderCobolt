using Angder.EchoesOfTheFuture.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Angder.EchoesOfTheFuture.Patches;

public class ACombatPatches
    {
        static ModEntry Instance => ModEntry.Instance;
        static Harmony Harmony => Instance.Harmony;
        public static void Apply()
        {
        
            Harmony.Patch(
                //logger: ModEntry.Instance.Logger,
                original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.TryPlayCard)),
                prefix: new HarmonyMethod(typeof(ACombatPatches), nameof(Combat_TryPlayCard_Prefix)),
                postfix: new HarmonyMethod(typeof(ACombatPatches), nameof(Combat_TryPlayCard_Postfix))
            );
        }
        
        private static bool Combat_TryPlayCard_Prefix(Combat __instance, State s, Card card, ref bool __state, bool playNoMatterWhatForFree = false, bool exhaustNoMatterWhat = false)
        {
        int num = ((!playNoMatterWhatForFree) ? card.GetCurrentCost(s) : 0);
        if (num <= __instance.energy && __instance.PlayerCanAct(s) == true)
        {
            __state = false;
        }
        else
        {
            __state = true;
        }
        return true;
        } 

        private static void Combat_TryPlayCard_Postfix(Combat __instance, State s, Card card, bool __state, bool playNoMatterWhatForFree = false, bool exhaustNoMatterWhat = false)
        {
        //Console.WriteLine(__state);
        Status Disposalprocess = Instance.Disposalprocess.Status;
        Status warmode = Instance.Warmode.Status;
        Status Written = Instance.Memory.Status;
        Ship ship = s.ship;
        CardMeta Metacards = card.GetMeta();
        CardData Datacards = card.GetData(s);
        if (Metacards.deck == Deck.trash && __state != true && Datacards.unplayable != true)
            {
            if (ship.Get(Disposalprocess) > 0)
                {
                __instance.QueueImmediate(new ADrawCard
                    {
                    count = ship.Get(Disposalprocess),
                    });
                }
            if (ship.Get(warmode) > 0)
            {
                __instance.QueueImmediate(new AAttack
                {
                    damage = card.GetDmg(s, ship.Get(warmode)),
                });
            }
        }

            //WELCOME TO JANK; POPULATION GRUNAN
            if (__state != true && ship.Get(Written) > 0 && Datacards.unplayable != true)
            {

                if (__state != true && GrunanTraitManager.IsUntrashable(card, s) == true)
                {
                    __instance.QueueImmediate(new AStatus
                    {
                        status = ModEntry.Instance.Memory.Status,
                        statusAmount = -1,
                        targetPlayer = true
                    });
                }
                __instance.Queue(new Refreshnotes { });


                /*
                if (ship.Get(Written) < 1)
                {

                foreach (Card item in __instance.discard)
                {
                    if (GrunanTraitManager.IsUntrashable(item, s) == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, null, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
                        GrunanTraitManager.SetUntrashable(item, s, false);
                    }
                }
                foreach (Card item in s.deck)
                {
                    if (GrunanTraitManager.IsUntrashable(item, s) == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, null, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
                        GrunanTraitManager.SetUntrashable(item, s, false);
                    }
                }
                foreach (Card item in __instance.hand)
                {
                    if (GrunanTraitManager.IsUntrashable(item, s) == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, null, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
                        GrunanTraitManager.SetUntrashable(item, s, false);
                    }
                }

                }

                if (s.ship.Get(Written) > 0)
                {
                foreach (Card item in s.deck)
                {
                    //Console.WriteLine("deck");
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, false, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
                        GrunanTraitManager.SetUntrashable(item, s, true);
                    }

                }

                foreach (Card item in __instance.hand)
                {
                    //Console.WriteLine("Hand");
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, false, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
                        GrunanTraitManager.SetUntrashable(item, s, true);
                    }

                }
                foreach (Card item in __instance.discard)
                {
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, false, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
                        GrunanTraitManager.SetUntrashable(item, s, true);
                    }

                }
                foreach (Card item in __instance.exhausted)
                {
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, false, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
                        GrunanTraitManager.SetUntrashable(item, s, true);
                    }

                }
                
            }
            */



        }
    }
}

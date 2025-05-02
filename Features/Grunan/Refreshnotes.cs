using FSPRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features
{


    public class Refreshnotes : CardAction
    {
        static ModEntry Instance => ModEntry.Instance;
        public override void Begin(G g, State s, Combat c)
        {
            //Console.WriteLine("Refreshing");
            Status Written = Instance.Memory.Status;
            //Ship ship = s.ship;
            //CardMeta Metacards = card.GetMeta();
            //CardData Datacards = card.GetData(s);

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
                
                foreach (Card item in c.hand)
                {
                    //Console.WriteLine("Hand");
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, false, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
                        GrunanTraitManager.SetUntrashable(item, s, true);
                    }

                }
                foreach (Card item in c.discard)
                {
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, false, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
                        GrunanTraitManager.SetUntrashable(item, s, true);
                    }

                }
                foreach (Card item in c.exhausted)
                {
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, false, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, true, false);
                        GrunanTraitManager.SetUntrashable(item, s, true);
                    }

                }
                
            }
            if (s.ship.Get(Written) < 1 || s.ship.Get(Written) == 0)
            {
                foreach (Card item in c.discard)
                {
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, null, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
                        GrunanTraitManager.SetUntrashable(item, s, false);
                    }
                }
                foreach (Card item in s.deck)
                {
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, null, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
                        GrunanTraitManager.SetUntrashable(item, s, false);
                    }
                }
                foreach (Card item in c.hand)
                {
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, null, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
                        GrunanTraitManager.SetUntrashable(item, s, false);
                    }
                }
                foreach (Card item in c.exhausted)
                {
                    if (item.GetData(s).singleUse == true)
                    {
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.SingleUseCardTrait, null, false);
                        Instance.Helper.Content.Cards.SetCardTraitOverride(s, item, Instance.Helper.Content.Cards.ExhaustCardTrait, null, false);
                        GrunanTraitManager.SetUntrashable(item, s, false);
                    }
                }
            }
        }
    }
}

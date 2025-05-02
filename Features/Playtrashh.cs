using FSPRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features
{
    public class PlayTrash : CardAction
    {
        public override void Begin(G g, State s, Combat c)
        {
            if (selectedCard != null)
            {
                Card card = selectedCard;
                if (card != null)
                {
                    s.RemoveCardFromWhereverItIs(card.uuid);
                    c.SendCardToHand(s, card);
                    c.TryPlayCard(s, card, true);
                }
            }

        }

        public override string? GetCardSelectText(State s)
        {
            return Loc.T("action.ChooseCardToPutInHand.GetCardSelectText", "Pick a card. Put it in your hand.");
        }
    }
}

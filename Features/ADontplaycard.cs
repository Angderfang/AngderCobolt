using FSPRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features;
public class Dontplaycard : CardAction
{
    public override void Begin(G g, State s, Combat c)
        {
            //Card card = selectedCard;
            if (selectedCard != null)
            {
                Audio.Play(Event.CardHandling);
                selectedCard.ExhaustFX();
                s.RemoveCardFromWhereverItIs(selectedCard.uuid);
                c.SendCardToExhaust(s, selectedCard);
            }
        }
    public override string? GetCardSelectText(State s)
    {
        return "Choose a card. It will be exhausted.";
    }
}

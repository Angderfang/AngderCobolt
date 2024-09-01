namespace Angder.EchoesOfTheFuture
{
    public class ASelfExhaust : CardAction
    {
        public bool FX = true;
        public int CardID;
        public override void Begin(G g, State s, Combat c)
        {
            var card = s.FindCard(CardID);
            if (card != null)
            {
                if (FX == true)
                {
                    card.ExhaustFX();
                }
                s.RemoveCardFromWhereverItIs(CardID);
                c.SendCardToExhaust(s, card);
            }
        }

    }
}
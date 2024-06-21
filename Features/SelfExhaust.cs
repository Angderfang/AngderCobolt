namespace Angder.Angdermod
{
    public class ASelfExhaust : CardAction
    {
        public int CardID;
        public override void Begin(G g, State s, Combat c)
        {
            var card = s.FindCard(CardID);
            if (card != null && card.exhaustOverrideIsPermanent == false)
            {
                card.ExhaustFX();
                s.RemoveCardFromWhereverItIs(CardID);
                c.SendCardToExhaust(s, card);
            }
        }

    }
}
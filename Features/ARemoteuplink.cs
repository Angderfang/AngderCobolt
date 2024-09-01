
namespace Angder.EchoesOfTheFuture;
public class ARemoteuplink : CardAction
{
    public int count = 1;
    public int CardID;
    public override void Begin(G g, State s, Combat c)
    {
        Card? card = selectedCard;
        if (card != null)
        {
            RemoteManager.SetRemote(card, s, true);
        }
    }
    public override Icon? GetIcon(State s)
    => new(ModEntry.Instance.RemoteControlIcon.Sprite, null, Colors.textMain);
}

public class AMassRemoteuplink : CardAction
{
    public int count = 1;
    public int CardID;
    public override void Begin(G g, State s, Combat c)
    {
        foreach (Card item in c.hand)
        {
            RemoteManager.SetRemote(item, s, true);
        }
    }
    public override Icon? GetIcon(State s)
    => new(ModEntry.Instance.RemoteControlIcon.Sprite, null, Colors.textMain);
}

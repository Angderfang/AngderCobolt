using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardGarbage : Card, IAngderCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Garbage", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = Deck.trash,
                //upgradesTo = [Upgrade.A, Upgrade.B],
                rarity = Rarity.common,

            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Garbage", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Maid_Scrap.Sprite,
            cost = upgrade == Upgrade.A ? 2 : 1,
            singleUse = upgrade == Upgrade.B ? false : true,

        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {

        List<CardAction> actions = new();

        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                };
                break;
        }
        return actions;
    }
}

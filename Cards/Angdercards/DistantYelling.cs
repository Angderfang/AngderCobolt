using Nickel;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardDistantYelling : Card, IAngderCard, IHasCustomCardTraits
{

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state) => new HashSet<ICardTraitEntry>() { ModEntry.Instance.RemoteControl };

    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DistantYelling", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,
                rarity = Rarity.common,
                //dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DistantYelling", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        RemoteManager.SetRemoteUnending(this, state, true);
        CardData data = new CardData()
        {
            cost = 1,
            retain = upgrade == Upgrade.B ? true : false,
            temporary = true,
            exhaust = true
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

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 6,
                        targetPlayer = true
                    },
                    //I think this might be bad, as well as bottled rage

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.B:
                actions = new()
                {

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 3,
                        targetPlayer = true
                    },
                    /*
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    */

                };
                break;
            case Upgrade.A:
                actions = new()
                {

                    new AStatus()
                    {
                        status = ModEntry.Instance.Rampage.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}

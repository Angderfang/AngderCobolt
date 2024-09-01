using Angder.EchoesOfTheFuture;
using Nickel;
using System.Collections.Generic;
using System.Reflection;



namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class CardRemotecontrol : Card, IAngderCard, IHasCustomCardTraits
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Remotecontrol", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.AngderDeck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Remotecontrol", "name"]).Localize
        });
    }
    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state) => new HashSet<ICardTraitEntry>() { ModEntry.Instance.RemoteControl };
    public override CardData GetData(State state)
    {
        RemoteManager.SetRemoteUnending(this, state, true);
        CardData data = new CardData()
        {
            exhaust = true,
            cost = upgrade == Upgrade.B ? 1 : 0,
            description = ModEntry.Instance.Localizations.Localize(["card", "Remotecontrol", "description", upgrade.ToString()]),
            art = ModEntry.Instance.Angder_RemoteUplink.Sprite,
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
                    new ACardSelect {
                        browseAction = new ARemoteuplink(),
                        filterUUID = uuid,
                        browseSource = CardBrowse.Source.Hand,
                        //Filter by what doesn't already have it?
                    }

                };
                /* Remember to always break it up! */
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ACardSelect {
                        browseAction = new ARemoteuplink(),
                        filterUUID = uuid,
                        browseSource = CardBrowse.Source.Hand,
                        //Filter by what doesn't already have it?
                        
                    },
                    new ACardSelect {
                        browseAction = new ARemoteuplink(),
                        filterUUID = uuid,
                        browseSource = CardBrowse.Source.Hand,
                        //Filter by what doesn't already have it?
                    }

                };
                break;
            case Upgrade.B:
                actions = new()
                {
                new AMassRemoteuplink(),
                };
                break;
        }
        return actions;
    }
}

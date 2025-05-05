using Angder.EchoesOfTheFuture;
using Angder.EchoesOfTheFuture.Features.Grunan;
using FMOD;
using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Cards;

internal sealed class Bloodritual : Card, IAngderCard //IHasCustomCardTraits
{
    //public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state) => new HashSet<ICardTraitEntry>() { ModEntry.Instance.RemoteControl };
    //public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state) => new HashSet<ICardTraitEntry>() { ModEntry.Instance.Consuming };
    //bool Used = false;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Bloodritual", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GrunanDeck.Deck,

                rarity = Rarity.rare,
                
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Bloodritual", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.Candle.Sprite,
            //description = ModEntry.Instance.Localizations.Localize(["card", "EndlessResearch", "description", upgrade.ToString(), flipped.ToString()]),
            description = ModEntry.Instance.Localizations.Localize(["card", "Bloodritual", "description", upgrade.ToString()], new { damage = GetDmg(state, 1), damage2 = GetDmg(state, 2), damage3 = GetDmg(state, GetHandTotal(state))}) ,
            cost = 1,
            exhaust = true,
            //floppable = false,

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
                    new AAddCard
                    {
                        card = new Fireball(),
                        amount = 1,
                        destination = CardDestination.Discard,
                    },
                    new AAttack
                    {
                        damage = GetDmg(s, 1)
                    }
                };
                break;
            //flipped = true;
            case Upgrade.A:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new Fireball(),
                        amount = 1,
                        destination = CardDestination.Discard,
                    },
                    new AAttack
                    {
                        damage = GetDmg(s, 2)
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAddCard
                    {
                        card = new Fireball(),
                        amount = GetHandTotal(s),
                        destination = CardDestination.Discard,
                    },
                    new AExhaustEntireHand
                    {

                    },
                    
                };
                break;
        }
        
        return actions;
    }

    public int GetHandTotal(State s)
    {
        int num = 0;
        if (s.route is Combat combat)
        {
            num = combat.hand.Count - 1;
        }

        return num;
    }
}

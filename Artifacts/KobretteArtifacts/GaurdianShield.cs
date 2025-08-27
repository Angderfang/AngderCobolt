using Angder.EchoesOfTheFuture.Cards;
using Angder.EchoesOfTheFuture.Features;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Artifacts.KobretteArtifacts;
internal sealed class GuardianShield : Artifact, IAngderArtifact
    {
    //public bool Readup; 

    public override List<Tooltip>? GetExtraTooltips()
    {
        return new FloatingShield().GetTooltips();
    }
    public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("GuardianShield", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModEntry.Instance.KobretteDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/GuardianShield.png")).Sprite,
                Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "GuardianShield", "name"]).Localize,
                Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "GuardianShield", "description"]).Localize
            });
        }
    public override void OnCombatStart(State state, Combat combat)
    {
        List<int> list = new List<int>();
        for (int i = state.ship.x - 1; i < state.ship.x + state.ship.parts.Count() + 1; i++)
        {
            if (!combat.stuff.ContainsKey(i))
            {
                list.Add(i);
            }
        }

        List<int> list2 = list.Shuffle(state.rngActions).Take(1).ToList();
        foreach (int item in list2)
        {
            combat.stuff.Add(item, new FloatingShield
            {
                targetPlayer = false,
                x = item,
                xLerped = item
            });
        }

        if (list2.Count > 0)
        {
            Pulse();
        }
    }
}
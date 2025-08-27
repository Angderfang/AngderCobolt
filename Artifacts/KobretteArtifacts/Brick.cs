using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Artifacts.KobretteArtifacts;
internal sealed class Brick : Artifact, IAngderArtifact
    {
    //public bool Readup; 
        public override List<Tooltip>? GetExtraTooltips()
        => StatusMeta.GetTooltips(ModEntry.Instance.Fortress.Status, 1);
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("Brick", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModEntry.Instance.KobretteDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/Brick.png")).Sprite,
                Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Brick", "name"]).Localize,
                Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Brick", "description"]).Localize
            });
        }

        public override void OnCombatStart(State state, Combat combat)
        {
            combat.QueueImmediate(new AStatus
            {
                targetPlayer = true,
                statusAmount = 1,
                status = ModEntry.Instance.Fortress.Status,
            });
        Pulse();
        }
    }
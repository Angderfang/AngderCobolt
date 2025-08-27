using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Artifacts.KobretteArtifacts;
internal sealed class Sandbag : Artifact, IAngderArtifact
    {
    public int Sandbagcount; 

    public override int? GetDisplayNumber(State s)
    {
        return Sandbagcount;
    }
        public static void Register(IModHelper helper)
        {
            helper.Content.Artifacts.RegisterArtifact("Sandbag", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModEntry.Instance.KobretteDeck.Deck,
                    pools = [ArtifactPool.Common]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/Sandbag.png")).Sprite,
                Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Sandbag", "name"]).Localize,
                Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Sandbag", "description"]).Localize
            });
        }
       
        public override void OnTurnStart(State state, Combat combat)
        {
        if (Sandbagcount < 6)
                {
                Sandbagcount = Sandbagcount + 1;
                if (Sandbagcount == 6)
                {
                    combat.QueueImmediate(new AHullMax
                    {
                    targetPlayer = true,
                    amount = 1,
                    });
                Pulse();
                }
           }
        }
        public override void OnCombatStart(State state, Combat combat)
        {
        Sandbagcount = 0;
        }
    }
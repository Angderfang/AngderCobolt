using Angder.EchoesOfTheFuture.Features.Grunan;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Artifacts.KobretteArtifacts;

    internal sealed class ArmouredCore : Artifact, IAngderArtifact
    {
    //public bool Readup; 
public static void Register(IModHelper helper)
        {

            helper.Content.Artifacts.RegisterArtifact("ArmouredCore", new()
            {
                ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
                Meta = new()
                {
                    owner = ModEntry.Instance.KobretteDeck.Deck,
                    pools = [ArtifactPool.Boss]
                },
                Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/ArmouredCore.png")).Sprite,
                Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ArmouredCore", "name"]).Localize,
                Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "ArmouredCore", "description"]).Localize
            });
        }
    //TridimensionalCockpit
    public override void OnReceiveArtifact(State state)
    {
        int num = 0;
        int num2 = state.ship.parts.Count;
        bool cockpitfound = false;
        Part obj = new Part
        {
            damageModifier = PDamMod.armor,
            type = PType.wing,
            skin = "shield_knight"

        };
        foreach (Part part in state.ship.parts)
        {
            if (part.type == PType.cockpit)
            {
                if (cockpitfound == false)
                {
                    cockpitfound = true;
                    num = Convert.ToInt32(part.xLerped);
                }
                num2 = Convert.ToInt32(part.xLerped) + 1;
            }
        }
        state.ship.parts.Insert(num2, Mutil.DeepCopy(obj));
        state.ship.parts.Insert(num, Mutil.DeepCopy(obj));


        state.ship.hullMax = state.ship.hullMax + 10;

        state.ship.hull = state.ship.hull + 10;
    }
}
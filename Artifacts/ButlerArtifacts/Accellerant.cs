using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Angder.EchoesOfTheFuture.Artifacts.ButlerArtifacts;

internal sealed class Accellerant : Artifact, IAngderArtifact
{
    public bool Shuffled; 
    public static void Register(IModHelper helper)
    {

        helper.Content.Artifacts.RegisterArtifact("Accellerant", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.ButlerDeck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/artifacts/AccellerantOn.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Accellerant", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Accellerant", "description"]).Localize
        });
    }
    //public override Spr GetSprite()
    //{
        //if (Shuffled == false)
        //{

            //return ModEntry.Instance.ChainAxe2.Sprite;
        //}
        //else
        //{
            //return ModEntry.Instance.ChainAxe1.Sprite;
        //}
    //}
    public override void OnCombatStart(State s, Combat c)
    {
        Shuffled = false;
    }
    public override void OnPlayerDeckShuffle(State state, Combat combat)
    {
        Shuffled = true;
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        if (Shuffled == false)
        {
            Pulse();
            combat.QueueImmediate(new ADrawCard()
            {
                count = 1
            });
        }
    }
}

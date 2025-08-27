using Nickel;

namespace Angder.EchoesOfTheFuture;


internal interface IAngderCard
{
    static abstract void Register(IModHelper helper);
}

internal interface IAngderArtifact
{
    //void InjectDialogue() { }
    static abstract void Register(IModHelper helper);
}
internal interface ShouldOverrideStatusRenderingAsBars
{
    static abstract void Register(IModHelper helper);
}
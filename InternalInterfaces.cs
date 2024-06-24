using Nickel;

namespace Angder.Angdermod;


internal interface IAngderCard
{
    static abstract void Register(IModHelper helper);
}

internal interface IAngderArtifact
{
    //void InjectDialogue() { }
    static abstract void Register(IModHelper helper);
}
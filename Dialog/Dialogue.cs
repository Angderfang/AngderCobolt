using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Angder.EchoesOfTheFuture.Dialog;
using Angder.EchoesOfTheFuture.Dialog.Grunan;
using Microsoft.Extensions.Logging;

namespace Angder.EchoesOfTheFuture;
internal static class Dialogue
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        RiggsAngder.Inject();
        DizzyAngder.Inject();
        EventDialogueMaid.Inject();
        EventDialogue.Inject();
        CombatDialogue.Inject();
        CombatDialogueMaid.Inject();
        ArtifactsDialogue.Inject();
        EnemyDialogue.Inject();
        GrunanBase.Inject();
        GrunanStory.Inject();
        GrunanMaxStory.Inject();
        GrunanAngderStory.Inject();
        KobretteStory.Inject();
        KobretteBase.Inject();
        /*
        foreach (var cardType in ModEntry.AllCards)
        {
            if (Activator.CreateInstance(cardType) is not IRegisterableCard card)
                continue;
            card.InjectDialogue();
        }
        */

        /*
         * [22:54]Shockah: don't dialogue selectors need to begin with a dot?
        [22:54]Shockah: for whatever reason?
        [22:54]TheJazMaster: Ohhhhhhhhhhhhhhh
        [22:54]Shockah: (and then no dot in the actual story)
        [22:54]TheJazMaster: That must be it
        */
    }
}
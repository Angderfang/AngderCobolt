using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Angder.Angdermod.Dialog;
using Microsoft.Extensions.Logging;

namespace Angder.Angdermod;
internal static class Dialogue
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        EventDialogue.Inject();
        CombatDialogue.Inject();
        ArtifactsDialogue.Inject();
        EnemyDialogue.Inject();
        /*
        foreach (var cardType in ModEntry.AllCards)
        {
            if (Activator.CreateInstance(cardType) is not IRegisterableCard card)
                continue;
            card.InjectDialogue();
        }
        */
    }
}
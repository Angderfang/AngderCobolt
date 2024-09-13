using Angder.EchoesOfTheFuture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ARenameDroid: CardAction
{
    public ModEntry.DroidNames droidname;

    public override void Begin(G g, State s, Combat c)
    {
        //s.storyVars.droidname = droidname;
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        return new List<Tooltip>();
    }
}
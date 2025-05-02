using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features;

//Stolen from TyAndSasha. I am a horrible coder. I am going to get banned from coding. I am going to jail.
public class ACardSelectImproved : ACardSelect
{
    public override List<Tooltip> GetTooltips(State s)
    {
        var list = base.GetTooltips(s);
        list.InsertRange(0, browseAction.GetTooltips(s));
        return list;
    }
}
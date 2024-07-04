using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Angder.Angdermod.IKokoroApi.IActionCostApi;

namespace Angder.Angdermod.Features;

    public class AStunEnemySmallicon : AStunShip
    {
        //AAttack
        
        public override Icon? GetIcon(State s)
        {
            return new Icon(ModEntry.Instance.StunSmallIcon.Sprite, null, Colors.textMain);
        }

    }


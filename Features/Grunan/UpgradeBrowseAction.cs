using FSPRO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features.Grunan
{
    //Seriously, I really needed to make this myself? It's probably in Kok, but I am bad at coding.
    public class UpgradeCardBrowseEmpower : CardAction
    {
        public bool allowCancel;
        public bool showCard;
        public Route? subRoute;
        public UIKey? _lastGpSelection;

        public override Route? BeginWithRoute(G g, State s, Combat c)
        {
            if (selectedCard != null)
            {
                //Console.WriteLine("LOOK MORTY. I TURNED MYSELF INTO A NONFUNCITONAL CARD");
                //NEVERMIND MORTY. I WORK NOW.
                _lastGpSelection = Input.currentGpKey;
                return subRoute = new CardUpgrade
                {
                    cardCopy = Mutil.DeepCopy(selectedCard),
                    isPreview = false,
                };
            }
            return null;
        }

        public override List<Tooltip> GetTooltips(State s)
        {
            return new List<Tooltip>
        {
            new TTGlossary("action.upgradeCard")
        };
        }

        ////public override Icon? GetIcon(State s)
        //{

            //return new Icon(ModEntry.Instance.ShatterIcon.Sprite, 1, Colors.textMain);
        //}
    }
}
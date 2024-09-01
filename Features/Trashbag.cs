using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features;
    public class Trashbag : StuffBase
    {
    //bool cardtooltip = false;
    public override Spr? GetIcon()
        {
            return ModEntry.Instance.TrashbagIcon.Sprite;
        }

        public override string GetDialogueTag()
        {
            return "asteroid";
        }

        public override double GetWiggleAmount()
        {
            return 1.0;
        }

        public override double GetWiggleRate()
        {
            return 1.0;
        }

        public override List<Tooltip> GetTooltips()
        {
            string key;
            string name;
            string description;
            ISpriteEntry icon;
            icon = ModEntry.Instance.TrashbagIcon;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Trashbag";
            name = ModEntry.Instance.Localizations.Localize(["Midrow", "Trashbag", "name"]);
            description = ModEntry.Instance.Localizations.Localize(["Midrow", "Trashbag", "description"]);
            List<Tooltip> list = [new GlossaryTooltip(key)
            {
                Icon = icon.Sprite,
                TitleColor = Colors.action,
                Title = name,
                Description = description
            }];
            if (bubbleShield)
            {
                list.Add(new TTGlossary("midrow.bubbleShield"));
            }
        //if (cardtooltip == false)
        //{
            list.Add(new TTCard
            {
                
                card = new ColorlessTrash()
            });
        //    cardtooltip = true;
        //}

        return list;
        }

        public override void Render(G g, Vec v)
        {
            DrawWithHilight(g, ModEntry.Instance.TrashbagMidrow.Sprite, v + GetOffset(g), Mutil.Rand((double)x + 0.1) > 0.5, Mutil.Rand((double)x + 0.2) > 0.5);
        }

        public override List<CardAction>? GetActionsOnDestroyed(State s, Combat c, bool wasPlayer, int worldX)
        {
        c.Queue(new AAddCard()
        {
            card = new ColorlessTrash(),
            destination = CardDestination.Hand,
            amount = 1,
        }
        );

            return null;
        }
    }
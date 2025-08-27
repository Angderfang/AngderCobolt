using Angder.EchoesOfTheFuture.Cards;
using Angder.EchoesOfTheFuture.Features.Midrow.MidrowActions;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Missile;

namespace Angder.EchoesOfTheFuture.Features;
    public class SpiritualWeapon : StuffBase
    {
    //public bool returning = false;
    int Damage = 1;
    public override bool IsHostile()
    {
        return targetPlayer;
    }




    //bool cardtooltip = false;
    public override Spr? GetIcon()
        {
            return ModEntry.Instance.MaceIcon.Sprite;
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
            icon = ModEntry.Instance.MaceIcon;
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::SpiritualWeapon";
            name = ModEntry.Instance.Localizations.Localize(["Midrow", "SpiritualWeapon", "name"]);
            description = ModEntry.Instance.Localizations.Localize(["Midrow", "SpiritualWeapon", "description"]);
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
        return list;
        }
    public override bool GetIsDoneAnimatingAsExtraStuff()
    {
        return yAnimation > 14.0;
    }
    public override void Render(G g, Vec v)
    {
        bool flag = targetPlayer;
        DrawWithHilight(g, ModEntry.Instance.Mace.Sprite, v + GetOffset(g), false, flag);
    }
    
        public override List<CardAction>? GetActions(State s, Combat c)
        {
        return new List<CardAction>
            {
                new ASpiritual
                {
                    worldX = x,
                    targetPlayer = targetPlayer,
                    outgoingDamage = 1,
                }
            };
        }
    
    }

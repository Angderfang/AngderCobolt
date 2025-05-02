using Angder.EchoesOfTheFuture.Cards;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.Features;
    public class FloatingShield : StuffBase
    {
    int Health = 5;
    //bool cardtooltip = false;
    public override Spr? GetIcon()
        {
                return ModEntry.Instance.ShieldIcon.Sprite;

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
            key = $"{ModEntry.Instance.Package.Manifest.UniqueName}:: Midshield";
            name = ModEntry.Instance.Localizations.Localize(["Midrow", "Midshield", "name"]);
            description = ModEntry.Instance.Localizations.Localize(["Midrow", "Midshield", "description"]);
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
       
        public override void Render(G g, Vec v)
        {
            switch (Health)
            {
            case < 2:
                DrawWithHilight(g, ModEntry.Instance.ShieldNormalCracked4.Sprite, v + GetOffset(g), false, false);
                break;
            case 2:
                DrawWithHilight(g, ModEntry.Instance.ShieldNormalCracked3.Sprite, v + GetOffset(g), false, false);
                break;
            case 3:
                DrawWithHilight(g, ModEntry.Instance.ShieldNormalCracked2.Sprite, v + GetOffset(g), false, false);
                break;
            case 4:
                DrawWithHilight(g, ModEntry.Instance.ShieldNormalCracked1.Sprite, v + GetOffset(g), false, false);
                break;
            case 5:
                DrawWithHilight(g, ModEntry.Instance.ShieldNormal.Sprite, v + GetOffset(g), false, false);
                break;
            case > 5:
                DrawWithHilight(g, ModEntry.Instance.ShieldNormalShine.Sprite, v + GetOffset(g), false, false);
                break;
            }
        }

        public override bool Invincible()
        {
        if (Health < 2)
        {
            return false;
        }
        else
        {
            return true;
        }
        }
    public override List<CardAction>? GetActionsOnBonkedWhileInvincible(State s, Combat c, bool wasPlayer, StuffBase thing)
        {
        int damage = 2;
        if (thing is Missile missile && missile.missileType == MissileType.heavy)
        {
            damage = 3;
        }
        
        return GetActionsOnShotWhileInvincible(s, c, wasPlayer, damage);
    }   
    
    public override List<CardAction>? GetActionsOnShotWhileInvincible(State s, Combat c, bool wasPlayer, int damage)
    {
        Health = Health - damage;
        if (Health < 1)
        {
            //GetActionsOnDestroyed(s, c, wasPlayer, x);
            c.DestroyDroneAt(s, x, wasPlayer);
        }
        
        return null;
    }
}
using HarmonyLib;

using Nickel;

using System.Collections.Generic;
using System.Linq;

namespace Angder.EchoesOfTheFuture;

//This is called "worseCleaveAction" because originally it was meant to be replaced. Then the rpelacement fell through and I patched this to be not as awful.

public class CleaveAction : AAttack
{
    public bool TargetPlayer;
    public required int Damage;
    public required int DamageAlt; // For the actual damage number.
    public required int Length; //Always 1 lower than the true length.
    public required int Direction; //Used to be a bool, but then I realized I wanted 3 states, not just 2.
    public required Card Thiscard; //Probably a better way to do this, but eh, it works -- Not with right click it doesn't
    public required bool Ignoresoverdrive; /* This used to only decide if it had the symbol. Then I stopped being dumb.*/
    public int? Xcard; // Used to tell seeing red to stilll benifit from Rampage.
    public bool escapeclause = false; /* I hate this variable, but this WILL keep the code running so... */
    private AAttack? volleyattack;

    // Ok but seriously, moving IgnoresOverdrive in here was actually a mess. The render code turned Ignores Overdrive to false to prevent some kind of infinite loop. So I had to create the escape clause Variable to replace it.
    // As otherwise I couldn't call it for a different function later.
    // So escape clause literally just exists to become true so the render code doesn't loop endlessly and crash the game.
    // I BELIEVE without it, there would be an infinite number of Overchargeno sprites next to "Reliable" attacks.


    //To anyone trying to follow how cleave works;
    /* New action (cleave)
     * 
     * generates a bunch of little attack actions, that flow away from the place of origin. As these are queue immediates, these are added backwards to the list. Like loading a cannon from the front.
     * 
     * Damage is done via getDmg and by Angderjustcleavethings.AngderCleaveDmg, 
     * 
     * Damagealt is a hasty rule patch, blame Max. Otherwise GetDmg triggered too late to know where in the hand the card was. Orginally, GetDmg was used everywhere it is used.
     * 
     * 
     */


    public override List<Tooltip> GetTooltips(State s)
    {
        int num = s.ship.x;
        foreach (Part part in s.ship.parts)
        {
            if (part.type == PType.cannon && part.active)
            {
                if (s.route is Combat combat && combat.stuff.ContainsKey(num))
                {
                    combat.stuff[num].hilight = 2;
                }

                part.hilight = true;
            }

            num++;
        }
        //var BurntoutFireRegulatorpresent = s.EnumerateAllArtifacts().OfType<BurntoutFireRegulator>().FirstOrDefault();
        string key;
        string key2;
        string name;
        string description;
        ISpriteEntry icon;
        int truedamage = Angderjustcleavethings.AngderCleaveDmg(s, Damage, Thiscard, Ignoresoverdrive, DamageAlt, Xcard);
        if (Direction == 1)
        {
            if (Length > 3)
            {
                icon = ModEntry.Instance.Cleavelongright;
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Cleave::Long";
                name = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "name", "RightLong"]);
                description = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "description", "Right"], new { Long = Length + 1, truedamage });
            }
            else
            {
                icon = ModEntry.Instance.Cleaveshortright;
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Cleave::Right";
                name = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "name", "Right"]);
                description = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "description", "Right"], new { Long = Length + 1, truedamage });
            }
        }
        else if (Direction == -1)
        {
            if (Length > 3)
            {
                icon = ModEntry.Instance.Cleavelongleft;
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Cleave::Long";
                name = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "name", "LeftLong"]);
                description = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "description", "Left"], new { Long = Length + 1, truedamage });
            }
            else
            {
                icon = ModEntry.Instance.Cleaveshortleft;
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Cleave::Left";
                name = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "name", "Left"]);
                description = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "description", "Left"], new { Long = Length + 1, truedamage });
            }
        }
        else
        {
            if (Length > 3)
            {
                icon = ModEntry.Instance.Cleavelongright;
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Cleave::unknown";
                name = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "name", "Unknown"]);
                description = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "description", "Unknown"], new { Long = Length + 1, truedamage });
            }
            else
            {
                icon = ModEntry.Instance.Cleaveshortright;
                key = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Cleave::unknown";
                name = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "name", "Unknown"]);
                description = ModEntry.Instance.Localizations.Localize(["action", "Cleave", "description", "Unknown"], new { Long = Length + 1, truedamage });
            }
        }

        if (Ignoresoverdrive == true)// && BurntoutFireRegulatorpresent == null)
        {
            key2 = $"{ModEntry.Instance.Package.Manifest.UniqueName}::Overdriveno";

            List<Tooltip> tooltips = [

            new GlossaryTooltip(key2)
                {
                    Icon = ModEntry.Instance.Overdriveno.Sprite,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Overdriveno", "name"]),
                    Description = ModEntry.Instance.Localizations.Localize(["action", "Overdriveno", "description", "Damage"])
                },
            new GlossaryTooltip(key)
                {
                    Icon = icon.Sprite,
                    TitleColor = Colors.action,
                    Title = name,
                    Description = description
                }
            ];
            return tooltips;
        }
        else
        {
            List<Tooltip> tooltips = [
            new GlossaryTooltip(key)
                {
                    Icon = icon.Sprite,
                    TitleColor = Colors.action,
                    Title = name,
                    Description = description
                }
        ];
            return tooltips;
        }
    }
    
    public override Icon? GetIcon(State s)
    {
        int truedamageIcon = Angderjustcleavethings.AngderCleaveDmg(s, Damage, Thiscard, Ignoresoverdrive, DamageAlt, Xcard);
        if (Direction == 1)
        {
            if (Length > 3)
            {
                return new(ModEntry.Instance.Cleavelongright.Sprite, truedamageIcon, Colors.hurt);
            }
            else
            {
                return new(ModEntry.Instance.Cleaveshortright.Sprite, truedamageIcon, Colors.hurt);
            }
        }
        else if (Direction == -1)
        {
            if (Length > 3)
            {
                return new(ModEntry.Instance.Cleavelongleft.Sprite, truedamageIcon, Colors.hurt);
            }
            else
            {
                return new(ModEntry.Instance.Cleaveshortleft.Sprite, truedamageIcon, Colors.hurt);
            }
        }
        else //If 0
        {
            if (Length > 3)
            {
                return new(ModEntry.Instance.Cleavelongleft.Sprite, truedamageIcon, Colors.hurt);
            }
            else
            {
                return new(ModEntry.Instance.Cleaveshortleft.Sprite, truedamageIcon, Colors.hurt);
            }
        }
    }
    public override void Begin(G g, State s, Combat c)
    {

        int truedamageDamage = Angderjustcleavethings.AngderCleaveDmg(s, Damage, Thiscard, Ignoresoverdrive, DamageAlt, Xcard);
        
        //By the time this is called, the card is no longer in hand. So it has no hand value, so it doesn't take effect. //What a Shitshow to fix. //Options: /* Code special exceptions for Right click specifically* move "getdmg into the cards themselves. */
        
        //Issue solved. but it's messy. Now both the modified and standard damage numbers are in the attacks.

        //int truedamageDamage = Damage;

        if (s.ship == null)
        {
            return;
        }
        else if (c.otherShip == null)
        {
            return;
        }


        
        volleyattack = new AAttack()
        {
            fast = true,
            damage = truedamageDamage, //RIGHT CLICK DOES NOW RESEPCT THIS
        };

        timer = 0.0;
        volleyattack.multiCannonVolley = true;
        List<AAttack> list = new List<AAttack>();
        int num = 0;
        int shootlocation = 0;
        int Lengthloop = Length;
        if (Direction == 0)
        {
            int Noitsnotzero = (int)s.rngActions.NextInt() % 2;
            if (Noitsnotzero == 0)
                Direction = -1;
            else
                Direction = 1;
        }
        while (Lengthloop >= 0)
        {
            c.QueueImmediate(new AJupiterShoot
            {
                attackCopy = Mutil.DeepCopy(volleyattack),
            });
            Lengthloop--;
        }
        Lengthloop = Length;
        switch (Direction)
        {
            case -1:
                foreach (Part part in s.ship.parts)
                        {
                        if (part.type == PType.cannon && part.active)
                            {
                                while (Lengthloop >= 0)
                                {
                                    volleyattack.fromX = num + shootlocation;
                                    list.Add(Mutil.DeepCopy(volleyattack));
                                    Lengthloop--;
                                    shootlocation--;
                                }
                            Lengthloop = Length;
                            shootlocation = 0;
                            }
                            num++;
                        }
            break;
            case 1:
                foreach (Part part in s.ship.parts)
                {
                    if (part.type == PType.cannon && part.active)
                    {
                        while (Lengthloop >= 0)
                        {
                            volleyattack.fromX = num + shootlocation;
                            list.Add(Mutil.DeepCopy(volleyattack));
                            Lengthloop--;
                            shootlocation++;

                        }
                        Lengthloop = Length;
                        shootlocation = 0;
                    }
                    num++;
                }
                break;
                }
        c.QueueImmediate(list);
    }
}



    public static class Angderjustcleavethings
    {
    //Used when calling the function. Does not respect Right click sometimes.
        public static int AngderCleaveDmg(State s, int baseDamage, Card cardused, bool ignore, int DamageAlternate, int? Xcard)
        {
            if (ignore == false)
                return DamageAlternate;

            else
                return baseDamage;
        }
    }
    internal sealed class CleaveManager
    {

        public CleaveManager()
        {
            ModEntry.Instance.Harmony.Patch(
                original: AccessTools.DeclaredMethod(typeof(Card), nameof(Card.RenderAction)),
                prefix: new HarmonyMethod(GetType(), nameof(Card_RenderAction_Prefix))

            );
            return;
        }
        private static bool Card_RenderAction_Prefix(G g, State state, CardAction action, bool dontDraw, int shardAvailable, int stunChargeAvailable, int bubbleJuiceAvailable, ref int __result)
        {

            //Overchargeno is a mess.
            //var BurntoutFireRegulatorpresent = state.EnumerateAllArtifacts().OfType<BurntoutFireRegulator>().FirstOrDefault();

            if (action is not CleaveAction attack)
                return true;
            if (attack.Ignoresoverdrive is not true || attack.escapeclause is not false) //BurntoutFireRegulatorpresent != null)
                return true;

            var copy = Mutil.DeepCopy(attack);
            copy.escapeclause = true;
            var position = g.Push(rect: new()).rect.xy;
            int initialX = (int)position.x;

            position.x += Card.RenderAction(g, state, copy, dontDraw, shardAvailable, stunChargeAvailable, bubbleJuiceAvailable);
            g.Pop();

            __result = (int)position.x - initialX;
            __result += 2;

            if (!dontDraw)
            {
                ISpriteEntry OverdriveNo = ModEntry.Instance.Overdriveno;
                Draw.Sprite(OverdriveNo.Sprite, initialX + __result, position.y, color: action.disabled ? Colors.disabledIconTint : Colors.white);
            }
            __result += 9;
            return false;
        }
    }
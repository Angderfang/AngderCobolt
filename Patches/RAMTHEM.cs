using Angder.EchoesOfTheFuture.Features.Kobrette;
using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardBrowse;

namespace Angder.EchoesOfTheFuture.Patches;

//HEAVILY BASED ON PURPLEAPPLES WORK. LIKE. IF THEY TELL ME TO REMOVE THIS CODE AS PLAGERISM, I PROBABLY WOULD. AS A LOT OF THIS IS JUST THEIR CODE REWRITTEN.
internal static class RamPatch
{
    static ModEntry Instance => ModEntry.Instance;
    internal static void Apply(Harmony harmony)
    {
        harmony.Patch(
            //logger: ModEntry.Instance.Logger,
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DrawShipOver)),
            prefix: new HarmonyMethod(typeof(RamPatch), nameof(DrawShipOver_Prefix)),
            postfix:new HarmonyMethod(typeof(RamPatch), nameof(DrawShipOver_Postfix))
        );
        harmony.Patch(
        //logger: ModEntry.Instance.Logger,
        original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DrawShipUnder)),
        prefix: new HarmonyMethod(typeof(RamPatch), nameof(DrawShipUnder_Prefix))
        
    );
    }
    static double agesleep = 0;
    public static void DrawShipOver_Postfix(G g, Vec v, Vec worldPos, Ship __instance)
    {
        
        double timetaken = g.dt * 40;
        string TinyAngder = ModEntry.Instance.Helper.ModData.GetModDataOrDefault<string>(g.state, key: "AngderTiny", defaultValue: "No");

        if (!__instance.isPlayerShip && __instance.hull <= 0 && TinyAngder != "No")
            ModEntry.Instance.Helper.ModData.SetModData(g.state, key: "AngderTiny", TinyAngder = "Freefloat");
        if (!__instance.isPlayerShip && TinyAngder != "No")
        {
            Part part = __instance.parts[__instance.parts.Count / 2];
            Vec vec2 = worldPos + new Vec(0, -20 + (part.offset.y));//((__instance.parts.Count * 16) / 2, -20 + (part.offset.y));
            BlendState Normal = BlendMode.Normal;
            if (TinyAngder == "Yes")
            {
                double num = v.x + vec2.x + BasicSinCos.Wander(__instance.parts.Count * 16, timetaken);
                double num2 = v.y + vec2.y + (BasicSinCos.Bounce(timetaken) * 10);
                Draw.Sprite(Instance.Angder_Stand.Sprite, num, num2, !BasicSinCos.turn, false, 0.0, null, null, null, null, null, Normal);
            }
            if (TinyAngder == "Sleep")
            {
                agesleep = agesleep + g.dt;
                if (agesleep < 1 || agesleep > 2)
                    Draw.Sprite(Instance.Angder_Sleep1.Sprite, v.x + vec2.x, v.y + vec2.y, !BasicSinCos.turn, false, 0.0, null, null, null, null, null, Normal);
                else
                    Draw.Sprite(Instance.Angder_Sleep2.Sprite, v.x + vec2.x, v.y + vec2.y, !BasicSinCos.turn, false, 0.0, null, null, null, null, null, Normal);
                if (agesleep > 2)
                {
                    agesleep = 0;
                }
            }
            if (TinyAngder == "Freefloat")
            {
                double num2 = v.y + vec2.y + (BasicSinCos.Freefloat(timetaken) * 10);
                Draw.Sprite(Instance.Angder_Float.Sprite, v.x + vec2.x + BasicSinCos.Wandertime, num2, !BasicSinCos.turn, false, BasicSinCos.Gentlespin(timetaken), new Vec(10, 10), null, null, null, null, Normal);
            }
        }
    }
    public static void DrawShipOver_Prefix(G g, ref Vec worldPos, Ship __instance)
        {
            if (__instance.isPlayerShip)
            {
                if (g.state.route is Combat c)
                {
                //bool particles = false;
                foreach (FX fx in c.fx)
                {
                    if (fx is RamFX Ram)
                    {
                        if (Ram.age < -0.7)
                        {
                        }
                        else if (Ram.age < -0.3)
                        {
                            double percent = BasicSinCos.InSin((Ram.age + 0.7)) / .4;
                            worldPos.y -= 400 * percent;
                        }
                        else if (Ram.age < 0)
                        {
                            worldPos.y = -200;
                        }
                        else if (Ram.age < 1)
                        {
                            double percent = BasicSinCos.OutSin(1 - ((Ram.age)));
                            worldPos.y -= -200 * percent;
                        }
                        break;
                    }
                }
            }

        }
    }

        public static void DrawShipUnder_Prefix(G g, ref Vec worldPos, Ship __instance)
        {
            if (__instance.isPlayerShip)
            {
                if (g.state.route is Combat c)
                {
                    foreach (FX fx in c.fx)
                    {
                    if (fx is RamFX Ram)
                    {
                        if (Ram.age < -0.7)
                        {
                        }
                        else if (Ram.age < -0.2)
                        {
                            double percent = BasicSinCos.InSin((Ram.age + 0.7)) / .5;
                            worldPos.y -= 400 * percent;
                        }
                        else if (Ram.age < 0)
                        {
                            worldPos.y = -200;
                        }
                        else if (Ram.age < 1)
                        {
                            double percent = BasicSinCos.OutSin(1 - ((Ram.age)));
                            worldPos.y -= -200 * percent;
                        }
                        break;
                    }
                }
                }
            }
        }
    }
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.Shrike;
using Nanoray.Shrike.Harmony;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;


namespace Angder.EchoesOfTheFuture.Features.Grunan
{
    internal static class Extensions
    {
        static int recursionLevel = 0;

        public static int GetCurrentCostNoRecursion(this Card card, State s)
        {
            recursionLevel++;
            int result;
            if (recursionLevel < 2)
                result = card.GetCurrentCost(s);
            else
                result = card.GetData(s).cost;
            recursionLevel = 0;
            return result;
        }

        public static SequencePointerMatcher<CodeInstruction> GetLeaveBranchTarget(this SequencePointerMatcher<CodeInstruction> self, out Label label)
        {
            if (ILMatches.Instruction(OpCodes.Leave).Matches(self.Element()) || ILMatches.Instruction(OpCodes.Leave_S).Matches(self.Element()))
                label = (Label)self.Element().operand;
            else
                throw new SequenceMatcherException($"{self.Element()} is not a branch instruction.");
            return self;
        }

        public static ElementMatch<CodeInstruction> GetLeaveBranchTarget(this ElementMatch<CodeInstruction> self, out StructRef<Label> labelReference)
        {
            StructRef<Label> reference = new(default);
            labelReference = reference;
            return self.WithDelegate((matcher, index, element) =>
            {
                matcher.MakePointerMatcher(index).GetLeaveBranchTarget(out var label);
                reference.Value = label;
                return matcher;
            });
        }

        public static T ApplyModData<K, T>(this T obj, string key, K data)
        {
            ModEntry.Instance.Helper.ModData.SetModData(obj!, key, data);
            return obj;
        }


        public static void QueueImmediate<T>(this List<T> source, T item)
        {
            source.Insert(0, item);
        }

        public static void Queue<T>(this List<T> source, T item)
        {
            source.Add(item);
        }

        public static T GetModulo<T>(this T[] arr, int index)
        {
            return arr[Mutil.Mod(index, arr.Length)];
        }

        public static T Random<T>(this List<T> list, Rand rng)
        {
            return list[rng.NextInt() % list.Count];
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy((T item) => rnd.Next());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Rand rng)
        {
            Rand rng2 = rng;
            return source.OrderBy((T item) => rng2.NextInt());
        }
    }
}
using Shockah.Kokoro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture
{
    /// <summary>
    /// Allows accessing all of Kokoro library APIs.
    /// </summary>
    public partial interface IKokoroApi
    {
        /// <inheritdoc cref="IV2"/>
        IV2 V2 { get; }

        /// <summary>
        /// Allows accessing Kokoro version 2 APIs. It is the recommended way of using Kokoro.
        /// </summary>
        public partial interface IV2
        {
            public interface ICardAction<out T> where T : CardAction
            {
                T AsCardAction { get; }
            }

            public interface IRoute<out T> where T : Route
            {
                T AsRoute { get; }
            }

            /// <summary>
            /// Marks a Kokoro version 2 API hook, as opposed to version 1 API hooks, which are not marked.
            /// </summary>
            public interface IKokoroV2ApiHook;

            public interface IHookPriority
            {
                double HookPriority { get; }
            }
        }
    }
}
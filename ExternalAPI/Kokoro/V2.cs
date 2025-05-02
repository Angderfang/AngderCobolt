using Shockah.Kokoro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angder.EchoesOfTheFuture.ExternalAPI.Kokoro
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
            /// <summary>
            /// A Kokoro wrapper for a custom <see cref="CardAction"/>.
            /// </summary>
            /// <typeparam name="T">The more concrete type of the card action being wrapped.</typeparam>
            public interface IActionApi
            {
                CardAction MakeExhaustEntireHandImmediate();
                CardAction MakePlaySpecificCardFromAnywhere(int cardId, bool showTheCardIfNotInHand = true);
                CardAction MakePlayRandomCardsFromAnywhere(IEnumerable<int> cardIds, int amount = 1, bool showTheCardIfNotInHand = true);

                CardAction MakeContinue(out Guid id);
                CardAction MakeContinued(Guid id, CardAction action);
                IEnumerable<CardAction> MakeContinued(Guid id, IEnumerable<CardAction> action);
                CardAction MakeStop(out Guid id);
                CardAction MakeStopped(Guid id, CardAction action);
                IEnumerable<CardAction> MakeStopped(Guid id, IEnumerable<CardAction> action);

                CardAction MakeHidden(CardAction action, bool showTooltips = false);
                AVariableHint SetTargetPlayer(AVariableHint action, bool targetPlayer);
                AVariableHint MakeEnergyX(AVariableHint? action = null, bool energy = true, int? tooltipOverride = null);
                AStatus MakeEnergy(AStatus action, bool energy = true);

                List<CardAction> GetWrappedCardActions(CardAction action);
                List<CardAction> GetWrappedCardActionsRecursively(CardAction action);
                List<CardAction> GetWrappedCardActionsRecursively(CardAction action, bool includingWrapperActions);

                void RegisterWrappedActionHook(IWrappedActionHook hook, double priority);
                void UnregisterWrappedActionHook(IWrappedActionHook hook);
            }
            /// <summary>
            /// A Kokoro wrapper for a custom <see cref="Route"/>.
            /// </summary>
            /// <typeparam name="T">The more concrete type of the route being wrapped.</typeparam>
            public interface IRoute<out T> where T : Route
            {
                /// <summary>
                /// Returns the actual usable route.
                /// </summary>
                T AsRoute { get; }
            }

            /// <summary>
            /// Marks a Kokoro version 2 API hook, as opposed to version 1 API hooks, which are not marked.
            /// </summary>
            public interface IKokoroV2ApiHook;

            /// <summary>
            /// Allows choosing the priority for an auto-implemented hook (for example, on <see cref="Artifact"/> types).
            /// </summary>
            public interface IHookPriority
            {
                /// <summary>
                /// The priority for the hook. Higher priority hooks are called before lower priority ones. Defaults to <c>0</c>.
                /// </summary>
                double HookPriority { get; }
            }
        }
    }
}
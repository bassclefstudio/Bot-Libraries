using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using BassClefStudio.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Actions
{
    /// <summary>
    /// Represents an action presented to the user by the <see cref="Bot"/> that can prompt a response at a later time.
    /// </summary>
    public interface IBotAction : IIdentifiable<string>
    {
        /// <summary>
        /// The name of the action button, given to the user.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// A description of the <see cref="IBotAction"/>, sometimes shown to the user.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// A <see cref="bool"/> indicating whether the <see cref="IBotAction"/> should be removed from the <see cref="BotChat.ActiveActions"/> after it is invoked.
        /// </summary>
        bool OneTime { get; }

        /// <summary>
        /// Completes the <see cref="IBotAction"/>, indicating a user has invoked it.
        /// </summary>
        void Complete();

        /// <summary>
        /// A <see cref="Task"/> that will complete if/when the <see cref="IBotAction"/> is invoked.
        /// </summary>
        Task AwaitCompletionTask { get; }
    }

    /// <summary>
    /// Represents an <see cref="IBotAction"/> presented to the user that can return a <typeparamref name="T"/> result when invoked.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBotAction<T> : IBotAction
    {
        /// <summary>
        /// A <see cref="Task{TResult}"/> that will, if/when the <see cref="IBotAction{T}"/> is invoked, return this action's <typeparamref name="T"/> result.
        /// </summary>
        Task<T> AwaitValueTask { get; }
    }

    /// <summary>
    /// Extension methods for the <see cref="IBotAction"/> interface.
    /// </summary>
    public static class BotActionExtensions
    {
        /// <summary>
        /// Awaits the completion of any of a collection of <see cref="IBotAction{T}"/>s.
        /// </summary>
        /// <param name="actions">The collection of <see cref="IBotAction"/>s to await.</param>
        public static async Task AwaitCompletionAsync(this IEnumerable<IBotAction> actions)
        {
            await Task.WhenAny(actions.Select(a => a.AwaitCompletionTask));
        }

        /// <summary>
        /// Awaits the completion of any of a collection of <see cref="IBotAction{T}"/>s and returns the result from the <see cref="IBotAction{T}.AwaitValueTask"/> of that invoked action.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="IBotAction{T}"/>s involved (and the type of the output).</typeparam>
        /// <param name="actions">The collection of <see cref="IBotAction{T}"/>s to await.</param>
        /// <returns>Asynchronously returns the <typeparamref name="T"/> output value of the first <see cref="IBotAction{T}"/> that completes.</returns>
        public static async Task<T> AwaitValueAsync<T>(this IEnumerable<IBotAction<T>> actions)
        {
            var completed = await Task.WhenAny(actions.Select(a => a.AwaitValueTask));
            return await completed;
        }
    }
}

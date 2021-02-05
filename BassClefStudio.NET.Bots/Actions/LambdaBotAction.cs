﻿using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Actions
{
    /// <summary>
    /// An <see cref="IBotAction{T}"/> that uses a <see cref="TaskCompletionSource{TResult}"/> to return a constant <typeparamref name="T"/> value when invoked.
    /// </summary>
    public class LambdaBotAction<T> : IBotAction<T>
    {
        /// <inheritdoc/>
        public string DisplayName { get; }

        /// <inheritdoc/>
        public string Description { get; }

        /// <inheritdoc/>
        public bool OneTime { get; }

        /// <inheritdoc/>
        public string Id { get; }

        /// <summary>
        /// A function returning a <typeparamref name="T"/> value that will be sent to the <see cref="AwaitValueTask"/> when the action is invoked (see <see cref="Complete"/>).
        /// </summary>
        public Func<T> ReturnFunc { get; }

        private TaskCompletionSource<T> CompletionSource { get; }
        /// <inheritdoc/>
        public Task<T> AwaitValueTask => CompletionSource.Task;
        /// <inheritdoc/>
        public Task AwaitCompletionTask => CompletionSource.Task;

        /// <summary>
        /// Creates a new <see cref="ResultBotAction{T}"/>.
        /// </summary>
        /// <param name="id">The unique ID of the <see cref="IBotAction"/>.</param>
        /// <param name="name">The name of the action button, given to the user.</param>
        /// <param name="returnFunc">A function returning a <typeparamref name="T"/> value that will be sent to the <see cref="AwaitValueTask"/> when the action is invoked (see <see cref="Complete"/>).</param>
        /// <param name="description">A description of the <see cref="IBotAction"/>, sometimes shown to the user.</param>
        /// <param name="oneTime">A <see cref="bool"/> indicating whether the <see cref="IBotAction"/> should be removed from the <see cref="BotChat.ActiveActions"/> after it is invoked.</param>
        public LambdaBotAction(string id, string name, Func<T> returnFunc, string description = null, bool oneTime = true)
        {
            Id = id;
            DisplayName = name;
            ReturnFunc = returnFunc;
            Description = description;
            OneTime = oneTime;
            CompletionSource = new TaskCompletionSource<T>();
        }

        /// <inheritdoc/>
        public void Complete()
        {
            CompletionSource.TrySetResult(ReturnFunc());
        }
    }
}

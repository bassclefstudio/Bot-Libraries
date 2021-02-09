using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Actions
{
    /// <summary>
    /// An <see cref="IBotAction"/> that uses an event-based <see cref="ContinueTask"/> instead of awaiting for a result, which is more performant for dealing with optional actions.
    /// </summary>
    public class ContinuationBotAction : IBotAction
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
        /// A function returning a <see cref="Task"/>that will be excuted when the action is invoked (see <see cref="Complete"/>).
        /// </summary>
        public Func<Task> ContinueTask { get; }

        /// <summary>
        /// Creates a new <see cref="ContinuationBotAction"/>.
        /// </summary>
        /// <param name="id">The unique ID of the <see cref="IBotAction"/>.</param>
        /// <param name="name">The name of the action button, given to the user.</param>
        /// <param name="continueTask">A function returning a <see cref="Task"/>that will be excuted when the action is invoked (see <see cref="Complete"/>).</param>
        /// <param name="description">A description of the <see cref="IBotAction"/>, sometimes shown to the user.</param>
        /// <param name="oneTime">A <see cref="bool"/> indicating whether the <see cref="IBotAction"/> should be removed from the <see cref="BotChat.ActiveActions"/> after it is invoked.</param>
        public ContinuationBotAction(string id, string name, Func<Task> continueTask, string description = null, bool oneTime = true)
        {
            Id = id;
            DisplayName = name;
            ContinueTask = continueTask;
            Description = description;
            OneTime = oneTime;
        }

        /// <inheritdoc/>
        public void Complete()
        {
            SynchronousTask continueTask = new SynchronousTask(ContinueTask);
            continueTask.RunTask();
        }
    }
}

using System;

namespace WackenDataAndUtilities.Utilities
{
    public class EventRaiser
    {
        /// <summary>
        /// Rises event with no arguments.
        /// </summary>
        /// <param name="eventHandler">Event handler for rising event.</param>
        /// <param name="sender">Object to rise event from.</param>
        public static void RiseWithEmptyArgs(EventHandler eventHandler, object sender)
        {
            var handler = eventHandler;
            if (handler != null)
                handler(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Rises event with specific argument.
        /// </summary>
        /// <typeparam name="T">Class inherits <see cref="EventArgs">EventArgs.</see></typeparam>
        /// <param name="eventHandler">Event handler for rising event.</param>
        /// <param name="sender">Object to rise event from.</param>
        /// <param name="args">Arguments attached to an event.</param>
        public static void RiseWithArgs<T>(EventHandler<T> eventHandler, object sender, T args) where T : EventArgs
        {
            var handler = eventHandler;
            if (handler != null)
                handler(sender, args);
        }
    }
}
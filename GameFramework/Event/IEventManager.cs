//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.Event
{
    /// <summary>
    /// Event manager interface.
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// Gets the number of event handlers.
        /// </summary>
        int EventHandlerCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of events.
        /// </summary>
        int EventCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of event handlers for the specified event id.
        /// </summary>
        /// <param name="id">Event type id.</param>
        /// <returns>The number of event handlers.</returns>
        int Count(int id);

        /// <summary>
        /// Checks whether the event handler exists.
        /// </summary>
        /// <param name="id">Event type id.</param>
        /// <param name="handler">Event handler to check.</param>
        /// <returns>Whether the event handler exists.</returns>
        bool Check(int id, EventHandler<GameEventArgs> handler);

        /// <summary>
        /// Subscribes to an event handler.
        /// </summary>
        /// <param name="id">Event type id.</param>
        /// <param name="handler">Event handler to subscribe.</param>
        void Subscribe(int id, EventHandler<GameEventArgs> handler);

        /// <summary>
        /// Unsubscribes from an event handler.
        /// </summary>
        /// <param name="id">Event type id.</param>
        /// <param name="handler">Event handler to unsubscribe.</param>
        void Unsubscribe(int id, EventHandler<GameEventArgs> handler);

        /// <summary>
        /// Sets the default event handler.
        /// </summary>
        /// <param name="handler">Default event handler to set.</param>
        void SetDefaultHandler(EventHandler<GameEventArgs> handler);

        /// <summary>
        /// Fires an event. This operation is thread-safe; handlers will be called on the main thread, but dispatched in the next frame.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        void Fire(object sender, GameEventArgs e);

        /// <summary>
        /// Fires an event immediately. This operation is not thread-safe and the event will be dispatched immediately.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        void FireNow(object sender, GameEventArgs e);
    }
}

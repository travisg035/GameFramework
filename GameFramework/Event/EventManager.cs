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
    /// Event manager.
    /// </summary>
    internal sealed class EventManager : GameFrameworkModule, IEventManager
    {
        private readonly EventPool<GameEventArgs> m_EventPool;

        /// <summary>
        /// Initializes a new instance of the EventManager class.
        /// </summary>
        public EventManager()
        {
            m_EventPool = new EventPool<GameEventArgs>(EventPoolMode.AllowNoHandler | EventPoolMode.AllowMultiHandler);
        }

        /// <summary>
        /// Gets the number of event handlers.
        /// </summary>
        public int EventHandlerCount
        {
            get
            {
                return m_EventPool.EventHandlerCount;
            }
        }

        /// <summary>
        /// Gets the number of events.
        /// </summary>
        public int EventCount
        {
            get
            {
                return m_EventPool.EventCount;
            }
        }

        /// <summary>
        /// Gets the priority of the game framework module.
        /// </summary>
        /// <remarks>Modules with higher priority will be polled first and shut down later.</remarks>
        internal override int Priority
        {
            get
            {
                return 7;
            }
        }

        /// <summary>
        /// Event manager update.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_EventPool.Update(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// Shutdown and cleanup the event manager.
        /// </summary>
        internal override void Shutdown()
        {
            m_EventPool.Shutdown();
        }

        /// <summary>
        /// Gets the number of event handlers for the specified event id.
        /// </summary>
        /// <param name="id">Event type id.</param>
        /// <returns>The number of event handlers.</returns>
        public int Count(int id)
        {
            return m_EventPool.Count(id);
        }

        /// <summary>
        /// Checks whether the event handler exists.
        /// </summary>
        /// <param name="id">Event type id.</param>
        /// <param name="handler">Event handler to check.</param>
        /// <returns>Whether the event handler exists.</returns>
        public bool Check(int id, EventHandler<GameEventArgs> handler)
        {
            return m_EventPool.Check(id, handler);
        }

        /// <summary>
        /// Subscribes to an event handler.
        /// </summary>
        /// <param name="id">Event type id.</param>
        /// <param name="handler">Event handler to subscribe.</param>
        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventPool.Subscribe(id, handler);
        }

        /// <summary>
        /// Unsubscribes from an event handler.
        /// </summary>
        /// <param name="id">Event type id.</param>
        /// <param name="handler">Event handler to unsubscribe.</param>
        public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventPool.Unsubscribe(id, handler);
        }

        /// <summary>
        /// Sets the default event handler.
        /// </summary>
        /// <param name="handler">Default event handler to set.</param>
        public void SetDefaultHandler(EventHandler<GameEventArgs> handler)
        {
            m_EventPool.SetDefaultHandler(handler);
        }

        /// <summary>
        /// Fires an event. This operation is thread-safe; handlers will be called on the main thread, but dispatched in the next frame.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        public void Fire(object sender, GameEventArgs e)
        {
            m_EventPool.Fire(sender, e);
        }

        /// <summary>
        /// Fires an event immediately. This operation is not thread-safe and the event will be dispatched immediately.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        public void FireNow(object sender, GameEventArgs e)
        {
            m_EventPool.FireNow(sender, e);
        }
    }
}

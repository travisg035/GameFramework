//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Task agent interface.
    /// </summary>
    /// <typeparam name="T">Task type.</typeparam>
    internal interface ITaskAgent<T> where T : TaskBase
    {
        /// <summary>
        /// Gets the task.
        /// </summary>
        T Task
        {
            get;
        }

        /// <summary>
        /// Initializes the task agent.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Updates the task agent per frame.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// Shutdowns and cleans up the task agent.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Starts processing the task.
        /// </summary>
        /// <param name="task">The task to process.</param>
        /// <returns>The start task status.</returns>
        StartTaskStatus Start(T task);

        /// <summary>
        /// Stops processing the task and resets the task agent.
        /// </summary>
        void Reset();
    }
}

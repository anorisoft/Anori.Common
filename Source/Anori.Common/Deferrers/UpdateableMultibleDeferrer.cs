// -----------------------------------------------------------------------
// <copyright file="UpdateableMultibleDeferrer.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Common.Deferrers
{
    using Anori.ExpressionObservers;
    using System;
    using System.Threading;

    /// <summary>
    ///     The Multible Deferrer class.
    /// </summary>
    public class UpdateableMultibleDeferrer
    {
        /// <summary>
        ///     The update.
        /// </summary>
        private readonly Action update;

        /// <summary>
        ///     The count.
        /// </summary>
        private int count;

        /// <summary>
        ///     The state.
        /// </summary>
        private DeferState state = DeferState.NotDeferred;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateableMultibleDeferrer" /> class.
        /// </summary>
        /// <param name="update">The update.</param>
        public UpdateableMultibleDeferrer(Action update)
        {
            this.update = update;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is defer.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is defer; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeferred => state != DeferState.NotDeferred;

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>Create new Disposable.</returns>
        public IDisposable Create()
        {
            if (Interlocked.Increment(ref count) == 1)
            {
                state = DeferState.Deferred;
            }

            return new Disposable(Decrement);
        }

        /// <summary>
        ///     Updates this instance.
        /// </summary>
        /// <returns>Is value updated [True] or defereed [False].</returns>
        public bool Update()
        {
            switch (state)
            {
                case DeferState.Update:
                    return false;

                case DeferState.Deferred:
                    state = DeferState.Update;
                    return false;

                default:
                    update();
                    return true;
            }
        }

        /// <summary>
        ///     Decrements this instance.
        /// </summary>
        private void Decrement()
        {
            if (Interlocked.Decrement(ref count) == 0)
            {
                if (state == DeferState.Update)
                {
                    update();
                }

                state = DeferState.NotDeferred;
            }
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file="MultibleDeferrer.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ExpressionObservers
{
    using System;
    using System.Threading;

    /// <summary>
    ///     The Multible Deferrer class.
    /// </summary>
    public class MultibleDeferrer
    {
        /// <summary>
        ///     The release.
        /// </summary>
        private readonly Action @catch;

        /// <summary>
        ///     The release.
        /// </summary>
        private readonly Action release;

        /// <summary>
        ///     The count.
        /// </summary>
        private int count;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultibleDeferrer" /> class.
        /// </summary>
        /// <param name="catch">The catch.</param>
        /// <param name="release">The release.</param>
        public MultibleDeferrer(Action @catch, Action release)
        {
            this.@catch = @catch;
            this.release = release;
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>Create new Disposable.</returns>
        public IDisposable Create()
        {
            if (Interlocked.Increment(ref this.count) == 1)
            {
                this.@catch();
            }

            return new Disposable(
                () =>
                    {
                        if (Interlocked.Decrement(ref this.count) == 0)
                        {
                            this.release();
                        }
                    });
        }
    }
}
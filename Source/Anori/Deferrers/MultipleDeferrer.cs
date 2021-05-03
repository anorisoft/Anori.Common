// -----------------------------------------------------------------------
// <copyright file="MultipleDeferrer.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Deferrers
{
    using System;
    using System.Threading;

    using Anori.Common;

    /// <summary>
    ///     The Multiple Deferrer class.
    /// </summary>
    public class MultipleDeferrer
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
        ///     Initializes a new instance of the <see cref="MultipleDeferrer" /> class.
        /// </summary>
        /// <param name="catch">The catch.</param>
        /// <param name="release">The release.</param>
        public MultipleDeferrer(Action @catch, Action release)
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
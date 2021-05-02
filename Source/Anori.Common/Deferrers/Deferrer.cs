// -----------------------------------------------------------------------
// <copyright file="Deferrer.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ExpressionObservers
{
    using System;

    /// <summary>
    ///     Deferrer.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public readonly struct Deferrer : IDisposable
    {
        /// <summary>
        ///     The release.
        /// </summary>
        private readonly Action release;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Deferrer" /> struct.
        /// </summary>
        /// <param name="catch">The catch.</param>
        /// <param name="release">The release.</param>
        public Deferrer(Action @catch, Action release)
        {
            @catch();
            this.release = release;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => this.release();
    }
}
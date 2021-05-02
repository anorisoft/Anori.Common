// -----------------------------------------------------------------------
// <copyright file="Disposable.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ExpressionObservers
{
    using System;

    public readonly struct Disposable : IDisposable
    {
        /// <summary>
        ///     The release.
        /// </summary>
        private readonly Action release;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Disposable" /> struct.
        /// </summary>
        /// <param name="catch">The catch.</param>
        /// <param name="release">The release.</param>
        public Disposable(Action release) => this.release = release;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => this.release();
    }
}
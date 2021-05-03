// -----------------------------------------------------------------------
// <copyright file="Disposable.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Common
{
    using System;

    /// <summary>
    ///     Disposable object.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public readonly struct Disposable : IDisposable
    {
        /// <summary>
        ///     The release.
        /// </summary>
        private readonly Action release;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Disposable" /> struct.
        /// </summary>
        /// <param name="release">The release.</param>
        public Disposable(Action release) => this.release = release;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => this.release();
    }
}
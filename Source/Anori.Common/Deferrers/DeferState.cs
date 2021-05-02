// -----------------------------------------------------------------------
// <copyright file="DeferState.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ExpressionObservers
{
    /// <summary>
    ///     Defer State.
    /// </summary>
    public enum DeferState
    {
        /// <summary>
        ///     The not deferred.
        /// </summary>
        NotDeferred = 0,

        /// <summary>
        ///     The deferred.
        /// </summary>
        Deferred = 1,

        /// <summary>
        ///     The update.
        /// </summary>
        Update = 2,
    }
}
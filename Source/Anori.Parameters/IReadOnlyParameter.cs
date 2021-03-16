// -----------------------------------------------------------------------
// <copyright file="IReadOnlyParameter.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Parameters
{
    using System;

    using Anori.Common;

    /// <summary>
    ///     The I Read Only Parameter interface.
    /// </summary>
    public interface IReadOnlyParameter
    {
        /// <summary>
        ///     Occurs when [value changed].
        /// </summary>
        event EventHandler<EventArgs<object>> ValueChanged;

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        object Value { get; }
    }
}
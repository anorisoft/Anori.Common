// -----------------------------------------------------------------------
// <copyright file="IActivatable{TSelf}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Common
{
    /// <summary>
    ///     Activatable Interface.
    /// </summary>
    /// <typeparam name="TSelf">The type of the self.</typeparam>
    /// <seealso cref="IActivated" />
    public interface IActivatable<out TSelf> : IActivated
    {
        /// <summary>
        ///     Activates this instance.
        /// </summary>
        /// <returns>The Self.</returns>
        TSelf Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        /// <returns>The Self.</returns>
        TSelf Deactivate();
    }
}
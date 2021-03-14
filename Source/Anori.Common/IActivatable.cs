// -----------------------------------------------------------------------
// <copyright file="IActivatable.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Common
{
    /// <summary>
    ///     Activatable Interface.
    /// </summary>
    public interface IActivatable : IActivated
    {
        /// <summary>
        ///     Activates this instance.
        /// </summary>
        void Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        void Deactivate();
    }
}
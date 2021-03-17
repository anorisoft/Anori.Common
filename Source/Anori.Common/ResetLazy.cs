// -----------------------------------------------------------------------
// <copyright file="ResetLazy.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;

    using JetBrains.Annotations;

    /// <summary>
    ///     ResetLazy.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    [ComVisible(false)]
    //[HostProtection(Action = SecurityAction.LinkDemand,
    //    Resources = HostProtectionResource.Synchronization | HostProtectionResource.SharedState)]
    public class ResetLazy<T>
    {
        /// <summary>
        ///     The mode.
        /// </summary>
        private readonly LazyThreadSafetyMode mode;

        /// <summary>
        ///     The box.
        /// </summary>
        private volatile Box? box;

        /// <summary>
        ///     The synchronize lock.
        /// </summary>
        [NotNull]
        private object syncLock = new object();

        /// <summary>
        ///     The value factory.
        /// </summary>
        [NotNull]
        private Func<T> valueFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetLazy{T}" /> class.
        /// </summary>
        /// <param name="valueFactory">The value factory.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="declaringType">Type of the declaring.</param>
        /// <exception cref="ArgumentNullException">valueFactory is null.</exception>
        public ResetLazy(
            [NotNull] Func<T> valueFactory,
            LazyThreadSafetyMode mode = LazyThreadSafetyMode.PublicationOnly,
            Type? declaringType = null)
        {
            this.mode = mode;
            this.valueFactory = valueFactory ?? throw new ArgumentNullException(nameof(valueFactory));
            this.DeclaringType = declaringType ?? valueFactory.Method.DeclaringType;
        }

        /// <summary>
        ///     Gets the boxed value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        [CanBeNull]
        public T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                switch (this.mode)
                {
                    case LazyThreadSafetyMode.Full:
                        {
                            lock (this.syncLock)
                            {
                                var b2 = this.box;
                                if (b2 != null)
                                {
                                    return b2.Value;
                                }

                                this.box = new Box(this.valueFactory());

                                return this.box.Value;
                            }
                        }

                    case LazyThreadSafetyMode.ExecutionAndPublication:
                        {
                            var b1 = this.box;
                            if (b1 != null)
                            {
                                return b1.Value;
                            }

                            lock (this.syncLock)
                            {
                                var b2 = this.box;
                                if (b2 != null)
                                {
                                    return b2.Value;
                                }

                                this.box = new Box(this.valueFactory());

                                return this.box.Value;
                            }
                        }

                    case LazyThreadSafetyMode.PublicationOnly:
                        {
                            var b1 = this.box;
                            if (b1 != null)
                            {
                                return b1.Value;
                            }

                            var newValue = this.valueFactory();

                            lock (this.syncLock)
                            {
                                var b2 = this.box;
                                if (b2 != null)
                                {
                                    return b2.Value;
                                }

                                this.box = new Box(newValue);

                                return this.box.Value;
                            }
                        }

                    default:
                        {
                            var b1 = this.box;
                            if (b1 != null)
                            {
                                return b1.Value;
                            }

                            var b = new Box(this.valueFactory());
                            this.box = b;
                            return b.Value;
                        }
                }
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is value created.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is value created; otherwise, <c>false</c>.
        /// </value>
        public bool IsValueCreated
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.box != null;
        }

        /// <summary>
        ///     Gets the type of the declaring.
        /// </summary>
        /// <value>
        ///     The type of the declaring.
        /// </value>
        public Type? DeclaringType { get; }

        /// <summary>
        ///     Loads this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Load() => _ = this.Value;

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        public void Reset()
        {
            if (this.mode != LazyThreadSafetyMode.None)
            {
                lock (this.syncLock)
                {
                    this.box = null;
                }
            }
            else
            {
                // ReSharper disable once InconsistentlySynchronizedField
                this.box = null;
            }
        }

        /// <summary>
        ///     The Box.
        /// </summary>
        private class Box
        {
            /// <summary>
            ///     The value.
            /// </summary>
            [CanBeNull]
            public readonly T Value;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Box" /> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public Box([CanBeNull] T value) => this.Value = value;
        }
    }
}
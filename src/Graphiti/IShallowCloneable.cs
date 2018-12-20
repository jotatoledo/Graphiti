// <copyright file="IShallowCloneable.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    /// <summary>
    /// Supports shallow cloning, which creates a new instance of a class
    /// with the same value as an existing instance.
    /// </summary>
    /// <typeparam name="T">The type to be cloned</typeparam>
    public interface IShallowCloneable<out T>
    {
        /// <summary>
        /// Generates a shallow clone of this
        /// </summary>
        /// <returns>A new instance of <typeparamref name="T"/></returns>
        T Clone();
    }
}

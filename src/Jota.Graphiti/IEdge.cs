// <copyright file="IEdge.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti
{
    /// <summary>
    /// An edge between two vertices
    /// </summary>
    /// <typeparam name="TValue">The type of the vertices</typeparam>
    public interface IEdge<TValue>
    {
        /// <summary>
        /// Gets the source vertex
        /// </summary>
        TValue Source { get; }

        /// <summary>
        /// Gets the target vertex
        /// </summary>
        TValue Target { get; }

        /// <summary>
        /// Creates a new edge with interchanged source and target vertices
        /// </summary>
        /// <returns>A new edge with the swaped vertices</returns>
        IEdge<TValue> Invert();
    }
}

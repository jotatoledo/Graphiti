// <copyright file="IVertexSet.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// A set of vertices
    /// </summary>
    /// <typeparam name="TVertex">The type of vertex</typeparam>
    public interface IVertexSet<TVertex>
    {
        /// <summary>
        /// Gets a value indicating whether this is empty or not
        /// </summary>
        bool HasNoVertices { get; }

        /// <summary>
        /// Gets the number of elements contained in this
        /// </summary>
        int VertexCount { get; }

        /// <summary>
        /// Gets an enumeration of the vertices in this
        /// </summary>
        IEnumerable<TVertex> Vertices { get; }

        /// <summary>
        /// Determines whether the given vertex is contained in this.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <returns>
        /// <c>true</c> if the given vertex is contained in this; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsVertex(TVertex vertex);
    }
}

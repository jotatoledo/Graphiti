// <copyright file="IMutableVertexSet.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// A mutable set of vertices
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertices</typeparam>
    public interface IMutableVertexSet<TVertex> : IVertexSet<TVertex>
    {
        /// <summary>
        /// Adds a vertex
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>
        /// <c>true</c> if the vertex was added; <c>false</c> otherwise
        /// </returns>
        bool AddVertex(TVertex v);

        /// <summary>
        /// Adds the vertices of the given collection
        /// </summary>
        /// <param name="vertices">The vertices</param>
        /// <returns>The number of succesfully added vertices</returns>
        int AddVertexRange(IEnumerable<TVertex> vertices);

        /// <summary>
        /// Removes a vertex
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>
        /// <c>true</c> if the vertex was removed; <c>false</c> otherwise
        /// </returns>
        bool RemoveVertex(TVertex v);
    }
}

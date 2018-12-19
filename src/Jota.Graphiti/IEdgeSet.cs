// <copyright file="IEdgeSet.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// A set of edges
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertices in the edges</typeparam>
    /// <typeparam name="TEdge">The type of the edges</typeparam>
    public interface IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Gets a value indicating whether this is empty or not
        /// </summary>
        bool HasNoEdges { get; }

        /// <summary>
        /// Gets the number of elements contained in this
        /// </summary>
        int EdgeCount { get; }

        /// <summary>
        /// Gets an enumeration of the edges in this
        /// </summary>
        IEnumerable<TEdge> Edges { get; }

        /// <summary>
        /// Determines whether a given edge is container in this.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <returns>
        /// <c>true</c> if this contains the edge; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsEdge(TEdge edge);
    }
}

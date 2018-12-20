// <copyright file="IVertexEdges.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// A set of edges associated to a vertex
    /// </summary>
    /// <typeparam name="TVertex">The vertex type</typeparam>
    /// <typeparam name="TEdge">The vertices type</typeparam>
    public interface IVertexEdges<TVertex, TEdge>
        : ISet<TEdge>,
        IShallowCloneable<IVertexEdges<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Gets the edge comparer
        /// </summary>
        IEqualityComparer<TEdge> EdgeComparer { get; }
    }
}

// <copyright file="IVertexAndEdgeSet.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    /// <summary>
    /// A set of vertices and edges
    /// </summary>
    /// <typeparam name="TVertex">The vertices type</typeparam>
    /// <typeparam name="TEdge">The edges type</typeparam>
    public interface IVertexAndEdgeSet<TVertex, TEdge>
        : IVertexSet<TVertex>,
        IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
    }
}

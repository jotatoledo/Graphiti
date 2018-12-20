// <copyright file="IVertexEdgeDictionary.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the association between a vertex and a set of edges
    /// </summary>
    /// <typeparam name="TVertex">The vertex type</typeparam>
    /// <typeparam name="TEdge">The edges type</typeparam>
    public interface IVertexEdgeDictionary<TVertex, TEdge>
        : IDictionary<TVertex, IVertexEdges<TVertex, TEdge>>,
        IShallowCloneable<IVertexEdgeDictionary<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
    }
}

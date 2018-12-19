// <copyright file="IDirectedGraph.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// Behavior contract for a directed graph
    /// </summary>
    /// <typeparam name="TVertex">The vertices type</typeparam>
    /// <typeparam name="TEdge">The edges type</typeparam>
    public interface IDirectedGraph<TVertex, TEdge>
        : IGraph<TVertex, TEdge>,
        IShallowCloneable<IDirectedGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Determines whether there are out-edges associated to <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="v"/> has no out-edges; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when the given vertex is not present</exception>
        bool HasOutEdges(TVertex v);

        /// <summary>
        /// Gets the count of out-edges of <paramref name="v"/>
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <returns>The count of out-edges of <paramref name="v"/></returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when the given vertex is not present</exception>
        int OutDegree(TVertex v);

        /// <summary>
        /// Gets the out-edges of <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>An enumeration of the out-edges of <paramref name="v"/></returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when this does not contain the given vertex</exception>
        IEnumerable<TEdge> OutEdges(TVertex v);

        /// <summary>
        /// Determines whether there are in-edges associated to <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="v"/> has no in-edges; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when the given vertex is not present</exception>
        bool HasInEdges(TVertex v);

        /// <summary>
        /// Gets the count of in-edges of <paramref name="v"/>
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <returns>The count of in-edges of <paramref name="v"/></returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when the given vertex is not present</exception>
        int InDegree(TVertex v);

        /// <summary>
        /// Gets the in-edges of <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>An enumeration of the in-edges of <paramref name="v"/></returns>
        /// <exception cref="VertexNotFoundException{TVertex}">Thrown when the given vertex is not present</exception>
        IEnumerable<TEdge> InEdges(TVertex v);
    }
}

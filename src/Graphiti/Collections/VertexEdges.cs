// <copyright file="VertexEdges.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// Internal implementation of <see cref="IVertexEdges{TVertex, TEdge}"/> based on <see cref="HashSet{T}"/>
    /// </summary>
    /// <typeparam name="TVertex">The vertices type</typeparam>
    /// <typeparam name="TEdge">The edges type</typeparam>
    internal sealed class VertexEdges<TVertex, TEdge> : HashSet<TEdge>, IVertexEdges<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexEdges{TVertex, TEdge}"/> class
        /// with default equality comparer.
        /// </summary>
        public VertexEdges()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexEdges{TVertex, TEdge}"/> class
        /// with the given equality comparer
        /// </summary>
        /// <param name="equalityComparer">The equality comparer</param>
        public VertexEdges(IEqualityComparer<TEdge> equalityComparer)
            : base(equalityComparer)
        {
        }

        private VertexEdges(VertexEdges<TVertex, TEdge> source)
            : base(source, source.Comparer)
        {
        }

        /// <inheritdoc/>
        public IEqualityComparer<TEdge> EdgeComparer => this.Comparer;

        /// <inheritdoc/>
        public IVertexEdges<TVertex, TEdge> Clone()
        {
            return new VertexEdges<TVertex, TEdge>(this);
        }
    }
}

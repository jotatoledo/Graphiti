// <copyright file="VertexEdgeDictionary.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// Internal implementation of <see cref="IVertexEdgeDictionary{TVertex, TEdge}"/> based on <see cref="Dictionary{TKey, TValue}"/>
    /// </summary>
    /// <typeparam name="TVertex">The vertices type</typeparam>
    /// <typeparam name="TEdge">The edges type</typeparam>
    internal sealed class VertexEdgeDictionary<TVertex, TEdge>
        : Dictionary<TVertex, IVertexEdges<TVertex, TEdge>>,
        IVertexEdgeDictionary<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexEdgeDictionary{TVertex, TEdge}"/> class.
        /// </summary>
        public VertexEdgeDictionary()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexEdgeDictionary{TVertex, TEdge}"/> class
        /// with the given initial capacity
        /// </summary>
        /// <param name="capacity">Initial capacity</param>
        public VertexEdgeDictionary(int capacity)
            : base(capacity)
        {
        }

        /// <inheritdoc/>
        public IVertexEdgeDictionary<TVertex, TEdge> Clone()
        {
            var clone = new VertexEdgeDictionary<TVertex, TEdge>(this.Count);
            foreach (var pair in this)
            {
                // Ensure that the mapped edge sets are shallow cloned
                clone.Add(pair.Key, pair.Value.Clone());
            }

            return clone;
        }
    }
}

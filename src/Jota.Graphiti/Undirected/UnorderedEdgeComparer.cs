// <copyright file="UnorderedEdgeComparer.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of <see cref="IEqualityComparer{T}"/> that handles edges as unordered pairs
    /// during the equality evaluation
    /// </summary>
    /// <typeparam name="TVertex">The vertex type</typeparam>
    /// <typeparam name="TEdge">The edge type</typeparam>
    internal sealed class UnorderedEdgeComparer<TVertex, TEdge> : IEqualityComparer<TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <inheritdoc/>
        public bool Equals(TEdge x, TEdge y) => x.Equals(y)
            || (x.Source.Equals(y.Target) && x.Target.Equals(y.Source));

        /// <inheritdoc/>
        public int GetHashCode(TEdge obj) => obj.GetHashCode();
    }
}

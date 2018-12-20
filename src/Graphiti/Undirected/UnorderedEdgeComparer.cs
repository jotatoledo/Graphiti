// <copyright file="UnorderedEdgeComparer.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    using System.Collections.Generic;

    /// <summary>
    /// Equates edges as unordered pairs
    /// </summary>
    /// <typeparam name="TVertex">The vertex type</typeparam>
    /// <typeparam name="TEdge">The edge type</typeparam>
    internal sealed class UnorderedEdgeComparer<TVertex, TEdge> : IEqualityComparer<TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <inheritdoc/>
        public bool Equals(TEdge x, TEdge y) => x.Equals(y) || x.Equals(y.Invert());

        /// <inheritdoc/>
        /// <remarks>
        /// Based on https://stackoverflow.com/a/70375/5394220
        /// </remarks>
        public int GetHashCode(TEdge obj) => obj.Source.GetHashCode() ^ obj.Target.GetHashCode();
    }
}

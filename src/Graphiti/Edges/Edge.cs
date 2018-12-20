// <copyright file="Edge.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    using System;

    /// <summary>
    /// Default implementation of <see cref="IEdge{TVertex}"/>
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertices</typeparam>
    [Serializable]
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public sealed class Edge<TVertex> : AbstractEdge<TVertex>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Edge{TVertex}"/> class.
        /// </summary>
        /// <param name="source">Source vertex</param>
        /// <param name="target">Target vertex</param>
        public Edge(TVertex source, TVertex target)
            : base(source, target)
        {
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Edge<TVertex> other && base.Equals(other);

        /// <inheritdoc/>
        public override IEdge<TVertex> Invert() => new Edge<TVertex>(this.Target, this.Source);
    }
}

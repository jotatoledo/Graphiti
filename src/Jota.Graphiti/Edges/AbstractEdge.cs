// <copyright file="AbstractEdge.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti
{
    using System;

    /// <summary>
    /// Base class for implemenations of the <see cref="IEdge{TVertex}"/>
    /// </summary>
    /// <typeparam name="TVertex">The vertices type</typeparam>
    public abstract class AbstractEdge<TVertex> : IEdge<TVertex>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractEdge{TVertex}"/> class
        /// with the given vertices.
        /// </summary>
        /// <param name="source">The source vertex</param>
        /// <param name="target">The target vertex</param>
        public AbstractEdge(TVertex source, TVertex target)
        {
            this.Source = source;
            this.Target = target;
        }

        /// <inheritdoc/>
        public TVertex Source { get; }

        /// <inheritdoc/>
        public TVertex Target { get; }

        /// <inheritdoc/>
        public abstract IEdge<TVertex> Invert();

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is AbstractEdge<TVertex> other
                && this.Source.Equals(other.Source)
                && this.Target.Equals(other.Target);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Tuple.Create(this.Source, this.Target).GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"({this.Source},{this.Target})";
        }
    }
}

// <copyright file="WeightedEdge.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti
{
    using System;

    /// <summary>
    /// Default implementation of <see cref="IWeightedEdge{TVertex}"/>
    /// </summary>
    /// <typeparam name="TVertex">Type of the vertices</typeparam>
    [Serializable]
    public class WeightedEdge<TVertex> : AbstractEdge<TVertex>, IWeightedEdge<TVertex>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedEdge{TVertex}"/> class with default weight
        /// </summary>
        /// <param name="source">The source vertex</param>
        /// <param name="target">The target vertex</param>
        public WeightedEdge(TVertex source, TVertex target)
            : this(default(double), source, target)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedEdge{TVertex}"/> class.
        /// </summary>
        /// <param name="weight">The weight</param>
        /// <param name="source">The source vertex</param>
        /// <param name="target">The target vertex</param>
        public WeightedEdge(double weight, TVertex source, TVertex target)
            : base(source, target)
        {
        }

        /// <inheritdoc/>
        public double Weight { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is WeightedEdge<TVertex> && base.Equals(obj);

        /// <inheritdoc/>
        public override IEdge<TVertex> Invert() => new WeightedEdge<TVertex>(this.Weight, this.Target, this.Source);
    }
}

// <copyright file="IWeightedEdge.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti
{
    /// <summary>
    /// An edge with a numeric weight
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertices</typeparam>
    public interface IWeightedEdge<TVertex> : IEdge<TVertex>
    {
        /// <summary>
        /// Gets or sets the weight of the edge
        /// </summary>
        double Weight { get; set; }
    }
}

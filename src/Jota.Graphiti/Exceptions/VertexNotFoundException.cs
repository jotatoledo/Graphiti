// <copyright file="VertexNotFoundException.cs" company="jotatoledon@gmail.com">
// Copyright (c) jotatoledon@gmail.com. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Jota.Graphiti
{
    using System;

    /// <summary>
    /// Thrown when a vertex is expected to exist in data structure but it does not.
    /// </summary>
    /// <typeparam name="TVertex">The vertex type</typeparam>
    public class VertexNotFoundException<TVertex> : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexNotFoundException{TVertex}"/> class.
        /// </summary>
        /// <param name="vertex">The vertex</param>
        public VertexNotFoundException(TVertex vertex)
            : base("Vertex not found")
        {
            this.Vertex = vertex;
        }

        /// <summary>
        /// Gets the vertex value
        /// </summary>
        public TVertex Vertex { get; }
    }
}

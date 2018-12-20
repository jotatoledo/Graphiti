// <copyright file="TestVertex.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti.Test
{
    public struct TestVertex
    {
        private TestVertex(char id)
        {
            this.Id = id;
        }

        public char Id { get; }

        public static TestVertex Wrap(char id) => new TestVertex(id);

        public override bool Equals(object obj) => obj is TestVertex other && this.Id.Equals(other.Id);

        public override int GetHashCode() => this.Id.GetHashCode();

        public override string ToString() => $"Vertex: {this.Id}";
    }
}

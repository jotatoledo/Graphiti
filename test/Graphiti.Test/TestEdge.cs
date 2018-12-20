// <copyright file="TestEdge.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti.Test
{
    public sealed class TestEdge : AbstractEdge<TestVertex>
    {
        public TestEdge(TestVertex source, TestVertex target)
            : base(source, target)
        {
        }

        public static TestEdge Wrap(char source, char target) => new TestEdge(TestVertex.Wrap(source), TestVertex.Wrap(target));

        public override IEdge<TestVertex> Invert() => new TestEdge(this.Target, this.Source);
    }
}

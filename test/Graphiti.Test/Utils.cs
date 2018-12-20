// <copyright file="Utils.cs" company="Graphiti">
// Copyright (c) Graphiti. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Graphiti.Test
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class Utils
    {
        internal static IEnumerable<TestVertex> ExtractDistinctVertices(IEnumerable<TestEdge> edges) => edges
                .SelectMany(edge => new[] { edge.Source, edge.Target })
                .Distinct();
    }
}

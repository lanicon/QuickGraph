﻿using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredIncidenceGraph<Vertex, Edge, Graph> :
        FilteredImplicitGraph<Vertex,Edge,Graph>,
        IIncidenceGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
        where Graph : IIncidenceGraph<Vertex,Edge>
    {
        public FilteredIncidenceGraph(
            Graph baseGraph,
            IVertexPredicate<Vertex> vertexPredicate,
            IEdgePredicate<Vertex,Edge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        {}

        public bool ContainsEdge(Vertex source, Vertex target)
        {
            if (!this.VertexPredicate.Test(source))
                return false;
            if (!this.VertexPredicate.Test(target))
                return false;

            foreach (Edge edge in this.BaseGraph.OutEdges(source))
                if (edge.Target.Equals(target) && this.EdgePredicate.Test(edge))
                    return true;
            return false;
        }

        public bool TryGetEdge(
            Vertex source,
            Vertex target,
            out Edge edge)
        {
            IEnumerable<Edge> unfilteredEdges;
            if (this.VertexPredicate.Test(source) &&
                this.VertexPredicate.Test(target) &&
                this.BaseGraph.TryGetEdges(source, target, out unfilteredEdges))
            {
                foreach (Edge ufe in unfilteredEdges)
                    if (this.EdgePredicate.Test(ufe))
                    {
                        edge = ufe;
                        return true;
                    }
            }
            edge = default(Edge);
            return false;
        }

        public bool TryGetEdges(
            Vertex source,
            Vertex target,
            out IEnumerable<Edge> edges)
        {
            edges = null;
            if (!this.VertexPredicate.Test(source))
                return false;
            if (!this.VertexPredicate.Test(target))
                return false;

            IEnumerable<Edge> unfilteredEdges;
            if (this.BaseGraph.TryGetEdges(source, target, out unfilteredEdges))
            {
                List<Edge> filtered = new List<Edge>();
                foreach (Edge edge in unfilteredEdges)
                    if (this.EdgePredicate.Test(edge))
                        filtered.Add(edge);
                edges = filtered;
                return true;
            }

            return false;
        }
    }
}

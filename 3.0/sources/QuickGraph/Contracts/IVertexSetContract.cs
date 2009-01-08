﻿#if CONTRACTS_FULL
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IVertexSet<>))]
    sealed class IVertexSetContract<TVertex>
        : IVertexSet<TVertex>
    {
        bool IVertexSet<TVertex>.IsVerticesEmpty
        {
            get 
            {
                IVertexSet<TVertex> ithis = this;
                Contract.Ensures(Contract.Result<bool>() == (ithis.VertexCount == 0));

                return default(bool);
            }
        }

        int IVertexSet<TVertex>.VertexCount
        {
            get
            {
                IVertexSet<TVertex> ithis = this;
                Contract.Ensures(Contract.Result<int>() == Enumerable.Count(ithis.Vertices));

                return default(int);
            }
        }

        IEnumerable<TVertex> IVertexSet<TVertex>.Vertices
        {
            get 
            {
                Contract.Ensures(Contract.Result<IEnumerable<TVertex>>() != null);

                return default(IEnumerable<TVertex>);
            }
        }

        [Pure]
        bool IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            IVertexSet<TVertex> ithis = this;
            Contract.Requires(vertex != null);
            Contract.Ensures(Contract.Result<bool>() == Contract.Exists(ithis.Vertices, v => v.Equals(vertex)));

            return default(bool);
        }
    }
}
#endif

using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.MinimumSpanningTree
{
    public class Kruskal
    {
        public static Dictionary<string, int> MinimumSpanningTree(UndirectedWeightedGraph graph)
        {
            var mst = new Dictionary<string, int>();
            var disjointSet = new DisjointSet<string>();

            foreach (var vertex in graph.GetVertices())
            {
                disjointSet.MakeSet(vertex);
            }

            var edges = graph.GetEdges().OrderBy(e => e.Item3).ToList();

            foreach (var edge in edges)
            {
                var root1 = disjointSet.Find(edge.Item1);
                var root2 = disjointSet.Find(edge.Item2);

                if (root1 != root2)
                {
                    mst.TryAdd(edge.Item1 + "-" + edge.Item2, edge.Item3);
                    disjointSet.Union(root1, root2);
                }
            }

            return mst;
        }
    }

        public class DisjointSet<T>
        {
            private Dictionary<T, T> _parent;
            private Dictionary<T, int> _rank;

            public DisjointSet()
            {
                _parent = new Dictionary<T, T>();
                _rank = new Dictionary<T, int>();
            }

            public void MakeSet(T item)
            {
                if (!_parent.ContainsKey(item))
                {
                    _parent.Add(item, item);
                    _rank.Add(item, 0);
                }
            }

            public T Find(T item)
            {
                if (!_parent[item].Equals(item))
                {
                    _parent[item] = Find(_parent[item]);
                }
                return _parent[item];
            }

            public void Union(T item1, T item2)
            {
                var root1 = Find(item1);
                var root2 = Find(item2);

                if (!root1.Equals(root2))
                {
                    if (_rank[root1] < _rank[root2])
                    {
                        _parent[root1] = root2;
                    }
                    else if (_rank[root1] > _rank[root2])
                    {
                        _parent[root2] = root1;
                    }
                    else
                    {
                        _parent[root2] = root1;
                        _rank[root1]++;
                    }
                }
            }
    }
}

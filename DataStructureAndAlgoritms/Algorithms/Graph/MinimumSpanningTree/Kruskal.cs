using DataStructureAndAlgorithms.DataStructures.Graph;
using DataStructureAndAlgorithms.DataStructures.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.MinimumSpanningTree
{
    //usa DisjointSet
    public class Kruskal
    {
        public static Dictionary<string, int> MinimumSpanningTree(UndirectedWeightedGraph graph)
        {
            var mst = new Dictionary<string, int>();
            var disjointSet = new DisjointSet<string>();

            // cada vertice es su propio padre
            foreach (var vertex in graph.GetVertices())
            {
                disjointSet.MakeSet(vertex);
            }

            // crea una cola de prioridad para almacenar las aristas
            var priorityQueue = new PriorityQueue<(string, string, int), int>();

            // agrega todas las aristas del grafo a la cola de prioridad
            foreach (var edge in graph.GetEdges())
            {
                priorityQueue.Enqueue(edge, edge.Item3);
            }

            while (priorityQueue.Count > 0)
            {
                var edge = priorityQueue.Dequeue();
                var weight = edge.Item3;
                var node1 = edge.Item1;
                var node2 = edge.Item2;

                // se hara en base a jerarquia por lo que el primer elemento siempre es menor
                // en base al primer y segundo elemento se busca cual es el padre
                //el disjoint en si mismo evita la redundancia
                var root1 = disjointSet.Find(node1);
                var root2 = disjointSet.Find(node2);

                // a no haber jerarquia entra en el if
                if (root1 != root2)
                {
                    // se agrega al mst y se unen esos dos elemento creando una jerarquia,
                    // al tener root de 0 el segundo elemento sera hijo
                    mst.TryAdd(node1 + "-" + node2, weight);
                    disjointSet.Union(root1, root2);
                }
            }

            return mst;
        }
    }
}

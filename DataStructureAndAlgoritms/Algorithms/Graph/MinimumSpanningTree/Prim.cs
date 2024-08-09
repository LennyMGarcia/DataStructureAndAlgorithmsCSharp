using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.MinimumSpanningTree
{
    public class Prim
    {
        //comienza desde un nodo especifico, va desencolando y revisando si esta visitado hasta llegar al final
        //no es obtino para un mst global, pero si para un mst con referencia al nodo que se eligio
        public static Dictionary<string, int> MinimumSpanningTree(UndirectedWeightedGraph graph, string start)
        {
            var mst = new Dictionary<string, int>();
            var visited = new HashSet<string>();
            //se guardan y se comparan como tuplas
            var edges = new PriorityQueue<(string, string, int), (string, string, int)>(new EdgeComparer());

            // Agregamos el vertice de inicio al MST con peso 0
            mst.Add(start, 0);
            visited.Add(start);
            //Se usan los primero vecinos para la cola, se agregan segun prioridad
            foreach (var neighbor in graph.GetNeighbors(start))
            {
                edges.Enqueue((start, neighbor.Item1, neighbor.Item2), (start, neighbor.Item1, neighbor.Item2));
            }

            while (edges.Count > 0)
            {
                //la cola desencolara el elemento de menor peso primero
                var minEdge = edges.Dequeue();
                var v = minEdge.Item2;
                var weight = minEdge.Item3;

                //si no se visito item 2 o la siguiente relacion del elemento desencolado
                if (!visited.Contains(v))
                {
                    //se agrega al mst, y se pone como que e visito
                    mst.Add(v, weight);
                    visited.Add(v);
                    //Se buscan los vecinos del elemento v, y verifica que no se han visitado
                    foreach (var neighbor in graph.GetNeighbors(v))
                    {
                        //si no se visito el primer elemento del neighbord, lo visita
                        if (!visited.Contains(neighbor.Item1))
                        {
                            //se agregan a la cola, el primero es para agregar y el segundo para comparar,TElement y TPriority
                            edges.Enqueue((v, neighbor.Item1, neighbor.Item2), (v, neighbor.Item1, neighbor.Item2));
                        }
                    }
                }
            }

            return mst;
        }
    }

    public class EdgeComparer : IComparer<(string, string, int)>
    {
        //Compare es un metodo especifico que usa el priority, aquis e usan tuplas y se comparar el valor
        public int Compare((string, string, int) x, (string, string, int) y)
        {
            return x.Item3.CompareTo(y.Item3);
        }
    }
}

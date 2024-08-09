using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.MinimumSpanningTree
{
    //posible error en la implementacion
    public class Boruvka
    {
        public List<(string, string, int)> MinimumSpanningTree(UndirectedWeightedGraph graph)
        {
            var mst = new List<(string, string, int)>();
            var connectedComponents = new Dictionary<string, HashSet<string>>();
            var visited = new HashSet<string>();
            var priorityQueue = new PriorityQueue<(int, string, string), int>();

            // cada componente estara inicializado con su hashset
            foreach (var vertex in graph.GetVertices())
            {
                connectedComponents[vertex] = new HashSet<string> { vertex };
            }

            // mientras haya componente conectadon y sea mayor a 1
            while (connectedComponents.Count > 1)
            {
                // por cada valor en el hashset
                foreach (var component in connectedComponents.Values)
                {
                    // por cada string dentro del hash
                    foreach (var node in component)
                    {
                        //una ves visitado los nodos no debe volver a agregar mas
                        if (!visited.Contains(node))
                        {
                            visited.Add(node);
                            // se obtienen los vecinos de esos strings
                            //asegura todas las relaciones y los visited quita la redundancia luego de visitarse
                            foreach (var adjacent in graph.GetNeighbors(node))
                            {
                                if (!component.Contains(adjacent.Item1))
                                {
                                    priorityQueue.Enqueue((adjacent.Item2, node, adjacent.Item1), adjacent.Item2);
                                }
                            }
                        }
                    }
                }

                

                while (priorityQueue.Count > 0)
                {
                    var edge = priorityQueue.Dequeue();
                    var weight = edge.Item1;
                    var node1 = edge.Item2;
                    var node2 = edge.Item3;

                    // si estan ambos nodos
                    //se mas para ver si se puede realizar una relacion con ambos nodos
                    if (connectedComponents.ContainsKey(node1) && connectedComponents.ContainsKey(node2))
                    {
                        // si el conected componente del nodo 1 no contiene el nodo 2
                        if (!connectedComponents[node1].Contains(node2))
                        {
                            //explicacion mas detallada al final
                            mst.Add((node1, node2, weight));
                            /* de { node1, [node1, node3, node4] },
                              { node2, [node2, node5, node6] } pasa a  { node1, [node1, node3, node4, node2, node5, node6] },*/
                            connectedComponents[node1].UnionWith(connectedComponents[node2]);
                            connectedComponents.Remove(node2);
                        }
                    }
                }
            }

            return mst;
        }
    }
}

/*Explicacion detallada con ejemplos:
 * se buscan las relaciones y se ordenan: 
 * edges = [
    (2, "B", "D"),
    (3, "A", "C"),
    (4, "C", "D"),
    (5, "A", "B")
]
 * se utilizan los componentes que se verian asi:
 * connectedComponents = {
    { "A", ["A"] },
    { "B", ["B"] },
    { "C", ["C"] },
    { "D", ["D"] }
}
 * Se busca y se verifica si hay conexion con B y D en
 * (connectedComponents.ContainsKey(node1) && connectedComponents.ContainsKey(node2))
 * y si no contiene la fucion lo fuciona y se agrega al mst: 
 * 
   mst = [( "B", "D", 2 )]
   connectedComponents = {
    { "A", ["A"] },
    { "B", ["B", "D"] },
    { "C", ["C"] }
}
  * sigue con la otra que es (3, "A", "C"),
    mst = [( "B", "D", 2 ), ( "A", "C", 3 )]
    connectedComponents = {
    { "A", ["A", "C"] },
    { "B", ["B", "D"] }
}
   * y verifica la otra relacion  (4, "C", "D"),
    como no esta connectedComponents y solo queda A y B la descarta

   * Verifica la otra relacion (5, "A", "B")
   * Como esta entonces fuciona los elementos
   * mst = [( "B", "D", 2 ), ( "A", "C", 3 ), ( "A", "B", 5 )]
    connectedComponents = {
    { "A", ["A", "B", "C", "D"] }
    
    Aqui vemos como ya encontro todos los elementos posibles, sigue recorriendo la lista hasta que sea mayor a 1
    para evitar que se agreguen mas vertices innecesariamente agregue visited, aunque de igual forma se iban a conectar
    de manera redundante sin afectar el algoritmo
}
 */

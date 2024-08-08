using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.Search
{
    //se usa bucles para explorar por anchura
    public class BreadthFirstSearch
    {
        public List<string> Search(IGraph graph, string startVertex)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            var result = new List<string>();

            queue.Enqueue(startVertex);
            visited.Add(startVertex);

            while (queue.Count > 0)
            {
                //se agrega el vertice desencolado al resultado
                var vertex = queue.Dequeue();
                result.Add(vertex);
                // se buscan los vecinos mas cercanos y se revisan, usa IEnumerable
                foreach (var neighbor in graph.GetNeighbors(vertex))
                {
                    //Si el vecino no se ha visitado se pone a la cola y se anade a visited
                    var neighborVertex = neighbor.Item1;
                    if (!visited.Contains(neighborVertex))
                    {
                        queue.Enqueue(neighborVertex);
                        visited.Add(neighborVertex);
                    }
                }
            }
            //una ves se han visitados todos y la cola no tiene mas valores, se retorna
            return result;
        }
    }
}

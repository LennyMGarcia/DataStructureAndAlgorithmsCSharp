using DataStructureAndAlgorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.Algorithms.Graph.Others
{
    public class FordFulkerson
    {
        private Dictionary<string, Dictionary<string, int>> _residualGraph;
        private DirectedWeightedGraph _graph;

        public FordFulkerson(DirectedWeightedGraph graph)
        {
            _graph = graph;
            _residualGraph = CreateResidualGraph();
        }
        //se crea las representaciones suficientes para el grafo
        private Dictionary<string, Dictionary<string, int>> CreateResidualGraph()
        {
            var residualGraph = new Dictionary<string, Dictionary<string, int>>();
            //se obtienen las claves
            foreach (var vertex in _graph.GetVertices())
            {
                residualGraph[vertex] = new Dictionary<string, int>();
                //se obtienen los valores
                foreach (var adjacent in _graph.GetNeighbors(vertex))
                {
                    //se asigna en el segundo diccionario el valor
                    residualGraph[vertex][adjacent.Item1] = adjacent.Item2;
                    //si no esta la clave se le crea un diccionario
                    if (!residualGraph.ContainsKey(adjacent.Item1))
                    {
                        residualGraph[adjacent.Item1] = new Dictionary<string, int>();
                    }
                    //se declara las relaciones inversas como 0
                    //las relaciones inversa son importante para mantener el correcto flujo de ambos lados
                    residualGraph[adjacent.Item1][vertex] = 0;
                }
            }
            
            foreach (var vertex in _graph.GetVertices())
            {
                //si no estan las vetices las crea
                if (!residualGraph.ContainsKey(vertex))
                {
                    residualGraph[vertex] = new Dictionary<string, int>();
                }
            }

            return residualGraph;
        }

        public int MaxFlow(string source, string sink)
        {
            int maxFlow = 0;

            while (true)
            {
                //busca la ruta
                var path = FindAugmentingPath(source, sink);
                //ya no hay mas caminos que se puedan aumentar o la fuente esta vacia o inalcazable, si es asi retorna null
                if (path == null) break;
                //encuentra el flujo minimo
                int flow = CalculateFlow(path);
               
                //suma los flujo minimo
                maxFlow += flow;
                //lo mantiene actualizado
                //cuando este hace que el residual llegue a 0 se arroja el null de arriba
                UpdateResidualGraph(path, flow);
            }
            return maxFlow;
        }

        private List<(string, string)> FindAugmentingPath(string source, string sink)
        {
            var stack = new Stack<string>();
            var parent = new Dictionary<string, string>();

            stack.Push(source);
            parent[source] = null;


            while (stack.Count > 0)
            {
                var vertex = stack.Pop();

                if (vertex == sink) break;
                //el parent funciona como visited y guarda tambie la ruta
                //se usa el valor del stack como vertice
                foreach (var adjacent in _residualGraph[vertex])
                {
                    //si su valor es mayor a 0 (tiene capacidad disponible para recorrer) y no esta su clave
                    if (adjacent.Value > 0 && !parent.ContainsKey(adjacent.Key))
                    {
                        //se agrega el elemento  en el stack y se agrega el vetice en el diccionario
                        // el vetice sera el mismo para todos los elementos dentro del diccionario del residual
                        stack.Push(adjacent.Key);
                        parent[adjacent.Key] = vertex;
                       
                    }
                }
                //por cada elemento del vetice que vino del stack y que se encuentre en el grafo actual y no residual
                //Esto permite que el algoritmo explore nuevas aristas en el grafo original que no estaban presentes en el grafo residual
                foreach (var v in _graph._adjacent[vertex])
                {
                    //si el residual no contiene ese elemento del grafo real, el ponderado es mayor a 0 
                    //y parent no contiene ese elemento o no ha sido visitado
                    if (!_residualGraph[vertex].ContainsKey(v.Item1) && v.Item2 > 0 && !parent.ContainsKey(v.Item1))
                    {
                        //se agrega al stack y  el parent tendra el vertice sacado de la pila 
                        stack.Push(v.Item1);
                        parent[v.Item1] = vertex;
                    }
                }
            }

            if (!parent.ContainsKey(sink)) return null;
            
            var path = new List<(string, string)>();
            var current = sink;

            //mientras sera diferente del origen
            while (current != source)
            {
                //se maneja el camino, donde se guardara el valor del parent y el current actual
                path.Add((parent[current], current));
                //y el current tendra el valor del parent
                current = parent[current];
            }

            path.Reverse();
            return path;
        }

        private int CalculateFlow(List<(string, string)> path)
        {
            int flow = int.MaxValue;
            //por cada elemento de la ruta
            foreach (var edge in path)
            {
                //si la arista existe y contiene la segunda arista, el valor sera el minimo entre el maxvalue y el valor de la arista
                if (_residualGraph.ContainsKey(edge.Item1) && _residualGraph[edge.Item1].ContainsKey(edge.Item2))
                {
                    //se busca la referencia de los valores en el grafo residual y se elije el menor
                    // se va actualizando y siempre usara el flujo minimo que encontro como referencia
                    flow = Math.Min(flow, _residualGraph[edge.Item1][edge.Item2]);
                }
                else
                {
                    throw new Exception($"arista ({edge.Item1}, {edge.Item2}) no se encontraron en el grafo residual");
                }
            }
            return flow;
        }

        private void UpdateResidualGraph(List<(string, string)> path, int flow)
        {
             foreach (var edge in path)
             {
                //el valor del vertice relacionado se le reduce, siempre que se encuentre el valor de la vertice y contenga la vertice relacionada
                //Para cada arista, se reduce el valor del flujo residual en la direccion de la arista
                if (_residualGraph.ContainsKey(edge.Item1) && _residualGraph[edge.Item1].ContainsKey(edge.Item2))
                {
                    /*
                     * La razon por la que se reduce el flujo residual es que se ha encontrado un camino aumentante que permite enviar mas flujo 
                     * a traves de la red. Al reducir el flujo residual en la direccion de la arista, se refleja que se ha utilizado parte de la 
                     * capacidad de la arista para enviar flujo
                     */
                    _residualGraph[edge.Item1][edge.Item2] -= flow;
                }
                else
                {
                    // Agregar la arista al grafo residual con flujo residual 0
                    if (!_residualGraph.ContainsKey(edge.Item1))
                    {
                        _residualGraph[edge.Item1] = new Dictionary<string, int>();
                    }
                    _residualGraph[edge.Item1][edge.Item2] = 0;
                }
                //si contiene el diccionario
                if (_residualGraph.ContainsKey(edge.Item2))
                {
                    //se busca busca en el dicionario que se contenga el segundo valor del residual y se aumenta
                    // el valor residual si apunta al primer elemento se considera inverso, 
                    if (_residualGraph[edge.Item2].ContainsKey(edge.Item1))
                    {
                        /*La razon por la que se aumenta el flujo residual en la direccion inversa es que se ha creado una arista inversa
                         * en el grafo residual que permite devolver flujo a traves de la red. Al aumentar el flujo residual en la direccion 
                         * inversa, se refleja que se ha creado una oportunidad para enviar flujo en la direccion opuesta
                         */
                        _residualGraph[edge.Item2][edge.Item1] += flow;
                    }
                    
                    else
                    {
                        //si no esta se le asigna, ya que inversamente necesita pasar lo mismo
                        _residualGraph[edge.Item2][edge.Item1] = flow;
                    }
                }
                // si el diccionario no esta se crea uno y se le anade el flujo
                else
                {
                    //crea la relacion inversa para que pueda ser asignada
                    _residualGraph[edge.Item2] = new Dictionary<string, int>();
                    _residualGraph[edge.Item2][edge.Item1] = flow;
                }
            }
        }
    }
}


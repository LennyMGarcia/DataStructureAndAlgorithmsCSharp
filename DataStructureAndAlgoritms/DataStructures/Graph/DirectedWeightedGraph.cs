using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructureAndAlgorithms.DataStructures.Graph
{

    public class DirectedWeightedGraph : IGraph
    {
        //Se veria asi: "A": { ("B", 5), ("C", 3) }, el hashset evita duplicados y tiene insersion y eliminacion rapida
        public Dictionary<string, HashSet<(string, int)>> _adjacent;

        public DirectedWeightedGraph()
        {
            //se anade un dictionary al llamarse, asi no hay que declararlo en cada metodo
            _adjacent = new Dictionary<string, HashSet<(string, int)>>();
        }
        public void AddVertex(string vertex)
        {
            if (!_adjacent.ContainsKey(vertex))
            {
                _adjacent[vertex] = new HashSet<(string, int)>();
            }
        }

        public void AddEdge(string origin, string destination, int weight)
        {
            //si contiene los valores de los vertices entonces agrega el vertice
            if (_adjacent.ContainsKey(origin) && _adjacent.ContainsKey(destination))
            {
                //Las tuplas ayudan mucho ya que son faciles de usar, crear y son inmutables (())
                _adjacent[origin].Add((destination, weight));
            }
            else
            {
                throw new Exception("El origen o el destino no existe");
            }
        }

        public void DeleteEdge(string origin, string destination)
        {
            if (_adjacent.ContainsKey(origin))
            {
                //se elimina cuando el primer elemento del hashtable es igual al destino
                _adjacent[origin].RemoveWhere(adjacent => adjacent.Item1 == destination);
            }
            else
            {
                throw new Exception("El origen no existe");
            }
        }
        //
        public void EditWeight(string origin, string destination, int newWeight)
        {
            if (_adjacent.ContainsKey(origin) && _adjacent.ContainsKey(destination))
            {
                //devuelve el primer elemento que cumpla con la condicion
                var edge = _adjacent[origin].FirstOrDefault(adjacent => adjacent.Item1 == destination);
                if (edge != default)
                {
                    //se recrea 
                    _adjacent[origin].Remove(edge);
                    _adjacent[origin].Add((destination, newWeight));
                }
                else
                {
                    throw new Exception("Arista no existe");
                }
            }
            else
            {
                throw new Exception("El origen no existe");
            }
        }

        public int GetWeight(string origin, string destination)
        {
            if (_adjacent.ContainsKey(origin))
            {
                foreach (var adjacent in _adjacent[origin])
                {
                    //da vueltas en los valores del dictionary y si encuentra el valor agrega el peso
                    if (adjacent.Item1 == destination)
                    {
                        return adjacent.Item2;
                    }
                }
            }
            throw new Exception("Arista no existe");
        }

        public IEnumerable<string> GetVertices()
        {
            return _adjacent.Keys;
        }

        public IEnumerable<(string, int)> GetNeighbors(string vertex)
        {
            return _adjacent[vertex];
        }

        public List<(string, string, int)> GetEdges()
        {
            var edges = new List<(string, string, int)>();
            foreach (var vertex in _adjacent)
            {
                //agrega los valores del hashtable y tambie el key del dictionary
                foreach (var adjacent in vertex.Value)
                {
                    edges.Add((vertex.Key, adjacent.Item1, adjacent.Item2));
                }
            }
            return edges;
        }
    }
}

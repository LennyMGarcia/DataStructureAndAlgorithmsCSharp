﻿using DataStructureAndAlgorithms.DataStructures.Graph;
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

            //cada componente estara inicializado con su hashset
            foreach (var vertex in graph.GetVertices())
            {
                connectedComponents[vertex] = new HashSet<string> { vertex };
            }

            //mientras haya componente conectado
            while (connectedComponents.Count > 1)
            {
                var edges = new List<(int, string, string)>();

                //por cada valor en el hashset
                foreach (var component in connectedComponents.Values)
                {
                 //por cada string dentro del hash
                    foreach (var node in component)
                    {
                       //se obtienen los vecinos de esos strings
                        foreach (var adjacent in graph.GetNeighbors(node))
                        {
                            //si ya se contiene ese vecino, entonces de vuelta a los ciclos
                            //si no esta, se agrega a la lista,
                            if (!component.Contains(adjacent.Item1))
                            {
                                edges.Add((adjacent.Item2, node, adjacent.Item1));
                            }
                        }
                    }
                }

               //usa quicksort o mergesort para hcerlo
                edges.Sort((a, b) => a.Item1.CompareTo(b.Item1));

                
                foreach (var edge in edges)
                {
                    var weight = edge.Item1;
                    var node1 = edge.Item2;
                    var node2 = edge.Item3;

                    //si estan ambos nodos
                    if (connectedComponents.ContainsKey(node1) && connectedComponents.ContainsKey(node2))
                    {
                        // si el conected componente del nodo 1 no contiene el nodo 2
                        if (!connectedComponents[node1].Contains(node2))
                        {
                           
                            mst.Add((node1, node2, weight));

                           
                            var component1 = connectedComponents[node1];
                            var component2 = connectedComponents[node2];
                            component1.UnionWith(component2);
                            connectedComponents.Remove(node2);
                        }
                    }
                }
            }

            return mst;
        }
    }
}

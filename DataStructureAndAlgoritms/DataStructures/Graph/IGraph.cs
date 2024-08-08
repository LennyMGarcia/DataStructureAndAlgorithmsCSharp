using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.DataStructures.Graph
{
    //util para trabajar con ambos grafos
    public interface IGraph
    {
        IEnumerable<string> GetVertices();
        IEnumerable<(string, int)> GetNeighbors(string vertex);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureAndAlgorithms.DataStructures.Advanced
{
    //cada elemento tiene su representante, el representante puede ser si mismo, parece un HATEOAS, pero con jerarquia y relacion dinamica
    // un disjoint set se asemeja mas a una colección de conjuntos disjuntos, se relacionan por cierta jerarquia
    // La relacion de padre-hijo entre los elementos no es necesariamente una relacion de árbol, sino mas bien una relacion de "pertenece a" o "es parte de"
    public class DisjointSet<T>
    {
        private Dictionary<T, T> _parent;
        private Dictionary<T, int> _rank;

        public DisjointSet()
        {
            _parent = new Dictionary<T, T>();
            _rank = new Dictionary<T, int>();
        }
        //crea un set
        public void MakeSet(T item)
        {
            if (!_parent.ContainsKey(item))
            {
                _parent.Add(item, item);
                _rank.Add(item, 0);
            }
        }
        //Busca su representante
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
                // si es de menor rango, el padre es el segundo
                if (_rank[root1] < _rank[root2])
                {
                    _parent[root1] = root2;
                }
                //si es de mayor rango, el padre es el primero
                else if (_rank[root1] > _rank[root2])
                {
                    _parent[root2] = root1;
                }
                //si ambos son los padres vuelve al segundo el hijo del primero
                else
                {
                    _parent[root2] = root1;
                    _rank[root1]++;
                }
            }
        }
    }
}

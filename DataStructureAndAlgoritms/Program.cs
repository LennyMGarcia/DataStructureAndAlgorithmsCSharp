using System.Linq;
using DataStructureAndAlgorithms.Models;
using DataStructureAndAlgorithms.Data;
using DataStructureAndAlgorithms.DataStructures.Basics;
using DataStructureAndAlgorithms.DataStructures.Trees;
using DataStructureAndAlgorithms.Algorithms.Sorting;
using DataStructureAndAlgorithms.Algorithms.Search;
using DataStructureAndAlgorithms.TestData;
using System.Collections.Generic;
using DataStructureAndAlgorithms.DataStructures.Graph;
using DataStructureAndAlgorithms.Algorithms.Graph.Others;
using DataStructureAndAlgorithms.Algorithms.Graph.ShortestPath;
// See https://aka.ms/new-console-template for more information
/*using ShopContext context = new ShopContext();

var customers = context.Customers
                    .OrderBy(p => p.CustomerName);
foreach (Customer customer in customers)
{
    Console.WriteLine($"id:     {customer.CustomerId}");
    Console.WriteLine($"Name:     {customer.CustomerName}");
    Console.WriteLine($"Email:     {customer.Email}");
    Console.WriteLine(new String('-', 20));
}*/
var objetoAnonimo = new
{
    Propiedad1 = "Valor1",
    Propiedad2 = 3,
    Propiedad3 = true
};

var objetoAnonimo2 = new
{
    Propiedad1 = "Valor2",
    Propiedad2 = 2,
    Propiedad3 = true
};

var objetoAnonimo3 = new
{
    Propiedad1 = "Valor3",
    Propiedad2 = 1,
    Propiedad3 = true
};

Console.WriteLine("SINGLE LINKED LIST");

SingleLinkedList linkedList = new SingleLinkedList();
linkedList.AddLast(objetoAnonimo);
linkedList.AddLast(objetoAnonimo2);
linkedList.AddLast(objetoAnonimo3);
linkedList.PrintList(); 
linkedList.AddAt(1, objetoAnonimo);
linkedList.PrintList(); 
linkedList.DeleteFirst();
linkedList.PrintList(); 
linkedList.DeleteLast();
linkedList.PrintList(); 
linkedList.DeleteAt(1);
linkedList.PrintList();
Console.WriteLine(new String('-', 20));

//----------------------------------------------------
Console.WriteLine("NODE STACK");
NodeStack<int> stack = new NodeStack<int>();
stack.Push(1);
stack.Push(2);
stack.Push(3);

Console.WriteLine("Size: " + stack.Size()); 
Console.WriteLine("Peek: " + stack.Peek()); 

int popped = stack.Pop();
Console.WriteLine("Popped: " + popped); 
Console.WriteLine("Size: " + stack.Size()); 

stack.Clear();
Console.WriteLine("Size: " + stack.Size()); 
Console.WriteLine("Is Empty: " + stack.IsEmpty()); 
Console.WriteLine(new String('-', 20));

//------------------------------------------------------------------
Console.WriteLine("SIMPLE QUEUE");
SimpleQueue<int> queue = new SimpleQueue<int>();

Console.WriteLine("Cola vacía: " + queue.IsEmpty);

queue.Enqueue(1);
queue.Enqueue(2);
queue.Enqueue(3);
queue.Enqueue(4);
queue.Enqueue(5);

Console.WriteLine("Elementos en la cola: " + queue.Count);

Console.WriteLine("Primer elemento: " + queue.Peek());

int dequeued = queue.Dequeue();
Console.WriteLine("Desencolado: " + dequeued);

Console.WriteLine("Elementos en la cola: " + queue.Count);

while (!queue.IsEmpty)
{
    dequeued = queue.Dequeue();
    Console.WriteLine("Desencolado: " + dequeued);
}

Console.WriteLine("Cola vacía: " + queue.IsEmpty);
Console.WriteLine(new String('-', 20));

//----------------------------------------------------


Person[] people = new Person[]
        {
            new Person("John", 25),
            new Person("Alice", 30),
            new Person("Bob", 20),
            new Person("Eve", 28),
            new Person("Charlie", 22)
        };

//---------------------------------------------------------------
Console.WriteLine("TREES");
BinarySearchTree<int> tree = new BinarySearchTree<int>();
//BinaryTree<int> tree = new BinaryTree<int>();
for (int i = 0; i < people.Length; i++)
{
    tree.Insert(people[i].Age);
    //tree.Add(people[i].Age);
}


//tree.PostOrderTraversal();
//tree.PreOrderTraversal();
//tree.InOrderTraversal(); 
tree.Print();
//tree.Print();
tree.Delete(25);
Console.WriteLine("---------");
tree.Print();
//tree.Print();

Console.WriteLine(new String('-', 20));
//----------------------------------------------------------------
Console.WriteLine("SORTING");
//BubbleSort.Sort("Age", people);
//SelectionSort.Sort("Age", people);
//InsertionSort.Sort("Age", people);
//IterativeMergeSort.Sort("Age", people);
//RecursiveMergeSort.Sort("Age", people);
//QuickSort.Sort("Age", people);
HeapSort.Sort("Age", people);

foreach (var person in people)
{
    Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
}
Console.WriteLine(new String('-', 20));

//--------------------------------------------------------------------------------
//  SEARCH  
Console.WriteLine("SEARCH");
Person target = new Person("Alice", 30);
LinearSearch.Search(people, target, (p1, p2) => p1.Name == p2.Name && p1.Age == p2.Age);

BinarySearch.Search(people, 30, "Age");

Console.WriteLine(new String('-', 20));

//----------------------------------------------------------------------------------

AvlTree avltree = new AvlTree();

avltree.Insert(5);
avltree.Insert(3);
avltree.Insert(7);
avltree.Insert(2);
avltree.Insert(4);
avltree.Insert(6);
avltree.Insert(8);

avltree.PreOrderTraversal();
avltree.Print();

Console.WriteLine(new String('-', 20));

//---------------------------------------------------------------------------------------

DirectedWeightedGraph graph = new DirectedWeightedGraph();

graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");
graph.AddVertex("D");
graph.AddVertex("E");

graph.AddEdge("A", "B", 3);
graph.AddEdge("A", "D", 3);
graph.AddEdge("B", "C", 2);
graph.AddEdge("B", "D", 2);
graph.AddEdge("D", "C", 3);
graph.AddEdge("D", "E", 2);
graph.AddEdge("A", "C", 5);
graph.AddEdge("C", "E", 4);


int weight = graph.GetWeight("A", "B");
Console.WriteLine(weight); 

IEnumerable<string> vertices = graph.GetVertices();
Console.WriteLine(string.Join(", ", vertices)); 

List<(string, string, int)> edges = graph.GetEdges();
foreach (var edge in edges)
{
    Console.WriteLine($"({edge.Item1}, {edge.Item2}, {edge.Item3})");
}
Console.WriteLine(new String('-', 20));

UndirectedWeightedGraph ugraph = new UndirectedWeightedGraph();

ugraph.AddVertex("A");
ugraph.AddVertex("B");
ugraph.AddVertex("C");
ugraph.AddVertex("D");

ugraph.AddEdge("A", "B", 5);
ugraph.AddEdge("A", "C", 3);
ugraph.AddEdge("B", "D", 2);
ugraph.AddEdge("C", "D", 4);

Console.WriteLine("Vertices:");
foreach (var vertex in ugraph.GetVertices())
{
    Console.WriteLine(vertex);
}

Console.WriteLine("Aristas:");
foreach (var edge in ugraph.GetEdges())
{
    Console.WriteLine($"({edge.Item1}, {edge.Item2}, {edge.Item3})");
}

Console.WriteLine("Peso de la arista A-B: " + ugraph.GetWeight("A", "B"));
Console.WriteLine("Peso de las aristas C-D: " + ugraph.GetWeight("C", "D"));

Console.WriteLine(new String('-', 20));

FordFulkerson fordFulkerson = new FordFulkerson(graph);
int maxFlow = fordFulkerson.MaxFlow("A", "E");
Console.WriteLine("Flujo maximo: " + maxFlow);

var shortestPath = Dijkstra.ShortestPath(graph, "A", "E");
Console.WriteLine(string.Join(" -> ", shortestPath));

using System.Linq;
using DataStructureAndAlgorithms.Models;
using DataStructureAndAlgorithms.Data;
using DataStructureAndAlgorithms.DataStructures.Basics;
using DataStructureAndAlgorithms.Algorithms.Sorting;
using DataStructureAndAlgorithms.Algorithms.Search;
using DataStructureAndAlgorithms.TestData;
using System.Collections.Generic;
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

Console.WriteLine("SORTING");
Person[] people = new Person[]
        {
            new Person("John", 25),
            new Person("Alice", 30),
            new Person("Bob", 20),
            new Person("Eve", 28),
            new Person("Charlie", 22)
        };

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
int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
int target2 = 5;

BinarySearch.Search(people, 30, "Age");



﻿using System.Linq;
using DataStructureAndAlgorithms.Models;
using DataStructureAndAlgorithms.Data;
using DataStructureAndAlgorithms.DataStructures.Basics;
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
    Propiedad2 = 123,
    Propiedad3 = true
};

var objetoAnonimo2 = new
{
    Propiedad1 = "Valor2",
    Propiedad2 = 1232,
    Propiedad3 = true
};

var objetoAnonimo3 = new
{
    Propiedad1 = "Valor3",
    Propiedad2 = 1234,
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

using System;
using System.Diagnostics;
using System.Threading;

namespace Double_Linked_List
{
   class DoubleLinkedList<T>
   {
      #region//----------------Конструкция на Node-----------------
      public class Node<D>
      {
         private D character;
         private Node<D> nextChar;
         private Node<D> previous;
         private int position;

         public D Character
         {
            get { return character; }
            set { character = value; }
         }
         internal Node<D> NextChar
         {
            get { return nextChar; }
            set { nextChar = value; }
         }
         internal Node<D> Previous
         {
            get { return previous; }
            set { previous = value; }
         }

         public Node(D character, int count)
         {
            position = count;
            Character = character;
         }
         public Node(D character, Node<D> prevNodeTail, int count)
         {
            position = count;
            Character = character;
            prevNodeTail.NextChar = this;
         }

         public void AddToPrev_Recursion(Node<D> node)
         {
            //Всеки път се влиза в tail и в неговия previous се слага head-a
            if (node.nextChar.nextChar == null) { previous = node; }
            else { AddToPrev_Recursion(node.nextChar); }
         }
         public void PrintNext_Recursion()
         {
            Console.WriteLine(" " + character);
            Thread.Sleep(70);
            if (nextChar != null)
            {
               nextChar.PrintNext_Recursion();
            }
         }
         public void PrintPrev_Recursion()
         {
            Console.WriteLine(" " + character);
            Thread.Sleep(70);
            if (previous != null)
            {
               previous.PrintPrev_Recursion();
            }
         }
      }
      #endregion

      //------------------------------Head-Tail-------------------------------
      private int count;
      private Node<T> head;
      private Node<T> tail;

      public void AddNode(T character)
      {
         count++;
         Node<T> newNode = null;
         if (head == null)
         {
            head = new Node<T>(character, count);
            tail = head;
         }
         else
         {
            newNode = new Node<T>(character, tail, count);
            tail = newNode;
         }
         if (count > 1)
         {
            tail.AddToPrev_Recursion(head);
         }
      }
      public void Print()
      {
         head.PrintNext_Recursion();
         Console.WriteLine();
         tail.PrintPrev_Recursion();
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         Stopwatch watch = new Stopwatch();
         DoubleLinkedList<string> linkedList = new DoubleLinkedList<string>();
         Console.WriteLine(">> Double Linked List - collecting cars <<\n");
         Thread.Sleep(1000);

         watch.Start();

         linkedList.AddNode("Audi");
         linkedList.AddNode("BMW");
         linkedList.AddNode("Citroen");
         linkedList.AddNode("Mercedes");
         linkedList.AddNode("Honda");
         linkedList.AddNode("Suzuki");
         linkedList.AddNode("Wolksvagen");
         linkedList.AddNode("Lada");
         linkedList.AddNode("Ford");
         linkedList.AddNode("Peugeot");
         linkedList.AddNode("Ferrari");
         linkedList.AddNode("Lamborghini");

         watch.Stop();
         linkedList.Print();


         Console.WriteLine("\n-- It's Done! --");
         Console.WriteLine(watch.Elapsed + "\n");
         Console.ReadLine();
      }
   }
}

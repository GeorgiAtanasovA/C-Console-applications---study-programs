using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLinkedList_V
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
            set { this.character = value; }
         }
         internal Node<D> NextChar
         {
            get { return nextChar; }
            set { this.nextChar = value; }
         }
         internal Node<D> Previous
         {
            get { return previous; }
            set { this.previous = value; }
         }

         public Node(D character, int count)
         {
            this.position = count;
            this.Character = character;
         }
         public Node(D character, Node<D> prevNodeTail, int count)
         {
            this.position = count;
            this.Character = character;
            prevNodeTail.NextChar = this;
         }

         public void AddToPrev_Recursion(Node<D> node)
         {
            //Идеята е,че всеки път влизам в tail и в неговия previous слагам head-a
            if (node.nextChar.nextChar == null) { previous = node; }
            else { AddToPrev_Recursion(node.nextChar); }
         }
         public void PrintNext_Recursion()
         {
            Console.WriteLine(character);
            if (nextChar != null)
            {
               nextChar.PrintNext_Recursion();
            }
         }
         public void PrintPrev_Recursion()
         {
            Console.WriteLine(character);
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
      /// Да направя нода +
      /// Да направя дърото
      /// Да направя 'AddNode()' метода +
      /// Да направя 'Print()' метода +
      /// 
      static void Main(string[] args)
      {
         DoubleLinkedList<string> linkedList = new DoubleLinkedList<string>();
         Stopwatch watch = new Stopwatch();

         watch.Start();
         linkedList.AddNode("ЕДНО");
         linkedList.AddNode("ДВЕ");
         linkedList.AddNode("ТРИ");
         linkedList.AddNode("ЧЕТИРИ");
         linkedList.AddNode("ПЕТ");
         linkedList.AddNode("ШЕСТ");
         linkedList.AddNode("СЕДЕМ");
         linkedList.AddNode("ОСЕМ");
         linkedList.AddNode("ДЕВЕТ");
         linkedList.AddNode("ДЕСЕТ");
         linkedList.AddNode("ЕДИНАЙСЕТ");
         linkedList.AddNode("ДВАНАЙСЕТ");
         linkedList.AddNode("ТРИНАЙСЕТ");
         linkedList.AddNode("ЧЕТИРИНАЙСЕТ");
         linkedList.AddNode("ПЕТНАЙСЕТ");
         linkedList.AddNode("ШЕСНАЙСЕТ");
         linkedList.AddNode("СЕДЕМНАЙСЕТ");
         linkedList.AddNode("ОСЕМНАЙСЕТ");
         linkedList.AddNode("ДЕВЕТНАЙСЕТ");

         watch.Stop();
         Console.WriteLine(watch.Elapsed + "\n");

         linkedList.Print();




         Console.WriteLine("It's Done!");
         Console.ReadLine();
      }
   }
}

using System;
using System.Threading;
using System.Windows.Forms;

namespace Dijkstras_Algorithm
{
   class Graph : Program
   {
      public int[,] graph;
      public bool[] visited = new bool[9];
      public int[] distance = new int[9];

      public Graph(int[,] matrix)
      {
         graph = matrix;
         endIterations = distance.Length;
         firstIteration = 0;
      }
      public int PositionMinValue(int[] distance)
      {
         int newStart = 0;
         minValue = int.MaxValue;
         for (int i = 0; i < distance.Length; i++)
         {
            if (minValue > distance[i] && distance[i] != 0 && !visited[i])//-взима най-късото разст.от не посетени нодове
            {
               minValue = distance[i];//-взима най-късото разст.
               newStart = i; //-взима следващ стартов нод който е с най-късото разст.
            }
         }
         return newStart;
      }
      //----------------------------------------------------------------
      public void Dijkstra_ShortestRoad()
      {
         for (int i = 0; i < distance.Length; i++)
         {
            if (i == startNode) { distance[i] = 0; } //слага се 0 на стартовия нод
            else { distance[i] = int.MaxValue; } //тук са останалите нодове
         }

         for (int i = 0; i < graph.GetLength(0); i++)
         {
            if (firstIteration == 0) //за първата итерация
            {
               if (graph[startNode, i] != 0) //проверка за стартовия нод
               {
                  //записване на разст. до съседните нодове в масива distance[]
                  distance[i] = graph[startNode, i];
               }
            }

            if (firstIteration == 1) //за следващите итерации
            {
               if (distance[i] > minValue + graph[startNode, i] //Това е формулата на Дейкстра
                 && !visited[i]
                 && graph[startNode, i] != 0)
               {
                  distance[i] = minValue + graph[startNode, i]; //записване на намерено по кратко разст. в масива distance[]
               }
            }

            if (i == graph.GetLength(0) - 1) //като се стигне до края на текущия ред в матрицата
            {
               i = -1; //това е за да започне цикъла от начало
               firstIteration = 1; //за следващите итерации
               visited[startNode] = true; //нода се маркира като посетен и не се посещава повече
               endIterations--; //за край на целия алгоритим
               if (endIterations == 0) { break; } //проверка за край на целия алгоритим
               startNode = PositionMinValue(distance); //взимани на позицията на следващ нод
            }
         }
      }

      public void Print()
      {
         for (int i = 0; i < graph.GetLength(0); i++)
         {
            for (int j = 0; j < graph.GetLength(1); j++)
            {
               Console.Write(graph[j, i] + " ");
               Thread.Sleep(15);
            }
            Console.WriteLine();
            Thread.Sleep(20);
         }
         Console.WriteLine();
      }
   }

   class Program
   {
      public int endIterations;
      public int firstIteration;
      public int startNode = start;
      static int start;
      public int minValue = int.MaxValue;

      static void Main(string[] args)
      {
         //Програмата намира минималните пътища от връх X от графа до всички останали.

         //Формула на Дейкстра --> distance[i] > minValue + graph[startNode, i]
         //-всеки път се пита: дали разстоянието от НАЧАЛНИЯ връх до всеки друг е по-голямо,по-малко или равно minValue + текущия нод, до негов съсед
         //-на първата итерация от стартовия нод се намират разстоянията до съседите и се записват в масива distance
         //-до нодовете не съседни на стартовия, растоянията са равни на int.MaxValue или безкрайност
         //-разстоянието в масива distance се смята за minValue за момента на работа на алгорлитъма
         //-правят се различни проверки, за посeтен нод, за '0' в матрицата и др.

         Console.Write("Число за стартов нод от 0 до 8: ");

         try
         {
            start = int.Parse(Console.ReadLine());

            if (start < 0 || start > 8)
            {
               start = 0;
               Console.WriteLine("Default: 0");
            }
         }
         catch (FormatException)
         {
            Console.WriteLine("Грешка: Трябва да се въведе стойност от 0 - 8 ");
            Console.WriteLine("Default: 0");
         }


         Graph graph = new Graph(new int[,] { 
           //  0 1 2 3 4 5 6 7 8
    /* 0 */  { 0,0,6,7,0,0,8,2,0 }, 
    /* 1 */  { 0,0,0,0,5,0,2,7,0 }, 
    /* 2 */  { 6,0,0,3,0,3,0,7,5 }, 
    /* 3 */  { 7,0,3,0,0,0,0,0,4 }, 
    /* 4 */  { 0,5,0,0,0,5,0,0,9 }, 
    /* 5 */  { 0,0,3,0,5,0,4,0,6 }, 
    /* 6 */  { 8,2,0,0,0,4,0,2,0 }, 
    /* 7 */  { 2,7,7,0,0,0,2,0,0 }, 
    /* 8 */  { 0,0,5,4,9,6,0,0,0 }
      });
         Console.WriteLine();
         graph.Print();
         graph.Dijkstra_ShortestRoad();

         for (int i = 0; i < graph.visited.Length; i++)
         {
            Console.WriteLine("От нод {0} до {1} разстоянието е {2}", start, i, graph.distance[i]);
            Thread.Sleep(150);
         }
         Console.WriteLine("END");
         MessageBox.Show("Най-кратък път", "Дейкстра алгоритъм", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

         Console.ReadLine();
      }
   }
}

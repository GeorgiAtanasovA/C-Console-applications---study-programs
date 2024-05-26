using System;
using System.Threading;

namespace Spiral_Matrix
{
   class Spiral_Matrix
   {
      static int countCycle = 0;
      static int sleepTime = 10;
      static int matrixHeigth = 0;
      static int matrixLength = 0;
      static char matrixSymbol = '☻';
      static bool cycleChange = true;

      static void Print(int row, int col, char[,] matrix)
      {
         if (col * 2 + 4 >= Console.WindowWidth)
         {
            Console.Clear();
            Thread.Sleep(50);
            Console.WriteLine("--- Spiral matrix ---");
            Console.WriteLine("- For Windows terminal -");
            Console.WriteLine("  Matrix heigth: {0}", matrixHeigth);
            Console.WriteLine("  Matrix height: {0}", matrixLength);
            Console.WriteLine("  Matrix speed: {0}", sleepTime);
            return;
         }
         matrix[row, col] = matrixSymbol;
         Console.SetCursorPosition(col * 2 + 4, row + 6);
         Console.WriteLine(matrix[row, col] + " ");
         Thread.Sleep(sleepTime);
      }

      static void WriteMatrix(int row, int col, char[,] matrix)
      {
         if (matrix[row, col] == matrixSymbol) { return; }
         if (countCycle == matrixHeigth + matrixLength) { return; }

         for (col = countCycle; col < matrix.GetLength(1) - countCycle; col++) //right
         {
            Print(row, col, matrix);
         }
         col--;

         for (row = countCycle + 1; row < matrix.GetLength(0) - countCycle; row++) //down
         {
            Print(row, col, matrix);
         }
         row--;

         for (col = matrix.GetLength(1) - 2 - countCycle; col >= countCycle; col--) //left
         {
            Print(row, col, matrix);
         }
         col++;

         for (row = matrix.GetLength(0) - 2 - countCycle; row >= countCycle; row--) //up
         {
            Print(row, col, matrix);
         }
         countCycle++;
         col = countCycle;
         row = countCycle;

         WriteMatrix(row, col, matrix);
      }

      static void Main(string[] args)
      {
         //Console.SetCursorPosition(2, 0);
         Console.WriteLine(" --- Spiral matrix ---");
         Console.Write("- For Windows terminal -");

         Console.SetCursorPosition(5, 2);
         Console.Write("Matrix heigth: ");
         matrixHeigth = int.Parse(Console.ReadLine());

         Console.SetCursorPosition(5, 3);
         Console.Write("Matrix height: ");
         matrixLength = int.Parse(Console.ReadLine());

         Console.SetCursorPosition(5, 4);
         Console.Write("Matrix speed: ");
         int matrixSpeed = int.Parse(Console.ReadLine());

         sleepTime = matrixSpeed;

         if (matrixHeigth >= 40)
         {
            matrixHeigth = 35;
         }
         if (matrixLength >= 40)
         {
            matrixLength = 35;
         }

         if (matrixHeigth <= 2) { matrixHeigth = 3; }
         if (matrixLength <= 2) { matrixLength = 3; }

         char[,] matrix = new char[matrixHeigth, matrixLength];

         Console.WindowWidth = matrixLength * 2 + 7;
         Console.WindowHeight = matrixHeigth + 7;

         while (true)
         {
            countCycle = 0;
            WriteMatrix(0, 0, matrix);

            if (cycleChange == true)
            {
               matrixSymbol = ' ';
               cycleChange = false;
            }
            else
            {
               matrixSymbol = '☻';
               cycleChange = true;
               Random randGen = new Random();
               int color = randGen.Next(0, 15);
               Console.ForegroundColor = (ConsoleColor)color;
            }
         }
      }
   }
}

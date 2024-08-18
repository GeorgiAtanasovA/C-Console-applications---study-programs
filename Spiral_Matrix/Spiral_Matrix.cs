using System;
using System.Threading;

namespace Spiral_Matrix
{
   class Spiral_Matrix
   {
      static int countCycle = 0;
      static int sleepTime = 5;
      static int matrixSpeed = 0;
      static int matrixHeigth = 0;
      static int matrixWidth = 0;
      static char matrixSymbol = '☻';
      static bool cycleChange = true;

      static void Print(int row, int col, char[,] matrix)
      {
         if (Console.WindowWidth < matrixWidth * 2 + 10 || Console.WindowHeight < matrixHeigth + 7)
         {
            Console.Clear();
            Console.WindowWidth = matrixWidth * 2 + 10;
            Console.WindowHeight = matrixHeigth + 7;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" -Spiral matrix-");
            Console.WriteLine(" Matrix Heigth: {0}", matrixHeigth);
            Console.WriteLine(" Matrix Width: {0}", matrixWidth);
            Console.WriteLine(" Matrix Speed: {0}", sleepTime);
            Thread.Sleep(100);
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
         if (countCycle == matrixHeigth + matrixWidth) { return; }

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
         Console.WriteLine(" -Spiral matrix-");

         try
         {
            Console.SetCursorPosition(2, 1);
            Console.Write("Matrix Heigth: ");
            matrixHeigth = int.Parse(Console.ReadLine());
         }
         catch (FormatException)
         {
            Console.SetCursorPosition(2, 1);
            Console.WriteLine("Default value: 5");
            matrixHeigth = 5;
         }

         try
         {
            Console.SetCursorPosition(2, 2);
            Console.Write("Matrix Width: ");
            matrixWidth = int.Parse(Console.ReadLine());
         }
         catch (FormatException)
         {
            Console.SetCursorPosition(2, 2);
            Console.WriteLine("Default value: 5");
            matrixWidth = 5;
         }

         try
         {
            Console.SetCursorPosition(2, 3);
            Console.Write("Matrix speed: ");
            matrixSpeed = int.Parse(Console.ReadLine());
         }
         catch (FormatException)
         {
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("Default value: 5");
            matrixSpeed = 5;
         }

         sleepTime = matrixSpeed;

         if (matrixHeigth >= 35) { matrixHeigth = 35; }
         if (matrixWidth >= 35) { matrixWidth = 35; }

         if (matrixHeigth < 4) { matrixHeigth = 5; }
         if (matrixWidth < 4) { matrixWidth = 5; }

         char[,] matrix = new char[matrixHeigth, matrixWidth];

         Console.WindowWidth = matrixWidth * 2 + 10;
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
               int color = randGen.Next(1, 15);
               Console.ForegroundColor = (ConsoleColor)color;
            }
         }
      }
   }
}

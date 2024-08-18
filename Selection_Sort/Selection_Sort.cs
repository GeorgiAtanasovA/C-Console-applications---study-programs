using System;
using System.Threading;

namespace SelectionSort
{
   class Selection_Sort
   {
      public static void PrintArr(int[] arr)
      {
         Console.WriteLine("Selection Sort");
         foreach (var item in arr)
         {
            Console.Write(item + " ");
         }
      }
      static void Main(string[] args)
      {
         int i = 0;
         int j = 1;
         int minNumb = 0;
         int[] arr = { 6, 5, 8, 4, 2, 1, 7, 9, 3, 10, 4, 11, 8, 12, 7 };

         Thread.Sleep(500);
         Console.WriteLine("Start:\n6 5 8 4 2 1 7 9 3 10 4 11 8 12 7 \n");

         while (true)
         {
            if (arr[i] > arr[j]) { i = j; }
            if (j == arr.Length - 1)
            {
               int tempNumb = arr[minNumb];
               arr[minNumb] = arr[i];
               arr[i] = tempNumb;
               minNumb++;
               i = minNumb;
               j = minNumb + 1;
            }
            else { j++; }
            if (j == arr.Length) { break; }
         }
         Thread.Sleep(500);
         PrintArr(arr);

         Thread.Sleep(500);
         Console.WriteLine("\n\n END!");
         Console.ReadLine();
      }
   }
}

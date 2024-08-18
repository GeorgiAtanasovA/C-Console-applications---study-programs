using System;

namespace Binary_Search
{
   class Program
   {
      static void Main(string[] args)
      {
         /*Двоично търсене (binary search) в сортиран целочислен масив.*/

         int i1 = 0;
         Console.Write("Дължина на масива: ");
         int arrLength = int.Parse(Console.ReadLine());
         //------------------------------------------------------------
         int[] arr = new int[arrLength];       //Дължина на масива
         for (int i = 0; i < arr.Length; i++)  //Запълване на масива
         {
            i1++; arr[i] = i1;
         }

         int left = arr.Length - arr.Length;
         int right = arr.Length;
         int middle = arr.Length / 2;
         //-------------------------------------------------------------
         
         Console.Write("Число за търсене: ");
         int n = int.Parse(Console.ReadLine());

         while (n > arrLength || n == 0)
         {
            Console.WriteLine("Грешка: числото '{0}' e извън масива ", n);
            Console.WriteLine("опитай отново!");
            n = int.Parse(Console.ReadLine());
         }
         //----------------------
         while (n != arr[middle])
         {
            if (n > arr[middle])
            {
               left = middle;
               middle = (left + right) / 2;
            }

            else if (n < arr[middle])
            {
               right = middle;
               middle = (left + right) / 2;
            }
         }
         //----------------------

         if (n == arr[middle])
         {
            Console.WriteLine("{0} е на позиция {1}", n, middle);
            Console.WriteLine(new string('-', 30));
         }
         left = 0;                 //Тук стойностите се връщат- 
         right = arr.Length;       //в начално състояние, за - 
         middle = arr.Length / 2;  //да може да работи "for" цикъла

         //Свързване на числата от масива със запетайки
         Console.WriteLine(string.Join(", ", arr));
         Console.WriteLine(new string('-', 30));

         Console.ReadLine();
      }
   }
}

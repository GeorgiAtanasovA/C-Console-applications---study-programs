using System;

class Factorial
{
   static void Main()
   {
      int n = 5;
      Console.Write(" n = ");
      string consoleInputLine = Console.ReadLine();

      try
      {
         n = Convert.ToInt32(consoleInputLine);
      }
      catch (Exception)
      {
         Console.WriteLine(" Default value: 5");
      }

      Console.Write(" n! = ");

      // "decimal" is the biggest integer type
      decimal factorial = 1;

      // Perform an infinite loop
      while (true)
      {
         Console.Write(n);
         if (n == 1)
         {
            break;
         }
         Console.Write(" * ");
         factorial *= n;
         n--;
      }
      Console.WriteLine("  = {0}", factorial);
      Console.ReadLine();
   }
}


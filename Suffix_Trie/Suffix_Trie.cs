using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace SuffixTrie
{
   public class SuffixTrie
   {
      public class Node
      {
         internal string rootChars;
         internal List<Node> children;
         internal int countOneWord;
         public Node(string bukva)
         {
            rootChars = bukva;
            children = new List<Node>();
            countOneWord = 0;
         }
         public Node(Node tail, string bukva)
         {
            rootChars = bukva;
            children = new List<Node>();
            countOneWord = 0;

            tail.children.Add(this);
         }
         public void PrintBranchChars_Recursion(Node root, int left, int top, string space)
         {
            Console.WriteLine(space + root.rootChars);
            for (int i = 0; i < root.children.Count; i++)
            {
               PrintBranchChars_Recursion(root.children[i], left, top + 1, space + "▒");
            }
            Console.WriteLine();
         }
         public override string ToString()
         {
            return this.rootChars + " " + this.children;
         }
      }
      //-----------------------------------------------------------------------
      private Node root;
      private Node tail;
      bool isExist;
      private int countAllWords;
      private int checkForOutOfRecursion;
      public int CountWords { get { return countAllWords; } }
      public int CountAllWords { get { return countAllWords; } }
      //-----------------------------------------------------------------------
      public void AddWord(string inputWord)
      {
         countAllWords++;
         checkForOutOfRecursion = inputWord.Length; //Важно! За излизане от рекурсията

         //Tuple трябва само за да прехвърля едновременно две и повечe стойности от един метод
         Tuple<Node, Node> root_ail = AddWord_Recursion(inputWord, root, tail, 0);
         root = root_ail.Item1;
         tail = root_ail.Item2;
      }
      private Tuple<Node, Node> AddWord_Recursion(string inputWord, Node root, Node tail, int c)
      {
         int rootCharPos = 0;
         char bukva_rootChars = ' ';
         char bukva_InputWord = ' ';
         bool identicalChars = false;
         Tuple<Node, Node> tuple = new Tuple<Node, Node>(null, null);
         //================================================================================
         #region
         if (root == null)// Създава първия корен
         {
            root = new Node(inputWord[0].ToString());
            tail = root;
            for (int i = 1; i < inputWord.Length; i++)
            {
               Node tempNode = new Node(tail, inputWord[i].ToString());
               tail = tempNode;//Тук ги слага в опашката на разклонението
            }
            Node tempNodeEndWord = new Node(tail, "$");//Тука прави празен нод след края на думата за означение на края на веригата
            tempNodeEndWord.countOneWord = 1;
            tail = tempNodeEndWord;//Тук ги слага в опашката на разклонението 
         }
         #endregion

         #region
         else
         {
            //-----------------------------------------------------------------------
            for (int i = c; i < inputWord.Length; i++)
            {
               if (checkForOutOfRecursion == 0) { break; }
               checkForOutOfRecursion--;
               bukva_InputWord = inputWord[i];

               for (int j = 0; j < root.rootChars.Length; j++)
               {
                  identicalChars = false;
                  bukva_rootChars = root.rootChars[j];

                  if (bukva_rootChars == '$') { break; }//Ако настоящата дума е по дълга от прдходната
                  if (bukva_InputWord == bukva_rootChars)
                  {
                     identicalChars = true;
                     rootCharPos = j;
                     break;
                  }
               }
               //-----------------------------------------------------------------------
               if (identicalChars == true)
               {
                  //Слагам бройката на думите в края на root-а защото тук е в края на root-a
                  if (checkForOutOfRecursion == 0)
                  {
                     if (SearchWord(inputWord) == true)
                     {
                        root.children[0].countOneWord = root.children[0].countOneWord + 1;
                     }
                  }
                  //Значи че следващата буква е еднаква и се прод.навътре рекурсивно
                  AddWord_Recursion(inputWord, root.children[rootCharPos], tail, c + 1);
               }
               else if (identicalChars == false)
               {
                  if (bukva_rootChars == '$')//Замества $ с буква и продължава новата дума
                  {
                     root.rootChars = bukva_InputWord.ToString();
                     tail = root;//Тук ги слага в опашката на разклонението
                     i++;
                     while (i < inputWord.Length)
                     {
                        checkForOutOfRecursion--;
                        Node tempNode1 = new Node(tail, inputWord[i].ToString());
                        tail = tempNode1;//Тук ги слага в опашката на разклонението
                        i++;
                     }
                  }
                  else
                  {
                     root.rootChars += bukva_InputWord.ToString();
                     tail = root;//Прави нов корен-разклонение
                     i++;
                     while (i < inputWord.Length)
                     {
                        checkForOutOfRecursion--;
                        Node tempNode2 = new Node(tail, inputWord[i].ToString());
                        tail = tempNode2;//Тук ги слага в опашката на разклонението
                        i++;
                     }
                  }
                  //Тук се слага Node с '$' за края на думата
                  Node tempNodeEndWord = new Node(tail, "$");
                  //Слагам бройката на думите в tail-а защото тук се прави нова дума която се слага в tail-a
                  tail = tempNodeEndWord;//Тук ги слага в опашката на разклонението 
                  tail.countOneWord = 1;
                  //-----------------------------------------------------------------------
               }
            }
         }
         tuple = new Tuple<Node, Node>(root, tail);
         return tuple;
         #endregion
      }
      //-----------------------------------------------------------------------
      public bool SearchWord(string inputWord)
      {
         checkForOutOfRecursion = inputWord.Length;//Важно! За излизане от рекурсията
         isExist = SearchWord_Recursion(inputWord, root, 0);
         return isExist;
      }
      private bool SearchWord_Recursion(string inputWord, Node root, int c)
      {   //Направих този за да проверявам за думите,който е същия като PrintCountOfWord_Recursion().Но този връща bool
         char bukva_InputWord = ' ';
         char bukva_rootChars = ' ';
         int childPos = 0;
         bool identicalChars = false;

         for (int i = c; i < inputWord.Length; i++)
         {
            //Тука взима всяка буква от inputWord и я сравнява със буквите които са в root.Chars
            //Ако намери прави break и влиза в рекурсия,подава следващ корен и пак търси,ако не намери значи цялата дума я няма
            if (checkForOutOfRecursion == 0)
            {
               break;
            }
            checkForOutOfRecursion--;
            bukva_InputWord = inputWord[i];

            for (int j = 0; j < root.rootChars.Length; j++)
            {
               bukva_rootChars = root.rootChars[j];
               if (bukva_InputWord == bukva_rootChars)
               {
                  //Console.Write(bukva_InputWord); //Ако намери буква я принтира
                  //if (checkForOutOfRecursion == 0) { Console.WriteLine(); break; }
                  identicalChars = true;
                  isExist = true;
                  childPos = j;
                  break;
               }
            }
            if (identicalChars == false)
            {
               //Ако това е false значи търсената дума е намерена, примерно до средата, а останалата част на думата я няма
               checkForOutOfRecursion = 0;
               Console.WriteLine();
               Console.WriteLine("Думата ({0}) не е в речника!", inputWord);
               isExist = false;
               break;
            }
            if (checkForOutOfRecursion == 0) { break; }
            SearchWord_Recursion(inputWord, root.children[childPos], c + 1);
         }
         return isExist;
      }
      //-----------------------------------------------------------------------
      public void Print_OneWord(string inputWord)
      {
         checkForOutOfRecursion = inputWord.Length;//Важно! За излизане от рекурсията
         Print_OneWord_Recursion(inputWord, root, 0);
      }
      private void Print_OneWord_Recursion(string inputWord, Node root, int c)
      {
         char bukva_InputWord = ' ';
         char bukva_rootChars = ' ';
         int childPos = 0;

         for (int i = c; i < inputWord.Length; i++)
         {
            //Тука взима всяка буква от inputWord и я сравнява със буквите които са в root.Chars
            //Ако намери прави break и влиза в рекурсия,подава следващ корен и пак търси,ако не намери значи цялата дума я няма
            if (checkForOutOfRecursion == 0)
            {
               break;
            }
            checkForOutOfRecursion--;
            bukva_InputWord = inputWord[i];

            for (int j = 0; j < root.rootChars.Length; j++)
            {
               bukva_rootChars = root.rootChars[j];
               if (bukva_InputWord == bukva_rootChars)
               {
                  isExist = true;
                  childPos = j;
                  break;
               }
            }
            if (checkForOutOfRecursion == 0)//Това се изпълнява само веднъж
            {
               Console.WriteLine(root.children[0].countOneWord);
               break;
            }
            Print_OneWord_Recursion(inputWord, root.children[childPos], c + 1);
         }
      }
      public void Print_AllWordsWithCount()
      {
         StringBuilder duma = new StringBuilder();
         int c = 0;
         Print_AllWordsWithCount_Recursion(root, duma, c);
      }
      private void Print_AllWordsWithCount_Recursion(Node root, StringBuilder duma, int c)
      {
         //Този принтира всички думи и count-a им наведнъж
         for (int i = 0; i < root.children.Count; i++)
         {
            duma.Append(root.rootChars[i]);
            if (root.children[i].countOneWord != 0)
            {
               Console.WriteLine(duma + " - " + root.children[0].countOneWord);
            }
            Print_AllWordsWithCount_Recursion(root.children[i], duma, c + 1);
            duma.Remove(c, 1);
         }
      }
      //-----------------------------------------------------------------------
      public void Print_BranchChars()
      {
         root.PrintBranchChars_Recursion(root, 0, 0, string.Empty);
      }
      public override string ToString()
      {
         return root.rootChars + " " + root.children;
      }
   }
   class Program
   {
      /// Да направя дърво +
      /// Да направя клон в children +
      /// Да направя цепенето на думите +
      /// Да направя всяка дума да прави собствена поредица от children[] +
      /// Да направя свестен Print() метод и обхождане в дълбочина +
      /// Да направя сравняване на буквите само които влизат в root-а +
      /// Да екстрактна разделителите +
      /// Да добавя броя на срещанията на всяка буква +
      /// Да направя принтиране на всички думи +
      public static char[] ExtractSeparators_FromText(string text)//Тестов метод
      {
         List<char> separators = new List<char>();
         foreach (char item in text)
         {
            if (!char.IsLetterOrDigit(item)) { separators.Add(item); }
         }
         return separators.ToArray();
      }
      public static char[] ExtractSeparators_FromInputFile(string inputFile)//Тестов метод
      {
         StreamReader reader = new StreamReader(inputFile);
         List<char> separators = new List<char>();
         string line = null;

         separators.Add(' ');//Слагам доп.space защото не се добавя в провeрката долу
         using (reader)
         {
            while ((line = reader.ReadLine()) != null)
            {
               foreach (char item in line)
               {
                  if (!char.IsLetterOrDigit(item) && !char.IsWhiteSpace(item)) { separators.Add(item); }
               }
            }
         }
         return separators.ToArray();
      }
      //---------------------------------------------------------------------------------------

      static void Main(string[] args)
      {
         //Програма, която брои думите в текстов файл, за дума счита всяка последователност от символи (подниз), а не само отделените с разделители. 
         //Например в текста "Аз съм студент в София" поднизовете "с", "сту", "а" и "аз съм" се срещат съответно 3, 1, 2 и 1 пъти.

         SuffixTrie suffixTrie = new SuffixTrie();

         string inputFile = "inputText.txt";
         StreamReader reader = new StreamReader(inputFile);
         StringBuilder strBuilder = new StringBuilder();
         string line = null;
         string duma = "";

         //-----------------Тест--------------------
         Stopwatch watch = new Stopwatch();
         watch.Start();
         using (reader)
         {
            line = reader.ReadToEnd();
            Console.WriteLine(" Програма, която брои думите и поднизовете в текстов файл");
            Console.WriteLine(" It's Working!");
            Thread.Sleep(2000);

            foreach (char item in line)
            {
               if (char.IsLetterOrDigit(item) && !string.IsNullOrWhiteSpace(item.ToString()))
               {
                  strBuilder.Append(item.ToString());
               }
               else
               {
                  duma = strBuilder.ToString();
                  duma = duma.ToLower();
                  if (duma != "")
                  {
                     suffixTrie.AddWord(duma);
                     strBuilder.Clear();
                  }
               }
            }
         }
         watch.Stop();
         string countWords = "Count All Words: " + suffixTrie.CountAllWords;
         string watchElapsed = watch.Elapsed.ToString();
         Console.WriteLine();
         suffixTrie.Print_AllWordsWithCount();

         Console.WriteLine("--------------------\n" + countWords);
         Console.WriteLine(watchElapsed);

         Console.WriteLine("\nIt's Done!");
         Console.ReadLine();

         #region //Основния метод по който е правена задачата
         //public void AddWord(string word)
         //{
         //   foreach (char bukva in word)
         //   {
         //      if (root == null)//За първия път, после root-а винаги е пълен
         //      {
         //         root = new Node(bukva.ToString());
         //         tail = root;
         //      }
         //      else if (char.IsUpper(bukva))
         //      {
         //         root.rootChars += bukva.ToString();
         //         tail = root; //Прави разклонение
         //      }
         //      else
         //      {
         //         Node tempNode = new Node(tail, bukva.ToString());
         //         tail = tempNode;//Тук ги слага в опашката на разклонението
         //      }
         //   }
         //}
         #endregion
      }
   }
}

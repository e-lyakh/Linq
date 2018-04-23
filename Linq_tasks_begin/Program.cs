using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Изучаемые методы LINQ:
//• First, FirstOrDefault, Last, LastOrDefault, Single, SingleOrDefault(поэлементные операции);
//• Count, Sum, Average, Max, Min, Aggregate(агрегирование);
//• Range(генерирование последовательностей).

namespace Linq_tasks_begin
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            var data = new int[] {r.Next(-10,10), r.Next(-10, 10), r.Next(-10, 10), r.Next(-10, 10), r.Next(-10, 10), r.Next(-10, 10),
                                    r.Next(-100,100),r.Next(-100,100),r.Next(-100,100),r.Next(-100,100),r.Next(-100,100),r.Next(-100,100),r.Next(-100,100)};

            var strings = new string[] { "One", "Two", "Data", "Dobro", "Ukraine", "Found", "Environment", "redivider", "азы", "буки", "веды", "глаголь", "добро", "истинно", "есть", "жизнь", "A", "B", "C" };

            var bigs = new string[] { "ONE", "LOVE", "UNION", "REDIVIDER", "TOGETHER" };
            int A = r.Next(0, 10);
            int B = r.Next(A+1, 20);
            char C = 'o';
            int D = r.Next(0, 10);
            int L = r.Next(0, 10);
            float N = r.Next(1, 15);
            
            // ================================

            //Отображение исходных данных:

            Console.Write("data: ");
            foreach (var i in data)
                Console.Write(i + " ");
            Console.WriteLine();

            Console.Write("string: ");
            foreach (var str in strings)
                Console.Write(str + " ");
            Console.WriteLine();

            Console.Write("bigs: ");
            foreach (var b in bigs)
                Console.Write(b + " ");
            Console.WriteLine();

            Console.WriteLine("D = " + D);
            Console.WriteLine("L = " + L);
            Console.WriteLine("C = " + C);
            Console.WriteLine("N = " + N);
            Console.WriteLine("A = " + A);
            Console.WriteLine("B = " + B);            

            // =================================


            //1.Вывести первый положительный элемент и последний отрицательный элемент коллекции data.

            Console.WriteLine();
            Console.WriteLine("---1---");

            var selectedPosItem = (from i in data
                                   where (i > 0)
                                   select i)
                                   .First();
            var selectedPosItemExt = data.Where(i => i > 0).First();
            Console.WriteLine("First positive: " + selectedPosItem);
            Console.WriteLine("First positive (ext): " + selectedPosItemExt);

            var selectedNegItem = (from i in data
                                   where (i < 0)
                                   select i)
                                   .Last();            
            var selectedNegItemExt = data.Where(i => i < 0).Last();            
            Console.WriteLine("Last negative: " + selectedNegItem);            
            Console.WriteLine("Last negative (ext): " + selectedNegItemExt);

            //2. Дана цифра D (однозначное целое число) и целочисленная последовательность data.Вывести первый положительный элемент последовательности data, оканчивающийся цифрой D.Если требуемых элементов в последовательности data нет, то вывести 0.

            Console.WriteLine();
            Console.WriteLine("---2---");

            var selectedItem2 = (from i in data
                                where (i<10) ? i == D : i%10 == D                                
                                select i)
                                .FirstOrDefault();
            Console.WriteLine("First positive with D = " + selectedItem2);
            var selectedItem2Ext = data.Where(i => (i < 10) ? i == D : i % 10 == D).FirstOrDefault();
            Console.WriteLine("First positive with D (ext) = " + selectedItem2Ext);

            //3. Дано целое число L (> 0) и строковая последовательность strings.Вывести последнюю строку из strings, начинающуюся с цифры и имеющую длину L.Если требуемых строк в последовательности strings нет, то вывести строку «Not found».
            //Указание.Для обработки ситуации, связанной с отсутствием требуемых строк, использовать оператор ??.

            Console.WriteLine();
            Console.WriteLine("---3---");
            
            var selectedStr = (from str in strings
                              where str.Length == L
                              select str)
                              .LastOrDefault();
            var selectedStrExt = strings.Where(s => s.Length == L).LastOrDefault();
            Console.WriteLine("Last string with L-length: {0} - {1}", L.ToString(), (selectedStr ?? "Not found") );
            Console.WriteLine("Last string with L-length (ext): {0} - {1}", L.ToString(), (selectedStrExt ?? "Not found"));

            //4. Дан символ С и строковая последовательность strings.Если strings содержит единственный элемент, оканчивающийся символом C, то вывести этот элемент; если требуемых строк в strings нет, то вывести пустую строку; если требуемых строк больше одной, то вывести строку «Error».
            //Указание.Использовать try-блок для перехвата возможного исключения.

            Console.WriteLine();
            Console.WriteLine("---4---");
            try
            {
                var selectedItem4 = (from str in strings
                                     where str[str.Length - 1] == C
                                     select str)
                                     .SingleOrDefault();
                var selectedItem4Ext = strings.Where( str => str[str.Length - 1] == C ).SingleOrDefault();
                Console.WriteLine("Result: {0}", (selectedItem4 ?? ""));
                Console.WriteLine("Result (ext): {0}", (selectedItem4Ext ?? ""));
                
            }
            catch 
            {
                Console.WriteLine("Result: Error");                
            }

            //5. Дан символ С и строковая последовательность strings.Найти количество элементов strings, которые содержат более одного символа и при этом начинаются и оканчиваются символом C.

            Console.WriteLine();
            Console.WriteLine("---5---");

            var count = (from str in strings                        
                        where str.Count(s => s == C) > 1
                        select str)
                        .Count(str => (str[0] == C) && (str[str.Length-1]==C) );
            Console.WriteLine("Count = {0}", count);

            //6. Дана строковая последовательность strings. Найти сумму длин всех строк, входящих в данную последовательность.

            Console.WriteLine();
            Console.WriteLine("---6---");           

            int sum = (from str in strings
                      select str.Length)
                      .Sum();
            int sumExt = strings.Sum(str => str.Length);
            Console.WriteLine("Sum = " + sum);
            Console.WriteLine("Sum (ext) = " + sumExt);

            //7. Дана целочисленная последовательность data. Найти количество ее отрицательных элементов, а также их сумму. Если отрицательные элементы отсутствуют, то дважды вывести 0.

            Console.WriteLine();
            Console.WriteLine("---7---");            

            int negCount = (from d in data
                            where d < 0
                            select d)
                            .Count();
            int negCountExt = data.Where(d => d < 0).Count();
            int negSum = (from d in data
                            where d < 0
                            select d)
                            .Sum();
            int negSumExt = data.Where(d => d < 0).Sum();
            Console.WriteLine("Count of negative elements: {0}", negCount);
            Console.WriteLine("Count of negative elements (ext): {0}", negCountExt);
            Console.WriteLine("Sum of negative elements: {0}", negSum);
            Console.WriteLine("Sum of negative elements (ext): {0}", negSumExt);

            //8. Дана целочисленная последовательность data. Найти количество ее положительных двузначных элементов, а также их среднее арифметическое (как вещественное число). Если требуемые элементы отсутствуют, то дважды вывести 0 (первый раз как целое, второй — как вещественное).

            Console.WriteLine();
            Console.WriteLine("---8---");

            int pos2dCount = (from d in data
                            where d > 9
                            select d)
                            .Count();
            int pos2dCountExt = data.Where(d => d > 9).Count();
            Console.WriteLine("Count of elements with two digits = " + pos2dCount);
            Console.WriteLine("Count of elements with two digits (ext) = " + pos2dCountExt);

            try
            {
                double pos2dAvg = (from d in data
                                   where d > 9
                                   select d)
                                   .Average();
                double pos2dAvgExt = data.Where(d => d > 9).Average();
                Console.WriteLine("Average of elements with two digits = " + pos2dAvg);
                Console.WriteLine("Average of elements with two digits (ext) = " + pos2dAvgExt);
            }
            catch
            {
                Console.WriteLine("Average of elements with two digits = 0.0 (none)");
            }            

            //9. Дана целочисленная последовательность data. Вывести ее минимальный положительный элемент или число 0, если последовательность не содержит положительных элементов.

            Console.WriteLine();
            Console.WriteLine("---9---");

            try
            {
                int minPos = (from d in data
                               where d > 0
                               select d)
                               .Min();
                int minPosExt = data.Where(d => d > 0).Min();
                Console.WriteLine("Min positive element = {0}", minPos);
                Console.WriteLine("Min positive element (ext) = {0}", minPosExt);
            }
            catch
            {
                Console.WriteLine("Min positive element = 0 (none)");
            }

            //10. Дано целое число L (> 0) и строковая последовательность bigs.Строки последовательности bigs содержат только заглавные буквы латинского алфавита. Среди всех строк из bigs, имеющих длину L, найти наибольшую (в смысле лексикографического порядка). Вывести эту строку или пустую строку, если последовательность не содержит строк длины L.

            Console.WriteLine();
            Console.WriteLine("---10---");

            var maxOfBigs = (from b in bigs
                             where b.Length == L
                             select b)
                             .Max();
            Console.WriteLine("Max of bigs with L-length: {0}", maxOfBigs);

            //11. Дана последовательность непустых строк strings. Используя метод Aggregate, получить строку, состоящую из начальных символов всех строк исходной последовательности.

            Console.WriteLine();
            Console.WriteLine("---11---");

            var aggrString = (from str in strings                              
                             select str[0].ToString())
                             .Aggregate( (c1, c2) => c1 + c2);
            Console.WriteLine("Aggregated string: " + aggrString);

            //12. Дана целочисленная последовательность data. Используя метод Aggregate, найти произведение последних цифр всех элементов последовательности.Чтобы избежать целочисленного переполнения, при вычислении произведения использовать вещественный числовой тип.

            Console.WriteLine();
            Console.WriteLine("---12---");

            double aggrMultipy = (from d in data
                                  where d > 0
                                  select (Math.Abs(d) < 10) ? Math.Abs(d) : Math.Abs(d) % 10)
                                  .Aggregate((d1, d2) => d1 * d2);
            Console.WriteLine("Aggregated multiplying of last digits: " + aggrMultipy);

            //13. Дано целое число N (> 0). Используя методы Range и Sum, найти сумму 1 + (1/2) + … + (1/N) (как вещественное число).

            Console.WriteLine();
            Console.WriteLine("---13---");

            IEnumerable<int> rangeSet = Enumerable.Range(1, (int)N);                          

            double aggrSum = (from num in rangeSet                           
                             select num)
                             .Sum( n => 1/ Convert.ToDouble(n) );
            double aggrSumExt = rangeSet.Sum(n => 1/Convert.ToDouble(n));
            Console.WriteLine("Aggregated multiplying of 1/N: {0:F}", aggrSum);
            Console.WriteLine("Aggregated multiplying of 1/N (ext): {0:F}", aggrSumExt);

            //14. Даны целые числа A и B (A<B). Используя методы Range и Average, найти среднее арифметическое квадратов всех целых чисел от A до B включительно: (A^2 + (A+1)^2 + … + B^2)/(B − A + 1) (как вещественное число).

            Console.WriteLine();
            Console.WriteLine("---14---");

            IEnumerable<int> rangeAB = Enumerable.Range(A, (B-A)+1 );
            double aggrAvg = (from num2 in rangeAB                              
                              select num2 * num2)
                              .Average();
            Console.WriteLine("Average of a set from A to B: {0:F}", aggrAvg);

            //15. Дано целое число N(0 ≤ N ≤ 15). Используя методы Range и Aggregate, найти факториал числа N: N! = 1·2·…·N при N ≥ 1; 0! = 1. Чтобы избежать целочисленного переполнения, при вычислении факториала использовать вещественный числовой тип.

            Console.WriteLine();
            Console.WriteLine("---15---");

            IEnumerable<int> rangeN = Enumerable.Range(1, (int)N);
            double factorial = (from rn in rangeN
                             select rn)
                             .Aggregate((n1, n2) => n1 * n2);
            Console.WriteLine("Facrorial for N: {0}! = {1}", N, factorial);

            // -----------------

            Console.WriteLine();

        }
    }
}

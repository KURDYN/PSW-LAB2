using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAB2
{
    class Zadania
    {
        static int[] createNumArray(string arrayString)
        {
            try
            {
                return arrayString.Trim().Split(' ').Select(int.Parse).ToArray();
            }
            catch (FormatException)
            {
                Console.WriteLine("Tablica niepoprawna! Podaj poprawne liczby całkowite.");
                return null;
            }
        }

        static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }
        }

        public static void zadanie1()
        {
            int[] array = null;
            string arrayString;

            Console.WriteLine("Podaj liczby do posortowania oddzielone spacją: ");
            while (array == null)
            {
                arrayString = Console.ReadLine();
                array = createNumArray(arrayString);
            }

            BubbleSort(array);
            Console.WriteLine(string.Join(" ", array));
        }


        public static void zadanie2()
        {
            Random random = new Random();
            double[] marks = [2, 3, 3.5, 4, 4.5, 5];

            Console.Write("Podaj długość tablicy: ");
            if (int.TryParse(Console.ReadLine(), out int length) && length > 0)
            {
                double[] array = new double[length];
                for (int i = 0; i < length; i++)
                {
                    array[i] = marks[random.Next(0, 6)];
                }

                Console.WriteLine($"Wylosowane wartości: {string.Join(" ", array)}");

                // średnia
                double average = array.Average();
                Console.WriteLine($"Średnia: {average}");

                // min / max
                Console.WriteLine($"Wartość minimalna: {array.Min()}, Wartość maksymalna: {array.Max()}");
                
                // większe / mniejsze niż średnia
                var higherThanAverage = array.Where(x => x > average).ToArray();
                var lowerThanAverage = array.Where(x => x < average).ToArray();
                Console.WriteLine("Wartości większe niż średnia: " + string.Join(" ", higherThanAverage));
                Console.WriteLine("Wartości mniejsze niż średnia: " + string.Join(" ", lowerThanAverage));

                // częstotliwość
                var frequency = array.GroupBy(x => x).ToImmutableSortedDictionary(g => g.Key, g => g.Count());
                Console.WriteLine("Częstotliwość występowania liczb:");
                foreach (var item in frequency)
                {
                    Console.WriteLine($"{item.Key}: {item.Value} razy");
                }

                // odchylenie standardowe
                double variance = array.Select(x => Math.Pow(x - average, 2)).Average();
                double standardDeviation = Math.Sqrt(variance);
                Console.WriteLine($"Odchylenie standardowe: {standardDeviation}");
            }
            else
            {
                Console.WriteLine("To nie jest poprawna liczba!");
            }
        }



        public static int[][] filledSquareMatrix(int dimension, int min, int max)
        {
            Random random = new Random();
            int[][] array = new int[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                array[i] = new int[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    array[i][j] = random.Next(min, max+1);
                }
            }
            return array;
        }

        public static void printMatrix(int[][] array)
        {
            foreach (var row in array)
            {
                foreach (var number in row)
                {
                    Console.Write(number + "\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public static int[][] squareMatrixAddition(int[][] matrixA, int[][] matrixB)
        {
            int dimension = matrixA.Length;
            int[][] result = new int[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                result[i] = new int[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    result[i][j] = matrixA[i][j] + matrixB[i][j];
                }
            }
            return result;
        }

        public static int[][] squareMatrixSubtraction(int[][] matrixA, int[][] matrixB)
        {
            int dimension = matrixA.Length;
            int[][] result = new int[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                result[i] = new int[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    result[i][j] = matrixA[i][j] - matrixB[i][j];
                }
            }
            return result;
        }

        public static int[][] squareMatrixMultiplication(int[][] matrixA, int[][] matrixB)
        {
            int dimension = matrixA.Length;
            int[][] result = new int[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                result[i] = new int[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    for (int k = 0; k < dimension; k++)
                    {
                        result[i][j] += matrixA[i][k] * matrixB[k][j];
                    }
                }
            }
            return result;
        }

        public static void zadanie3()
        {
            Console.Write("Podaj wymiar macierzy kwadratowej: ");
            if (int.TryParse(Console.ReadLine(), out int dimension) && dimension > 0)
            {
                var matrixA = filledSquareMatrix(dimension, -10, 10);
                var matrixB = filledSquareMatrix(dimension, -10, 10);

                Console.WriteLine("Macierz A:");
                printMatrix(matrixA);
                Console.WriteLine("Macierz B:");
                printMatrix(matrixB);

                Console.WriteLine("Macierz A+B:");
                printMatrix(squareMatrixAddition(matrixA, matrixB));

                Console.WriteLine("Macierz A-B:");
                printMatrix(squareMatrixSubtraction(matrixA, matrixB));

                Console.WriteLine("Macierz A*B:");
                printMatrix(squareMatrixMultiplication(matrixA, matrixB));
            }
            else
            {
                Console.WriteLine("To nie jest poprawna liczba!");
            }
        }


        public static void zadanie4()
        {
            string[] wordArray;

            Console.WriteLine("Podaj słowa do tablicy oddzielone spacją: ");
            string arrayString = Console.ReadLine();
            wordArray = arrayString.Trim().Split(' ').ToArray();

            Console.WriteLine($"Wpisane wartości: {string.Join(" ", wordArray)}");

            // Najkrótsze i najdłuższe słowo
            string shortest = wordArray.OrderBy(w => w.Length).First();
            string longest = wordArray.OrderBy(w => w.Length).Last();
            Console.WriteLine($"\nSlowo najkrótsze: {shortest}, słowo najdłuższe: {longest}");

            // Słowa posortowane rosnąco i malejąco
            List<string> sortedAsc = wordArray.OrderBy(w => w).ToList();
            List<string> sortedDesc = wordArray.OrderByDescending(w => w).ToList();
            Console.WriteLine($"Słowa posortowane rosnąco: {string.Join(" ", sortedAsc)}");
            Console.WriteLine($"Słowa posortowane malejąco: {string.Join(" ", sortedDesc)}");

            // Średnia długość słowa
            double avgLength = wordArray.Average(w => w.Length);
            Console.WriteLine($"Średnia długość słowa: {avgLength}");

            // Słowa dłuższe niż średnia
            List<string> longerThanAverage = wordArray.Where(w => w.Length > avgLength).ToList();
            Console.WriteLine($"Słowa dłuższe niż średnia: {string.Join(" ", longerThanAverage)}");

            //Anna Maria Paweł Piotr Julia Michał Katarzyna Adam Monika Tomasz Karolina
        }
    }
}

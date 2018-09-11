using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace digitsRecognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var trainingRecords = File.ReadAllLines("trainingsample.csv")
                .Skip(1)
                .Select(x => x.Split(','))
                .Select(x => x.Select(y => int.Parse(y)))
                .Select(x => new {digit = x.First(), pixels = x.Skip(1).ToArray()})
                .ToArray();

            var validationRecords = File.ReadAllLines("validationsample.csv")
                .Skip(1)
                .Select(x => x.Split(','))
                .Select(x => x.Select(y => int.Parse(y)))
                .Select(x => new {digit = x.First(), pixels = x.Skip(1).ToArray()})
                .Take(50)
                .ToArray();

            int correctlyRecognized = 0;
            int wronglyRecognized = 0;

            for (var i = 0; i < validationRecords.Length; i++)
            {
                var validationRecord = validationRecords[i];
                int number = Predict(validationRecord.pixels, trainingRecords);
                if (number == validationRecord.digit)
                {
                    correctlyRecognized++;
                }
                else
                {
                    wronglyRecognized++;
                }

                if (i % 100 == 0)
                {
                    Console.WriteLine($"{i}/{validationRecords.Length}");
                }
            }

            double percentCorrectlyRecognized = ((double) correctlyRecognized) /validationRecords.Length;
            Console.WriteLine($"{correctlyRecognized}/{validationRecords.Length}={percentCorrectlyRecognized}");
            Console.WriteLine("enter to exit");
            Console.ReadLine();
        }

        private static int Predict(int[] pixels, dynamic[] trainingRecords)
        {
            var closestDigit = trainingRecords
                .Select(x => new {distance = CalculateDistance(pixels, x.pixels), x.digit})
                .OrderBy(x => x.distance)
                .First().digit;

            return closestDigit;
        }

        private static double CalculateDistance(int[] pixelsA, int[] pixelsB)
        {
            double sum = 0;
            for (int i = 0; i < pixelsA.Length; i++)
            {
                sum += Math.Pow(pixelsA[i] - pixelsB[i], 2);
            }
            return Math.Sqrt(sum);
        }
    }
}

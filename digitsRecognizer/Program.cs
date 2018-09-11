using System;
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

            var classifier = new Classifier(trainingRecords);

            var validationRecords = File.ReadAllLines("validationsample.csv")
                .Skip(1)
                .Select(x => x.Split(','))
                .Select(x => x.Select(y => int.Parse(y)))
                .Select(x => new {digit = x.First(), pixels = x.Skip(1).ToArray()})
                .ToArray();

            int correctlyRecognized = 0;

            var predictions = classifier.PredictAll(validationRecords.Select(x => x.pixels).ToArray());

            for (int i = 0; i < validationRecords.Length; i++)
            {
                if (predictions[i] == validationRecords[i].digit)
                {
                    correctlyRecognized++;
                }
            }

            double percentCorrectlyRecognized = ((double) correctlyRecognized) /validationRecords.Length;
            Console.WriteLine($"{correctlyRecognized}/{validationRecords.Length}={percentCorrectlyRecognized}");
            Console.WriteLine("enter to exit");
            Console.ReadLine();
        }

    }
}

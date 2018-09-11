﻿using System;
using System.Linq;

namespace digitsRecognizer
{
    class Classifier
    {
        private dynamic[] _trainingRecords;

        public Classifier(dynamic[] trainingRecords)
        {
            _trainingRecords = trainingRecords;
        }

        public int[] PredictAll(int[][] validationRecords)
        {
            var results = validationRecords
                .Select(x => Predict(x))
                .ToArray();
            return results;
        }


        public int Predict(int[] pixels)
        {
            var closestDigit = _trainingRecords
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathProblemCreator.BussinessLogik.Problems
{
    class EuclideanAlgorithmProblem //: IProblem
    {
        readonly int _minNum;
        readonly int _maxNum;
        readonly int _minAnsw;
        readonly int _maxAnsw;
        readonly int _minSteps;
        readonly int _maxSteps;
        static ushort _seed = (ushort) (new Random().Next(1000));

        public EuclideanAlgorithmProblem(string param)
        {
            var data = param.Split(',');

            _minNum = int.Parse(data[0]);
            _maxNum = int.Parse(data[1]);
            _minAnsw = int.Parse(data[2]);
            _maxAnsw = int.Parse(data[3]);
            _minSteps = int.Parse(data[4]);
            _maxSteps = int.Parse(data[5]);
        }

        public string Generate()
        {
            return Format(GetGeneratorResult());
        }

        private (int Number, int NumberOfSteps) Gcd(int x, int y)
        {
            int count = 0;

            while (x != 0 && y != 0)
            {
                if (x > y)
                {
                    x %= y;
                }
                else
                {
                    y %= x;
                }
                count += 1;
            }
            return (x + y, count);
        }

        private (int Num1, int Num2) GetGeneratorResult()
        {
            _seed = (ushort)((_seed + 1) % ushort.MaxValue);
            var random = new Random(_seed);

            int wantedAnswer = random.Next(_minAnsw, _maxAnsw);
            int num1 = random.Next(_minNum, _maxNum);
            int num2 = random.Next(_minNum, _maxNum);

            var gcdTmp = Gcd(num1, num2);
            int numSteps = gcdTmp.NumberOfSteps;
            int realAnswer = gcdTmp.Number;

            while (!(numSteps >= _minSteps && numSteps <= _maxSteps) || (realAnswer != wantedAnswer))
            {
                num1 = random.Next(_minNum, _maxNum);
                num2 = random.Next(_minNum, _maxNum);

                gcdTmp = Gcd(num1, num2);
                numSteps = gcdTmp.NumberOfSteps;
                realAnswer = gcdTmp.Number;
            }

            return (num1, num2);
        }

        private string Format((int Num1, int Num2) resultOfGenerator)
        {
            return $"Find gcd of numbers: {resultOfGenerator.Num1} and {resultOfGenerator.Num2}";
        }
    }
}
    
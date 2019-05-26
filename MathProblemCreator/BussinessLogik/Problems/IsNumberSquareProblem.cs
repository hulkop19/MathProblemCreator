using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathProblemCreator.BussinessLogik.Problems
{
    class IsNumberSquareProblem : IProblem
    {
        private int _div = 1;
        static ushort _seed = (ushort)(new Random().Next(1000));

        public (string Problem, string Answer) Generate()
        {
            int generated = GetResultOfGeneration();
            return (Format(generated), GetSolve(generated));
        }

        public IsNumberSquareProblem(string param)
        {
            _div = int.Parse(param);
        }

        private int GetResultOfGeneration()
        {
            var rand = new Random(_seed);
            int num = rand.Next(100000000, 999999900);

            while (!(num % _div == 0 && num % (_div * _div) != 0))
            {
                ++num;
            }

            return num;
        }

        private string Format(int resultOfGeneration)
        {
            return $"Является ли число {resultOfGeneration} полным квадратом?";
        }

        private string GetSolve(int resultOfGeneration)
        {
            return $"нет не является, так как {resultOfGeneration} делится на {_div} но не делится на его квадрат,\n" +
                   $"что можно заметить используя признаки делимости на {_div} и {_div * _div}";
        }
    }
}

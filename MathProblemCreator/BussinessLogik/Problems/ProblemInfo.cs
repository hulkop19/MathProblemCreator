using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathProblemCreator.BussinessLogik.Problems
{
    class ProblemInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<((string Name, string Discription) Difficulty, string Parametrs)> ProblemParametrs { get; set; }
    }
}

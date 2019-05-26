using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using global::MathProblemCreator.BussinessLogik.Problems;

namespace MathProblemCreator.BussinessLogik
{
    public class Work
    {
        public int Id { get; }
        public string Name { get; }
        public int NumberOfVariants { get; }
        public List<List<(string Problem, string Answer)>> Variants { get; set; }

        [JsonConstructor]
        public Work(int id, string name, int numberOfVariants)
        {
            Id = id;
            Name = name;
            NumberOfVariants = numberOfVariants;

            Variants = new List<List<(string Problem, string Answer)>>();
        }

        public Work(string name, int numberOfVariants)
        {
            Id = GetNewId();
            Name = name;
            NumberOfVariants = numberOfVariants;

            Variants = new List<List<(string Problem, string Answer)>>();
            
            for (int i = 0; i < numberOfVariants; ++i)
            {
                Variants.Add(new List<(string Problem, string Answer)>());
            }
        }

        private int GetNewId()
        {
            int id = JsonConvert.DeserializeObject<int>(DataBaseEmulator.ReadData(@"./Data/lastWorkId.json")) + 1;

            DataBaseEmulator.WriteData(@"./Data/lastWorkId.json", id.ToString());

            return id;
        }

        public void AddProblem(IProblem problem)
        {
            foreach (var variant in Variants)
            {
                var generated = problem.Generate();
                variant.Add((generated.Problem, generated.Answer));
            }
        }

        public void DeleteProblem(int problemIndex)
        {
            for (int i = 0; i < Variants.Count; ++i)
            {
                Variants[i].RemoveAt(problemIndex);
            }
        }
    }
}

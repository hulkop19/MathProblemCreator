using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using global::MathProblemCreator.BussinessLogik.Problems;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

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

        public void CreateDoc(string outFile, bool IsAnswers)
        {
            using (WordprocessingDocument wordDocument =
            WordprocessingDocument.Create(outFile, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                para.ParagraphProperties = new ParagraphProperties();

                for (int i = 0; i < Variants.Count; ++i)
                {
                    Run run = para.AppendChild(new Run());
                    para.ParagraphProperties.Append(new Justification() { Val = JustificationValues.Center });

                    run.RunProperties = new RunProperties();
                    run.RunProperties.Append(new FontSize() { Val = "40" });
                    run.AppendChild(new Text($"Вариант {i + 1}\n"));

                    int ind = 1;
                    foreach (var problem in Variants[i])
                    {
                        para = body.AppendChild(new Paragraph());
                        para.Append(new ParagraphProperties());
                        para.ParagraphProperties.Append(new SpacingBetweenLines() { Before = "100", After = "100" });
                        run = para.AppendChild(new Run());
                        run.RunProperties = new RunProperties();
                        run.RunProperties.Append(new FontSize() { Val = "30" });

                        if (IsAnswers)
                        {
                            run.AppendChild(new Text($"{ind++}) {problem.Answer}\n"));
                        }
                        else
                        {
                            run.AppendChild(new Text($"{ind++}) {problem.Problem}\n"));
                        }

                    }

                    para = body.AppendChild(new Paragraph());
                    para.ParagraphProperties = new ParagraphProperties();
                    para.ParagraphProperties.PageBreakBefore = new PageBreakBefore();
                }
            }
        }
    }
}

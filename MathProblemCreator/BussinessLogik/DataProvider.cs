using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Word;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MathProblemCreator.BussinessLogik.Problems;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MathProblemCreator.BussinessLogik
{
    static class DataProvider
    {
        public static List<Work> GetWorksList()
        {
            var jsonString = DataBaseEmulator.ReadData(@".\Data\worksData.json");

            return JsonConvert.DeserializeObject<List<Work>>(jsonString);
        }

        public static List<ProblemInfo> GetProblemsInfo()
        {
            var jsonString = DataBaseEmulator.ReadData(@".\Data\Problems\problemsInfo.json");

            return JsonConvert.DeserializeObject<List<ProblemInfo>>(jsonString);
        }

        public static void SetWorkList(List<Work> works)
        {
            var jsonString = JsonConvert.SerializeObject(works);

            DataBaseEmulator.WriteData(@".\Data\worksData.json", jsonString);
        }

        public static void Initialize()
        {
            var dataPath = @".\Data";

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);

                DataBaseEmulator.WriteData(Path.Combine(dataPath, "lastWorkId.json"), "0\n");
                SetWorkList(new List<Work>());
            }
        }

        public static void CreateDoc(Work work, string outFile, bool IsAnswers)
        {
            using (WordprocessingDocument wordDocument =
            WordprocessingDocument.Create(outFile, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                para.ParagraphProperties = new ParagraphProperties();

                for (int i = 0; i < work.Variants.Count; ++i)
                {
                    Run run = para.AppendChild(new Run());
                    para.ParagraphProperties.Append(new Justification() { Val = JustificationValues.Center });

                    run.RunProperties = new RunProperties();
                    run.RunProperties.Append(new FontSize() { Val = "40" });
                    run.AppendChild(new Text($"Вариант {i + 1}\n"));

                    int ind = 1;
                    foreach (var problem in work.Variants[i])
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

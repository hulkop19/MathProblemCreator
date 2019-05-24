using iTextSharp.text;
using iTextSharp.text.pdf;
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
        private List<List<string>> _variants;
        public List<List<string>> Variants { get; set; }

        [JsonConstructor]
        public Work(int id, string name, int numberOfVariants)
        {
            Id = id;
            Name = name;
            NumberOfVariants = numberOfVariants;

            Variants = new List<List<string>>();
        }

        public Work(string name, int numberOfVariants)
        {
            Id = GetNewId();
            Name = name;
            NumberOfVariants = numberOfVariants;

            Variants = new List<List<string>>();
            
            for (int i = 0; i < numberOfVariants; ++i)
            {
                Variants.Add(new List<string>());
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
                variant.Add(problem.Generate());
            }
        }

        public void CreatePdf()
        {
            var output = "./Output.pdf";
            var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            using (FileStream fs = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None))
            using (Document doc = new Document(PageSize.A4, 2, 2, 10, 10))
            using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
            {
                PdfContentByte _pcb;
                doc.Open();

                for (int i = 0; i < Variants.Count; ++i) {
                    doc.NewPage();
                    _pcb = writer.DirectContent;
                    _pcb.SetFontAndSize(bf, 12);
                    _pcb.BeginText();
                    _pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"Variant: {i + 1}", 270, 800, 0);

                    for (int j = 0; j < Variants[i].Count; ++j)
                    {
                        _pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"{j + 1}) {Variants[i][j]}", 50, 800 - (35 * (j + 1)), 0);
                    }
                    _pcb.EndText();
                }

                doc.Close();
            }
        }
    }
}

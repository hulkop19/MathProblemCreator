using System.IO;

namespace MathProblemCreator.BussinessLogik
{
    static class DataBaseEmulator
    {
        public static string ReadData(string file)
        {
            using (var reader = new StreamReader(file))
            {
                return reader.ReadToEnd();
            }
        }

        public static void WriteData(string file, string data)
        {
            File.Create(file).Close();

            using (var writer = new StreamWriter(file))
            {
                writer.WriteLine(data);
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}

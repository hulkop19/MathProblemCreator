using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathProblemCreator.BussinessLogik
{
    static class WorkChanger
    {
        public static List<Work> AddWork(Work work)
        {
            List<Work> works = DataProvider.GetWorksList();
            works.Add(work);

            DataProvider.SetWorkList(works);

            return works;
        }

        public static List<Work> DeleteWork(int workId)
        {
            List<Work> works = DataProvider.GetWorksList();
            works.RemoveAll((work) => work.Id == workId);

            DataProvider.SetWorkList(works);

            return works;
        }
    }
}

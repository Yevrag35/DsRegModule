using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG.DsReg
{
    public class Section<T> where T : BaseDetail
    {
        private const int DetailsStartPlus = 3;

        public int Index { get; set; }
        public string Name { get; set; }

        public Section(int headerIndex, string name)
        {
            this.Index = headerIndex;
            this.Name = name;
        }

        public T ApplyFromLines(string[] allLines)
        {
            int start = this.Index + DetailsStartPlus;
            for (int i = start; true; i++)
            {

            }
        }
    }
}

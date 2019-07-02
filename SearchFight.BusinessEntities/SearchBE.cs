using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.BusinessEntities
{
    public class SearchBE
    {
        public string SearchEngine { get; set; }
        public string Query { get; set; }
        public long? TotalResult { get; set; }
        public string FormatTotalResult { get; set; }
        public double? Time { get; set; }
    }
}
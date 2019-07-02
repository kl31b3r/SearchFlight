using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.BusinessEntities
{
    public class SearchResult
    {
        public SearchResult()
        {
            TotalData = new List<SearchBE>();
            Info = new List<SearchBE>();
        }
        public List<SearchBE> TotalData  { get; set; }
        public List<SearchBE> Info { get; set; }
        public string TotalWinner { get; set; }
    }
}
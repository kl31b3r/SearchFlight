using SearchFight.BusinessEntities;
using SearchFight.ServiceAgents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Engine
{
    public class SearchEngine
    {
        public static SearchResult Search(List<string> queries)
        {
            SearchResult r = new SearchResult();

            double? totalMax = 0;
            string totalQuery = string.Empty;

            queries.ForEach(q =>
            {
                r.TotalData.AddRange(GetResultByQuery(q));
            });

            r.TotalData.ForEach(item =>
            {
                if (item.TotalResult > totalMax)
                { totalMax = item.TotalResult; totalQuery = item.Query; }
            });
            r.TotalWinner = totalQuery;

                // getting max by SearchEngine
                SearchEngines().ForEach(e =>
            {
                double? max = 0;
                string query = string.Empty;
                List<SearchBE> itemsByEngine = r.TotalData.FindAll(g => (g.SearchEngine == e));
                itemsByEngine.ForEach(ibe =>
                {
                    if (ibe.TotalResult > max)
                    { max = ibe.TotalResult; query = ibe.Query; }
                }
                );
                r.Info.Add(new SearchBE() { Time = max, Query = query, SearchEngine = e });
            });

            return r;
        }
        static List<string> SearchEngines()
        {
            List<string> searchEngines = new List<string>();
            searchEngines.Add("Google");
            searchEngines.Add("BING");
            return searchEngines;
        }

        static List<SearchBE> GetResultByQuery(string query)
        {
            List<string> searchEngines = SearchEngines();
            List<SearchBE> r = new List<SearchBE>();

            searchEngines.ForEach(se =>
            {
                if (se == "Google")
                {                   
                    r.Add(new GoogleEngineServiceAgent().Search(query));
                }
                if (se == "BING")
                {
                    r.Add(new BINGEngineServiceAgent().Search(query));
                }
            });

            return r;
        }
    }
}
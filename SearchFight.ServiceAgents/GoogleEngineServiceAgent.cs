using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using SearchFight.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.ServiceAgents
{
    public class GoogleEngineServiceAgent
    {
        public SearchBE Search(string query)
        {
            SearchBE r = new SearchBE();

            try
            {
                string apiKey = AppSettingsHelper.GetValue("GoogleAPIKey");
                string searchEngineID = AppSettingsHelper.GetValue("GoogleSearchEngineID");

                CustomsearchService service = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
                CseResource.ListRequest rq = service.Cse.List(query);
                rq.Cx = searchEngineID;

                Search search = rq.Execute();
                if (search != null)
                {
                    r.SearchEngine = "Google";
                    r.Query = query;
                    r.TotalResult = search.SearchInformation.TotalResults;
                    r.FormatTotalResult = search.SearchInformation.FormattedTotalResults;
                    r.Time = search.SearchInformation.SearchTime;
                }
            }
            catch (Exception e)
            {
                r.SearchEngine = "Google";
                r.Query = query + e.Message;
                r.TotalResult = 0;
                r.Time = 0;
            }
            return r;
        }
    }
}
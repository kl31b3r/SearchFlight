using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using SearchFight.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.ServiceAgents
{
    public class BINGEngineServiceAgent
    {
        public SearchBE Search(string query)
        {
            SearchBE r = new SearchBE();

            try
            {
                string accessKey = AppSettingsHelper.GetValue("BINGAccessKey");
                string uriBase = AppSettingsHelper.GetValue("BINGUriBase");

                var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(query);

                WebRequest rq = HttpWebRequest.Create(uriQuery);
                rq.Headers["Ocp-Apim-Subscription-Key"] = accessKey;
                HttpWebResponse rp = (HttpWebResponse)rq.GetResponseAsync().Result;

                string json = new StreamReader(rp.GetResponseStream()).ReadToEnd();
                string uniqueProperty = "totalEstimatedMatches";
                int index = json.IndexOf(uniqueProperty) + uniqueProperty.Length + 3;
                int endIndex = json.IndexOf(",", index);
                string value = json.Substring(index, endIndex - index);

                if (value!=null)
                {
                    r.SearchEngine = "BING";
                    r.Query = query;
                    r.TotalResult = Convert.ToInt64(value);
                    r.FormatTotalResult = String.Format("{0:0,0}", r.TotalResult);
                    r.Time = 0;
                }

            }
            catch (Exception e)
            {
                r.SearchEngine = "BING";
                r.Query = query + e.Message;
                r.TotalResult = 0;
                r.Time = 0;
            }
            return r;
        }
    }
}
using SearchFight.BusinessEntities;
using SearchFight.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] { ".net", "java" }; //TEST

            List<string> queries = new List<string>();

            if (args.Count() > 0)
            {
                for (int i = 0; i < args.Count(); i++)
                {
                    queries.Add(args[i]);
                }

                SearchResult r = SearchEngine.Search(queries);
                ShowResults(r, queries);
            }
            else {
                Console.WriteLine("Put 2 args.");
            }

            Console.ReadLine();
        }
        static void ShowResults(SearchResult r, List<string> queries)
        {
            queries.ForEach(q =>
            {
                StringBuilder sb = new StringBuilder();
                List<SearchBE> rByQuery = r.TotalData.FindAll(td => td.Query == q);

                rByQuery.ForEach(ccc =>
                {
                    sb.Append(ccc.SearchEngine).Append(": ").Append(ccc.TotalResult.ToString()).Append(" | ");
                });
                Console.WriteLine(q + ": " + sb);
            }
            );

            Console.WriteLine(Environment.NewLine);
            r.Info.ForEach(x => { Console.WriteLine(x.SearchEngine + " Winner: " + x.Query); });
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Total Winner :" + r.TotalWinner);
        }
    }
}
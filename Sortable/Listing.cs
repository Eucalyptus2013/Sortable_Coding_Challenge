using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sortable
{
    class Listing
    {
        public string title { get; set; }
        public string manufacturer { get; set; }
        public string currency { get; set; }
        public string price { get; set; }
        
        /* 
            For each node, we look that the string is contained in
            the listing title. If it's the case, we recursively do the 
            same for its children.
            At last level, we add the Node's data to the List of string.
        */
        public void FindMatches(Node N, ref List<string> Matches)
        {
            foreach (Node Temp in N.Children)
            {
                if (title.Contains(Temp.Data))
                    FindMatches(Temp, ref Matches);

                if (Temp.Level == new Product().LevelsOrder().Length)
                    Matches.Add(Temp.Data);                
            }
        }

        // If we have several products that match the listing,
        // we rank them alphabetically and return the first.
        public string Ranking_Function(List<string> FM)
        {
            FM.Sort();
            return FM[0];
        }

        // Get the Matches and return the top rank.
        public string Match(ListProducts LP)
        {
            string result = null;
            List<string> FM = new List<string>();
            FindMatches(LP.Head, ref FM);

            if (FM.Count > 1)
                result=Ranking_Function(FM);

            return result;
        }

    }
}

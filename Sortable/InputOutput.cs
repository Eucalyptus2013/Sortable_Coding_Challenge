using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace Sortable
{
    class InputOutput
    {
        // Some text processing: lowercase, without space
        public static string ProcessText(string s)
        {
            string temp = s;
            temp=temp.ToLower();
            temp=temp.Replace(" ", "");
            return temp;
        }

                
        public static ListProducts GetProducts(string path, JavaScriptSerializer J)
        {
            ListProducts L = new ListProducts();

            // We deserialize each line into a Product object.
            // This Product object is added to the ListProducts L.
            StreamReader S = new StreamReader(path);
            string line;
            while ((line = S.ReadLine()) != null)
            {
                L.Add(J.Deserialize<Product>(line));
            }

            S.Close();
            return L;
        }

        public static List<Listing> GetListings(string path, JavaScriptSerializer J)
        {
            List<Listing> L = new List<Listing>();

            // We deserialize each line into a Listing object.
            // This Listing object is added to the List of Listing L.
            StreamReader S = new StreamReader(path);
            string line;
            while ((line = S.ReadLine()) != null)
            {
                L.Add(J.Deserialize<Listing>(line));
            }

            S.Close();
            return L;
        }

        public static void WriteResults(string path, Dictionary<string, List<Listing>> Dico, JavaScriptSerializer J)
        {
            StreamWriter W = new StreamWriter(path);

            // Using key and value, we create an object Result
            // that we serialize and then write to the results file.
            foreach (string key in Dico.Keys)
            {
                W.WriteLine(J.Serialize(new Result(key, Dico[key])));
            }

            W.Close();
        }
    }

    // Simple class used for serialization
    class Result
    {
        public string product_name { get; set; }
        public List<Listing> listings { get; set; }

        public Result(string pn, List<Listing> L)
        {
            product_name = pn;
            listings = L;
        }
    }
}

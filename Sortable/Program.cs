using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;       

namespace Sortable
{
    class Program
    {

        static void Solve(string path_products, string path_listings, string path_results)
        {
            /*   
                We get the products and the listings by deserializing JSON files.
                The products are in a ListProducts which is a tree.
                The listings are in a list of Listing.
            */
            Console.WriteLine("Getting of the data.");
            JavaScriptSerializer J = new JavaScriptSerializer();
            ListProducts LP = InputOutput.GetProducts(path_products, J);
            List<Listing> LL = InputOutput.GetListings(path_listings, J);

            Console.WriteLine("Computing the results.");
            // For the results, we use a dictionary with a produc_id associated to list
            // of listings.
            Dictionary<string, List<Listing>> Dico = new Dictionary<string, List<Listing>>();
            string temp;

            foreach (Listing L in LL)
            {
                // We get the match of the listing (product_id or null).
                temp = L.Match(LP);
                if (temp != null)
                {
                    if (!Dico.ContainsKey(temp))
                        Dico.Add(temp, new List<Listing>());

                    Dico[temp].Add(L);
                }
            }

            // Creation of the JSON file by serializing Result objects.
            Console.WriteLine("Writing of the results.");
            InputOutput.WriteResults(path_results, Dico, J);
        }
        
        static void Main(string[] args)
        {
            string path_prod, path_list, path_res;

            path_prod = System.IO.Path.Combine(Environment.CurrentDirectory, "../../../Input/products.txt");
            path_list = System.IO.Path.Combine(Environment.CurrentDirectory, "../../../Input/listings.txt");
            path_res = System.IO.Path.Combine(Environment.CurrentDirectory, "../../../Output/results.txt");

            Solve(path_prod, path_list, path_res);
        }
    }
}

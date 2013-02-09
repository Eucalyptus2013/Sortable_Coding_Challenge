using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sortable
{
    class Product
    {
        public string product_name { get; set; }
        public string manufacturer { get; set; }
        public string family { get; set; }
        public string model { get; set; }
        public string announced_date { get; set; }

        /* 
            We set the hierarchy of the class variables.
            Here we have nothing then manufacturer then
            ... then product_name.
        */
        public string[] LevelsOrder()
        {
            return new string[] { "", manufacturer, family, model, product_name };
        }

        // If correct level, return the 
        // class variable of the hierarchial level.
        public string Level(int level)
        {
            if (level < 0 || level >= LevelsOrder().Length)
                return null;
            else
                return LevelsOrder()[level];
        }
    }

    // Simple Node class for Tree construction
    class Node
    {
        public int Level { get; set; }
        public List<Node> Children { get; set; }
        public string Data { get; set; }

        public Node(int _Level, string _Data)
        {
            Level = _Level;
            Data = _Data;
            Children = new List<Node>();
        }
    }
    
    // Tree
    class ListProducts
    {
        public Node Head;
        /* 
            Head corresponds to level 0.
            All the levels different from 0 corresponds to
            the hierarchy of class variables of Product.
        */

        public ListProducts() 
        {
            Head=new Node(0,"");
        }

        
        public void Add(Product P)
        {
            Node N=Head;
            
            /* 
                We traverse all the levels looking for an exact match
                between the Product data (corresponding to the level)
                and the nodes. If there's not match, we create a new Node,
                else we continue with the matching node.
            */
            for (int level = 1; level < P.LevelsOrder().Length; level++)
            {
                if (P.Level(level) != null)
                {
                    Node res = N.Children.Find((Node Temp) => Temp.Data.Equals(P.Level(level)));
                    if (res == null)
                    {
                        N.Children.Add(new Node(level + 1, P.Level(level)));
                        res = N.Children[N.Children.Count - 1];
                    }
                    N = res;
                }
            }
        }

    }
}

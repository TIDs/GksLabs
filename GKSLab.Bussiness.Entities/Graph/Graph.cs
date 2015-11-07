using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GKSLab.Bussiness.Entities.Graph
{
    public class Graph
    {
        /// <summary>
        /// List of graph nodes 
        /// </summary>
       public Graph()
       {
           Roots = new List<Node<string>>();
       }

       public List<Node<string>> Roots { get; private set; }
        /// <summary>
        /// Find node by value
        /// </summary>
        /// <param name="value">
        /// Node value
        /// </param>
        /// <returns>
        /// Return Node of Graph
        /// </returns>
        public Node<string> Find(string value)
        {
            return Roots.FirstOrDefault(x => x.Value == value);
        }
        /// <summary>
        /// Constructor of class Graph
        /// </summary>
        /// <param name="roots">
        /// Array of Nodes
        /// </param>
        public Graph(params Node<string>[] roots)
        {
            Roots.AddRange(roots);
        }

        /// <summary>
        /// Add new node to graph. 
        /// </summary>
        /// <param name="value">Value of the node.</param>
        /// <param name="children">Children of a tree</param>
        public void Add(string value, params Node<string>[] children)
        {
            var node = Find(value);
            //if node exist -> don't create it
            if (node != null)
            {
                foreach (var child in children)
                {
                    //if node contain this Child we will not add child to current node
                    if (node.Children.FirstOrDefault(x => x.Value == child.Value) == null)
                    {
                        node.Children.Add(child);
                        var a = node.Children.Find(x => x.Value == child.Value);
                        node.Children.Find(x=>x.Value==child.Value).Parents.Add(node);
                    }
                }
            }
            else // node exist
            {
                var newNode = new Node<string>(value:value,children: children.ToList(), parents: new List<Node<string>>());
                Roots.Add(newNode);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="children"></param>
        public void UpdateNode(string value, params Node<string>[] children)
        {
        }

    }
}

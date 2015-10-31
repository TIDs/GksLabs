using System.Collections.Generic;

namespace GKSLab.Bussiness.Entities.Graph
{
    public class Graph<T>
    {
        /// <summary>
        /// List of graph nodes 
        /// </summary>
        public List<Node<T>> Roots { get; } = new List<Node<T>>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roots"></param>
        public Graph(params Node<T>[] roots)
        {
            Roots.AddRange(roots);
        }
        /// <summary>
        /// Add new node to graph.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        /// <param name="children">Children of a tree</param>
        public void Add(T value, params Node<T>[] children)
        {
            var newNode = new Node<T>(children:children);
            Roots.Add(newNode);
            foreach (var item in Roots)
            {
                if (newNode.Children.Contains(item))
                {
                    item.Parents.Add(item);
                }
            }
        }
        //public void CreateModule()

    }
}

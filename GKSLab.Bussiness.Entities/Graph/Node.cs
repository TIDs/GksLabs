using System.Collections.Generic;
using System.Linq;

namespace GKSLab.Bussiness.Entities.Graph
{
    /// <summary>
    /// Represent a grapg node
    /// </summary>
    /// <typeparam name="T">Type of node value</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Represent Array of Children
        /// </summary>
        public List<Node<T>> Children { get; set; }
        /// <summary>
        /// Represent Array of Parent nodes
        /// </summary>
        public List<Node<T>> Parents { get; set; }
        public bool HasChildren { get { return Children.FirstOrDefault() != null; } }
        public bool HasParrents { get { return Parents.FirstOrDefault() != null; } }
        /// <summary>
        /// Value of grapg node
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Allow to determine type of graph node (it's module or not)
        /// </summary>
        public NodeType Type { get; set; }
        public Node()
        {
            Parents = new List<Node<T>>();
            Children = new List<Node<T>>();
        }
        public Node(T value, List<Node<T>> children = null, List<Node<T>> parents = null)
        {
            Value = value;
            Children = children;
            Parents = parents;
        }
    }
}

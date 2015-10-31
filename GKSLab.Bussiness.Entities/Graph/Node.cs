using System.Collections.Generic;
using System.Linq;

namespace GKSLab.Bussiness.Entities.Graph
{
    public class Node<T>
    {
        public List<Node<T>> Children { get; set; } = new List<Node<T>>();
        public List<Node<T>> Parents { get; set; } = new List<Node<T>>();
        public bool HasChildren => Children.FirstOrDefault() != null;
        public bool HasParrents => Parents.FirstOrDefault() != null;
        public T Value { get; set; }
        public NodeType Type { get; set; }
        public Node(T value,List<Node<T>> children = null,List<Node<T>> parents = null)
        {
            Value = value;
            Children = children;
            Parents = parents;
        }
    }
}

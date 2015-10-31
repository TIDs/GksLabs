using System.Collections.Generic;
using System.Linq;

namespace GKSLab.Bussiness.Entities.Graph
{
    public class Node<T>
    {
        public IList<Node<T>> Children { get; } = new List<Node<T>>();
        public IList<Node<T>> Parents { get; } = new List<Node<T>>();
        public bool HasChildren => Children.FirstOrDefault() != null;
        public bool HasParrents => Parents.FirstOrDefault() != null;
        public T Value { get; set; }
        public NodeType Type { get; set; }
        public Node(IList<Node<T>> children = null,IList<Node<T>> parents = null)
        {
            Children = children;
            Parents = parents;
        }
    }
}

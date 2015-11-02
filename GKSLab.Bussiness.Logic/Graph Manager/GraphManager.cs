using System.Collections.Generic;
using System.Linq;
using GKSLab.Bussiness.Entities.Graph;

namespace GKSLab.Bussiness.Logic.Graph_Manager
{
    /// <summary>
    /// Graph manager. Represents a class for working with Graph
    /// </summary>
    public static class GraphManager
    {
        /// <summary>
        /// Find children for element in row.
        /// </summary>
        /// <param name="graph">Operating graph. Implement Graph</param>
        /// <param name="row">Row of group</param>
        /// <param name="element">Element of row</param>
        /// <returns>Return children of element</returns>
        private static Node<string> FindChildNodeInRow(Graph graph, IList<string> row, string element)
        {
            var elementPosition = row.IndexOf(element);
            string childValue;
            if (elementPosition >= row.Count - 1)
                childValue = string.Empty;
            else// It's obviously that last element in row doesn't have child
                childValue = row.ElementAt(elementPosition + 1);
            return graph.Find(childValue);
        }
        /// <summary>
        /// Create graph. Return implementation of Graph
        /// </summary>
        /// <param name="groups">Array of operation numbers.For example [1,2,5,4]</param>
        /// <param name="operations">Arrays of operations</param>
        /// <returns>Return implementation of class Graph</returns>
        public static Graph Create(List<int> groups, List<List<string>> operations)
        {
            var graph = new Graph();
            //Add new elements to graph
            foreach (var groupN in groups)
            {
                var itemList = operations.ElementAt(groupN);
                //just adding all nodes 
                foreach (var item in itemList)
                {
                    graph.Add(item);
                }
                //Updating all nodes. Adding children to existing nodes
                foreach (var item in itemList)
                {
                    //item is parent node, childNode - it's child node
                    var childNode = FindChildNodeInRow(graph, itemList, item);
                    if (childNode != null)
                        graph.Add(item, childNode);
                }
            }
            return graph;
        }
    }
}

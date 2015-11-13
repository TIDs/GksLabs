using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GKSLab.Bussiness.Entities.Graph;

namespace GKSLab.Bussiness.Logic.Graph_Manager
{
    class SearchInDepthCycle
    {
        /// <summary>
        /// All finded cycles in graph
        /// </summary>
        private static List<Node<string>> findedCycles = new List<Node<string>>();

        public List<Node<string>> FindCycle(Graph graph)
        {
            List<Node<string>> elementInCycle;

            for (int i = 0; i < graph.Roots.Count; i++)
            {
                graph.Roots.ForEach(x => x.colorNode = 1);
                elementInCycle = new List<Node<string>>();
                elementInCycle.Add(graph.Roots[i]);
                DFSCycle(graph.Roots[i], graph.Roots[i], graph, elementInCycle);
                if (findedCycles.Count > 0) break;
            }

            return findedCycles;
        }

        private static void DFSCycle(Node<string> currentNode, Node<string> endNode, Graph graph, List<Node<string>> cycle)
        {
            List<Node<string>> newCycle;

            if (currentNode != endNode) currentNode.colorNode = 2;
            else if (cycle.Count >= 2 && cycle.Count <= 5)
            {
                cycle.ForEach(x => findedCycles.Add(x));
            }

            for (int i = 0; i < currentNode.Children.Count; i++)
            {
                // check whether this node are used
                if (currentNode.Children[i].colorNode == 1)
                {
                    newCycle = new List<Node<string>>(cycle);
                    newCycle.Add(currentNode.Children[i]);
                    DFSCycle(currentNode.Children[i], endNode, graph, newCycle);
                }
            }
        }
    }
}

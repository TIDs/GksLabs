﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GKSLab.Bussiness.Entities.Graph;

namespace GKSLab.Bussiness.Logic.Graph_Manager
{
    class SearchInDepthFifthCase
    {
        /// <summary>
        /// Find first fifth cases in graph
        /// </summary>
        private static List<Node<string>> findedFifthCase = new List<Node<string>>();

        static SearchInDepthFifthCase()
        {
            findedFifthCase.Clear();
        }
        public List<Node<string>> FindFifthCase(Graph graph)
        {
            List<Node<string>> elementInCycle;
            findedFifthCase.Clear();
            bool union = true;

            for (int i = 0; i < graph.Roots.Count; i++)
            {
                graph.Roots.ForEach(x => x.colorNode = 1);
                elementInCycle = new List<Node<string>>();
                elementInCycle.Add(graph.Roots[i]);
                DFSFifthCase(graph.Roots[i], graph.Roots[i], graph, elementInCycle, union);
                if (findedFifthCase.Count > 0) break;
            }

            return findedFifthCase;
        }

        private static void DFSFifthCase(Node<string> currentNode, Node<string> firstNode, Graph graph, List<Node<string>> cycle, bool union)
        {
            List<Node<string>> newCycle;

            if (firstNode.Children.Count < 2) return;
           
            if (currentNode != firstNode) currentNode.colorNode = 2;

            if (currentNode != firstNode && !(currentNode.Parents.Contains(firstNode) && cycle.Count >= 3)) {
                if (currentNode.Children.Count > 1 || currentNode.Parents.Count > 1)
                {
                    return;
                }
            } 

            if (currentNode != firstNode && currentNode.Parents.Contains(firstNode) && cycle.Count >= 3)
            {
                var amount = 0;
                union = true;
                for (int i = 0; i < cycle.Count; i++)
                {
                    amount += cycle[i].CountUnion;
                }

                if (amount >= 6)
                {
                    union = false;
                }

                if (union == true)
                {
                    cycle.ForEach(x => findedFifthCase.Add(x));
                    return;
                }
            }
 
            for (int i = 0; i < currentNode.Children.Count; i++)
            {
                // check whether this node are used
                if (currentNode.Children[i].colorNode == 1)
                {
                    newCycle = new List<Node<string>>(cycle);
                    newCycle.Add(currentNode.Children[i]);
                    DFSFifthCase(currentNode.Children[i], firstNode, graph, newCycle, union);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Anreton.DotNetGraph.Algorithms.Abstractions;
using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.Algorithms.Implementations
{
	public class BreadthFirstGraphTraversal<TNodeData, TEdge> : GraphTraversalBase<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		protected override IList<INode<TNodeData>> TraverseReachableNodes(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, IList<INode<TNodeData>> visitedNodes, Action<INode<TNodeData>> nodeAction)
		{
			if (graph == null)
			{
				throw new ArgumentNullException($"Argument {nameof(graph)} cannot be null.");
			}

			if (startNode == null)
			{
				throw new ArgumentNullException($"Argument {nameof(startNode)} cannot be null.");
			}

			if (visitedNodes == null)
			{
				throw new ArgumentNullException($"Argument {nameof(visitedNodes)} cannot be null.");
			}

			if (nodeAction == null)
			{
				throw new ArgumentNullException($"Argument {nameof(nodeAction)} cannot be null.");
			}

			if (!graph.ContainsNode(startNode))
			{
				throw new ArgumentException($"Argument {nameof(startNode)}: node {startNode} doesn't exist in the graph.");
			}

			var traversalReachableSequence = new List<INode<TNodeData>>();

			var queue = new Queue<INode<TNodeData>>();
			queue.Enqueue(startNode);

			while (queue.Count > 0)
			{
				var currentNode = queue.Dequeue();
				nodeAction(currentNode);
				traversalReachableSequence.Add(currentNode);

				var adjacencyNodes = graph
					.GetAdjacencyNodes(currentNode)
					.Where
					(
						node => !queue.Contains(node)
							&& !traversalReachableSequence.Contains(node)
							&& !visitedNodes.Contains(node)
					);

				foreach (var adjacencyNode in adjacencyNodes)
				{
					queue.Enqueue(adjacencyNode);
				}
			}

			return traversalReachableSequence;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Anreton.DotNetGraph.Algorithms.Abstractions;
using Anreton.DotNetGraph.DataStructures.Abstractions;
using Anreton.DotNetGraph.DataStructures.Implementations;

namespace Anreton.DotNetGraph.Algorithms.Implementations
{
	public class BreadthFirstGraphPathfinding<TNodeData, TEdge>: GraphPathfindingBase<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		protected override IDictionary<INode<TNodeData>, IList<IPathSegment<TNodeData, TEdge>>> FindPathSegments(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, INode<TNodeData> goalNode)
		{
			if (graph == null)
			{
				throw new ArgumentNullException($"Argument {nameof(graph)} cannot be null.");
			}

			if (startNode == null)
			{
				throw new ArgumentNullException($"Argument {nameof(startNode)} cannot be null.");
			}

			if (!graph.ContainsNode(startNode))
			{
				throw new ArgumentException($"Argument {nameof(startNode)}: node {startNode} doesn't exist in the graph.");
			}

			if (goalNode != null && !graph.ContainsNode(goalNode))
			{
				throw new ArgumentException($"Argument {nameof(goalNode)}: node {goalNode} doesn't exist in the graph.");
			}

			var pathSegmentsForVisitedNodes = new Dictionary<INode<TNodeData>, IList<IPathSegment<TNodeData, TEdge>>>();

			var queue = new Queue<INode<TNodeData>>();
			queue.Enqueue(startNode);

			var startNodePathSegments = new List<IPathSegment<TNodeData, TEdge>>()
			{
				new UnweightedPathSegment<TNodeData, TEdge>(startNode)
			};

			pathSegmentsForVisitedNodes.Add(startNode, startNodePathSegments);

			while (queue.Count > 0)
			{
				var currentNode = queue.Dequeue();

				if (goalNode != null && currentNode == goalNode)
				{
					break;
				}

				var adjacencyNodes = graph
					.GetAdjacencyNodes(currentNode)
					.Where(node => !queue.Contains(node) && !pathSegmentsForVisitedNodes.ContainsKey(node));

				foreach (var adjacencyNode in adjacencyNodes)
				{
					queue.Enqueue(adjacencyNode);

					var incomingEdgesToAdjacencyNode = graph.GetEdges(currentNode, adjacencyNode);
					var incomingEdge = this.GetRandomEdge(incomingEdgesToAdjacencyNode);

					var pathSegment = new UnweightedPathSegment<TNodeData, TEdge>(incomingEdge, adjacencyNode);
					var pathSegments = pathSegmentsForVisitedNodes[currentNode].ToList();
					pathSegments.Add(pathSegment);

					pathSegmentsForVisitedNodes.Add(adjacencyNode, new List<IPathSegment<TNodeData, TEdge>>(pathSegments));
				}
			}

			return pathSegmentsForVisitedNodes;
		}
	}
}

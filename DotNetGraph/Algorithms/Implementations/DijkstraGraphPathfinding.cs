using System;
using System.Collections.Generic;
using System.Linq;
using Anreton.DotNetGraph.Algorithms.Abstractions;
using Anreton.DotNetGraph.DataStructures.Abstractions;
using Anreton.DotNetGraph.DataStructures.Implementations;

namespace Anreton.DotNetGraph.Algorithms.Implementations
{
	public class DijkstraGraphPathfinding<TNodeData, TEdge> : GraphPathfindingBase<TNodeData, TEdge> where TEdge : class, IWeightedEdge<TNodeData>
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
			var pathSegmentsForNonVisitedNodes = new Dictionary<INode<TNodeData>, IList<IPathSegment<TNodeData, TEdge>>>();

			var startNodePathSegments = new List<IPathSegment<TNodeData, TEdge>>()
			{
				new WeightedPathSegment<TNodeData, TEdge>(startNode)
			};

			pathSegmentsForNonVisitedNodes.Add(startNode, startNodePathSegments);

			while (pathSegmentsForNonVisitedNodes.Count > 0)
			{
				var currentNode = this.GetNodeWithMinimumLabel(pathSegmentsForNonVisitedNodes);
				var currentNodeLabelValue = pathSegmentsForNonVisitedNodes[currentNode].Sum(segment => segment.Length);

				pathSegmentsForVisitedNodes.Add(currentNode, pathSegmentsForNonVisitedNodes[currentNode]);

				if (goalNode != null && currentNode == goalNode)
				{
					break;
				}

				var adjacencyNodes = graph
					.GetAdjacencyNodes(currentNode)
					.Where(node => !pathSegmentsForVisitedNodes.ContainsKey(node));

				foreach (var adjacencyNode in adjacencyNodes)
				{
					var incomingEdgesToAdjacencyNode = graph.GetEdges(currentNode, adjacencyNode);
					var incomingEdge = this.GetEdgeWithMinimumWeigth(incomingEdgesToAdjacencyNode);

					if (!pathSegmentsForNonVisitedNodes.ContainsKey(adjacencyNode))
					{
						var pathSegment = new WeightedPathSegment<TNodeData, TEdge>(incomingEdge, adjacencyNode);
						var pathSegments = pathSegmentsForVisitedNodes[currentNode].ToList();
						pathSegments.Add(pathSegment);

						pathSegmentsForNonVisitedNodes.Add(adjacencyNode, new List<IPathSegment<TNodeData, TEdge>>(pathSegments));
					}

					var adjacencyNodeLabelValue = pathSegmentsForNonVisitedNodes[adjacencyNode].Sum(segment => segment.Length);

					if (adjacencyNodeLabelValue > (currentNodeLabelValue + incomingEdge.Weight))
					{
						var pathSegment = new WeightedPathSegment<TNodeData, TEdge>(incomingEdge, adjacencyNode);
						var adjacencyNodePathSegments = pathSegmentsForNonVisitedNodes[adjacencyNode];
						adjacencyNodePathSegments[adjacencyNodePathSegments.Count - 1] = pathSegment;
					}
				}

				pathSegmentsForNonVisitedNodes.Remove(currentNode);
			}

			return pathSegmentsForVisitedNodes;
		}

		private INode<TNodeData> GetNodeWithMinimumLabel(IDictionary<INode<TNodeData>, IList<IPathSegment<TNodeData, TEdge>>> pathSegmentsForNonVisitedNodes)
		{
			if (pathSegmentsForNonVisitedNodes == null)
			{
				throw new ArgumentNullException($"Argument {nameof(pathSegmentsForNonVisitedNodes)} cannot be null.");
			}

			if (!pathSegmentsForNonVisitedNodes.Any())
			{
				throw new ArgumentException($"Argument {nameof(pathSegmentsForNonVisitedNodes)}: the dictionary of labeled nodes is empty.");
			}

			var minimumLabel = pathSegmentsForNonVisitedNodes.Min(item => item.Value.Sum(segment => segment.Length));

			var nodeWithMinimumLabel = pathSegmentsForNonVisitedNodes.FirstOrDefault(item => item.Value.Sum(segment => segment.Length) == minimumLabel).Key;
			if (nodeWithMinimumLabel == null)
			{
				throw new InvalidOperationException($"Local variable {nameof(nodeWithMinimumLabel)}: a node with a minimum label was not found.");
			}

			return nodeWithMinimumLabel;
		}

		private TEdge GetEdgeWithMinimumWeigth(IEnumerable<TEdge> edges)
		{
			if (edges == null)
			{
				throw new ArgumentNullException($"Argument {nameof(edges)} cannot be null.");
			}

			if (!edges.Any())
			{
				throw new ArgumentException($"Argument {nameof(edges)}: the sequence of edges is empty.");
			}

			var minimumWeight = edges.Min(edge => edge.Weight);

			var edgeWithMinimumWeight = edges.FirstOrDefault(edge => edge.Weight == minimumWeight);
			if (edgeWithMinimumWeight == null)
			{
				throw new InvalidOperationException($"Local variable {nameof(edgeWithMinimumWeight)}: an edge with a minimum weight not found.");
			}

			return edgeWithMinimumWeight;
		}
	}
}

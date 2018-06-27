using System;
using System.Collections.Generic;
using System.Linq;
using Anreton.DotNetGraph.DataStructures.Abstractions;
using Anreton.DotNetGraph.DataStructures.Implementations;

namespace Anreton.DotNetGraph.Algorithms.Abstractions
{
	public abstract class GraphPathfindingBase<TNodeData, TEdge> : ISinglePairGraphPathfinding<TNodeData, TEdge>, ISingleSourceGraphPathfinding<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		public virtual IPath<TNodeData, TEdge> FindPath(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, INode<TNodeData> goalNode)
		{
			if (graph == null)
			{
				throw new ArgumentNullException($"Argument {nameof(graph)} cannot be null.");
			}

			if (startNode == null)
			{
				throw new ArgumentNullException($"Argument {nameof(startNode)} cannot be null.");
			}

			if (goalNode == null)
			{
				throw new ArgumentNullException($"Argument {nameof(goalNode)} cannot be null.");
			}

			if (!graph.ContainsNode(startNode))
			{
				throw new ArgumentException($"Argument {nameof(startNode)}: node {startNode} doesn't exist in the graph.");
			}

			if (!graph.ContainsNode(goalNode))
			{
				throw new ArgumentException($"Argument {nameof(goalNode)}: node {goalNode} doesn't exist in the graph.");
			}

			var pathSegmentsForVisitedNodes = this.FindPathSegments(graph, startNode, goalNode);

			IList<IPathSegment<TNodeData, TEdge>> pathSegments;
			if (pathSegmentsForVisitedNodes.ContainsKey(goalNode))
			{
				pathSegments = pathSegmentsForVisitedNodes[goalNode];
			}
			else
			{
				pathSegments = new List<IPathSegment<TNodeData, TEdge>>();
			}

			return new Path<TNodeData, TEdge>(pathSegments);
		}

		public virtual IDictionary<INode<TNodeData>, IPath<TNodeData, TEdge>> FindPaths(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode)
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

			var pathSegmentsForVisitedNodes = this.FindPathSegments(graph, startNode, goalNode: null);

			var nodes = graph.GetNodes().ToList();
			var paths = new Dictionary<INode<TNodeData>, IPath<TNodeData, TEdge>>();

			foreach (var node in nodes)
			{
				IList<IPathSegment<TNodeData, TEdge>> pathSegments;
				if (pathSegmentsForVisitedNodes.ContainsKey(node))
				{
					pathSegments = pathSegmentsForVisitedNodes[node];
				}
				else
				{
					pathSegments = new List<IPathSegment<TNodeData, TEdge>>();
				}

				paths.Add(node, new Path<TNodeData, TEdge>(pathSegments));
			}

			return paths;
		}

		protected abstract IDictionary<INode<TNodeData>, IList<IPathSegment<TNodeData, TEdge>>> FindPathSegments(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, INode<TNodeData> goalNode);

		protected TEdge GetRandomEdge(IEnumerable<TEdge> edges)
		{
			if (edges == null)
			{
				throw new ArgumentNullException($"Argument {nameof(edges)} cannot be null.");
			}

			var edgesCount = edges.Count();
			if (edgesCount <= 0)
			{
				throw new ArgumentException($"Argument {nameof(edges)}: the sequence of nodes is empty.");
			}

			var edgesList = edges.ToList();

			var random = new Random();
			var randomIndex = random.Next(edgesCount);

			return edgesList[randomIndex];
		}
	}
}

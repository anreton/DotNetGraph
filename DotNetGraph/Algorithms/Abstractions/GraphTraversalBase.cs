using System;
using System.Collections.Generic;
using System.Linq;
using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.Algorithms.Abstractions
{
	public abstract class GraphTraversalBase<TNodeData, TEdge> : IGraphTraversal<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		public virtual IList<INode<TNodeData>> Traverse(IGraph<TNodeData, TEdge> graph, bool traverseAllNodes)
		{
			if (graph == null)
			{
				throw new ArgumentNullException($"Argument {nameof(graph)} cannot be null.");
			}

			var nodes = graph.GetNodes();
			if (!nodes.Any())
			{
				throw new ArgumentException($"Argument {nameof(graph)}: the graph doesn't contain nodes.");
			}

			var startNode = this.GetRandomNode(nodes);

			return this.Traverse(graph, startNode, traverseAllNodes);
		}

		public virtual IList<INode<TNodeData>> Traverse(IGraph<TNodeData, TEdge> graph, bool traverseAllNodes, Action<INode<TNodeData>> nodeAction)
		{
			if (graph == null)
			{
				throw new ArgumentNullException($"Argument {nameof(graph)} cannot be null.");
			}

			if (nodeAction == null)
			{
				throw new ArgumentNullException($"Argument {nameof(nodeAction)} cannot be null.");
			}

			var nodes = graph.GetNodes();
			if (!nodes.Any())
			{
				throw new ArgumentException($"Argument {nameof(graph)}: the graph doesn't contain nodes.");
			}

			var startNode = this.GetRandomNode(nodes);

			return this.Traverse(graph, startNode, traverseAllNodes, nodeAction);
		}

		public virtual IList<INode<TNodeData>> Traverse(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, bool traverseAllNodes)
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

			return this.Traverse(graph, startNode, traverseAllNodes, node => {});
		}

		public virtual IList<INode<TNodeData>> Traverse(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, bool traverseAllNodes, Action<INode<TNodeData>> nodeAction)
		{
			if (graph == null)
			{
				throw new ArgumentNullException($"Argument {nameof(graph)} cannot be null.");
			}

			if (startNode == null)
			{
				throw new ArgumentNullException($"Argument {nameof(startNode)} cannot be null.");
			}

			if (nodeAction == null)
			{
				throw new ArgumentNullException($"Argument {nameof(nodeAction)} cannot be null.");
			}

			if (!graph.ContainsNode(startNode))
			{
				throw new ArgumentException($"Argument {nameof(startNode)}: node {startNode} doesn't exist in the graph.");
			}

			var graphTraversalSequence = new List<INode<TNodeData>>(graph.NodesCount);

			var traversalReachableSequence = this.TraverseReachableNodes(graph, startNode, graphTraversalSequence, nodeAction);
			graphTraversalSequence.AddRange(traversalReachableSequence);

			if (traverseAllNodes)
			{
				var allNodes = graph.GetNodes();
				var unvisitedNodes = allNodes.Where(node => !graphTraversalSequence.Contains(node));

				while (unvisitedNodes.Any())
				{
					var newStartNode = this.GetRandomNode(unvisitedNodes);

					traversalReachableSequence = this.TraverseReachableNodes(graph, newStartNode, graphTraversalSequence, nodeAction);
					graphTraversalSequence.AddRange(traversalReachableSequence);
				}
			}

			return graphTraversalSequence;
		}

		protected abstract IList<INode<TNodeData>> TraverseReachableNodes(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, IList<INode<TNodeData>> visitedNodes, Action<INode<TNodeData>> nodeAction);

		private INode<TNodeData> GetRandomNode(IEnumerable<INode<TNodeData>> nodes)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException($"Argument {nameof(nodes)} cannot be null.");
			}

			var nodesCount = nodes.Count();
			if (nodesCount <= 0)
			{
				throw new ArgumentException($"Argument {nameof(nodes)}: the sequence of nodes is empty.");
			}

			var nodesList = nodes.ToList();

			var random = new Random();
			var randomIndex = random.Next(nodesCount);

			return nodesList[randomIndex];
		}
	}
}

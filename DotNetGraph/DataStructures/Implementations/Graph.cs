using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class Graph<TNodeData, TEdge> : GraphBase<TNodeData, TEdge> where TEdge: class, IEdge<TNodeData>
	{
		public Graph()
		{
			this.AdjacencyList = new Dictionary<INode<TNodeData>, ISet<TEdge>>();
		}

		protected IDictionary<INode<TNodeData>, ISet<TEdge>> AdjacencyList { get; }

		public override void AddNode(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (this.ContainsNode(node))
			{
				throw new ArgumentException($"Argument {nameof(node)}: node ({node}) already exists in the graph.");
			}

			this.AdjacencyList.Add(node, new HashSet<TEdge>());
		}

		public override bool RemoveNode(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			var incomingEdges = this.GetIncomingEdges(node).ToList();

			foreach (var incomingEdge in incomingEdges)
			{
				this.RemoveEdge(incomingEdge);
			}

			return this.AdjacencyList.Remove(node);
		}

		public override bool ContainsNode(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			return this.AdjacencyList.ContainsKey(node);
		}

		public override bool HasEdge(INode<TNodeData> source, INode<TNodeData> destination)
		{
			if (source == null)
			{
				throw new ArgumentNullException($"Argument {nameof(source)} cannot be null.");
			}

			if (destination == null)
			{
				throw new ArgumentNullException($"Argument {nameof(destination)} cannot be null.");
			}

			if (!this.ContainsNode(source))
			{
				throw new ArgumentException($"Argument {nameof(source)}: node ({source}) doesn't exist in the graph.");
			}

			if (!this.ContainsNode(destination))
			{
				throw new ArgumentException($"Argument {nameof(destination)}: node ({destination}) doesn't exist in the graph.");
			}

			return this.AdjacencyList[source].Any(edge => edge.Destination == destination);
		}

		public override void AddEdge(TEdge edge)
		{
			if (edge == null)
			{
				throw new ArgumentNullException($"Argument {nameof(edge)} cannot be null.");
			}

			this.AddEdge(edge, allowAddNewNodes: true);
		}

		public void AddEdge(TEdge edge, bool allowAddNewNodes)
		{
			if (edge == null)
			{
				throw new ArgumentNullException($"Argument {nameof(edge)} cannot be null.");
			}

			this.AddNewNodeFromEdge(edge.Source, allowAddNewNodes);
			this.AddNewNodeFromEdge(edge.Destination, allowAddNewNodes);

			this.AdjacencyList[edge.Source].Add(edge);
		}

		public override void AddUndirectedEdge(TEdge edge)
		{
			if (edge == null)
			{
				throw new ArgumentNullException($"Argument {nameof(edge)} cannot be null.");
			}

			this.AddUndirectedEdge(edge, allowAddNewNodes: true);
		}

		public void AddUndirectedEdge(TEdge edge, bool allowAddNewNodes)
		{
			if (edge == null)
			{
				throw new ArgumentNullException($"Argument {nameof(edge)} cannot be null.");
			}

			this.AddEdge(edge, allowAddNewNodes);

			var reversedEdge = edge.GetReverseEdge() as TEdge;
			if (reversedEdge == null)
			{
				throw new ArgumentException($"Argument {nameof(edge)}: edge is not of type {nameof(TEdge)}.");
			}

			this.AddEdge(reversedEdge, allowAddNewNodes);
		}

		public override bool RemoveEdge(TEdge edge)
		{
			if (edge == null)
			{
				throw new ArgumentNullException($"Argument {nameof(edge)} cannot be null.");
			}

			return this.ContainsEdge(edge) && this.AdjacencyList[edge.Source].Remove(edge);
		}

		public override bool ContainsEdge(TEdge edge)
		{
			if (edge == null)
			{
				throw new ArgumentNullException($"Argument {nameof(edge)} cannot be null.");
			}

			return this.ContainsNode(edge.Source)
				&& this.ContainsNode(edge.Destination)
				&& this.AdjacencyList[edge.Source].Contains(edge);
		}

		public override void Clear(bool onlyEdges)
		{
			if (onlyEdges)
			{
				foreach (var item in this.AdjacencyList)
				{
					item.Value.Clear();
				}
			}
			else
			{
				this.AdjacencyList.Clear();
			}
		}

		public override IEnumerable<INode<TNodeData>> GetNodes()
		{
			return this.AdjacencyList.Keys;
		}

		public override IEnumerable<INode<TNodeData>> GetAdjacencyNodes(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (!this.ContainsNode(node))
			{
				throw new ArgumentException($"Argument {nameof(node)}: node ({node}) doesn't exist in the graph.");
			}

			return this.AdjacencyList[node]
				.Select(edge => edge.Destination)
				.Where(adjacencyNode => adjacencyNode != node)
				.Distinct();
		}

		public override IEnumerable<TEdge> GetEdges()
		{
			return this.AdjacencyList.SelectMany(item => item.Value);
		}

		public override IEnumerable<TEdge> GetEdges(INode<TNodeData> source, INode<TNodeData> destination)
		{
			if (source == null)
			{
				throw new ArgumentNullException($"Argument {nameof(source)} cannot be null.");
			}

			if (destination == null)
			{
				throw new ArgumentNullException($"Argument {nameof(destination)} cannot be null.");
			}

			if (!this.ContainsNode(source))
			{
				throw new ArgumentException($"Argument {nameof(source)}: node ({source}) doesn't exist in the graph.");
			}

			if (!this.ContainsNode(destination))
			{
				throw new ArgumentException($"Argument {nameof(destination)}: node ({destination}) doesn't exist in the graph.");
			}

			return this.AdjacencyList[source].Where(edge => edge.Destination == destination);
		}

		public override IEnumerable<TEdge> GetOutgoingEdges(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (!this.ContainsNode(node))
			{
				throw new ArgumentException($"Argument {nameof(node)}: node {node} doesn't exist in the graph.");
			}

			return this.AdjacencyList[node];
		}

		public override string ToString()
		{
			var graphStringBuilder = new StringBuilder();

			graphStringBuilder.AppendLine("The adjacency list of the graph:");

			foreach (var item in this.AdjacencyList)
			{
				var node = item.Key;
				var edges = item.Value;

				var stringNode = node.ToString();
				var stringEdges = edges.Select(edge => edge.ToString());
				var stringJoinedEdges = String.Join("; ", stringEdges);

				graphStringBuilder.AppendLine($"{stringNode}: {stringJoinedEdges}");
			}

			return graphStringBuilder.ToString();
		}

		private void AddNewNodeFromEdge(INode<TNodeData> node, bool allowAddNewNodes)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (!this.ContainsNode(node))
			{
				if (allowAddNewNodes)
				{
					this.AddNode(node);
				}
				else
				{
					throw new ArgumentException($"Argument {nameof(node)}: node ({node}) doesn't exist in the graph.");
				}
			}
		}
	}
}

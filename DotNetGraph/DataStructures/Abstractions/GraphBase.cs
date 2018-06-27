using System;
using System.Collections.Generic;
using System.Linq;

namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public abstract class GraphBase<TNodeData, TEdge> : IGraph<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		public virtual int NodesCount
		{
			get
			{
				return this.GetNodes().Count();
			}
		}

		public virtual int EdgesCount
		{
			get
			{
				return this.GetEdges().Count();
			}
		}

		public abstract void AddNode(INode<TNodeData> node);
		public abstract bool RemoveNode(INode<TNodeData> node);
		public abstract bool ContainsNode(INode<TNodeData> node);
		public abstract bool HasEdge(INode<TNodeData> source, INode<TNodeData> destination);

		public abstract void AddEdge(TEdge edge);
		public abstract void AddUndirectedEdge(TEdge edge);
		public abstract bool RemoveEdge(TEdge edge);
		public abstract bool ContainsEdge(TEdge edge);

		public abstract IEnumerable<INode<TNodeData>> GetNodes();
		public abstract IEnumerable<INode<TNodeData>> GetAdjacencyNodes(INode<TNodeData> node);

		public abstract IEnumerable<TEdge> GetEdges();
		public abstract IEnumerable<TEdge> GetEdges(INode<TNodeData> source, INode<TNodeData> destination);

		public virtual IEnumerable<TEdge> GetIncomingEdges(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (!this.ContainsNode(node))
			{
				throw new ArgumentException($"Argument {nameof(node)}: node {node} doesn't exist in the graph.");
			}

			return this.GetEdges().Where(edge => edge.Destination == node);
		}

		public virtual IEnumerable<TEdge> GetOutgoingEdges(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (!this.ContainsNode(node))
			{
				throw new ArgumentException($"Argument {nameof(node)}: node {node} doesn't exist in the graph.");
			}

			return this.GetEdges().Where(edge => edge.Source == node);
		}

		public abstract void Clear(bool onlyEdges);

		public virtual int GetDegree(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (!this.ContainsNode(node))
			{
				throw new ArgumentException($"Argument {nameof(node)}: node {node} doesn't exist in the graph.");
			}

			var indegree = this.GetIndegree(node);
			var outdegree = this.GetOutdegree(node);

			return indegree + outdegree;
		}

		public virtual int GetIndegree(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (!this.ContainsNode(node))
			{
				throw new ArgumentException($"Argument {nameof(node)}: node {node} doesn't exist in the graph.");
			}

			return this.GetIncomingEdges(node).Count();
		}

		public virtual int GetOutdegree(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (!this.ContainsNode(node))
			{
				throw new ArgumentException($"Argument {nameof(node)}: node {node} doesn't exist in the graph.");
			}

			return this.GetOutgoingEdges(node).Count();
		}
	}
}

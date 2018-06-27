using System;

namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public abstract class PathSegmentBase<TNodeData, TEdge> : IPathSegment<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		protected PathSegmentBase(INode<TNodeData> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			this.IncomingEdge = null;
			this.Node = node;
		}

		protected PathSegmentBase(TEdge incomingEdge, INode<TNodeData> node)
		{
			if (incomingEdge == null)
			{
				throw new ArgumentNullException($"Argument {nameof(incomingEdge)} cannot be null.");
			}

			if (node == null)
			{
				throw new ArgumentNullException($"Argument {nameof(node)} cannot be null.");
			}

			if (incomingEdge.Destination != node)
			{
				throw new ArgumentException($"Arguments {nameof(incomingEdge)}, {nameof(node)}: edge {incomingEdge} and node {node} aren't incidency.");
			}

			this.IncomingEdge = incomingEdge;
			this.Node = node;
		}

		public TEdge IncomingEdge { get; }
		public INode<TNodeData> Node { get; }

		public abstract double Length { get; }

		public override string ToString()
		{
			var stringIncomingEdge = (this.IncomingEdge != null) ? ($" - ({this.IncomingEdge}) - ") : String.Empty;

			return $"{stringIncomingEdge}[{this.Node}]";
		}
	}
}

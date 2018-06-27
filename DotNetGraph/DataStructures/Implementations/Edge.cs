using System;
using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class Edge<TNodeData> : IEdge<TNodeData>
	{
		public Edge(INode<TNodeData> source, INode<TNodeData> destination)
		{
			if (source == null)
			{
				throw new ArgumentNullException($"Argument {nameof(source)} cannot be null.");
			}

			if (destination == null)
			{
				throw new ArgumentNullException($"Argument {nameof(destination)} cannot be null.");
			}

			this.Source = source;
			this.Destination = destination;
		}

		public INode<TNodeData> Source { get; }
		public INode<TNodeData> Destination { get; }

		public virtual IEdge<TNodeData> GetReverseEdge() => new Edge<TNodeData>(source: this.Destination, destination: this.Source);

		public override string ToString() => $"{this.Source} -> {this.Destination}";
	}
}

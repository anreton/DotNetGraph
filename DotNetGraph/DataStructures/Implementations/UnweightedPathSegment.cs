using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class UnweightedPathSegment<TNodeData, TEdge> : PathSegmentBase<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		public UnweightedPathSegment(INode<TNodeData> node) : base(node)
		{
		}

		public UnweightedPathSegment(TEdge incomingEdge, INode<TNodeData> node) : base(incomingEdge, node)
		{
		}

		public override double Length => (this.IncomingEdge != null) ? 1 : 0;
	}
}

using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class WeightedPathSegment<TNodeData, TEdge> : PathSegmentBase<TNodeData, TEdge> where TEdge : class, IWeightedEdge<TNodeData>
	{
		public WeightedPathSegment(INode<TNodeData> node) : base(node)
		{
		}

		public WeightedPathSegment(TEdge incomingEdge, INode<TNodeData> node) : base(incomingEdge, node)
		{
		}

		public override double Length => this.IncomingEdge?.Weight ?? 0;
	}
}

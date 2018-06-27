namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public interface IWeightedEdge<TNodeData> : IEdge<TNodeData>
	{
		double Weight { get; }
	}
}

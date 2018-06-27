namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public interface IWeightedDataEdge<TNodeData, TEdgeData> : IWeightedEdge<TNodeData>, IDataEdge<TNodeData, TEdgeData>
	{
	}
}

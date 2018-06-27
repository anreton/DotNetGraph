namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public interface IDataEdge<TNodeData, TEdgeData> : IEdge<TNodeData>
	{
		TEdgeData Data { get; }
	}
}

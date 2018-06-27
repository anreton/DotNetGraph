namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public interface IDataGraph<TNodeData, TEdge, TGraphData> : IGraph<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		TGraphData Data { get; }
	}
}

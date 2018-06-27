namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public interface IPathSegment<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		TEdge IncomingEdge { get; }
		INode<TNodeData> Node { get; }
		double Length { get; }
	}
}

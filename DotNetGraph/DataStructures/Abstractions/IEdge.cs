namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public interface IEdge<TNodeData>
	{
		INode<TNodeData> Source { get; }
		INode<TNodeData> Destination { get; }

		IEdge<TNodeData> GetReverseEdge();
	}
}

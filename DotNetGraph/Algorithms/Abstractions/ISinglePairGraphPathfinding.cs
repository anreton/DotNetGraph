using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.Algorithms.Abstractions
{
	public interface ISinglePairGraphPathfinding<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		IPath<TNodeData, TEdge> FindPath(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, INode<TNodeData> goalNode);
	}
}

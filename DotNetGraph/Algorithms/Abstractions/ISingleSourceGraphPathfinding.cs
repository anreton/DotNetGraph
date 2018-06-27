using System.Collections.Generic;
using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.Algorithms.Abstractions
{
	public interface ISingleSourceGraphPathfinding<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		IDictionary<INode<TNodeData>, IPath<TNodeData, TEdge>> FindPaths(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode);
	}
}

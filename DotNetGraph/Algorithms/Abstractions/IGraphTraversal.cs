using System;
using System.Collections.Generic;
using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.Algorithms.Abstractions
{
	public interface IGraphTraversal<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		IList<INode<TNodeData>> Traverse(IGraph<TNodeData, TEdge> graph, bool traverseAllNodes);
		IList<INode<TNodeData>> Traverse(IGraph<TNodeData, TEdge> graph, bool traverseAllNodes, Action<INode<TNodeData>> nodeAction);
		IList<INode<TNodeData>> Traverse(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, bool traverseAllNodes);
		IList<INode<TNodeData>> Traverse(IGraph<TNodeData, TEdge> graph, INode<TNodeData> startNode, bool traverseAllNodes, Action<INode<TNodeData>> nodeAction);
	}
}

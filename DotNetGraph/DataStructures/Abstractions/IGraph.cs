using System.Collections.Generic;

namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public interface IGraph<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		int NodesCount { get; }
		int EdgesCount { get; }

		void AddNode(INode<TNodeData> node);
		bool RemoveNode(INode<TNodeData> node);
		bool ContainsNode(INode<TNodeData> node);
		bool HasEdge(INode<TNodeData> source, INode<TNodeData> destination);

		void AddEdge(TEdge edge);
		void AddUndirectedEdge(TEdge edge);
		bool RemoveEdge(TEdge edge);
		bool ContainsEdge(TEdge edge);

		IEnumerable<INode<TNodeData>> GetNodes();
		IEnumerable<INode<TNodeData>> GetAdjacencyNodes(INode<TNodeData> node);

		IEnumerable<TEdge> GetEdges();
		IEnumerable<TEdge> GetEdges(INode<TNodeData> source, INode<TNodeData> destination);
		IEnumerable<TEdge> GetIncomingEdges(INode<TNodeData> node);
		IEnumerable<TEdge> GetOutgoingEdges(INode<TNodeData> node);

		void Clear(bool onlyEdges);

		int GetDegree(INode<TNodeData> node);
		int GetIndegree(INode<TNodeData> node);
		int GetOutdegree(INode<TNodeData> node);
	}
}

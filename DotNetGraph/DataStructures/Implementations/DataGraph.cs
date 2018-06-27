using System;
using System.Linq;
using System.Text;
using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class DataGraph<TNodeData, TEdge, TGraphData> : Graph<TNodeData, TEdge>, IDataGraph<TNodeData, TEdge, TGraphData> where TEdge : class, IEdge<TNodeData>
	{
		public DataGraph(TGraphData data)
		{
			this.Data = data;
		}

		public TGraphData Data { get; }

		public override string ToString()
		{
			var graphStringBuilder = new StringBuilder();

			graphStringBuilder.AppendLine(this.Data.ToString());
			graphStringBuilder.AppendLine();
			graphStringBuilder.AppendLine("The adjacency list of the graph:");

			foreach (var item in this.AdjacencyList)
			{
				var node = item.Key;
				var edges = item.Value;

				var stringNode = node.ToString();
				var stringEdges = edges.Select(edge => edge.ToString());
				var stringJoinedEdges = String.Join("; ", stringEdges);

				graphStringBuilder.AppendLine($"{stringNode}: {stringJoinedEdges}");
			}

			return graphStringBuilder.ToString();
		}
	}
}

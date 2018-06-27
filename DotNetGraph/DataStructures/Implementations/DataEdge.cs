using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class DataEdge<TNodeData, TEdgeData> : Edge<TNodeData>, IDataEdge<TNodeData, TEdgeData>
	{
		public DataEdge(INode<TNodeData> source, INode<TNodeData> destination, TEdgeData data) : base(source, destination)
		{
			this.Data = data;
		}

		public TEdgeData Data { get; }

		public override IEdge<TNodeData> GetReverseEdge() => new DataEdge<TNodeData, TEdgeData>(source: this.Destination, destination: this.Source, data: this.Data);

		public override string ToString() => $"{this.Source} ---[{this.Data}]---> {this.Destination}";
	}
}

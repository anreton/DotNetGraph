using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class WeightedDataEdge<TNodeData, TEdgeData> : Edge<TNodeData>, IWeightedDataEdge<TNodeData, TEdgeData>
	{
		public WeightedDataEdge(INode<TNodeData> source, INode<TNodeData> destination, double weight, TEdgeData data) : base(source, destination)
		{
			this.Weight = weight;
			this.Data = data;
		}

		public double Weight { get; }
		public TEdgeData Data { get; }

		public override IEdge<TNodeData> GetReverseEdge() => new WeightedDataEdge<TNodeData, TEdgeData>(source: this.Destination, destination: this.Source, weight: this.Weight, data: this.Data);

		public override string ToString() => $"{this.Source} ---({this.Weight})-[{this.Data}]---> {this.Destination}";
	}
}

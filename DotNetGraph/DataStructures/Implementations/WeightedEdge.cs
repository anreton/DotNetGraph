using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class WeightedEdge<TNodeData> : Edge<TNodeData>, IWeightedEdge<TNodeData>
	{
		public WeightedEdge(INode<TNodeData> source, INode<TNodeData> destination, double weight) : base(source, destination)
		{
			this.Weight = weight;
		}

		public double Weight { get; }

		public override IEdge<TNodeData> GetReverseEdge() => new WeightedEdge<TNodeData>(source: this.Destination, destination: this.Source, weight: this.Weight);

		public override string ToString() => $"{this.Source} ---({this.Weight})---> {this.Destination}";
	}
}

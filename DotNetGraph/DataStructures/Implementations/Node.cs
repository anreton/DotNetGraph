using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class Node<TNodeData> : INode<TNodeData>
	{
		public Node(TNodeData data)
		{
			this.Data = data;
		}

		public TNodeData Data { get; }

		public override string ToString() => $"{this.Data}";
	}
}

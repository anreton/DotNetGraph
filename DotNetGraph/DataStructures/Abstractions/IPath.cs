using System.Collections.Generic;

namespace Anreton.DotNetGraph.DataStructures.Abstractions
{
	public interface IPath<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		IList<IPathSegment<TNodeData, TEdge>> Segments { get; }
		double Length { get; }
	}
}

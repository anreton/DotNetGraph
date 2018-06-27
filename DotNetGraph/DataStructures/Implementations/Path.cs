using System;
using System.Collections.Generic;
using System.Linq;
using Anreton.DotNetGraph.DataStructures.Abstractions;

namespace Anreton.DotNetGraph.DataStructures.Implementations
{
	public class Path<TNodeData, TEdge> : IPath<TNodeData, TEdge> where TEdge : class, IEdge<TNodeData>
	{
		public Path(IList<IPathSegment<TNodeData, TEdge>> segments)
		{
			if (segments == null)
			{
				throw new ArgumentNullException($"Argument {nameof(segments)} cannot be null.");
			}

			this.Segments = segments;
		}

		public IList<IPathSegment<TNodeData, TEdge>> Segments { get; }

		public double Length => (this.Segments.Count > 0) ? this.Segments.Sum(segment => segment.Length) : Double.PositiveInfinity;

		public override string ToString() => $"{{{String.Join(String.Empty, this.Segments)}}} ({this.Length})";
	}
}

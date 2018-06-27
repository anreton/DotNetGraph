# DotNetGraph #

DotNetGraph is class library for .NET Framework 4.6.1. This library is designed to work with generic graphs and some algorithms for them.

Data structures:

* Node with data (label, etc)
* Edge without weight
* Edge with weight (type of **Double**)
* Edge with data (label, etc)
* Edge with weight (type of **Double**) and data
* Graph consisting of nodes and a certain type of edge (specified above) without data
* Graph consisting of nodes and a certain type of edge (specified above) with data

Algorithms:

* Graph traversal - Depth-first search (non-recursive)
* Graph traversal - Breadth-first search
* Graph pathfinding - single-pair shortest path problem - Depth-first search (non-recursive)
* Graph pathfinding - single-pair shortest path problem - Breadth-first search
* Graph pathfinding - single-pair shortest path problem - Dijkstra's algorithm
* Graph pathfinding - single-source shortest path problem - Depth-first search (non-recursive)
* Graph pathfinding - single-source shortest path problem - Breadth-first search
* Graph pathfinding - single-source shortest path problem - Dijkstra's algorithm

## Examples ##

```csharp
// simple graph with edges without weight and data
var graph = new Graph<string, IEdge<string>();

// simple graph with edges with weight and without data
var graph = new Graph<string, IWeightedEdge<string>();

// simple graph with edges without weight and with data (type of String - second generic parameter)
var graph = new Graph<string, IDataEdge<string, string>();

// simple graph with edges with weight and data (type of String - second generic parameter)
var graph = new Graph<string, IWeightedDataEdge<string, string>();

// nodes that contain data of type string
var node1 = new Node<string>("n1");
var node2 = new Node<string>("n2");
var node3 = new Node<string>("n3");

// weighted data edges
var edge1 = new WeightedDataEdge<string, string>(node1, node1, 2, "e1");
var edge2 = new WeightedDataEdge<string, string>(node1, node1, 5, "e2");
var edge3 = new WeightedDataEdge<string, string>(node1, node2, 5, "e3");

// adding nodes
graph.AddNode(node1);
graph.AddNode(node2);
graph.AddNode(node3);

// adding edges
graph.AddEdge(edge1);
graph.AddEdge(edge2);
graph.AddEdge(edge3);

// graph traversal
var traversal = new DepthFirstGraphTraversal<string, IWeightedDataEdge<string, string>>();
var traversalPath = traversal.Traverse(graph, startNode: node2, traverseAllNodes: false);

// graph pathfinding
var pathfinder = new DijkstraGraphPathfinding<string, IWeightedDataEdge<string, string>>();
var path = pathfinder.FindPath(graph, startNode: node1, goalNode: node3);
var paths = pathfinder.FindPaths(graph, startNode: node1);
```
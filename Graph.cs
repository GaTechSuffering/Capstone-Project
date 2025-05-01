//create the graph class to store a graph
public class Graph<T> 
{ 
    //store the nodes of the graph and if it is weighted and directed
    private bool _isDirected = false; 
    private bool _isWeighted = false; 
    public List<Node<T>> Nodes { get; set; } = new List<Node<T>>(); 

    //initialize the graph with passed values for direction and weight
    public Graph(bool isDirected, bool isWeighted) 
    { 
        _isDirected = isDirected; 
        _isWeighted = isWeighted; 
    }

    //create a function to get an edge between two nodes
    public Edge<T>? this[int from, int to] 
    { 
        get 
        { 
            Node<T> nodeFrom = Nodes[from]; 
            Node<T> nodeTo = Nodes[to]; 
            int i = nodeFrom.Neighbors.IndexOf(nodeTo); 
            if (i >= 0) 
            { 
                Edge<T> edge = new Edge<T>() 
                { 
                    From = nodeFrom, 
                    To = nodeTo, 
                    Weight = i < nodeFrom.Weights.Count ? nodeFrom.Weights[i] : 0 
                }; 
                return edge; 
            } 
    
            return null; 
        } 
    }

    //create a function to add a node
    public Node<T> AddNode(T value) 
    { 
        Node<T> node = new Node<T>() { Data = value }; 
        Nodes.Add(node); 
        UpdateIndices(); 
        return node; 
    }

    //create a function to remove a node
    public void RemoveNode(Node<T> nodeToRemove) 
    { 
        Nodes.Remove(nodeToRemove); 
        UpdateIndices(); 
        foreach (Node<T> node in Nodes) 
        { 
            RemoveEdge(node, nodeToRemove); 
        } 
    }

    //create a function to add an edge
    public void AddEdge(Node<T> from, Node<T> to, int weight = 0) 
    { 
        from.Neighbors.Add(to); 
        if (_isWeighted) 
        { 
            from.Weights.Add(weight); 
        } 
    
        if (!_isDirected) 
        { 
            to.Neighbors.Add(from); 
            if (_isWeighted) 
            { 
                to.Weights.Add(weight); 
            } 
        } 
    }

    //create a function to remove an edge
    public void RemoveEdge(Node<T> from, Node<T> to) 
    { 
        int index = from.Neighbors.FindIndex(n => n == to); 
        if (index >= 0) 
        { 
            from.Neighbors.RemoveAt(index);
            if (_isWeighted)
            { 
                from.Weights.RemoveAt(index); 
            }
        } 
    }

    //create a function to get edges
    public List<Edge<T>> GetEdges() 
    { 
        List<Edge<T>> edges = new List<Edge<T>>(); 
        foreach (Node<T> from in Nodes) 
        { 
            for (int i = 0; i < from.Neighbors.Count; i++) 
            { 
                Edge<T> edge = new Edge<T>() 
                { 
                    From = from, 
                    To = from.Neighbors[i], 
                    Weight = i < from.Weights.Count ? from.Weights[i] : 0 
                }; 
                edges.Add(edge); 
            } 
        } 
        return edges; 
    }

    //create a function to update indices
    private void UpdateIndices() 
    { 
        int i = 0; 
        Nodes.ForEach(n => n.Index = i++); 
    }
}
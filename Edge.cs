//create the edge class for the graph to store the from node, to node, and weight
public class Edge<T> 
{ 
    public Node<T> From { get; set; } = new Node<T>();
    public Node<T> To { get; set; } = new Node<T>();
    public int Weight { get; set; } 
 
    public override string ToString() 
    { 
        return $"Edge: {From.Data} -> {To.Data}, weight: {Weight}"; 
    } 
}
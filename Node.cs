//create a class to store the node of a graph, including index, data, the neighbors, and the weight of the neighbors
public class Node<T> 
{ 
    public int Index { get; set; } 
    public T? Data { get; set; }
    public List<Node<T>> Neighbors { get; set; }  
        = new List<Node<T>>(); 
    public List<int> Weights { get; set; } = new List<int>(); 
 
    public override string ToString() 
    { 
        return $"Node with index {Index}: {Data}, neighbors: {Neighbors.Count}"; 
    } 
}
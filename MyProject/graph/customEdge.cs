using QuickGraph;

public class CustomEdge<T> : IEdge<T>
{
    public T Source { get; set; }
    public T Target { get; set; }
    public double Weight { get; set; }

    public CustomEdge(T source, T target, double weight)
    {
        Source = source;
        Target = target;
        Weight = weight;
    }
    
}
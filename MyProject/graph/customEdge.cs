using QuickGraph;
namespace customedge;
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

    public T GetSource()
    {
        return Source;
    }

    public void SetSource(T source)
    {
        Source = source;
    }

    public T GetTarget()
    {
        return Target;
    }

    public void SetTarget(T target)
    {
        Target = target;
    }

    public double GetWeight()
    {
        return Weight;
    }

    public void SetWeight(double weight)
    {
        Weight = weight;
    }
}

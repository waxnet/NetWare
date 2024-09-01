using NetWare.Attributes;

namespace NetWare.Models;

public sealed class Range<T>
{
    public Range()
    { }

    public Range(T min, T max)
    {
        Minimum = min;
        Maximum = max;
    }

    [ConfigProperty] public T Minimum { get; set; }
    [ConfigProperty] public T Maximum { get; set; }
}

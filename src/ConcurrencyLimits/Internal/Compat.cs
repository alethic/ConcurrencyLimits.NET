namespace ConcurrencyLimits.Internal;

/// <summary>
/// Members of the base class library that netstandard2.0 lacks, with the same behaviour as the ones .NET provides.
/// </summary>
internal static class Compat
{
#if !NETCOREAPP3_0_OR_GREATER
    private static readonly ThreadLocal<Random> LocalRandom = new(() => new Random(Guid.NewGuid().GetHashCode()));
#endif

    /// <summary>A thread-safe random number generator, as <c>Random.Shared</c>.</summary>
    public static Random Random =>
#if NET6_0_OR_GREATER
        Random.Shared;
#else
        LocalRandom.Value!;
#endif

    /// <summary>The value clamped to the inclusive range, as <c>Math.Clamp</c>.</summary>
    public static long Clamp(long value, long min, long max) => value < min ? min : value > max ? max : value;

    /// <summary>The value clamped to the inclusive range, as <c>Math.Clamp</c>.</summary>
    public static int Clamp(int value, int min, int max) => value < min ? min : value > max ? max : value;

    /// <summary>The whole duration in nanoseconds, as <c>TimeSpan.TotalNanoseconds</c>.</summary>
    public static double TotalNanoseconds(TimeSpan value) => value.Ticks * 100.0;

    /// <summary>A duration of the microseconds given, as <c>TimeSpan.FromMicroseconds</c>.</summary>
    public static TimeSpan FromMicroseconds(long microseconds) => TimeSpan.FromTicks(microseconds * 10);
}

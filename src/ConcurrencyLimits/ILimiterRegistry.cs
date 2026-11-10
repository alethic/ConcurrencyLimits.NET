namespace ConcurrencyLimits;

/// <summary>
/// <see cref="ILimiter{TContext}"/> lookup for integrations that support multiple limiters,
/// i.e. one per RPC method.
/// </summary>
public interface ILimiterRegistry<TContext>
{
    ILimiter<TContext> Get(string key);

#if NETCOREAPP3_0_OR_GREATER
    static ILimiterRegistry<TContext> Single(ILimiter<TContext> limiter) => LimiterRegistry.Single(limiter);
#endif
}

/// <summary>
/// Factory methods for <see cref="ILimiterRegistry{TContext}"/>.
/// </summary>
public static class LimiterRegistry
{
    /// <summary>
    /// A registry that returns the same limiter for every key.
    /// </summary>
    public static ILimiterRegistry<TContext> Single<TContext>(ILimiter<TContext> limiter) => new SingleLimiterRegistry<TContext>(limiter);
}

internal sealed class SingleLimiterRegistry<TContext>(ILimiter<TContext> limiter) : ILimiterRegistry<TContext>
{
    public ILimiter<TContext> Get(string key) => limiter;
}

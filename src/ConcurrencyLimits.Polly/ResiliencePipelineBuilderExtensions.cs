using Polly;

namespace ConcurrencyLimits.Polly;

/// <summary>
/// Extensions for adding the ConcurrencyLimits resilience strategy to a Polly pipeline.
/// </summary>
public static class ResiliencePipelineBuilderExtensions
{
    /// <summary>
    /// Add a concurrency-limit strategy gated by the supplied limiter.
    /// </summary>
    public static TBuilder AddConcurrencyLimit<TBuilder>(this TBuilder builder, ILimiter<ResilienceContext> limiter)
        where TBuilder : ResiliencePipelineBuilderBase
    {
        if (builder is null) throw new ArgumentNullException(nameof(builder));
        if (limiter is null) throw new ArgumentNullException(nameof(limiter));
        return builder.AddConcurrencyLimit(new ConcurrencyLimitStrategyOptions { Limiter = limiter });
    }

    /// <summary>
    /// Add a concurrency-limit strategy configured by <paramref name="options"/>.
    /// </summary>
    public static TBuilder AddConcurrencyLimit<TBuilder>(this TBuilder builder, ConcurrencyLimitStrategyOptions options)
        where TBuilder : ResiliencePipelineBuilderBase
    {
        if (builder is null) throw new ArgumentNullException(nameof(builder));
        if (options is null) throw new ArgumentNullException(nameof(options));

        builder.AddStrategy(
            _ => new ConcurrencyLimitStrategy(options.Limiter, options.RejectionExceptionFactory),
            options);
        return builder;
    }
}

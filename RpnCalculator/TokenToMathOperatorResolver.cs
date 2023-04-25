using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using RpnCalculator.Abstractions;

namespace RpnCalculator;

public interface ITokenToMathOperatorResolver
{
    public bool TryResolveMathOperator(string token, [NotNullWhen(true)] out IMathOperator? mathOperator);
}

public class TokenToMathOperatorResolver : ITokenToMathOperatorResolver
{
    private readonly ILogger<TokenToMathOperatorResolver> _logger;
    private readonly IImmutableDictionary<string, IMathOperator> _operators;

    public TokenToMathOperatorResolver(ILogger<TokenToMathOperatorResolver> logger, IEnumerable<IMathOperator> operators)
    {
        _logger = logger;
        _operators = operators.ToImmutableDictionary(x => x.IdentityToken);

        _logger.LogInformation("Loaded {Count} total math operators", _operators.Count);
    }

    public bool TryResolveMathOperator(string token, [NotNullWhen(true)] out IMathOperator? mathOperator)
    {
        _logger.LogInformation("Attempt to resolve token '{Token}'", token);

        return _operators.TryGetValue(token, out mathOperator);
    }
}
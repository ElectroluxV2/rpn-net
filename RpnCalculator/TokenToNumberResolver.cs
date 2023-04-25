using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using RpnCalculator.Abstractions;

namespace RpnCalculator;

public interface ITokenToNumberResolver
{
    public bool TryResolveNumber(string token, [NotNullWhen(true)] out long? number);
}

public class TokenToNumberResolver : ITokenToNumberResolver
{
    private readonly ILogger<TokenToNumberResolver> _logger;
    private readonly ICollection<INumberParser> _parsers;

    public TokenToNumberResolver(ILogger<TokenToNumberResolver> logger, IEnumerable<INumberParser> parsers)
    {
        _logger = logger;
        _parsers = parsers
            .OrderByDescending(x => x.Priority)
            .ToList();
        
        _logger.LogInformation("Loaded {Count} total number parsers", _parsers.Count);
    }

    public bool TryResolveNumber(string token, [NotNullWhen(true)] out long? number)
    {
        _logger.LogInformation("Attempt to resolve token '{Token}'", token);

        foreach (var parser in _parsers)
        {
            if (!parser.TryParseNumber(token, out var parsed)) continue;

            _logger.LogInformation("Parser {ParserName} successfully resolved number {Parsed}", parser.GetType().Name, parsed);
            number = parsed;
            return true;
        }

        number = null;
        return false;
    }
}
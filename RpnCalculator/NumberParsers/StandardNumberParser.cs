using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using RpnCalculator.Abstractions;

namespace RpnCalculator.NumberParsers;

public class StandardNumberParser : INumberParser
{
    private readonly ILogger<StandardNumberParser> _logger;

    public StandardNumberParser(ILogger<StandardNumberParser> logger)
    {
        _logger = logger;
    }

    public int Priority => 0;

    public bool TryParseNumber(string input, [NotNullWhen(true)] out long? number)
    {
        _logger.LogInformation("Attempting to parse {Input}", input);

        try
        {
            number = Convert.ToInt32(input);
            return true;
        }
        catch (FormatException)
        {
            _logger.LogInformation("Input string is not a sequence of digits");
        }
        catch (OverflowException)
        {
            _logger.LogInformation("The number cannot fit in an Int32");
        }

        number = null;
        return false;
    }
}
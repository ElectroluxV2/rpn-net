using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using RpnCalculator.Abstractions;

namespace RpnCalculator.NumberParsers;

public class OctalNumberParser : INumberParser
{
    private readonly ILogger<OctalNumberParser> _logger;

    public OctalNumberParser(ILogger<OctalNumberParser> logger)
    {
        _logger = logger;
    }

    public int Priority => 0;
    public bool TryParseNumber(string input, [NotNullWhen(true)] out long? number)
    {
        if (!input.EndsWith("o"))
        {
            number = null;
            _logger.LogInformation("Input string is not a octal number");
            return false;
        }
        
        try
        {
            number = Convert.ToInt32(input[..^1], 8);
            return true;
        }
        catch (FormatException)
        {
            _logger.LogInformation("Input string is not a sequence of octal number");
        }
        catch (OverflowException)
        {
            _logger.LogInformation("The number cannot fit in an Int32");
        }

        number = null;
        return false;
    }
}
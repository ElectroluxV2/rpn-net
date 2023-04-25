using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using RpnCalculator.Abstractions;

namespace RpnCalculator.NumberParsers;

public class BinaryNumberParser : INumberParser
{
    private readonly ILogger<BinaryNumberParser> _logger;

    public BinaryNumberParser(ILogger<BinaryNumberParser> logger)
    {
        _logger = logger;
    }

    public int Priority => 0;
    public bool TryParseNumber(string input, [NotNullWhen(true)] out long? number)
    {
        if (!input.EndsWith("b"))
        {
            number = null;
            _logger.LogInformation("Input string is not a binary number");
            return false;
        }
        
        try
        {
            number = Convert.ToInt32(input[..^1], 2);
            return true;
        }
        catch (FormatException)
        {
            _logger.LogInformation("Input string is not a sequence of binary number");
        }
        catch (OverflowException)
        {
            _logger.LogInformation("The number cannot fit in an Int32");
        }

        number = null;
        return false;
    }
}
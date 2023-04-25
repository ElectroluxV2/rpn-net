using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using RpnCalculator.Abstractions;

namespace RpnCalculator.NumberParsers;

public class HexadecimalNumberParser : INumberParser
{
    private readonly ILogger<HexadecimalNumberParser> _logger;

    public HexadecimalNumberParser(ILogger<HexadecimalNumberParser> logger)
    {
        _logger = logger;
    }

    public int Priority => 0;
    public bool TryParseNumber(string input, [NotNullWhen(true)] out long? number)
    {
        if (!input.StartsWith("0x"))
        {
            number = null;
            _logger.LogInformation("Input string is not a hexadecimal number");
            return false;
        }
        
        try
        {
            number = Convert.ToInt32(input[2..], 16);
            return true;
        }
        catch (FormatException)
        {
            _logger.LogInformation("Input string is not a sequence of hexadecimal number");
        }
        catch (OverflowException)
        {
            _logger.LogInformation("The number cannot fit in an Int32");
        }

        number = null;
        return false;
    }
}
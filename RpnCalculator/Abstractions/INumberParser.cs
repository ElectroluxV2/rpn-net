using System.Diagnostics.CodeAnalysis;

namespace RpnCalculator.Abstractions;

public interface INumberParser
{
    public int Priority { get; }
    public bool TryParseNumber(string input, [NotNullWhen(true)] out long? number);
}
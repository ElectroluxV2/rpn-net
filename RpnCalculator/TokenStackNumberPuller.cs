using System.Diagnostics.CodeAnalysis;

namespace RpnCalculator;

public interface ITokenStackNumberPuller
{
    public bool TryPullNextNumber([NotNullWhen(true)] out long? number);
}

public class TokenStackNumberPuller : ITokenStackNumberPuller
{
    private readonly Stack<long> _numbers;

    public TokenStackNumberPuller(ref Stack<long> numbers)
    {
        _numbers = numbers;
    }

    public bool TryPullNextNumber([NotNullWhen(true)] out long? number)
    {
        // For some reason Stack<T>::TryPop does not declare out var as nullable
        var result = _numbers.TryPop(out var top);
        
        number = top;
        return result;
    }
}
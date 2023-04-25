using RpnCalculator.Abstractions;
using RpnCalculator.Exceptions;

namespace RpnCalculator.MathOperators;

public class AdditionMathOperator : IMathOperator
{
    public string IdentityToken => "+";

    public long Execute(ITokenStackNumberPuller tokenStackNumberPuller)
    {
        if (!tokenStackNumberPuller.TryPullNextNumber(out var a))
            throw new NotEnoughNumbersException("Expected exactly 2 numbers, but no number were available");
        
        if (!tokenStackNumberPuller.TryPullNextNumber(out var b))
            throw new NotEnoughNumbersException("Expected exactly 2 numbers, but only 1 number was available");

        return a.Value + b.Value;
    }
}
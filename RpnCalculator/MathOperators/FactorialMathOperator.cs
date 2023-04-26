using MathNet.Numerics;
using RpnCalculator.Abstractions;
using RpnCalculator.Exceptions;

namespace RpnCalculator.MathOperators;

public class FactorialMathOperator : IMathOperator
{
    public string IdentityToken => "!";
    public long Execute(ITokenStackNumberPuller tokenStackNumberPuller)
    {
        if (!tokenStackNumberPuller.TryPullNextNumber(out var a))
            throw new NotEnoughNumbersException("Expected exactly 1 number, but no numbers were available");

        return (long) SpecialFunctions.Factorial(a.Value);
    }
}
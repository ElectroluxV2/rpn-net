using MathNet.Numerics;
using RpnCalculator.Abstractions;
using RpnCalculator.Exceptions;

namespace RpnCalculator.MathOperators;

public class ModuloMathOperator : IMathOperator
{
    public string IdentityToken => "mod";
    public long Execute(ITokenStackNumberPuller tokenStackNumberPuller)
    {
        if (!tokenStackNumberPuller.TryPullNextNumber(out var a))
            throw new NotEnoughNumbersException("Expected exactly 1 number, but no numbers were available");

        return Math.Abs(a.Value);
    }
}
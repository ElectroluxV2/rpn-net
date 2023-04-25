using RpnCalculator.Abstractions;
using RpnCalculator.Exceptions;

namespace RpnCalculator.MathOperators;

public class AverageMathOperator : IMathOperator
{
    public string IdentityToken => "avg";

    public long Execute(ITokenStackNumberPuller tokenStackNumberPuller)
    {
        var numbers = new List<long>();

        while (tokenStackNumberPuller.TryPullNextNumber(out var number))
        {
            numbers.Add(number.Value);
        }
        
        if (numbers.Count == 0)
            throw new NotEnoughNumbersException("Expected at least 1 number, but no numbers were available");

        return (long) numbers.Average();
    }
}
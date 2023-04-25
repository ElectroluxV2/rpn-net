namespace RpnCalculator.Exceptions;

public class UnexpectedTokenException : Exception
{
    public UnexpectedTokenException(string message) : base(message)
    {
    }
}
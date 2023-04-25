namespace RpnCalculator.Exceptions;

public class NotEnoughNumbersException : Exception
{
    public NotEnoughNumbersException(string message) : base(message)
    {
    }
}
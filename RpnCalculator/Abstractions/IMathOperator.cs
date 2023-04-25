namespace RpnCalculator.Abstractions;

public interface IMathOperator
{
    public string IdentityToken { get; }
    
    public long Execute(ITokenStackNumberPuller tokenStackNumberPuller);
}
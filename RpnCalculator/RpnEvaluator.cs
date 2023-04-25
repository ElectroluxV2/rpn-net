using Microsoft.Extensions.Logging;
using RpnCalculator.Exceptions;

namespace RpnCalculator;

public interface IRpnEvaluator
{
    public long Evaluate(string expression);
}

public class RpnEvaluator : IRpnEvaluator
{
    private readonly ILogger<RpnEvaluator> _logger;
    private readonly ITokenToMathOperatorResolver _operatorResolver;
    private readonly ITokenToNumberResolver _numberResolver;

    public RpnEvaluator(ILogger<RpnEvaluator> logger, ITokenToMathOperatorResolver operatorResolver, ITokenToNumberResolver numberResolver)
    {
        _logger = logger;
        _operatorResolver = operatorResolver;
        _numberResolver = numberResolver;
    }

    public long Evaluate(string expression)
    {
        _logger.LogInformation("Evaluating expression: '{Expression}'", expression);

        var numbers = new Stack<long>();
        var numberPuller = new TokenStackNumberPuller(ref numbers);
        
        var tokens = expression.Split(null);
        foreach (var token in tokens)
        {
            if (_numberResolver.TryResolveNumber(token, out var number))
            {
                numbers.Push(number.Value);
                continue;
            }

            if (_operatorResolver.TryResolveMathOperator(token, out var mathOperator))
            {
                var result = mathOperator.Execute(numberPuller);
                numbers.Push(result);
                continue;
            }

            throw new UnexpectedTokenException($"'{token}' is not valid number neither math operator");
        }

        if (numbers.Count != 1) throw new InvalidOperationException("Too many numbers were given");

        return numbers.Pop();
    }
}
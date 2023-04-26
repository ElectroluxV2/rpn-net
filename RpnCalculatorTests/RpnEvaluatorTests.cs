using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RpnCalculator;
using RpnCalculator.Abstractions;
using RpnCalculator.Exceptions;
using RpnCalculator.MathOperators;
using RpnCalculator.NumberParsers;

namespace RpnCalculatorTests;

public class RpnEvaluatorTests
{
    private readonly IRpnEvaluator _rpn;

    public RpnEvaluatorTests()
    {
        var host = Host
            .CreateDefaultBuilder()
            .UseConsoleLifetime()
            .ConfigureServices(services =>
            {
                services.AddSingleton<INumberParser, StandardNumberParser>();
                services.AddSingleton<INumberParser, HexadecimalNumberParser>();
                services.AddSingleton<INumberParser, OctalNumberParser>();
                services.AddSingleton<INumberParser, BinaryNumberParser>();

                services.AddSingleton<IMathOperator, AdditionMathOperator>();
                services.AddSingleton<IMathOperator, SubtractionMathOperator>();
                services.AddSingleton<IMathOperator, MultiplicationMathOperator>();
                services.AddSingleton<IMathOperator, DivisionMathOperator>();
                services.AddSingleton<IMathOperator, AverageMathOperator>();
                services.AddSingleton<IMathOperator, FactorialMathOperator>();
                services.AddSingleton<IMathOperator, ModuloMathOperator>();

                services.AddSingleton<ITokenToNumberResolver, TokenToNumberResolver>();
                services.AddSingleton<ITokenToMathOperatorResolver, TokenToMathOperatorResolver>();
                services.AddSingleton<IRpnEvaluator, RpnEvaluator>();
            })
            .Build();

        _rpn = host.Services.GetRequiredService<IRpnEvaluator>();
    }

    [Test]
    public void SingleDigit_ShouldReturnSingleDigit()
    {
        _rpn
            .Evaluate("1")
            .Should()
            .Be(1);
    }
    
    [Test]
    public void MultipleDigit_ShouldReturnMultipleDigit()
    {
        _rpn
            .Evaluate("2137")
            .Should()
            .Be(2137);
    }
    
    [Test]
    public void TwoNumbersWithoutOperator_ShouldThrowException()
    {
        _rpn
            .Invoking(x => x.Evaluate("21 37"))
            .Should()
            .Throw<InvalidOperationException>();
    }

    [Test]
    public void AddingTwoNumbers_ShouldReturnCorrectResult()
    {
        _rpn
            .Evaluate("1 2 +")
            .Should()
            .Be(3);
    }
    
    [Test]
    public void SubtractingTwoNumbers_ShouldReturnCorrectResult()
    {
        _rpn
            .Evaluate("2 1 -")
            .Should()
            .Be(-1);
    }
    
    [Test]
    public void MultiplyingTwoNumbers_ShouldReturnCorrectResult()
    {
        _rpn
            .Evaluate("2 3 *")
            .Should()
            .Be(6);
    }
    
    [Test]
    public void DividingTwoNumbers_ShouldReturnCorrectResult()
    {
        _rpn
            .Evaluate("2 6 /")
            .Should()
            .Be(3);
    }

    [Test]
    public void Fractional_ShouldWork()
    {
        _rpn
            .Evaluate("3 !")
            .Should()
            .Be(6);
    }

    [Test]
    public void Modulo_ShouldWork()
    {
        _rpn
            .Evaluate("-3 mod")
            .Should()
            .Be(3);
    }

    [Test]
    public void Average_ShouldWork()
    {
        _rpn
            .Evaluate("1 2 3 avg")
            .Should()
            .Be(2);
    }

    [Test]
    public void MultipleBaseSystems_ShouldWork()
    {
        _rpn
            .Evaluate("2137 111b 0xFF 337o + - *")
            .Should()
            .Be(1006527);
    }

    [Test]
    public void RpnShouldThrow_WhenUnexpectedTokenOccur()
    {
        _rpn
            .Invoking(x => x.Evaluate("not a number"))
            .Should()
            .Throw<UnexpectedTokenException>();
    }

    [Test]
    public void AdditionOperator_ShouldThrow_WhenOnlyOneNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("2 +"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void SubtractionOperator_ShouldThrow_WhenOnlyOneNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("2 -"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void MultiplicationOperator_ShouldThrow_WhenOnlyOneNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("2 *"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void DivisionOperator_ShouldThrow_WhenOnlyOneNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("2 /"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void AdditionOperator_ShouldThrow_WhenNoNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("+"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void SubtractionOperator_ShouldThrow_WhenNoNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("-"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void MultiplicationOperator_ShouldThrow_WhenNoNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("*"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void DivisionOperator_ShouldThrow_WhenNoNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("/"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void FactorialOperator_ShouldThrow_WhenNoNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("!"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void ModuloOperator_ShouldThrow_WhenNoNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("mod"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }
    
    [Test]
    public void AverageOperator_ShouldThrow_WhenNoNumberIsGiven()
    {
        _rpn
            .Invoking(x => x.Evaluate("avg"))
            .Should()
            .Throw<NotEnoughNumbersException>();
    }

    [Test]
    public void RpnShouldWorkForComplexCases()
    {
        _rpn
            .Evaluate("1 2 3 avg 15 7 1 1 + - / 3 * 2 1 1 + + - +")
            .Should()
            .Be(6);
    }
}
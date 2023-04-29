using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RpnCalculator.Abstractions;
using RpnCalculator.MathOperators;
using RpnCalculator.NumberParsers;

namespace RpnCalculator;

public static class ServiceProviderFactory
{
    public static IServiceProvider CreateServiceProvider()
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

        return host.Services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RpnCalculator;
using RpnCalculator.Abstractions;
using RpnCalculator.MathOperators;
using RpnCalculator.NumberParsers;

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

var logger = host.Services.GetRequiredService<ILogger<Program>>();
var evaluator = host.Services.GetRequiredService<IRpnEvaluator>();

// var result = evaluator.Evaluate("15 7 1 1 + - / 3 * 2 1 1 + + -"); // =4
// var result = evaluator.Evaluate("0xFF 11111111b -"); // =0
// var result = evaluator.Evaluate("0xFF 377o -"); // =0
// var result = evaluator.Evaluate("0xFF 377b -"); // InvalidTokenException
// var result = evaluator.Evaluate("1 2 3 avg"); // =2
// var result = evaluator.Evaluate("1 2 3 avg 15 7 1 1 + - / 3 * 2 1 1 + + - +"); // =6
// var result = evaluator.Evaluate("3 !"); // =6
var result = evaluator.Evaluate("-3 mod"); // =3

logger.LogInformation("Eval: {Result}", result);
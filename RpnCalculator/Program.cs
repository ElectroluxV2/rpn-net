using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RpnCalculator;

var sp = ServiceProviderFactory.CreateServiceProvider();

var logger = sp.GetRequiredService<ILogger<Program>>();
var evaluator = sp.GetRequiredService<IRpnEvaluator>();

// var result = evaluator.Evaluate("15 7 1 1 + - / 3 * 2 1 1 + + -"); // =4
// var result = evaluator.Evaluate("0xFF 11111111b -"); // =0
// var result = evaluator.Evaluate("0xFF 377o -"); // =0
// var result = evaluator.Evaluate("0xFF 377b -"); // InvalidTokenException
// var result = evaluator.Evaluate("1 2 3 avg"); // =2
// var result = evaluator.Evaluate("1 2 3 avg 15 7 1 1 + - / 3 * 2 1 1 + + - +"); // =6
// var result = evaluator.Evaluate("3 !"); // =6
var result = evaluator.Evaluate("-3 mod"); // =3

logger.LogInformation("Eval: {Result}", result);
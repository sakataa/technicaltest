using Microsoft.Extensions.DependencyInjection;
using RobotSimulation.Console;
using RobotSimulation.Console.Commands;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Validators;

ServiceCollection serviceCollection = new();
serviceCollection.AddTransient<ICommandArgumentsValidator, CommandArgumentsValidator>();
serviceCollection.AddTransient<IPlaceCommandValidator, PlaceCommandValidator>();

serviceCollection.AddScoped<ICommandFactory, CommandFactory>();

ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

var runner = new ConsoleRunner(serviceProvider);
runner.Run();
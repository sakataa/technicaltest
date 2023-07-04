using Microsoft.Extensions.DependencyInjection;
using RobotSimulation.Console;
using RobotSimulation.Console.Commands;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;
using RobotSimulation.Console.Utilities;
using RobotSimulation.Console.Validators;

ServiceCollection serviceCollection = new();
serviceCollection.AddTransient<ICommandArgumentsValidator, CommandArgumentsValidator>();
serviceCollection.AddScoped<ICommandFactory, CommandFactory>();

ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

List<Point> tablePositions = new();

for (var x = 0; x < 5; x++)
{
    for (var y = 0; y < 5; y++)
    {
        tablePositions.Add(new Point(x, y));
    }
}

System.Console.WriteLine();
System.Console.WriteLine("Welcome to the robot simulator!");

Location? robotLocation = null;

while (true)
{
    string promptMessage = robotLocation is null ? "Please enter your command:" : "Please enter your next command:";
    System.Console.WriteLine(promptMessage);
    string enteredCommand = System.Console.ReadLine() ?? string.Empty;

    ICommandArgumentsValidator validator = serviceProvider.GetRequiredService<ICommandArgumentsValidator>();
    IEnumerable<string> validationMessages = validator.Validate(enteredCommand);

    if (validationMessages.Any())
    {
        string validationMessage = validationMessages.First();
        System.Console.WriteLine(validationMessage);
        continue;
    }

    CommandArgument commandArgument = CommandArgumentParser.FromCommandLineInput(enteredCommand);

    if (commandArgument.CommandType == CommandType.Report && robotLocation is not null)
    {
        System.Console.WriteLine($"{robotLocation.X},{robotLocation.Y},{robotLocation.FacingType}");
        break;
    }

    ICommandFactory commandFactory = serviceProvider.GetRequiredService<ICommandFactory>();
    ICommand? command = commandFactory.GetCommand(commandArgument.CommandType);

    if (command is null)
    {
        System.Console.WriteLine("Invalid command");
        continue;
    }

    if (commandArgument.CommandType != CommandType.Place && robotLocation is not null)
    {
        commandArgument = new(commandArgument.CommandType, robotLocation.X, robotLocation.Y, robotLocation.FacingType);
    }

    Location candidateLocation = command.Handle(commandArgument);

    bool isValidLocation = tablePositions.Any(p => p.X == candidateLocation.X && p.Y == candidateLocation.Y);

    if (isValidLocation)
    {
        robotLocation = candidateLocation;

        System.Console.WriteLine($"Current position: {robotLocation.X},{robotLocation.Y},{robotLocation.FacingType}");
    }
    else
    {
        System.Console.WriteLine($"Invalid location. The moving is ignored");

        if (robotLocation is not null)
        {
            System.Console.WriteLine($"Current position: {robotLocation.X},{robotLocation.Y},{robotLocation.FacingType}");
        }
    }
}

System.Console.WriteLine("Press any key to exit...");
System.Console.ReadKey(true);

namespace RobotSimulation.Console
{
    public record Point(int X, int Y);
}
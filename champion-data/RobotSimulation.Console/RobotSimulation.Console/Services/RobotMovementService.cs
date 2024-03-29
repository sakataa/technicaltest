﻿using RobotSimulation.Console.Constants;
using RobotSimulation.Console.Extensions;
using RobotSimulation.Console.Interfaces;
using RobotSimulation.Console.Models;
using RobotSimulation.Console.Utilities;

namespace RobotSimulation.Console.Services;

public class RobotMovementService : IRobotMovementService
{
    private static readonly IReadOnlyCollection<Point> _tablePositions = InitValidPositions();

    private readonly ICommandArgumentsValidator _commandArgumentsValidator;
    private readonly IPlaceCommandValidator _placeCommandValidator;
    private readonly ICommandFactory _commandFactory;

    public RobotMovementService(ICommandArgumentsValidator commandArgumentsValidator, IPlaceCommandValidator placeCommandValidator, ICommandFactory commandFactory)
    {
        _commandArgumentsValidator = commandArgumentsValidator;
        _placeCommandValidator = placeCommandValidator;
        _commandFactory = commandFactory;
    }

    public OperationResult Move(string commandLine, Location? currentLocation)
    {
        commandLine = commandLine.Trim();
        if (currentLocation is null)
        {
            OperationResult firstCommandValidationResult = ValidateFirstCommand(commandLine);

            if (!firstCommandValidationResult.IsSuccessful)
            {
                return firstCommandValidationResult;
            }
        }

        IEnumerable<string> validationMessages = _commandArgumentsValidator.Validate(commandLine);

        if (validationMessages.Any())
        {
            return new OperationResult
            {
                Value = currentLocation,
                ErrorMessage = validationMessages.First(),
                ShouldStopProcessing = false
            };
        }

        CommandArgument commandArgument = CommandArgumentParser.FromCommandLine(commandLine);

        if (commandArgument.CommandType == CommandType.Report && currentLocation is not null)
        {
            return new OperationResult
            {
                Value = currentLocation,
                ReportMessage = $"Current position: {currentLocation.X},{currentLocation.Y},{currentLocation.FacingType.ToString().ToUpperInvariant()}"
            };
        }

        Location? nextLocation = ProcessForSingleMovement(commandArgument, currentLocation);

        return new OperationResult
        {
            Value = nextLocation,
        };
    }

    private Location? ProcessForSingleMovement(CommandArgument commandArgument, Location? currentLocation)
    {
        ICommand command = _commandFactory.GetCommand(commandArgument.CommandType);

        CommandArgument commandArgumentTohandle = commandArgument.CommandType != CommandType.Place && currentLocation is not null ?
            new(commandArgument.CommandType, currentLocation.X, currentLocation.Y, currentLocation.FacingType) :
            commandArgument;

        Location candidateLocation = command.Handle(commandArgumentTohandle);

        bool isValidLocation = _tablePositions.Any(p => p.X == candidateLocation.X && p.Y == candidateLocation.Y);

        return isValidLocation ? candidateLocation : currentLocation;
    }

    private OperationResult ValidateFirstCommand(string commandLine)
    {
        commandLine = commandLine.Trim();

        var defaultResult = new OperationResult
        {
            ErrorMessage = "The first valid command to the robot is a PLACE command",
            ShouldStopProcessing = true
        };

        if (string.IsNullOrWhiteSpace(commandLine))
        {
            return defaultResult;
        }

        (string CommandName, IEnumerable<string> Arguments) = CommandArgumentParser.ToCommandNameAndArguments(commandLine);

        if (!CommandName.Match(CommandType.Place.ToString()))
        {
            return defaultResult;
        }

        IEnumerable<string> placeCommandValidationMessages = _placeCommandValidator.Validate(Arguments);

        if (placeCommandValidationMessages.Any())
        {
            return new OperationResult
            {
                ErrorMessage = placeCommandValidationMessages.First(),
                ShouldStopProcessing = true
            };
        }

        return new OperationResult();
    }

    private static IReadOnlyCollection<Point> InitValidPositions()
    {
        List<Point> result = new();

        for (var x = 0; x < AppConstants.TableWidth; x++)
        {
            for (var y = 0; y < AppConstants.TableHeight; y++)
            {
                result.Add(new Point(x, y));
            }
        }

        return result.AsReadOnly();
    }
}
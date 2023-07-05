# Champion Data - Robot Simulator
## The technical test

## Overview
- Written by C# language with .NET 6. The type of application is console.
- The application support 2 modes from input commands. Enter **1** for command line mode and **2** for reading file mode.
- I also build a release package and copied all files to the app-run directory in the root of repository, so you can go inside it, open up the `.exe` file to play or modify the data test in the `TestFiles/commands.txt`
- If you open with Visual Studio or Visual Studio Code The path of file text is `TestFiles/commands.txt`. We need to re-build the project if we have any modification for that file.
- The project is simple, so just open visual studio and press F5. Or you can use the dotnet CLI.
    ```bash
    cd champion-data/RobotSimulation.Console/RobotSimulation.Console
    dotnet run
    ```
- `AppRunner.cs` will be the entry point of the application. Base on your selection with mode, it will determine the appropriate runner and we can start controlling the robot movement.
- Apply command pattern for each type of command input.
- Use factory method to get the appropriate command by type (place, move, left, right, report).
## Workflow
- The main workflow will be put in the `RobotMovementService.cs` class with following steps in the high level:
    - Read the command input (from command line or file).
    - Validation to make sure it in correct format.
    - Parse the command line to the `CommandArgument` object.
    - Process the movement of robot with above `CommandArgument` object.
    - Prevent robot to fall from the table by validating the new position of robot with the acceptable position range.
    - Allow to keep moving the robot after printing report.
- The output for report command will be following format:
    `Current position: X,Y,Facing`

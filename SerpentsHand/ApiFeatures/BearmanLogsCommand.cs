using System;
using CommandSystem;

namespace SerpentsHand.ApiFeatures;

[CommandHandler(typeof(GameConsoleCommandHandler))]
public class BearmanLogsSH : ICommand
{
    public string Command => "bearmanlogsSH";

    public string[] Aliases { get; } = ["bmlogsSH"];

    public string Description => "Sends collected plugin logs to the log server and returns the log id.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var getLogHistory = LogManager.GetLogHistory();
        response = getLogHistory.logResult;
        return getLogHistory.success;
    }
}
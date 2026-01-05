using System;
using Logger = LabApi.Features.Console.Logger;

namespace SerpentsHand.Managers;

internal static class LogManager
{
    private static bool DebugEnabled => SerpentsHand.Singleton.Config!.Debug;

    public static void Debug(string message)
    {
        if (!DebugEnabled)
            return;

        Logger.Raw($"[DEBUG] [{SerpentsHand.Singleton.Name}] {message}", ConsoleColor.Green);
    }

    public static void Info(string message, ConsoleColor color = ConsoleColor.Cyan)
    {
        Logger.Raw($"[INFO] [{SerpentsHand.Singleton.Name}] {message}", color);
    }

    public static void Warn(string message)
    {
        Logger.Warn(message);
    }

    public static void Error(string message)
    {
        Logger.Error(message);
    }
}
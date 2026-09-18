using System;
using System.Linq;
using System.Reflection;
using SerpentsHand.ApiFeatures;
using SerpentsHand.ShWave;

namespace SerpentsHand.Integrations;

internal static class RespawnTimerIntegration
{
    private const string TimerApiTypeName = "RespawnTimer.API.TimerAPI";

    private const string DisplayName = "<color=#FF96DE>Serpent's Hand</color>";

    private const string Placeholder = "sh";

    private const float SpawnDuration = 14f;

    private static Type TimerApi => AppDomain.CurrentDomain.GetAssemblies()
        .Select(GetTimerApiType)
        .FirstOrDefault(type => type is not null);

    internal static void Enable()
    {
        var timerApi = TimerApi;
        if (timerApi is null)
        {
            LogManager.Debug("RespawnTimer: Plugin not found, skipping integration.");
            return;
        }

        var register = timerApi.GetMethod("RegisterWave", BindingFlags.Public | BindingFlags.Static, null,
            [typeof(Type), typeof(string), typeof(string), typeof(float)], null);

        if (register is null)
        {
            LogManager.Warn(
                "RespawnTimer: TimerAPI.RegisterWave(Type, string, string, float) not found, skipping integration. Is RespawnTimer up to date?");
            return;
        }

        try
        {
            register.Invoke(null, [typeof(SerpentsHandWave), DisplayName, Placeholder, SpawnDuration]);
            LogManager.Debug(
                "RespawnTimer: Integration enabled, {shminutes} and {shseconds} placeholders registered.");
        }
        catch (Exception e)
        {
            LogManager.Error($"RespawnTimer: Failed to register the Serpent's Hand wave: {e.Message}");
        }
    }

    internal static void Disable()
    {
        var timerApi = TimerApi;

        var unregister = timerApi?.GetMethod("UnregisterWave", BindingFlags.Public | BindingFlags.Static, null,
            [typeof(Type)], null);

        if (unregister is null)
            return;

        try
        {
            unregister.Invoke(null, [typeof(SerpentsHandWave)]);
        }
        catch (Exception e)
        {
            LogManager.Error($"RespawnTimer: Failed to unregister the Serpent's Hand wave: {e.Message}");
        }
    }

    private static Type GetTimerApiType(Assembly assembly)
    {
        try
        {
            return assembly.GetType(TimerApiTypeName, false);
        }
        catch (Exception)
        {
            return null;
        }
    }
}

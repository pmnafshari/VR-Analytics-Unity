using UnityEngine;
using GameAnalyticsSDK;

public static class GAEvents
{
    // General helper
    private static void Design(string eventId, float? value = null)
    {
        if (value.HasValue)
            GameAnalytics.NewDesignEvent(eventId, value.Value);
        else
            GameAnalytics.NewDesignEvent(eventId);
    }

    // --- Your real events ---
    public static void GrabStart(string targetName = null)
    {
        var id = string.IsNullOrEmpty(targetName)
            ? "interaction:grab:start"
            : $"interaction:grab:start:{targetName}";
        Design(id);
    }

    public static void GrabEnd(string targetName = null)
    {
        var id = string.IsNullOrEmpty(targetName)
            ? "interaction:grab:end"
            : $"interaction:grab:end:{targetName}";
        Design(id);
    }

    // durationSeconds can be float
    public static void GazeHold(string targetName, float durationSeconds)
    {
        // label = targetName, value = duration
        var id = string.IsNullOrEmpty(targetName)
            ? "interaction:gaze:hold"
            : $"interaction:gaze:hold:{targetName}";
        Design(id, durationSeconds);
    }

    public static void Teleport(string destinationName = null)
    {
        var id = string.IsNullOrEmpty(destinationName)
            ? "movement:teleport"
            : $"movement:teleport:{destinationName}";
        Design(id);
    }
}
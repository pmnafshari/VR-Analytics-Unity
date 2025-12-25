using UnityEngine;

public class TeleportEvents : MonoBehaviour
{
    [SerializeField] private string category = "Teleport";

    public void LogTeleport(string targetName = "")
    {
        if (AnalyticsManager.Instance == null) return;

        string target = string.IsNullOrEmpty(targetName) ? "Teleport" : targetName;

        AnalyticsManager.Instance.LogEvent(
            category,
            "Teleport",
            target,
            1f,
            ""
        );
    }
}
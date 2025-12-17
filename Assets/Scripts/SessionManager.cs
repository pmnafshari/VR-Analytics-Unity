using UnityEngine;

public class SessionManager : MonoBehaviour
{
    private float startTime;

    void Start()
    {
        startTime = Time.time;

        AnalyticsManager.Instance?.LogEvent(
            "Session",
            "SessionStart",
            "Scene",
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    private void OnApplicationQuit()
    {
        float duration = Time.time - startTime;

        AnalyticsManager.Instance?.LogEvent(
            "Session",
            "SessionEnd",
            "Application",
            "",
            $"Duration={duration:0.00}"
        );
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    private void Start()
    {
        if (AnalyticsManager.Instance == null) return;

        string scene = SceneManager.GetActiveScene().name;

        AnalyticsManager.Instance.LogEvent("Session", "SessionStart", "Scene", 0f, scene);
        AnalyticsManager.Instance.LogEvent("Session", "GameStarted", scene, 0f, "");
    }
}
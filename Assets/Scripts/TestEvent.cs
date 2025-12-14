using UnityEngine;

public class TestEvent : MonoBehaviour
{
    private void Start()
    {
        // وقتی سکانس لود می‌شود
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.LogEvent("GameStarted", "Scene loaded");
        }
    }
}

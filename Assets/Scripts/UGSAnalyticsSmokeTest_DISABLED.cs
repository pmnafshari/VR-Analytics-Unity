using UnityEngine;

public class UGSAnalyticsSmokeTest : MonoBehaviour
{
    [SerializeField] private bool sendOnStart = true;

    private void Start()
    {
        Debug.Log("[TEST] Scene started");

        if (!sendOnStart) return;

        if (UnityAnalyticsManager.Instance == null)
        {
            Debug.LogError("[TEST] UnityAnalyticsManager.Instance is NULL");
            return;
        }

        UnityAnalyticsManager.Instance.LogEvent("test", "smoke", "editor", 1, "hello");
        Debug.Log("[TEST] Smoke event sent");
    }
}
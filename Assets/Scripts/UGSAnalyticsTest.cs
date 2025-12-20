using UnityEngine;

public class UGSAnalyticsTest : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("[TEST] Scene started");

        if (UnityAnalyticsManager.Instance == null)
        {
            Debug.LogError("[TEST] UnityAnalyticsManager.Instance is NULL");
            return;
        }

        UnityAnalyticsManager.Instance.LogEvent(
            action: "TestEvent",
            target: gameObject.name,
            value: "1",
            details: "HelloFromStart",
            category: "Debug"
        );

        Debug.Log("[TEST] TestEvent sent");
    }
}
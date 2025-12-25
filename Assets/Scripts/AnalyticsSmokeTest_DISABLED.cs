using UnityEngine;

public class AnalyticsSmokeTest : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("[TEST] Analytics smoke test started");

        if (AnalyticsHub.Instance == null)
        {
            Debug.LogError("[TEST] AnalyticsHub.Instance is NULL");
            return;
        }

        AnalyticsHub.Instance.Track(
            category: "test",
            action: "smoke_test",
            target: "scene_start",
            value: 1f,
            details: "basic analytics pipeline test"
        );

        Debug.Log("[TEST] Smoke event sent");
    }
}
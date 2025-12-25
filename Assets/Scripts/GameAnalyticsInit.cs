using UnityEngine;

public class GameAnalyticsInit : MonoBehaviour
{
    void Awake()
    {
        GameAnalyticsSDK.GameAnalytics.Initialize();
        Debug.Log("[GA] Initialized");
    }
}
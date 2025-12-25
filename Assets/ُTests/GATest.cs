using UnityEngine;
using GameAnalyticsSDK;

public class GATest : MonoBehaviour
{
    [SerializeField] private bool sendOnStart = true;

    private void Awake()
    {
        // Make sure GA SDK is initialized (required in recent versions)
        GameAnalytics.Initialize();
    }

    private void Start()
    {
        if (sendOnStart)
        {
            SendTestEvent();
        }
    }

    [ContextMenu("Send GA Test Event")]
    public void SendTestEvent()
    {
        Debug.Log("[GA] Sending design event: test:scene_start");
        GameAnalytics.NewDesignEvent("test:scene_start");
    }
}
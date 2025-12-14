using UnityEngine;

public class InteractionEvents : MonoBehaviour
{
    public void OnHoverEnter()
    {
        if (AnalyticsManager.Instance == null) return;
        AnalyticsManager.Instance.LogEvent("HoverStart", gameObject.name);
    }

    public void OnHoverExit()
    {
        if (AnalyticsManager.Instance == null) return;
        AnalyticsManager.Instance.LogEvent("HoverEnd", gameObject.name);
    }

    public void OnGrab()
    {
        if (AnalyticsManager.Instance == null) return;
        AnalyticsManager.Instance.LogEvent("GrabStart", gameObject.name);
    }

    public void OnRelease()
    {
        if (AnalyticsManager.Instance == null) return;
        AnalyticsManager.Instance.LogEvent("GrabEnd", gameObject.name);
    }
}

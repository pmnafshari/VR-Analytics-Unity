using UnityEngine;

public class InteractionEvents : MonoBehaviour
{
    public void OnHoverEnter()
    {
        AnalyticsManager.Instance?.LogEvent(
            "Interaction",
            "HoverStart",
            gameObject.name
        );
    }

    public void OnHoverExit()
    {
        AnalyticsManager.Instance?.LogEvent(
            "Interaction",
            "HoverEnd",
            gameObject.name
        );
    }

    public void OnGrab()
    {
        AnalyticsManager.Instance?.LogEvent(
            "Interaction",
            "GrabStart",
            gameObject.name
        );
    }

    public void OnRelease()
    {
        AnalyticsManager.Instance?.LogEvent(
            "Interaction",
            "GrabEnd",
            gameObject.name
        );
    }
}

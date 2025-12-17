using UnityEngine;

public class TeleportEvents : MonoBehaviour
{
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    // this function is called when the player teleports to a new position
    public void OnTeleport()
    {
        Vector3 newPosition = transform.position;

        Debug.Log($"[Analytics][Teleport] From {lastPosition} To {newPosition}");

        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.LogEvent(
                "Teleport",
                "Teleport",
                gameObject.name,
                $"From:{lastPosition} To:{newPosition}"
            );
        }

        lastPosition = newPosition;
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportEvents : MonoBehaviour
{
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    // This function is called when the player teleports to a new position
    public void OnTeleport(SelectEnterEventArgs args)
    {
        Vector3 newPosition = args.interactorObject.transform.position;

        Debug.Log($"[Analytics] Teleport | From: {lastPosition} | To: {newPosition}");

        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.LogEvent(
                "Teleport",
                $"From:{lastPosition} To:{newPosition}"
            );
        }

        lastPosition = newPosition;
    }
}

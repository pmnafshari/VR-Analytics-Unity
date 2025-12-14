using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportEvents : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider teleportProvider;

    private void Awake()
    {
        // Find the TeleportationProvider in the scene
        teleportProvider = FindObjectOfType<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider>();

        if (teleportProvider == null)
        {
            Debug.LogError("TeleportationProvider not found in scene!");
        }
    }

    private void OnEnable()
    {
        if (teleportProvider != null)
        {
            // When the teleportation is finished, call the OnTeleportFinished function
            teleportProvider.endLocomotion += OnTeleportFinished;
        }
    }

    private void OnDisable()
    {
        if (teleportProvider != null)
        {
            teleportProvider.endLocomotion -= OnTeleportFinished;
        }
    }

    private void OnTeleportFinished(LocomotionSystem locomotionSystem)
    {
        Debug.Log("Teleport event finished!");

        // Send the event to the analytics system
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.LogEvent("Teleport", "User Teleported");
        }
    }
}

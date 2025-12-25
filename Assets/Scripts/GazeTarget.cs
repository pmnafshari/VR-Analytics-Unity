using UnityEngine;

public class GazeTarget : MonoBehaviour
{
    [SerializeField] private string category = "Gaze";
    [SerializeField] private float holdValue = 0.5f;

    public void LogGazeHold()
    {
        if (AnalyticsManager.Instance == null) return;

        AnalyticsManager.Instance.LogEvent(
            category,
            "GazeHold",
            gameObject.name,
            holdValue,
            ""
        );
    }
}
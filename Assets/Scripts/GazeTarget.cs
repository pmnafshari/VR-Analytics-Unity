using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GazeTarget : MonoBehaviour
{
    public float minGazeTime = 0.5f;

    private float timer;
    private bool gazing;

    public void OnGazeEnter(HoverEnterEventArgs args)
    {
        gazing = true;
        timer = 0f;
    }

    public void OnGazeExit(HoverExitEventArgs args)
    {
        gazing = false;
        timer = 0f;
    }

    void Update()
    {
        if (!gazing) return;

        timer += Time.deltaTime;
        if (timer >= minGazeTime)
        {
            AnalyticsManager.Instance?.LogEvent(
                "Gaze",
                "GazeHold",
                gameObject.name,
                minGazeTime.ToString("0.00")
            );

            timer = 0f;
        }
    }
}

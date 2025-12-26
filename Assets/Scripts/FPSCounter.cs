using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [Tooltip("Update interval in seconds")]
    public float updateInterval = 1f;

    public float CurrentFPS { get; private set; }

    private float timeAccumulator = 0f;
    private int frameCount = 0;

    void Update()
    {
        frameCount++;
        timeAccumulator += Time.unscaledDeltaTime;

        if (timeAccumulator >= updateInterval)
        {
            CurrentFPS = frameCount / timeAccumulator;

            // Debug فقط برای تست
            Debug.Log($"[FPS] {CurrentFPS:F1}");

            frameCount = 0;
            timeAccumulator = 0f;
        }
    }
}
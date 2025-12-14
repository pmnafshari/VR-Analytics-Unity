using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GazeTarget : MonoBehaviour
{
    public float minGazeTime = 0.5f;
    private float gazeTimer = 0f;
    private bool isGazed = false;

    public void OnGazeEnter(HoverEnterEventArgs args)
    {
        isGazed = true;
        gazeTimer = 0f;
    }

    public void OnGazeExit(HoverExitEventArgs args)
    {
        isGazed = false;
        gazeTimer = 0f;
    }

    private void Update()
    {
        if (!isGazed) return;

        gazeTimer += Time.deltaTime;
        if (gazeTimer >= minGazeTime)
            {
                // log/analytics here
            Debug.Log($"Gazed: {gameObject.name}");
            gazeTimer = 0f;
        }
    }
}

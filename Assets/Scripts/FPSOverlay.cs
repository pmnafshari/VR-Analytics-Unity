using UnityEngine;
using TMPro;

public class FPSOverlay : MonoBehaviour
{
    public FPSCounter fpsCounter;
    public TMP_Text text;
    public float refreshSeconds = 0.25f;

    private float t;

    void Awake()
    {
        if (fpsCounter == null) fpsCounter = FindObjectOfType<FPSCounter>();
        if (text == null) text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (fpsCounter == null || text == null) return;

        t += Time.unscaledDeltaTime;
        if (t < refreshSeconds) return;
        t = 0f;

        text.text = $"FPS: {fpsCounter.CurrentFPS:F1}";
    }
}
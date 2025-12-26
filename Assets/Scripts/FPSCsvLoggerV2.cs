using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public class FPSCsvLoggerV2 : MonoBehaviour
{
    [Header("Refs")]
    public FPSCounter fpsCounter;

    [Header("Config")]
    public float logIntervalSeconds = 1f;
    public string filePrefix = "fps";
    public bool includeHeader = true;

    private float _t;
    private string _filePath;

    void Awake()
    {
        if (fpsCounter == null) fpsCounter = FindObjectOfType<FPSCounter>();

        var dir = Application.persistentDataPath;
        Directory.CreateDirectory(dir);

        var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        _filePath = Path.Combine(dir, $"{filePrefix}_{ts}.csv");

        if (includeHeader && !File.Exists(_filePath))
        {
            File.AppendAllText(_filePath, "timestamp_utc,scene,fps\n");
        }

        Debug.Log($"[FPSCSV] Logging to: {_filePath}");
    }

    void Update()
    {
        if (fpsCounter == null) return;

        _t += Time.unscaledDeltaTime;
        if (_t < logIntervalSeconds) return;
        _t = 0f;

        var utc = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        var fps = fpsCounter.CurrentFPS.ToString("F2", CultureInfo.InvariantCulture);

        File.AppendAllText(_filePath, $"{utc},{scene},{fps}\n");
        Debug.Log($"[FPSCSV] fps={fps}");
    }
}
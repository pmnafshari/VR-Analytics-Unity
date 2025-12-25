using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }

    [SerializeField] private bool enableCsv = true;
    [SerializeField] private bool enableUgs = true;

    private AnalyticsFileLogger _fileLogger;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _fileLogger = FindObjectOfType<AnalyticsFileLogger>();
    }

    public void LogEvent(string category, string action, string target = "", float value = 0f, string details = "")
    {
        if (enableCsv && _fileLogger != null)
        {
            _fileLogger.LogEvent(category, action, target, value, details);
        }

        if (enableUgs && UnityAnalyticsManager.Instance != null)
        {
            int valueInt = Mathf.RoundToInt(value * 1000f);
            UnityAnalyticsManager.Instance.LogEvent(category, action, target, valueInt, details);
        }
    }
}
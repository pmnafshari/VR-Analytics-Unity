using UnityEngine;

public class AnalyticsHub : MonoBehaviour
{
    public static AnalyticsHub Instance { get; private set; }

    [Header("Outputs")]
    public bool enableCsv = true;
    public bool enableUgs = true;

    private AnalyticsFileLogger _csv;
    private AnalyticsManager _ugs;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _csv = FindObjectOfType<AnalyticsFileLogger>();
        _ugs = FindObjectOfType<AnalyticsManager>();
    }

    public void Track(string category, string action, string target = "", float value = 0f, string details = "")
    {
        if (enableCsv && _csv != null)
            _csv.LogEvent(category, action, target, value, details);

        if (enableUgs && _ugs != null)
            _ugs.LogEvent(category, action, target, value, details);
    }
}
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// استاندارد واحد لاگ
    /// </summary>
    /// <param name="category">UI, Gaze, Session, Teleport, Interaction</param>
    /// <param name="action">Action name</param>
    /// <param name="name">Object/UI name</param>
    /// <param name="value">Optional value</param>
    /// <param name="details">Optional details</param>
    public void LogEvent(
        string category,
        string action,
        string name = "",
        string value = "",
        string details = ""
    )
    {
        string log =
            $"[Analytics]" +
            $"[{category}] " +
            $"Action={action} " +
            $"Name={name} " +
            $"Value={value} " +
            $"Details={details} " +
            $"Time={Time.time:0.00}";

        Debug.Log(log);

        // اگر CSV Logger هست، همزمان بنویس
        if (AnalyticsFileLogger.Instance != null)
        {
            AnalyticsFileLogger.Instance.LogEvent(
                action,
                name,
                value,
                details
            );
        }
    }
}

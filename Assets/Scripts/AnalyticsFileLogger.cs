using System;
using System.IO;
using System.Text;
using UnityEngine;

public class AnalyticsFileLogger : MonoBehaviour
{
    public static AnalyticsFileLogger Instance { get; private set; }

    [Header("File Settings")]
    public string filePrefix = "session";
    public bool writeHeader = true;

    private StreamWriter writer;
    private string filePath;
    private float sessionStartTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        OpenFile();
        sessionStartTime = Time.time;
    }

    private void OnApplicationQuit()
    {
        CloseFile();
    }

    // --------------------------------------------------
    // PUBLIC LOGGING API (used by AnalyticsManager)
    // Schema:
    // Time, SessionTime, UserID, Category, Action, Target, Value, Details
    // --------------------------------------------------
    public void LogEvent(
        string category,
        string action,
        string target = "",
        string value = "",
        string details = ""
    )
    {
        if (writer == null) return;

        string time = Time.time.ToString("F2");
        string sessionTime = (Time.time - sessionStartTime).ToString("F2");

        WriteRow(
            time,
            sessionTime,
            GetUserId(),
            category,
            action,
            target,
            value,
            details
        );
    }

    // --------------------------------------------------
    // FILE HANDLING
    // --------------------------------------------------
    private void OpenFile()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"{filePrefix}_{timestamp}.csv";
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        writer = new StreamWriter(filePath, false, Encoding.UTF8);

        if (writeHeader)
        {
            writer.WriteLine(
                "Time,SessionTime,UserID,Category,Action,Target,Value,Details"
            );
            writer.Flush();
        }

        Debug.Log($"[Analytics] CSV Logging Started -> {filePath}");
    }

    private void CloseFile()
    {
        if (writer == null) return;

        writer.Flush();
        writer.Close();
        writer = null;

        Debug.Log($"[Analytics] CSV Logging Closed -> {filePath}");
    }

    private void WriteRow(
        string time,
        string sessionTime,
        string userId,
        string category,
        string action,
        string target,
        string value,
        string details
    )
    {
        string row =
            $"{Csv(time)},{Csv(sessionTime)},{Csv(userId)}," +
            $"{Csv(category)},{Csv(action)},{Csv(target)}," +
            $"{Csv(value)},{Csv(details)}";

        writer.WriteLine(row);
        writer.Flush();
    }

    // --------------------------------------------------
    // HELPERS
    // --------------------------------------------------
    private string GetUserId()
    {
        // Simple anonymous user/device identifier
        return SystemInfo.deviceUniqueIdentifier;
    }

    private string Csv(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";

        s = s.Replace("\n", " ").Replace("\r", " ");
        if (s.Contains(",") || s.Contains("\""))
        {
            s = s.Replace("\"", "\"\"");
            s = $"\"{s}\"";
        }
        return s;
    }

    public string GetFilePath() => filePath;
}

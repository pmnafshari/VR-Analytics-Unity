using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public class AnalyticsFileLogger : MonoBehaviour
{
    private StreamWriter _writer;
    private float _startTime;
    private string _userId;
    private string _filePath;

    private void Awake()
    {
        _startTime = Time.time;
        _userId = SystemInfo.deviceUniqueIdentifier;
        StartNewFile();
    }

    private void OnApplicationQuit()
    {
        CloseFile();
    }

    private void StartNewFile()
    {
        string dir = Path.Combine(Application.persistentDataPath);
        Directory.CreateDirectory(dir);

        string ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        _filePath = Path.Combine(dir, $"session_{ts}.csv");

        _writer = new StreamWriter(_filePath, false);
        _writer.AutoFlush = true;

        _writer.WriteLine("Time,SessionTime,UserID,Category,Action,Target,Value,Details");

        Debug.Log($"[Analytics] CSV Logging Started -> {_filePath}");
    }

    private void CloseFile()
    {
        if (_writer == null) return;
        _writer.Flush();
        _writer.Close();
        _writer = null;

        Debug.Log($"[Analytics] CSV Logging Closed -> {_filePath}");
    }

    public void LogEvent(string category, string action, string target, float value, string details)
    {
        if (_writer == null) return;

        float now = Time.time;
        float sessionTime = now - _startTime;

        string v = value.ToString("0.###", CultureInfo.InvariantCulture);
        string d = Escape(details);
        string t = Escape(target);

        _writer.WriteLine($"{now:0.00},{sessionTime:0.00},{_userId},{Escape(category)},{Escape(action)},{t},{v},{d}");
    }

    private string Escape(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";
        s = s.Replace("\"", "\"\"");
        if (s.Contains(",") || s.Contains("\n") || s.Contains("\r"))
            return $"\"{s}\"";
        return s;
    }
}
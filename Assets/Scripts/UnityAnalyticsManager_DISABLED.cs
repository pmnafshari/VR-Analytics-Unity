using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class UnityAnalyticsManager : MonoBehaviour
{
    public static UnityAnalyticsManager Instance { get; private set; }

    [Header("UGS")]
    [SerializeField] private bool enableUgs = true;
    [SerializeField] private bool debugLogs = true;

    private bool _initialized;

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

    private async void Start()
    {
        await InitializeAsync();
    }

    public async Task InitializeAsync()
    {
        if (!enableUgs || _initialized) return;

        try
        {
            await UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();

            _initialized = true;

            if (debugLogs) Debug.Log("[UGS][Analytics] Initialized successfully");
        }
        catch (Exception ex)
        {
            _initialized = false;
            Debug.LogError("[UGS][Analytics] Initialize failed: " + ex.Message);
        }
    }

    public void LogEvent(string category, string action, string target = "", int value = 0, string details = "")
    {
        if (!enableUgs) return;

        if (!_initialized)
        {
            if (debugLogs) Debug.LogWarning("[UGS][Analytics] Not initialized yet. Event skipped: " + action);
            return;
        }

        string eventName = ToEventName(category, action);

        try
        {
            var ev = new CustomEvent(eventName)
            {
                { "category", category ?? "" },
                { "action", action ?? "" },
                { "target", target ?? "" },
                { "details", details ?? "" },
                { "value", value }
            };

            AnalyticsService.Instance.RecordEvent(ev);
        }
        catch (Exception ex)
        {
            Debug.LogError("[UGS][Analytics] RecordEvent failed: " + ex.Message);
        }
    }

    public void LogEventFloatAsInt(string category, string action, string target, float valueFloat, string details = "", int scale = 1000)
    {
        int valueInt = Mathf.RoundToInt(valueFloat * scale);
        LogEvent(category, action, target, valueInt, details);
    }

    private static string ToEventName(string category, string action)
    {
        string c = (category ?? "").Trim().ToLowerInvariant();
        string a = (action ?? "").Trim().ToLowerInvariant();

        c = c.Replace(" ", "_");
        a = a.Replace(" ", "_");

        if (string.IsNullOrEmpty(c)) c = "misc";
        if (string.IsNullOrEmpty(a)) a = "event";

        return $"{c}_{a}";
    }
}
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

using Unity.Services.Core;
using Unity.Services.Analytics;
using Unity.Services.Authentication;

public class UnityAnalyticsManager : MonoBehaviour
{
    public static UnityAnalyticsManager Instance { get; private set; }

    [Header("UGS Analytics Settings")]
    public bool enableAnalytics = true;
    public bool flushAfterEachEvent = false;

    private bool initialized = false;

    private async void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (enableAnalytics)
            await InitializeServices();
    }

    private async Task InitializeServices()
    {
        try
        {
            if (initialized) return;

            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            AnalyticsService.Instance.StartDataCollection();

            initialized = true;
            Debug.Log("[UGS Analytics] Initialized successfully");
        }
        catch (Exception e)
        {
            enableAnalytics = false;
            Debug.LogError("[UGS Analytics] Initialization failed: " + e.Message);
        }
    }

    public void LogEvent(
        string action,
        string target = "",
        string value = "",
        string details = "",
        string category = "Custom"
    )
    {
        if (!enableAnalytics || !initialized)
            return;

        string eventName = NormalizeEventName(action);

        var analyticsEvent = new CustomEvent(eventName);

        analyticsEvent.Add("category", category);
        analyticsEvent.Add("scene", SceneManager.GetActiveScene().name);

        if (!string.IsNullOrEmpty(target))
            analyticsEvent.Add("target", target);

        if (!string.IsNullOrEmpty(value))
            analyticsEvent.Add("value", value);

        if (!string.IsNullOrEmpty(details))
            analyticsEvent.Add("details", details);

        AnalyticsService.Instance.RecordEvent(analyticsEvent);

        if (flushAfterEachEvent)
            AnalyticsService.Instance.Flush();
    }

    private string NormalizeEventName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Event";

        name = name.Trim()
                   .Replace(" ", "_")
                   .Replace("|", "_")
                   .Replace(":", "_");

        return name.Length > 64 ? name.Substring(0, 64) : name;
    }
}
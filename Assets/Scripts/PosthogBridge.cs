using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class PosthogBridge : MonoBehaviour
{
    [Header("PostHog Settings")]
    [SerializeField] private string posthogHost = "https://app.posthog.com";
    [SerializeField] private string projectApiKey = ""; // از PostHog کپی کردی

    [Header("Identity")]
    [SerializeField] private string distinctIdPrefix = "unity_";

    private string _distinctId;

    void Awake()
    {
        _distinctId = GetOrCreateDistinctId();
    }

    public void Capture(string eventName, Dictionary<string, object> properties = null)
    {
        if (string.IsNullOrWhiteSpace(projectApiKey))
        {
            Debug.LogWarning("[PostHog] API key is empty. Set it in Inspector.");
            return;
        }

        var payload = new Dictionary<string, object>
        {
            { "api_key", projectApiKey },
            { "event", eventName },
            { "distinct_id", _distinctId },
            { "properties", properties ?? new Dictionary<string, object>() }
        };

        // چند property پایه
        ((Dictionary<string, object>)payload["properties"])["unity_version"] = Application.unityVersion;
        ((Dictionary<string, object>)payload["properties"])["platform"] = Application.platform.ToString();
        ((Dictionary<string, object>)payload["properties"])["app_version"] = Application.version;

        StartCoroutine(PostCapture(payload));
    }

    private IEnumerator<UnityWebRequestAsyncOperation> PostCapture(Dictionary<string, object> payload)
    {
        var url = posthogHost.TrimEnd('/') + "/capture/";
        var json = MiniJson.Serialize(payload);
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        using (var req = new UnityWebRequest(url, "POST"))
        {
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
                Debug.LogWarning($"[PostHog] Failed: {req.responseCode} {req.error} | {req.downloadHandler.text}");
            else
                Debug.Log($"[PostHog] Sent: {payload["event"]}");
        }
    }

    private string GetOrCreateDistinctId()
    {
        const string key = "posthog_distinct_id";
        if (PlayerPrefs.HasKey(key)) return PlayerPrefs.GetString(key);

        var id = distinctIdPrefix + Guid.NewGuid().ToString("N");
        PlayerPrefs.SetString(key, id);
        PlayerPrefs.Save();
        return id;
    }

    // --- Tiny JSON helper (MiniJson) ---
    // Unity-friendly minimal JSON serializer (only what we need)
    private static class MiniJson
    {
        public static string Serialize(object obj) => JsonUtilityWrapper.Serialize(obj);

        // JsonUtility خودش Dictionary رو مستقیم ساپورت نمی‌کنه، پس یک serializer خیلی سبک داریم
        private static class JsonUtilityWrapper
        {
            public static string Serialize(object obj)
            {
                return SimpleJson.Serialize(obj);
            }
        }

        private static class SimpleJson
        {
            public static string Serialize(object obj)
            {
                if (obj == null) return "null";
                if (obj is string s) return $"\"{Escape(s)}\"";
                if (obj is bool b) return b ? "true" : "false";
                if (obj is int || obj is long || obj is float || obj is double || obj is decimal)
                    return Convert.ToString(obj, System.Globalization.CultureInfo.InvariantCulture);

                if (obj is Dictionary<string, object> dict)
                {
                    var sb = new StringBuilder();
                    sb.Append("{");
                    bool first = true;
                    foreach (var kv in dict)
                    {
                        if (!first) sb.Append(",");
                        first = false;
                        sb.Append($"\"{Escape(kv.Key)}\":{Serialize(kv.Value)}");
                    }
                    sb.Append("}");
                    return sb.ToString();
                }

                if (obj is Dictionary<string, string> dictStr)
                {
                    var sb = new StringBuilder();
                    sb.Append("{");
                    bool first = true;
                    foreach (var kv in dictStr)
                    {
                        if (!first) sb.Append(",");
                        first = false;
                        sb.Append($"\"{Escape(kv.Key)}\":\"{Escape(kv.Value)}\"");
                    }
                    sb.Append("}");
                    return sb.ToString();
                }

                if (obj is IEnumerable<object> list)
                {
                    var sb = new StringBuilder();
                    sb.Append("[");
                    bool first = true;
                    foreach (var item in list)
                    {
                        if (!first) sb.Append(",");
                        first = false;
                        sb.Append(Serialize(item));
                    }
                    sb.Append("]");
                    return sb.ToString();
                }

                // fallback: ToString
                return $"\"{Escape(obj.ToString())}\"";
            }

            private static string Escape(string s)
            {
                return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
            }
        }
    }
}
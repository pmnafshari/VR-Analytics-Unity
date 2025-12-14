using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    // دسترسی سراسری
    public static AnalyticsManager Instance;

    private void Awake()
    {
        // فقط یک نسخه نگه داریم
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // توی سکانس‌های بعدی هم بمونه
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // نسخه ساده فقط با نام رویداد
    public void LogEvent(string eventName)
    {
        Debug.Log($"[Analytics] Event: {eventName}");
    }

    // نسخه با داده اضافی (مثلاً نام آبجکت، موقعیت و ...)
    public void LogEvent(string eventName, string data)
    {
        Debug.Log($"[Analytics] Event: {eventName} | Data: {data}");
    }
}

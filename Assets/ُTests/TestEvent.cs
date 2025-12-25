using UnityEngine;
using UnityEngine.SceneManagement;

public class TestEvent : MonoBehaviour
{
    private void Start()
    {
        // when the scene is loaded
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.LogEvent(
                "Session",
                "GameStarted",
                SceneManager.GetActiveScene().name
            );
        }

        Debug.Log("[Analytics][Session] GameStarted");
    }
}

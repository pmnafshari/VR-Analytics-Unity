using UnityEngine;

public class SessionManager : MonoBehaviour
{
    private float sessionStartTime;
    private bool sessionEnded = false;

    void Awake()
    {
        sessionStartTime = Time.time;

        Debug.Log("[Analytics][Session] Started");
    }

    void OnApplicationQuit()
    {
        EndSession("ApplicationQuit");
    }

    void OnDestroy()
    {
        // For when Stop is pressed
        if (!sessionEnded)
        {
            EndSession("EditorStop");
        }
    }

    private void EndSession(string reason)
    {
        sessionEnded = true;

        float duration = Time.time - sessionStartTime;

        Debug.Log($"[Analytics][Session] Ended | Reason: {reason} | Duration: {duration:0.00}s");
    }
}

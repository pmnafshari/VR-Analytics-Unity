using System.Collections.Generic;
using UnityEngine;

public class PosthogSmokeTest : MonoBehaviour
{
    void Start()
    {
        var ph = GetComponent<PosthogBridge>();
        ph.Capture("test_scene_start", new Dictionary<string, object> {
            {"scene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name},
            {"build", Application.version}
        });
    }
}
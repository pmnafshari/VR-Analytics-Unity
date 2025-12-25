using UnityEngine;

public class GATestRealEvents : MonoBehaviour
{
    void Start()
    {
        GAEvents.GrabStart("Cube");
        GAEvents.GrabEnd("Cube");
        GAEvents.GazeHold("Panel", 1.7f);
        GAEvents.Teleport("AreaA");
        Debug.Log("[GA] Real events test sent");
    }
}
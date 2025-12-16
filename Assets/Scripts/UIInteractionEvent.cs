using UnityEngine;

public class UIInteractionEvent : MonoBehaviour
{
    [Header("UI Info")]
    [Tooltip("Unique name of this UI element (Button, Slider, etc.)")]
    public string uiName = "UI_Element";

    [Tooltip("Optional UI category (Menu, VideoPlayer, Settings, etc.)")]
    public string uiCategory = "Default";

    // -------------------------
    // BUTTON
    // -------------------------
    public void OnButtonClick()
    {
        LogEvent("ButtonClick");
    }

    // -------------------------
    // SLIDER
    // -------------------------
    public void OnSliderValueChanged(float value)
    {
        LogEvent($"SliderValueChanged | Value: {value:0.00}");
    }

    // -------------------------
    // TOGGLE
    // -------------------------
    public void OnToggleChanged(bool isOn)
    {
        LogEvent($"ToggleChanged | State: {isOn}");
    }

    // -------------------------
    // DROPDOWN
    // -------------------------
    public void OnDropdownChanged(int index)
    {
        LogEvent($"DropdownChanged | Index: {index}");
    }

    // -------------------------
    // SESSION
    // -------------------------
    public void OnSessionEnd()
    {
        LogEvent("SessionEnded");
        Application.Quit();
    }

    // -------------------------
    // CORE LOGGER
    // -------------------------
    private void LogEvent(string action)
    {
        string message =
            $"[Analytics][UI] " +
            $"Action: {action} | " +
            $"Name: {uiName} | " +
            $"Category: {uiCategory} | " +
            $"Time: {Time.time:0.00}";

        Debug.Log(message);

        // TODO: Send the event to the analytics service
    }
}

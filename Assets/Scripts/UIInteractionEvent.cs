using UnityEngine;

public class UIInteractionEvent : MonoBehaviour
{
    public string uiName = "UI_Element";
    public string uiCategory = "Default";

    public void OnButtonClick()
    {
        AnalyticsManager.Instance?.LogEvent(
            "UI",
            "ButtonClick",
            uiName,
            "",
            uiCategory
        );
    }

    public void OnSliderValueChanged(float value)
    {
        AnalyticsManager.Instance?.LogEvent(
            "UI",
            "SliderChanged",
            uiName,
            value.ToString("0.00"),
            uiCategory
        );
    }
}

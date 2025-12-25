using UnityEngine;

public class UIInteractionEvent : MonoBehaviour
{
    [SerializeField] private string uiName = "UI_Element";
    [SerializeField] private string uiCategory = "UI";

    public void OnButton()
    {
        if (AnalyticsManager.Instance == null) return;

        AnalyticsManager.Instance.LogEvent(
            uiCategory,
            "ButtonClicked",
            uiName,
            0f,
            ""
        );
    }

    public void OnSliderChanged(float value)
    {
        if (AnalyticsManager.Instance == null) return;

        AnalyticsManager.Instance.LogEvent(
            uiCategory,
            "SliderChanged",
            uiName,
            value,
            ""
        );
    }
}
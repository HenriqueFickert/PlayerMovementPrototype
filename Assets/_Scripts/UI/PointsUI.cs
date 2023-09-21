using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PointsUI : MonoBehaviour
{
    private TextMeshProUGUI pointsText;

    public UnityEvent OnTextChange;

    private void Awake()
    {
        pointsText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetPoints(int value)
    {
        pointsText.SetText(value.ToString());
        OnTextChange.Invoke();
    }
}

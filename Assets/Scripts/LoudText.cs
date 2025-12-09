using UnityEngine;
using TMPro;

public class LoudnessDebugUI : MonoBehaviour
{
    public TMP_Text text;

    void Update()
    {
        if (MicInput.Instance == null || text == null) return;

        text.text = $"Loudness: {MicInput.Instance.Loudness:F3}";
    }
}
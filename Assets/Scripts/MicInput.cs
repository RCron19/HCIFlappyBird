using System.Collections;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    public static MicInput Instance { get; private set; }

    [Header("Mic Settings")]
    public int sampleWindow = 256;    // how many samples we average
    public float sensitivity = 50f;   // multiplier to make it more “visible”

    [SerializeField] private float debugLoudness;   // visible in Inspector
    public float Loudness { get; private set; }     // used by other scripts

    private string _device;
    private AudioClip _clip;
    private float[] _samples;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    IEnumerator Start()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone detected!");
            yield break;
        }

        _device = Microphone.devices[0];
        Debug.Log("Using mic: " + _device);

        // 10-second looping clip
        _clip = Microphone.Start(_device, true, 10, 44100);
        _samples = new float[sampleWindow];

        // Wait until mic starts
        while (Microphone.GetPosition(_device) <= 0)
        {
            yield return null;
        }

        Debug.Log("Mic started and ready.");
    }

    void Update()
    {
        if (_clip == null) return;

        int micPos = Microphone.GetPosition(_device) - sampleWindow;
        if (micPos < 0) return; // not enough data yet

        // Read raw samples straight from the mic clip
        _clip.GetData(_samples, micPos);

        float sum = 0f;
        for (int i = 0; i < sampleWindow; i++)
        {
            float sample = _samples[i];
            sum += sample * sample;
        }

        float rms = Mathf.Sqrt(sum / sampleWindow);
        Loudness = rms * sensitivity;
        debugLoudness = Loudness; // shows in Inspector

        // Optional: spam-log every frame just to prove it's alive
        // Debug.Log("Loudness: " + Loudness);
    }
}
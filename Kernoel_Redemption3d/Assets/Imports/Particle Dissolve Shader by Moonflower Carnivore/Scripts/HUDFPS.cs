using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))] 
public class HUDFPS : MonoBehaviour
{
    // Attach this to a Text to make a frames/second indicator.
    //
    // It calculates frames/second over each updateInterval,
    // so the display does not keep changing wildly.
    //
    // It is also fairly accurate at very low FPS counts (<10).
    // We do this not by simply counting frames per interval, but
    // by accumulating FPS for each frame. This way we end up with
    // correct overall FPS even if the interval renders something like
    // 5.5 frames.

    public float updateInterval = 0.5F;

    private float _accum; // FPS accumulated over the interval
    private int _frames; // Frames drawn over the interval
    private float _timeLeft; // Left time for current interval
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        
        if (!_text)
        {
            Debug.Log("UtilityFramesPerSecond needs a Text component!");
            enabled = false;
            return;
        }

        _timeLeft = updateInterval;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;

        // Interval ended - update GUI text and start new interval
        if (!(_timeLeft <= 0.0) || !_text) return;
        
        // display two fractional digits (f2 format)
        var fps = _accum / _frames;
        var format = $"{fps:F2} FPS";
        _text.text = format;

        if (fps < 30)
            _text.material.color = Color.yellow;
        else if (fps < 10)
            _text.material.color = Color.red;
        else
            _text.material.color = Color.green;
        //	DebugConsole.Log(format,level);
        _timeLeft = updateInterval;
        _accum = 0.0F;
        _frames = 0;
    }
}
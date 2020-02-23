using UnityEngine;

/// <summary>
/// Plays a sound in a certain interval.
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{
    public AudioClip audioClip;
    public float interval;
    public float firstPlayAfter;

    private void Start()
    {
        InvokeRepeating(nameof(MakeSound), firstPlayAfter, interval);
    }

    private void MakeSound()
    {
        if (audioClip) AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}
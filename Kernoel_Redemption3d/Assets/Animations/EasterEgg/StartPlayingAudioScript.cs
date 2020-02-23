using UnityEngine;

namespace Animations.EasterEgg
{
    [RequireComponent(typeof(AudioSource))]
    public class StartPlayingAudioScript : MonoBehaviour
    {
        private AudioSource _audioData;

        private void Start()
        {
            _audioData = transform.parent.GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !_audioData.isPlaying)
            {
                _audioData.Play();                
            }
        }
    }
}

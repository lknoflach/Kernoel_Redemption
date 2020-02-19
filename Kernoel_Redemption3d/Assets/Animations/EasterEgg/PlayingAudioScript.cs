using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayingAudioScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    AudioSource audioData;
    public bool enter;
    public bool soundPlayed;
    void Start()
    {
        enter = false;
        soundPlayed = false;
        player = GameObject.FindGameObjectWithTag("Player");
        audioData = GetComponent<AudioSource>();
    }

 


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && soundPlayed == false)
        {
            audioData.Play();
            soundPlayed = true;

        }
    }


}

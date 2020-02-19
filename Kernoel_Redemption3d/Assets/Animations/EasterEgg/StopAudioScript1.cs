using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class StopAudioScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    AudioSource audioData;
    public bool enter;
    void Start()
    {
        enter = false;
        player = GameObject.FindGameObjectWithTag("Player");
        audioData = GetComponent<AudioSource>();
    }

 


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioData.Stop();

        }
    }


}

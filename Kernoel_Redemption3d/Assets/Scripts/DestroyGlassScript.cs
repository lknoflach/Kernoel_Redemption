﻿using System;
using UnityEngine;

public class DestroyGlassScript : MonoBehaviour
{
    private AudioSource _audioData; 

    private void Start()
    {
        _audioData = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Bullet":
                if (_audioData) _audioData.Play();
                Destroy(gameObject);
                break;
        }
    }
}
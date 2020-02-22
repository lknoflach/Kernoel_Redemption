﻿using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Variables 
    public Transform player;
    public float smooth = 0.3f;

    public float height;

    public float ofSetX = -9f;

    private Vector3 velocity = Vector3.zero;
    //Methods


    // Update is called once per frame
    private void Update()
    {
        if (player)
        {
            var pos = new Vector3();
            var position = player.position;
            pos.x = position.x;
            pos.z = position.z + ofSetX;
            pos.y = position.y + height;
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
        }
    }
}
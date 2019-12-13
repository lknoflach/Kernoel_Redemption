using System;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

public class BimTrigger : MonoBehaviour
{
    /** BIM STUFF **/
    // The bimPrefab is used to create a bim object.
    public GameObject bimPrefab;
    // The bim is used to move from startMarker to endMarker. It will get destroyed when it reaches the endMarker. 
    private GameObject bim;
    
    /** CHARACTER STUFF **/
    private readonly List<string> validTags = new List<string>() {"Player", "Clone"};
    
    /** MOVEMENT STUFF **/
    public Transform endMarker;
    public Transform startMarker;
    public float moveSpeed = 20f;

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        if (validTags.Contains(target.tag))
        {
            CreateBimAndStartMovement();
        }
    }

    private void CreateBimAndStartMovement()
    {
        if (bimPrefab && !bim)
        {
            // instantiate the bim
            bim = Instantiate(bimPrefab);
            // update the MoveObjectAToB script of the bim
            var moveObjectAToB = bim.GetComponent<MoveObjectAToB>();
            moveObjectAToB.endMarker = endMarker;
            moveObjectAToB.startMarker = startMarker;
            moveObjectAToB.moveSpeed = moveSpeed;
            moveObjectAToB.SetJourneyLength();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class BimTrigger : MonoBehaviour
{
    /** BIM STUFF **/
    // The bimPrefab is used to create a bim object.
    public GameObject bimPrefab;

    // The bim is used to move from startMarker to endMarker. It will get destroyed when it reaches the endMarker. 
    private GameObject _bim;

    /** CHARACTER STUFF **/
    private readonly List<string> _validTags = new List<string>() {"Player", "Clone"};

    /** MOVEMENT STUFF **/
    public Transform endMarker;

    public Transform startMarker;
    public float moveSpeed = 20f;

    private void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject;
        Debug.Log("BimTrigger->OnCollisionEnter: target.tag = " + target.tag + ", target.name = " + target.name);
        if (_validTags.Contains(target.tag))
        {
            CreateBimAndStartMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        Debug.Log("BimTrigger->OnTriggerEnter: target.tag = " + target.tag + ", target.name = " + target.name);
        if (_validTags.Contains(target.tag))
        {
            CreateBimAndStartMovement();
        }
    }

    private void CreateBimAndStartMovement()
    {
        if (!bimPrefab || _bim) return;

        // Instantiate the bim
        _bim = Instantiate(bimPrefab);
        // Activate the GameObject if it is disabled
        if (!_bim.activeSelf) _bim.SetActive(true);
        // Update the MoveObjectAToB script of the bim
        var moveObjectAToB = _bim.GetComponent<MoveObjectAToB>();
        moveObjectAToB.endMarker = endMarker;
        moveObjectAToB.startMarker = startMarker;
        moveObjectAToB.moveSpeed = moveSpeed;
        moveObjectAToB.SetJourneyLength();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BimEndLevel : MonoBehaviour
{
    /** BIM STUFF **/
    // The bimPrefab is used to create a bim object.
    public GameObject bimPrefab;

    public GameObject toDestroy;

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

        Destroy(toDestroy);

        var coUpdate = CoUpdate();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private static IEnumerator CoUpdate()
    {
        yield return new WaitForSeconds(5);
        yield return null;
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
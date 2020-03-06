using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BimEndLevel : MonoBehaviour
{
    /** BIM STUFF **/
    // The bimPrefab is used to create a bim object.
    public GameObject bimPrefab;
    public GameObject toDestroy;
    public string nextLevelName;

    // The bim is used to move from startMarker to endMarker. It will get destroyed when it reaches the endMarker. 
    private GameObject _bim;
    private CameraScript _cameraScript;
    private bool _isStarted;


    /** Clone Counter at End **/
    public GameObject[] clones;
    public GameObject clonesPrefab;
    public float CloneCounter = 0;

    /** CHARACTER STUFF **/
    private readonly List<string> _validTags = new List<string>() {"Player", "Clone"};
    private GameObject _player;
    
    /** MOVEMENT STUFF **/
    public Transform endMarker;

    public Transform startMarker;
    public float moveSpeed = 20f;

    private void Start()
    {
        _player = GameObject.Find("PlayerHans");
        if (Camera.main) _cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    private void OnCollisionEnter(Collision other)
    {
        /*var target = other.gameObject;
        Debug.Log("BimTrigger->OnCollisionEnter: target.tag = " + target.tag + ", target.name = " + target.name);
        if (_validTags.Contains(target.tag))
        {
            CreateBimAndStartMovement();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        // Debug.Log("BimTrigger->OnTriggerEnter: target.tag = " + target.tag + ", target.name = " + target.name);
        if (!_validTags.Contains(target.tag) || _isStarted) return;

        // Set flag to prevent restart
        _isStarted = true;
        
        // hide Player
        if (_player) _player.gameObject.SetActive(false);

        // Set Camera onto BIM instead of Player
        CreateBimAndStartMovement();
        
        // Destroy(toDestroy);
        GameManager.Instance.finishedLevelAmount++;
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(5);
        GameManager.Instance.FinishLevel(nextLevelName);
        yield return null;
    }

    private void CreateBimAndStartMovement()
    {
        if (!bimPrefab || _bim) return;

        // Instantiate the bim
        _bim = Instantiate(bimPrefab);
        
        // Activate the GameObject if it is disabled
        if (!_bim.activeSelf) _bim.SetActive(true);

        // Set the camera on the new Bim Object.
        if (_cameraScript)
        {
            _cameraScript.height = 25f;
            _cameraScript.player = _bim.transform.GetChild(0).gameObject;
            // _cameraScript.player = _bim.transform;
        }
        
        // Update the MoveObjectAToB script of the bim
        var moveObjectAToB = _bim.GetComponent<MoveObjectAToB>();
        moveObjectAToB.endMarker = endMarker;
        moveObjectAToB.startMarker = startMarker;
        moveObjectAToB.moveSpeed = moveSpeed;
        moveObjectAToB.SetJourneyLength();
    }
}
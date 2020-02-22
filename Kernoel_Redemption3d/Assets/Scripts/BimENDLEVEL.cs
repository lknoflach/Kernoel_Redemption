using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BimENDLEVEL : MonoBehaviour
{

    /** BIM STUFF **/
    // The bimPrefab is used to create a bim object.
    public GameObject bimPrefab;
    public GameObject toDestroy;
    // The bim is used to move from startMarker to endMarker. It will get destroyed when it reaches the endMarker. 
    private GameObject bim;

    /** CHARACTER STUFF **/
    private readonly List<string> validTags = new List<string>() { "Player", "Clone" };

    /** MOVEMENT STUFF **/
    public Transform endMarker;

    public Transform startMarker;
    public float moveSpeed = 20f;

  

    private void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject;
      
        if (validTags.Contains(target.tag))
        {
            CreateBimAndStartMovement();
        }
     

    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        Debug.Log("BimTrigger->OnTriggerEnter: target.tag = " + target.tag + ", target.name = " + target.name);
        if (validTags.Contains(target.tag))
        {
            CreateBimAndStartMovement();
        }

        Destroy(toDestroy);

        CoUpdate();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator CoUpdate()
    {
        yield return new WaitForSeconds(5);
       
        yield return null;
    }
        private void CreateBimAndStartMovement()
    {
        if (bimPrefab && !bim)
        {
            // Instantiate the bim
            bim = Instantiate(bimPrefab);
            // Activate the GameObject if it is disabled
            if (!bim.activeSelf) bim.SetActive(true);
            // Update the MoveObjectAToB script of the bim
            var moveObjectAToB = bim.GetComponent<MoveObjectAToB>();
            moveObjectAToB.endMarker = endMarker;
            moveObjectAToB.startMarker = startMarker;
            moveObjectAToB.moveSpeed = moveSpeed;
            moveObjectAToB.SetJourneyLength();
        }
    }
}

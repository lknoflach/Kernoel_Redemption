using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CloningScript : MonoBehaviour
{
    /** CLONING STUFF **/

    //checks if the player is standing on the cloningPlatform
    private bool _standsOnCloningPlatform;

    // the object which enables the cloning
    public GameObject cloningCapsule;

    // the prototype for new clone objects
    public GameObject clonePrototype;

    // the prototype for new zombie objects
    // the prototype for new clone objects
    public GameObject zombiePrototype;

    // amount of good seed oil
    public int highGradeSeedOil;

    // the likeliness that the clone created turns out to be a zombie with good seed oil
    public float probabilityOfMutationGoodSeedOil;

    // the likeliness that the clone created turns out to be a zombie with bad seed oil
    public int probabilityOfMutationBadSeedOil;

    //the text for the amout of Kernöl
    public Text kernölAmountText;

    // Start is called before the first frame update
    private void Start()
    {
        cloningCapsule.GetComponent<Transform>();
        kernölAmountText.text = "Kernöl: " + GameManager.Instance.KernoilScore;
    }


    // just a random boolean function with a set likeliness that it turns out to be false 
    private static bool RandomBoolean(float likelinessInPercent)
    {
        // return true if the random number between 0 and 100 is smaller then your likeliness for a zombie
        return (Random.Range(0f, 100f) > likelinessInPercent);
    }

    private void OnTriggerEnter(Collider other)
    {
        // maybe should be added in on trigger not sure 
        // add the seed oil to the player if he touches one 
        switch (other.gameObject.tag)
        {
            case "goodSeedOil":
                //Debug.Log("picked up high grade seed oil");
                GameManager.Instance.KernoilScore += 2;
                Destroy(other.gameObject);
                updateUI();
                break;

            case "badSeedOil":
                //Debug.Log("picked up low grade seed oil");
                GameManager.Instance.KernoilScore += 1;
                Destroy(other.gameObject);
                updateUI();
                break;

            case "CloningCapsule":
                // enable cloning
                _standsOnCloningPlatform = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "CloningCapsule":
                // enable cloning
                _standsOnCloningPlatform = false;
                break;
        }
    }


    // creates the clone if we have seed oil and with a possibility that it turns out to be a zombie 
    public void CreateClone()
    {
        if (highGradeSeedOil > 0)
        {
            var spawnPosition = transform.position;
            spawnPosition.z -= 3;

            Instantiate(
                RandomBoolean(probabilityOfMutationGoodSeedOil) ? clonePrototype : zombiePrototype,
                spawnPosition, transform.rotation
            );

            highGradeSeedOil -= 3;
            updateUI();
        }
    }

 
    // Update is called once per frame
    private void Update()
    {
        // cloning Button
        if (Input.GetKeyDown(KeyCode.E) && _standsOnCloningPlatform) CreateClone();
    }

    public void updateUI(){
        kernölAmountText.text = "Kernöl: " + GameManager.Instance.KernoilScore;
    }
}
using UnityEngine;

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

    // amount of seed oil with less quality is more likely to produce a zombie clone 
    public int lowGradeSeedOil;

    // the likeliness that the clone created turns out to be a zombie with good seed oil
    public float probabilityOfMutationGoodSeedOil;

    // the likeliness that the clone created turns out to be a zombie with bad seed oil
    public int probabilityOfMutationBadSeedOil;


    // Start is called before the first frame update
    private void Start()
    {
        cloningCapsule.GetComponent<Transform>();
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
                Debug.Log("picked up high grade seed oil");
                highGradeSeedOil++;
                Destroy(other.gameObject);
                break;

            case "badSeedOil":
                Debug.Log("picked up low grade seed oil");
                lowGradeSeedOil++;
                Destroy(other.gameObject);
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

            highGradeSeedOil--;
        }
        else if (lowGradeSeedOil > 0)
        {
            var spawnPosition = transform.position;
            spawnPosition.z -= 3;

            Instantiate(
                RandomBoolean(probabilityOfMutationBadSeedOil) ? clonePrototype : zombiePrototype,
                spawnPosition, transform.rotation
            );

            lowGradeSeedOil--;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 50), "good Kernöl: " + highGradeSeedOil);
        GUI.Label(new Rect(10, 30, 150, 50), "bad Kernöl: " + lowGradeSeedOil);
    }

    // Update is called once per frame
    private void Update()
    {
        // cloning Button
        if (Input.GetKeyDown(KeyCode.E) && _standsOnCloningPlatform) CreateClone();
    }
}
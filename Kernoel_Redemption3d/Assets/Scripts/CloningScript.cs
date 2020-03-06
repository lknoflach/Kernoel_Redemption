using UnityEngine;

public class CloningScript : MonoBehaviour
{
    /** CLONING STUFF **/

    //checks if the player is standing on the cloningPlatform
    private bool _standsOnCloningPlatform;

    // the prototype for new clone objects
    public GameObject clonePrototype;

    private GameObject _player;
    
    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("PlayerHans");
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
                GameManager.Instance.UpdateSeedOilAmount(2);
                var goodSeedOil = other.gameObject;
                var goodSeedOilAudioData = goodSeedOil.GetComponent<AudioSource>();
                if (goodSeedOilAudioData) goodSeedOilAudioData.Play();
                Destroy(goodSeedOil);
                break;

            case "badSeedOil":
                GameManager.Instance.UpdateSeedOilAmount(1);
                var badSeedOil = other.gameObject;
                var badSeedOilAudioData = badSeedOil.GetComponent<AudioSource>();
                if (badSeedOilAudioData) badSeedOilAudioData.Play();
                Destroy(badSeedOil);
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
        if (GameManager.Instance.seedOilAmount > 2)
        {
            var spawnPosition = transform.position;
            spawnPosition.z -= 3;
            Instantiate(clonePrototype, spawnPosition, transform.rotation);
            
            GameManager.Instance.UpdateSeedOilAmount(-3);
        }
    }
    
    private void Update()
    {
        // cloning Button
        if (Input.GetKeyDown(KeyCode.E) && _standsOnCloningPlatform) CreateClone();
    }
    
    private void OnGUI()
    {
        if (!_player || !_standsOnCloningPlatform) return;
        
        var targetPos = Camera.main.WorldToScreenPoint(_player.transform.position);
        const string text = "E: Create Clone";
        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 100, 20), text);
    }
}
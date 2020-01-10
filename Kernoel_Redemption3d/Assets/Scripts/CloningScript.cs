using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningScript : MonoBehaviour
{
   
    /** CLONING STUFF **/
    //checks if the player is standing on the cloningplattform
    private bool stands_on_cloningplattform;

    // the object which enables the cloning
    public GameObject cloningCapsule;

    
    // the prototype for new clone objects
    public GameObject clonePrototype;

    // the prototype for new zombie objects
       // the prototype for new clone objects
    public GameObject zombiePrototype;

    //amount of good kernöl
    public int high_grade_kernoel = 0;
    
    //amout of kernöl with less quality is more likely to produce a zombie clone 
    public int low_grade_kernoel = 0;

    //the likelynes that the clone created turnes out to be a zombie with good kernoel
    public float propability_of_mutation_good_kernoel;
    
    //the likelynes that the clone created turnes out to be a zombie with bad kernoel
    public int propability_of_mutation_bad_kernoel;

   
   // Start is called before the first frame update
    void Start()
    {
        cloningCapsule.GetComponent<Transform>();
    }


    //just a random boolean function with a set likelness that it turnes out to be false 
    public bool randomBoolean(float likelynes_in_percent){
        
        //return true if the random number between 0 and 100 is smaller then your likelines for a zombie
        return (UnityEngine.Random.Range(0f, 100f) > likelynes_in_percent);
        
    }


    //adds Kernöl if we run into it
    private void OnCollisionEnter(Collision other)
    {
        //mabe should be added in on trigger not sure 
            //add the Kernöl to the player if he touches one 
           
         
        switch (other.gameObject.tag)
        {  
            case "gutesKernoel":
                high_grade_kernoel++;
                Debug.Log("picked up high grade Kernoel0");
                Destroy(other.gameObject);
                break;  
            case "schelchtesKernoel":
                Debug.Log("picked up low grade Kernoel");
                low_grade_kernoel++; 
                Destroy(other.gameObject);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "CloningCapsule":
                // enable cloning
                stands_on_cloningplattform = true;
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
                stands_on_cloningplattform = false;
                break;
        }
    }


    //creates the clone if we have Kernöl and with a posibility that it turnes out to be a zombie 
    public void createClone()
    {
        if(high_grade_kernoel > 0)
        {
            var spawnPosition = transform.position;
            spawnPosition.z -= 3;
            
            if(randomBoolean(propability_of_mutation_good_kernoel)){
                Instantiate(clonePrototype, spawnPosition, transform.rotation);
            }else{
                Instantiate(zombiePrototype, spawnPosition, transform.rotation);
            }
            
            high_grade_kernoel--;
        }else if(low_grade_kernoel > 0){
            var spawnPosition = transform.position;
            spawnPosition.z -= 3;

             if(randomBoolean(propability_of_mutation_bad_kernoel)){
                Instantiate(clonePrototype, spawnPosition, transform.rotation);
            }else{
                Instantiate(zombiePrototype, spawnPosition, transform.rotation);
            }
            low_grade_kernoel--;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 50), "good Kernöl: "+high_grade_kernoel);
        GUI.Label(new Rect(10, 30, 150, 50), "bad Kernöl: " + low_grade_kernoel);

    } 
   
    // Update is called once per frame
    void Update()
    {
        //cloning Button
        if (Input.GetKeyDown(KeyCode.E) && stands_on_cloningplattform)
        {
           createClone();
        }
    }
}

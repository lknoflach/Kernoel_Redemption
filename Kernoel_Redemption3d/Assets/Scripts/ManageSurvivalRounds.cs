using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageSurvivalRounds : MonoBehaviour
{
    
    
    //The amount of Round you have to play to win 
    public int RoundsToWin = 3;

    
    //SpawnStuff--------------------------------------------
    //if the survival starts lets enemies spawn
    public bool RoundHasStarted = false;

    //Amount of enemys that spawn in the first round
    public int AmountOfEnmys = 4;
    
    //Multiplier for enemys every round
    public int MultiplierOfEnmysPerRound = 2; 


    //for every enemy x amount of zombies spawn
    public int RelationOfZombiesAndEnemy = 4;
    
    //List with all enemies if its empty (all enemies are dead) the next round starts
    //enemys get added after the spawn every round
    public List <GameObject> enemies = new List<GameObject>();
    
    
    //Array of spawnpoint add spawnpoints here and they spawn random on one of them
    public Transform[] spawnpoints;

    //object to spawn the enemys
    public GameObject Zombie;
    public GameObject Enemy;
    
    //The number of the current survial round
    public int RoundNumber = 1;
    
    //player to look at player on spawn
    public Transform player;

    //lets player continue if he wants to in the GUI starts another round
    private bool continueGame = false;

    //UI Stuff ------------------------------------------------------------
    public Text roundNumberText;
    
    public Canvas  GUIMenu;
    // Update is called once per frame
    void Start(){
         StartCoroutine(removeDeadEnemysFromList());
        //roundNumberText = GetComponent<Text> ();
        roundNumberText.enabled = false;
        GUIMenu.gameObject.SetActive(false);
    }


    void Update()
    {
        // RoundNumberText.text="Roundnumber : ";
        if(RoundHasStarted == true){
            if(enemies.Count == 0){
                //show round numer 
                
                
                if(RoundNumber <= 3){
                    
                    StartCoroutine(showRoundNumber());
                   
                    SpawnEnemies(); 
                    
                    ++RoundNumber;
                    
                }else if(RoundHasStarted && continueGame == true){
                   // Debug.Log("You won to you want to countinue");
                    StartCoroutine(showRoundNumber());
                    SpawnEnemies();
                    ++RoundNumber;
                    //show dialog
                }else if(RoundHasStarted && continueGame == false){
                     GUIMenu.gameObject.SetActive(true);
                }

                
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        //start survival round
        //Show UI survive 3 rounds
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Player Collided");
            
            if(RoundHasStarted == false){
                RoundHasStarted = true;

                Debug.Log("ROund started");
            }
        }
        
        //to you want to continue 
    }


    private void SpawnEnemies(){
        //spawn enemies on random spawnpoint
         //yield return new WaitForSeconds(4);
         continueGame = false;
        for(int i = 0; i < 2; i++){
           
            if(Random.Range(1, RelationOfZombiesAndEnemy+1) == RelationOfZombiesAndEnemy){
                Debug.Log("SpawnZombies");

                //get random spawnpoint
                GameObject spawnedEnemy = (GameObject) Instantiate(Enemy, spawnpoints[Random.Range(0, spawnpoints.Length)]);
                spawnedEnemy.transform.position = new Vector3((spawnedEnemy.transform.position.x+Random.Range(-3f, 3f)), spawnedEnemy.transform.position.y, (spawnedEnemy.transform.position.z+Random.Range(-3f, 3f)));
                spawnedEnemy.transform.LookAt(player);
                enemies.Add(spawnedEnemy);
              //  yield return new WaitForSeconds(1);
            }else
            {
             
                Debug.Log("SpawnEnemy");
                 //TODO: Also chose random spawnpoint 
                 GameObject spawnedEnemy = (GameObject) Instantiate(Zombie, spawnpoints[Random.Range(0, spawnpoints.Length)]);
                spawnedEnemy.transform.position = new Vector3((spawnedEnemy.transform.position.x+Random.Range(-3f, 3f)), spawnedEnemy.transform.position.y, (spawnedEnemy.transform.position.z+Random.Range(-3f, 3f)));
                spawnedEnemy.transform.LookAt(player);
                enemies.Add(spawnedEnemy);
             //   yield return new WaitForSeconds(1);
            }
        }
    }
     IEnumerator removeDeadEnemysFromList(){
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(RoundHasStarted){
                
                for(int i = 0; i < enemies.Count; i++){
                    if(enemies[i] == null){
                        Debug.Log("Dead");
                        enemies.RemoveAt(i);
                       
                    }
                   
                //resultlist.RemoveAt(1);
                }
            }
        }
    }
    //UI Stuff

    IEnumerator showRoundNumber(){
        roundNumberText.enabled = true;
        roundNumberText.text = "Roundnumber: " + RoundNumber; 
        yield return new WaitForSeconds(3);
        roundNumberText.enabled = false;
    }
    
    void showYouWon(){
        roundNumberText.enabled = true;
        roundNumberText.text = "You Won"; 
    }

    public void continueGameHandler(){
        continueGame = true;
        GUIMenu.gameObject.SetActive(false);
    }

    public void stopGameHander(){
        SceneManager.LoadScene("DancingZombies");
    }

}

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
    public float MultiplierOfEnmysPerRound = 1.5f; 


    //for every enemy x amount of zombies spawn
    public int RelationOfZombiesAndEnemy = 4;


    public int AmountOfKernoel = 1;
    public int MultiplierOfKernOelPerRound = 2;

    
    //List with all enemies if its empty (all enemies are dead) the next round starts
    //enemys get added after the spawn every round
    public List <GameObject> enemies = new List<GameObject>();
    private List <GameObject> Barrels = new List<GameObject>();
    
    //Array of spawnpoint add spawnpoints here and they spawn random on one of them
    public Transform[] EnemySpawnpoints;
    public Transform[] BarrelSpawnpoints;
    public Transform KernoelSpawnpoint;

    //object to spawn the enemys
    public GameObject Zombie;
    public GameObject Enemy;
    public GameObject Barrel;
    public GameObject Kernoel;

    //The number of the current survial round
    public int RoundNumber = 1;
    
    //player wants to continue
    public bool PlayerWantsToContinue = true;
    //player to look at player on spawn
    public Transform player;

    //lets player continue if he wants to in the GUI starts another round
    private bool continueGame = false;

    //UI Stuff ------------------------------------------------------------
    private bool showedGUIMenu = false;
    public Text roundNumberText;
    
    public Canvas  GUIMenu;

    
    public Canvas  GUIUpgrade;
    // Update is called once per frame
    void Start(){
        StartCoroutine(removeDeadEnemysFromList());
        //StartCoroutine(removeExplodedBarrelsFromList());
        //roundNumberText = GetComponent<Text> ();
        roundNumberText.enabled = false;
        GUIUpgrade.gameObject.SetActive(false);
        GUIMenu.gameObject.SetActive(false);
    }


    void Update()
    {
        // RoundNumberText.text="Roundnumber : ";
        if(RoundHasStarted == true && PlayerWantsToContinue){
            if(enemies.Count == 0){
                //show round numer 
                
                
                if(RoundNumber <= 3){
                    
                    StartCoroutine(showRoundNumber());
                   
                    SpawnEnemies(); 
                    spawnBarrels();
                    SpawnKernoel();
                    ++RoundNumber;
                    
                }else if(RoundHasStarted && continueGame == true){
                   // Debug.Log("You won to you want to countinue");
                    StartCoroutine(showRoundNumber());
                    spawnBarrels();
                    SpawnEnemies();
                    SpawnKernoel();
                    ++RoundNumber;
                    //show dialog
                }else if(RoundHasStarted && continueGame == false && showedGUIMenu == false){
                    showGUIMenu();
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
         AmountOfEnmys = (int)(AmountOfEnmys * MultiplierOfEnmysPerRound);
        for(int i = 0; i < AmountOfEnmys; i++){
           
            if(Random.Range(1, RelationOfZombiesAndEnemy+1) == RelationOfZombiesAndEnemy){
                Debug.Log("SpawnZombies");

                //get random spawnpoint
                GameObject spawnedEnemy = (GameObject) Instantiate(Enemy, EnemySpawnpoints[Random.Range(0, EnemySpawnpoints.Length)]);
                spawnedEnemy.transform.position = new Vector3((spawnedEnemy.transform.position.x+Random.Range(-3f, 3f)), spawnedEnemy.transform.position.y, (spawnedEnemy.transform.position.z+Random.Range(-3f, 3f)));
                spawnedEnemy.transform.LookAt(player);
                enemies.Add(spawnedEnemy);
              //  yield return new WaitForSeconds(1);
            }else
            {
             
                Debug.Log("SpawnEnemy");
                 //TODO: Also chose random spawnpoint 
                 GameObject spawnedEnemy = (GameObject) Instantiate(Zombie, EnemySpawnpoints[Random.Range(0, EnemySpawnpoints.Length)]);
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
                    }
                //resultlist.RemoveAt(1);
            }
        }
    }

   

    void spawnBarrels(){
         //remove all barrels before spawn new
        for(int i = 0; i < Barrels.Count; i++){
            if(Barrels[i] != null){
                Destroy(Barrels[i]);
                Barrels.RemoveAt(i);
            }
        }
        for(int i = 0; i < BarrelSpawnpoints.Length; i++){
            Barrels.Add((GameObject) Instantiate(Barrel, BarrelSpawnpoints[i]));
        }
    }


    void SpawnKernoel(){
        AmountOfKernoel = AmountOfKernoel * MultiplierOfKernOelPerRound;
        for(int i = 0; i <AmountOfKernoel; i++)
        {
            Debug.Log("spawnedKernoel");
            GameObject spawnedKernoel = (GameObject) Instantiate(Kernoel, KernoelSpawnpoint);
            spawnedKernoel.transform.position = new Vector3((spawnedKernoel.transform.position.x+Random.Range(-3f, 3f)), spawnedKernoel.transform.position.y, (spawnedKernoel.transform.position.z+Random.Range(-3f, 3f)));
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
        PlayerWantsToContinue = false;
        GUIMenu.gameObject.SetActive(false);
    }

    public void showGUIMenu(){
        // GUIUpgrade.gameObject.SetActive(false);
        showedGUIMenu = true;
        GUIMenu.gameObject.SetActive(true);
    }

    public void showGUIUpgrade(){
        GUIMenu.gameObject.SetActive(false);
        GUIUpgrade.gameObject.SetActive(true);
        Debug.Log("ShowUpgrade");
    }

    public void backToGUIMenu(){
        GUIUpgrade.gameObject.SetActive(false);
        GUIMenu.gameObject.SetActive(true);
    }

}

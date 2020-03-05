using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageSurvivalRounds : MonoBehaviour
{
    // object to spawn the enemies
    public GameObject zombie;
    public GameObject enemy;
    public GameObject crawlingZombie;
    public GameObject golem;
    public GameObject barrel;

    // The amount of Round you have to play to win 
    public int roundsToWin = 3;

    // SpawnStuff--------------------------------------------
    // if the survival starts lets enemies spawn
    public bool roundHasStarted;

    // Amount of enemies that spawn in the first round
    public int amountOfEnemies = 4;

    // Multiplier for enemies every round
    public float multiplierOfEnemiesPerRound = 1.5f;

    // Array of spawn point add spawn points here and they spawn random on one of them
    public Transform[] enemySpawnPoints;
    public Transform[] barrelSpawnPoints;

    // List with all enemies if its empty (all enemies are dead) the next round starts
    // enemies get added after the spawn every round
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> barrels = new List<GameObject>();

    //gets activated on every end of the round
    public bool endOfRound;

    // The number of the current survival round
    public int roundNumber = 1;

    // player to look at player on spawn
    private GameObject _player;

    // lets player continue if he wants to in the GUI starts another round
    public bool continueGame;

    //the text of the Round number
    public Text roundNumberText;

    //UI managers
    public bool showGUISurvivalRounds = false;
    public bool showGUIUpgrade = false;

    private void Start()
    {
        _player = GameObject.Find("PlayerHans");
        
        StartCoroutine(RemoveDeadEnemiesFromList());
        // StartCoroutine(removeExplodedBarrelsFromList());
        
        roundNumberText.enabled = false;
        foreach (var barrelSpawnPoint in barrelSpawnPoints)
        {
            var spawnedBarrel = Instantiate(barrel);
            spawnedBarrel.transform.position = barrelSpawnPoint.position;
            spawnedBarrel.SetActive(true);
            barrels.Add(spawnedBarrel);
        }
    }

    private void Update()
    {
        // RoundNumberText.text = "Round number: ";
        if (roundHasStarted && endOfRound)
        {
            // show round number 
            if (roundNumber <= 3)
            {
                StartCoroutine(ShowRoundNumber());
                endOfRound = false;
                
                SpawnBarrels();
                SpawnEnemies();
                
                ++roundNumber;
            }
            else if (continueGame)
            {
                // show dialog
                //Debug.Log("You won! Do you want to continue");
                StartCoroutine(ShowRoundNumber());
                endOfRound = false;
                
                SpawnBarrels();
                SpawnEnemies();
                
                ++roundNumber;
            }
            else if (!continueGame && !showGUIUpgrade)
            {
                endOfRound = false;
                showGUISurvivalRounds = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // start survival round
        // Show UI survive 3 rounds
        if (other.gameObject.CompareTag("Player"))
        {
            if (roundHasStarted == false) roundHasStarted = true;
        }

        // to you want to continue 
    }

    private void SpawnEnemies()
    {
        // spawn enemies on random spawn point
        // yield return new WaitForSeconds(4);
        continueGame = false;
        amountOfEnemies = (int) (amountOfEnemies * multiplierOfEnemiesPerRound);
        
        for (var i = 0; i < amountOfEnemies; i++)
        {
            var randomNumber = Random.Range(1, 11 + 1);
            if (randomNumber >= 5 && randomNumber <= 7)
            {
                var spawnedEnemy = Instantiate(enemy, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);

                /*var position = spawnedEnemy.transform.position;
                position = new Vector3((position.x + Random.Range(-1f, 1f)), position.y, (position.z + Random.Range(-1f, 1f)));
                spawnedEnemy.transform.position = position;*/
                spawnedEnemy.transform.LookAt(_player.transform);
                
                spawnedEnemy.SetActive(true);
                
                enemies.Add(spawnedEnemy);
            }
            else if (randomNumber >= 8 && randomNumber <= 9)
            {
                var spawnedGolem = Instantiate(golem, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);
                // spawnedGolem.transform.position = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].position;
                
                var position = spawnedGolem.transform.position;
                position = new Vector3((position.x + Random.Range(-3f, 3f)), position.y + 1f, (position.z + Random.Range(-3f, 3f)));
                spawnedGolem.transform.position = position;
                spawnedGolem.transform.LookAt(_player.transform);
                
                spawnedGolem.SetActive(true);
                
                enemies.Add(spawnedGolem);
            }
            else if (randomNumber >= 10)
            {
                var spawnedCrawlingZombie = Instantiate(crawlingZombie, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);
                // spawnedCrawlingZombie.transform.position = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].position;
                
                var position = spawnedCrawlingZombie.transform.position;
                position = new Vector3((position.x + Random.Range(-3f, 3f)), position.y, (position.z + Random.Range(-3f, 3f)));
                spawnedCrawlingZombie.transform.position = position;
                spawnedCrawlingZombie.transform.LookAt(_player.transform);
                
                spawnedCrawlingZombie.SetActive(true);
                
                enemies.Add(spawnedCrawlingZombie);
            }
            else
            {
                var spawnedZombie = Instantiate(zombie, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);
                //spawnedZombie.transform.position = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].position;
                
                var position = spawnedZombie.transform.position;
                position = new Vector3((position.x + Random.Range(-3f, 3f)), position.y, (position.z + Random.Range(-3f, 3f)));
                spawnedZombie.transform.position = position;
                spawnedZombie.transform.LookAt(_player.transform);
                
                spawnedZombie.SetActive(true);
                
                enemies.Add(spawnedZombie);
            }
        }
    }

    private IEnumerator RemoveDeadEnemiesFromList()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (!roundHasStarted) continue;
            for (var i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null) continue;
                enemies.RemoveAt(i);
            }

            if (enemies.Count == 0)
            {
                endOfRound = true;
            }
        }
    }

    private void SpawnBarrels()
    {
        // remove all barrels before spawn new
        foreach (var barrel in barrels) if (barrel != null) Destroy(barrel);

        foreach (var barrelSpawnPoint in barrelSpawnPoints)
        {
            var spawnedBarrel = Instantiate(barrel);
            spawnedBarrel.transform.position = barrelSpawnPoint.position;
            spawnedBarrel.SetActive(true);
            barrels.Add(spawnedBarrel);
        }
    }
    
    // UI Stuff
    private IEnumerator ShowRoundNumber()
    {
        roundNumberText.enabled = true;
        roundNumberText.text = "Round number: " + roundNumber;
        yield return new WaitForSeconds(3);
        roundNumberText.enabled = false;
    }

    public void StopGame()
    {
        SceneManager.LoadScene("Victory");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageSurvivalRounds : MonoBehaviour
{

    //Please do not touch
    //its ugly but it works ;)

    // The amount of Round you have to play to win 
    public int roundsToWin = 3;

    // SpawnStuff--------------------------------------------
    // if the survival starts lets enemies spawn
    public bool roundHasStarted;

    // Amount of enemies that spawn in the first round
    public int amountOfEnemies = 4;

    // Multiplier for enemies every round
    public float multiplierOfEnemiesPerRound = 1.5f;

    // for every enemy x amount of zombies spawn
    public int relationOfZombiesAndEnemy = 4;

    public int amountOfSeedOil = 1;
    public int multiplierOfSeedOilPerRound = 2;

    // Array of spawn point add spawn points here and they spawn random on one of them
    public Transform[] enemySpawnPoints;
    public Transform[] barrelSpawnPoints;
    public Transform seedOilSpawnPoint;

    // List with all enemies if its empty (all enemies are dead) the next round starts
    // enemies get added after the spawn every round
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> _barrels = new List<GameObject>();

  

    // object to spawn the enemies
    public GameObject zombie;
    public GameObject enemy;
    public GameObject barrel;
    public GameObject seedOil;

    // The number of the current survival round
    public int roundNumber = 1;

    // player wants to continue
    public bool playerWantsToContinue = true;

    // player to look at player on spawn
    public Transform player;

    // lets player continue if he wants to in the GUI starts another round
    private bool _continueGame;

    // UI Stuff ------------------------------------------------------------
    private bool _showedGuiMenuOnClick;
    public Text roundNumberText;

    public Canvas guiMenu;
    public Canvas guiUpgrade;

    private void Start()
    {
        StartCoroutine(RemoveDeadEnemiesFromList());
        // StartCoroutine(removeExplodedBarrelsFromList());
        // roundNumberText = GetComponent<Text> ();
        roundNumberText.enabled = false;
        guiUpgrade.gameObject.SetActive(false);
        guiMenu.gameObject.SetActive(false);
        for(int i = 0; i < barrelSpawnPoints.Length; i++){
            _barrels.Add((GameObject) Instantiate(barrel, barrelSpawnPoints[i]));
        }
    }

    private void Update()
    {
        // RoundNumberText.text = "Round number: ";
        if (roundHasStarted)
        {
            if (enemies.Count == 0)
            {
                // show round number 
                if (roundNumber <= 3)
                {
                    StartCoroutine(ShowRoundNumber());

                    SpawnEnemies();
                    SpawnBarrels();
                    SpawnSeedOil();
                    ++roundNumber;
                }
                else if (roundHasStarted && _continueGame)
                {
                    Debug.Log("You won to you want to continue");
                    StartCoroutine(ShowRoundNumber());
                    SpawnBarrels();
                    SpawnEnemies();
                    SpawnSeedOil();
                    ++roundNumber;
                    // show dialog
                }
                else if (roundHasStarted && _continueGame == false && _showedGuiMenuOnClick == false)
                {
                    ShowGuiMenu();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // start survival round
        // Show UI survive 3 rounds
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Collided");

            if (roundHasStarted == false)
            {
                roundHasStarted = true;

                Debug.Log("Round started");
            }
        }

        // to you want to continue 
    }


    private void SpawnEnemies()
    {
        // spawn enemies on random spawn point
        // yield return new WaitForSeconds(4);
        _continueGame = false;
        amountOfEnemies = (int) (amountOfEnemies * multiplierOfEnemiesPerRound);
        for (var i = 0; i < amountOfEnemies; i++)
        {
            if (Random.Range(1, relationOfZombiesAndEnemy + 1) == relationOfZombiesAndEnemy)
            {
                Debug.Log("SpawnZombies");

                // get random spawn point
                var spawnedEnemy = Instantiate(enemy, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);
                spawnedEnemy.transform.position = new Vector3((spawnedEnemy.transform.position.x + Random.Range(-3f, 3f)),
                        spawnedEnemy.transform.position.y, (spawnedEnemy.transform.position.z + Random.Range(-3f, 3f)));
                spawnedEnemy.transform.LookAt(player);
                enemies.Add(spawnedEnemy);
                // yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log("SpawnEnemy");
                // TODO: Also chose random spawn point 
                var spawnedEnemy = Instantiate(zombie, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)]);
                spawnedEnemy.transform.position =
                    new Vector3((spawnedEnemy.transform.position.x + Random.Range(-3f, 3f)),
                        spawnedEnemy.transform.position.y, (spawnedEnemy.transform.position.z + Random.Range(-3f, 3f)));
                spawnedEnemy.transform.LookAt(player);
                enemies.Add(spawnedEnemy);
                // yield return new WaitForSeconds(1);
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

                Debug.Log("Dead");
                enemies.RemoveAt(i);
            }

            // resultList.RemoveAt(1);
        }
    }

    private void SpawnBarrels()
    {
        // remove all barrels before spawn new

        int lenghtBarrelsList = _barrels.Count;
        Debug.Log(lenghtBarrelsList);
        for(int i = 0; i < lenghtBarrelsList; i++){
            if(_barrels[i] != null){
                Destroy(_barrels[i]);
            }
            _barrels[i] = (GameObject) Instantiate(barrel, barrelSpawnPoints[i]);
            Debug.Log("Destroyed");
        }
    }

    private void SpawnSeedOil()
    {
        amountOfSeedOil = amountOfSeedOil * multiplierOfSeedOilPerRound;
        for (var i = 0; i < amountOfSeedOil; i++)
        {
            Debug.Log("spawned seed oil");
            var spawnedSeedOil = Instantiate(seedOil, seedOilSpawnPoint);
            spawnedSeedOil.transform.position =
                new Vector3((spawnedSeedOil.transform.position.x + Random.Range(-3f, 3f)),
                    spawnedSeedOil.transform.position.y, (spawnedSeedOil.transform.position.z + Random.Range(-3f, 3f)));
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

    void ShowYouWon()
    {
        roundNumberText.enabled = true;
        roundNumberText.text = "You Won";
    }

    public void ContinueGameHandler()
    {
        _showedGuiMenuOnClick = false;
        _continueGame = true;
        guiMenu.gameObject.SetActive(false);
    }

    public void StopGameHandler()
    {
        playerWantsToContinue = false;
        guiMenu.gameObject.SetActive(false);
    }

    public void ShowGuiMenu()
    {
        // GUIUpgrade.gameObject.SetActive(false);
        _showedGuiMenuOnClick = true;
        guiMenu.gameObject.SetActive(true);
    }

    public void ShowGuiUpgrade()
    {
        guiMenu.gameObject.SetActive(false);
        guiUpgrade.gameObject.SetActive(true);
        Debug.Log("ShowUpgrade");
    }

    public void BackToGuiMenu()
    {
        guiUpgrade.gameObject.SetActive(false);
        guiMenu.gameObject.SetActive(true);
    }
}
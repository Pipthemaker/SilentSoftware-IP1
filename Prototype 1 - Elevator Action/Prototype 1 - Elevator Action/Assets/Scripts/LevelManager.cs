using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Class Variables for Player Data
    public int playerFloor;
    private GameObject player;

    //Class Variables for Spawning Enemies
    public GameObject enemyPrefab;

    public GameObject[] FloorList;
    public List<Transform> spawnPoints;

    public float currentSpawnTimer;
    public float maxSpawnTimer;

    public int maxEnemies = 5;
    public int currentEnemies = 0;

    //Class Variables For UI
    public int score;
    public Text scoreText;

    public int lives;
    public Text livesText;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");

        currentSpawnTimer = maxSpawnTimer;

        lives = player.GetComponent<PlayerMovement>().life;

        scoreText.text = "Grade = % " + score;
        livesText.text = "Lives = " + lives;
	}
	

	// Update is called once per frame
	void Update ()
    {
        playerFloor = player.GetComponent<PlayerMovement>().currentFloor;

        if (currentSpawnTimer <= 0 && currentEnemies < maxEnemies)
        {
            if (spawnPoints.Count > 0)
            {
                spawnPoints[Random.Range(0, spawnPoints.Count)].GetComponent<EnemyDoorSpawn>().SpawnEnemy();
                currentEnemies++;
            }
            currentSpawnTimer = maxSpawnTimer;
        }
        else
        {
            currentSpawnTimer -= (1 * Time.deltaTime);
        }

        spawnPoints.Clear();

        if (playerFloor - 1 != -1)
        {
            if (FloorList[playerFloor - 1].GetComponent<FloorData>().hasEnemy == false)
            {
                for (int i = 0; i < FloorList[playerFloor - 1].transform.childCount; i++)
                {
                    Transform child = FloorList[playerFloor - 1].transform.GetChild(i);
                    if (child.tag == "Door")
                    {
                        spawnPoints.Add(child.gameObject.transform);
                    }
                }
            }
        }
        if (playerFloor + 1 != FloorList.Length)
        {
            if (FloorList[playerFloor + 1].GetComponent<FloorData>().hasEnemy == false)
            {
                for (int i = 0; i < FloorList[playerFloor + 1].transform.childCount; i++)
                {
                    Transform child2 = FloorList[playerFloor + 1].transform.GetChild(i);
                    if (child2.tag == "Door")
                    {
                        spawnPoints.Add(child2.gameObject.transform);
                    }
                }
            }
        }
    }
    

    //Methods For Updating UI
    public void UpdateScore()
    {
        scoreText.text = "Grade = % " + score;
    }

    public void UpdateLife()
    {
        livesText.text = "Lives = " + lives;
    }


    //Methods For Changing Scenes
    public void Respawn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int mode;
    public float moveSpeed;
    public float maxWaitTime;
    public float currentWaitTime;
    public int targetPosX;

    //Class Variables to find other Gameobject Data
    public GameObject parentFloor;
    public int currentFloor;
    public int playerFloor;
    private GameObject player;

    //floor assets
    public List<GameObject> StairBottoms;
    public List<GameObject> StairTops;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");

        mode = 0;
        currentWaitTime = Random.Range(maxWaitTime-1, maxWaitTime+2);
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerFloor = player.GetComponent<PlayerMovement>().currentFloor;

        /*if (currentFloor == playerFloor)
        {
            mode = 1;
        }*/

        switch (mode)
        {
            //Enemy Stops Moving for X seconds and then chooses a movement mode
            case 0:
                if (currentWaitTime <= 0)
                {
                    targetPosX = Random.Range(-30, 31);
                    mode = Random.Range(2, 5);
                }
                else
                {
                    currentWaitTime -= Time.deltaTime;
                }
                break;

            case 1: //Enemy moves to where the player is
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                break;

            case 2: //Enemy finds the stairs to the level up and uses them
                if (StairBottoms.Count > 0)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, StairBottoms[0].transform.position, moveSpeed * Time.deltaTime);

                    if (gameObject.transform.position.x == StairBottoms[0].transform.position.x)
                    {
                        StairBottoms[0].GetComponent<StairScript>().StairsMove(gameObject);
                    }
                }
                else
                {
                    mode = 0;
                    currentWaitTime = Random.Range(maxWaitTime - 1, maxWaitTime + 2);
                }
                break;

            case 3: //Enemy finds the stairs to the level down and uses them
                if (StairTops.Count > 0)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, StairTops[0].transform.position, moveSpeed * Time.deltaTime);

                    if (gameObject.transform.position.x == StairTops[0].transform.position.x)
                    {
                        StairTops[0].GetComponent<StairScript>().StairsMove(gameObject);
                    }
                }
                else
                {
                    mode = 0;
                    currentWaitTime = Random.Range(maxWaitTime - 1, maxWaitTime + 2);
                }
                break;

            case 4: //Enemy Randomly moves About Floor
                if (transform.position.x == targetPosX)
                {
                    mode = 0;
                    currentWaitTime = Random.Range(maxWaitTime - 1, maxWaitTime + 2);
                }
                else
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(targetPosX, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                }
                break;
        }

        if (currentFloor - 4 == playerFloor || currentFloor + 4 == playerFloor)
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().currentEnemies--;
            parentFloor.GetComponent<FloorData>().hasEnemy = false;
            Destroy(gameObject);
        }
    }

    public void FindEnvironment()
    {
        StairBottoms.Clear();
        StairTops.Clear();

        for (int i = 0; i < parentFloor.transform.childCount; i++)
        {
            Transform child = parentFloor.transform.GetChild(i);
            if (child.name == "StairsBottom")
            {
                StairBottoms.Add(child.gameObject);
            }
            if (child.name == "StairsTop")
            {
                StairTops.Add(child.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoorSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform spawnPoint;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

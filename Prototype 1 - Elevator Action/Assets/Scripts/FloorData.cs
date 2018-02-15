using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorData : MonoBehaviour
{
    public int floorNumber;

    public bool hasEnemy;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().currentFloor = floorNumber;

            GameObject lvManager = GameObject.Find("LevelManager");
            lvManager.GetComponent<LevelManager>().playerFloor = floorNumber;
        }

        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement>().parentFloor = gameObject;
            other.GetComponent<EnemyMovement>().currentFloor = floorNumber;
            other.GetComponent<EnemyMovement>().FindEnvironment();
            other.transform.parent = transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            hasEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            hasEnemy = false;
        }
    }
}

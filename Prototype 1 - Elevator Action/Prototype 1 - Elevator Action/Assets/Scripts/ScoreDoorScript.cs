using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDoorScript : MonoBehaviour
{
    public bool player;

    public int scoreValue;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (player)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject lvManager = GameObject.Find("LevelManager");
                lvManager.GetComponent<LevelManager>().score += scoreValue;
                lvManager.GetComponent<LevelManager>().UpdateScore();

                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = false;
    }

    /*private void OnMouseDown()
    {
            GameObject lvManager = GameObject.Find("LevelManager");
            lvManager.GetComponent<LevelManager>().score += scoreValue;
            lvManager.GetComponent<LevelManager>().UpdateScore();

            Destroy(gameObject);
    }*/
}

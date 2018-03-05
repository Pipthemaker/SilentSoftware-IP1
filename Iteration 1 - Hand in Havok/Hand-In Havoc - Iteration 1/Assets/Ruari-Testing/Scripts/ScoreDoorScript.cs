using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDoorScript : MonoBehaviour
{
    //from prototype 1

    public bool player; //bool for checking if player is on front of door

    public int scoreValue; //int value to be added to player score if door is triggered;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if player is in front of door and presses E
		if (player)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                /*
                //local variable for accessing the levelmanager in the scene
                GameObject lvManager = GameObject.Find("LevelManager");
                //door's score value is added to the players current score stored in the levelmanager and the UI is updated to reflect this
                lvManager.GetComponent<LevelManager>().score += scoreValue;
                lvManager.GetComponent<LevelManager>().UpdateScore();
                */

                //removes door from scene so it can't be triggered again
                Destroy(gameObject);
            }
        }
	}

    //player can trigger door if he or she is in front of it
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = true;
        }
    }

    //makes it so the player cant trigger the door if he or she isnt in front of it
    private void OnTriggerExit(Collider other)
    {
        player = false;
    }
}

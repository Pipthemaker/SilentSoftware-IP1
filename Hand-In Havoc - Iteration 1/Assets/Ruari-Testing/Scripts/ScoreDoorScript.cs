using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDoorScript : MonoBehaviour
{
    //from prototype 1

    public bool playerReady; //bool for checking if player is on front of door

    public int scoreValue; //int value to be added to player score if door is triggered;

    public bool activated; //Bool So the Door Can't Be Activated More Than Once

    public GameObject player;
    public GameObject scoreDoorImage; //Visual That The Door Still Needs To Be Triggered
    public GameObject activatedDoorImage; //Visual That The Door Has Been Triggered

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if player is in front of door and presses E
		if (playerReady)
        {
            if ((Input.GetKeyDown(KeyCode.E) || player.GetComponent<PlayerController>().tap) && !activated)
            {
                //local variable for accessing the levelmanager in the scene
                GameObject lvManager = GameObject.Find("LevelManager");
                //door's score value is added to the players current score stored in the levelmanager and the UI is updated to reflect this
                lvManager.GetComponent<LevelManager>().currentScore += scoreValue;
                lvManager.GetComponent<LevelManager>().UpdateScoreText();

                //The Last Door On The Level Will Have This Tag
                if (gameObject.tag == "Last Door")
                {
                    lvManager.GetComponent<LevelManager>().lastDoor = true;
                }

                StartCoroutine(ScoreSound());

                //Sets Activated Bool To True so it can't be triggered again
                activated = true;

                //Changes Door Visual
                scoreDoorImage.SetActive(false);
                activatedDoorImage.SetActive(true);
            }
        }
	}
    
    //player can trigger door if he or she is in front of it
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerReady = true;
        }
    }

    //makes it so the player cant trigger the door if he or she isnt in front of it
    private void OnTriggerExit(Collider other)
    {
        playerReady = false;
    }

    IEnumerator ScoreSound()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
    }
}

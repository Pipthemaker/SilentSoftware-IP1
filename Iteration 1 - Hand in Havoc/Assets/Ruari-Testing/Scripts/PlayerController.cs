using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float minMoveSpeed = 10;
    public float currentMoveSpeed = 50;
    public float maxMoveSpeed = 50;
    public float acceleration;

    public static int direction = 0; //-1 if facing left, 1 if facing right

    public bool canMove = true;
    public bool moving = false;
    public bool dodging;
    public bool swipeRight, swipeLeft, swipeUp, swipeDown, tap; //Bools for mobile input implementation
    private bool elevatorTrigger;

    public GameObject playerSprite;
    public GameObject[] playerArt;
    public GameObject[] tutorials;
    private GameObject elevator;

    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip bumpSound;

    private Animator playerAnimator;

    private IEnumerator playerHitInstance;

	// Use this for initialization
	void Start ()
    {
        direction = 1;
        moving = false;

        currentMoveSpeed = maxMoveSpeed;

        playerAnimator = playerSprite.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update ()
    {
        //Make it so the player can't move too fast or too slow
        if (currentMoveSpeed < minMoveSpeed)
        {
            currentMoveSpeed = minMoveSpeed;
        }
        if (currentMoveSpeed > maxMoveSpeed)
        {
            currentMoveSpeed = maxMoveSpeed;
        }

        if (moving)
        {
            //Moves Player Character
            transform.position = new Vector3(transform.position.x + (currentMoveSpeed * direction * Time.deltaTime), transform.position.y, 0);

            //Only Increases Player Speed If Gameobject is moving
            currentMoveSpeed *= (acceleration);
        }

        if (!dodging)
        {
            //Swiping Right
            if (Input.GetKeyDown(KeyCode.D) || swipeRight)
            {
                if (moving && direction != 1)
                {
                    currentMoveSpeed /= 2;
                }
                direction = 1;

                if (canMove)
                {
                    moving = true;
                    LevelManager.timerStart = true;
                }
            }
            //Swiping Left
            if (Input.GetKeyDown(KeyCode.A) || swipeLeft)
            {
                if (moving && direction != -1)
                {
                    currentMoveSpeed /= 2;
                }
                direction = -1;

                if (canMove)
                {
                    moving = true;
                    LevelManager.timerStart = true;
                }
            }
        }

        if (elevatorTrigger)
        {
            ElevatorMovement elevatorController = elevator.gameObject.GetComponent<ElevatorMovement>();

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || swipeUp)
            {
                elevatorController.ActivateElevator(1);

                if (Time.timeScale != 1)
                {
                    Time.timeScale = 1;

                    foreach (GameObject tut in tutorials)
                    {
                        tut.SetActive(false);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || swipeDown)
            {
                elevatorController.ActivateElevator(-1);
            }
        }

        //Swiping Up
        if ((Input.GetKeyDown(KeyCode.W) || swipeUp) && !dodging && moving)
        {
            StartCoroutine(PlayerDodging());
        }

        //Updates The AnimatorController's Paramaters To Match The Class's Variables
        playerAnimator.SetBool("Moving", moving);
        playerAnimator.SetInteger("Direction", direction);
        playerAnimator.SetBool("Jumping", dodging);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If Player Hits One of the Walls Of The Floor
        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<AudioSource>().PlayOneShot(bumpSound);

            //Reverses Player's Direction
            if (direction == 1)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            //Slows The Player Down Dramastically
            currentMoveSpeed /= 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stairs")
        {
            other.GetComponent<Collider>().enabled = false;
            other.GetComponent<StairsController>().Activate();
        }

        if (other.tag == "Obstacle")
        {
            if (!dodging)
            {
                if (other.GetComponent<Animator>().GetBool("Hit") == false)
                {
                    currentMoveSpeed /= 2.5f;
                    playerHitInstance = PlayerHit();
                    StartCoroutine(playerHitInstance);

                    GetComponent<AudioSource>().PlayOneShot(bumpSound);

                    other.GetComponent<Animator>().SetInteger("Direction", direction);
                    other.GetComponent<Animator>().SetBool("Hit", true);
                }
            }
        }

        if (other.tag == "Elevator")
        {
            elevator = other.gameObject;
            elevatorTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Elevator")
        {
            elevator = null;
            elevatorTrigger = false;
        }
    }

    public void StopPlayerHit()
    {
        if (playerHitInstance != null)
        {
            StopCoroutine(playerHitInstance);
        }
    }

    public void SwipeInput(string inputType)
    {
        //Debug.Log(inputType);
        StartCoroutine(RunSwipeInput(inputType));
    }

    //Runs When Player Presses W
    IEnumerator PlayerDodging()
    {
        dodging = true;
        GetComponent<AudioSource>().PlayOneShot(jumpSound);
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(landSound);
        dodging = false;
    }

    //Runs If Player Fails To Dodge An Obstacle
    IEnumerator PlayerHit()
    {

        Handheld.Vibrate(); //Phone Vibrates For 1 Second

        for (int i = 0; i <= 5; i++)
        {
            foreach (GameObject part in playerArt)
            {
                part.GetComponent<SpriteRenderer>().color = new Color(part.GetComponent<SpriteRenderer>().color.r, part.GetComponent<SpriteRenderer>().color.g, part.GetComponent<SpriteRenderer>().color.b, 0);
            }
            yield return new WaitForSeconds(0.1f);

            foreach (GameObject part in playerArt)
            {
                part.GetComponent<SpriteRenderer>().color = new Color(part.GetComponent<SpriteRenderer>().color.r, part.GetComponent<SpriteRenderer>().color.g, part.GetComponent<SpriteRenderer>().color.b, 250);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Implimneting detection for mobile controls
    IEnumerator RunSwipeInput(string inputType)
    {
        switch (inputType)
        {
            case "Tap":
                Debug.Log("Player Tapped");
                tap = true;
                yield return new WaitForSeconds(0.01f);
                tap = false;
                break;

            case "Swipe Right":
                Debug.Log("Player Swiped Right");
                swipeRight = true;
                yield return new WaitForSeconds(0.01f);
                swipeRight = false;
                break;

            case "Swipe Left":
                Debug.Log("Player Swiped Left");
                swipeLeft = true;
                yield return new WaitForSeconds(0.01f);
                swipeLeft = false;
                break;

            case "Swipe Up":
                Debug.Log("Player Swiped Up");
                swipeUp = true;
                yield return new WaitForSeconds(0.01f);
                swipeUp = false;
                break;

            case "Swipe Down":
                Debug.Log("Player Swiped Down");
                swipeDown = true;
                yield return new WaitForSeconds(0.01f);
                swipeDown = false;
                break;
        }
    }
}

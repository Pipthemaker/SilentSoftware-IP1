using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float minMoveSpeed = 10;
    public float currentMoveSpeed = 50;
    public float maxMoveSpeed = 50;
    public float acceleration;

    public static int direction = 0; //-1 if moving left, 0 if paused, 1 if moving right

    public bool moving;
    public bool canMove = true;
    public bool dodging;

	// Use this for initialization
	void Start ()
    {
        currentMoveSpeed = maxMoveSpeed;
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

        //Only Increases Player Speed If Gameobject is moving
        if (moving)
        {
            currentMoveSpeed *= (acceleration);
        }

        //Moves Player Character
        transform.position = new Vector3(transform.position.x + (currentMoveSpeed * direction * Time.deltaTime), transform.position.y, 0);

        //Swiping Right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (moving && direction!= 1)
            {
                currentMoveSpeed /= 2;
            }
            direction = 1;
            moving = true;
        }
        //Swiping Left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (moving && direction != -1)
            {
                currentMoveSpeed /= 2;
            }
            direction = -1;
            moving = true;
        }
        //Swiping Up
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(PlayerDodging());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If Player Hits One of the Walls Of The Floor
        if (collision.gameObject.tag == "Wall")
        {
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
            other.GetComponent<StairsController>().Activate();
        }

        if (other.tag == "Obstacle")
        {
            if(dodging)
            {

            }
            else
            {
                currentMoveSpeed /= 2;
                StartCoroutine(PlayerHit());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Elevator")
        {
            ElevatorMovement elevatorController = other.gameObject.GetComponent<ElevatorMovement>();

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                elevatorController.ActivateElevator(1);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                elevatorController.ActivateElevator(-1);
            }
        }
    }

    IEnumerator PlayerDodging()
    {
        dodging = true;
        yield return new WaitForSeconds(1f);
        dodging = false;
    }

    IEnumerator PlayerHit()
    {
        for (int i = 0; i <= 5; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

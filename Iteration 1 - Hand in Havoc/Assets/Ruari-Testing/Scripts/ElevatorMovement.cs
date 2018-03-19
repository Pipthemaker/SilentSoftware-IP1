using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    public Transform[] movePoints;

    public int target;

    public float moveSpeed;

    public bool moving;

    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[target].position, moveSpeed * Time.deltaTime);
            player.transform.position = gameObject.transform.position;
            player.GetComponent<PlayerController>().moving = false;
        }
	}

    public void ActivateElevator (int changeTarget)
    {
        if (!moving)
        {
            int newTarget = target + changeTarget;

            if (newTarget < 0)
            {
                newTarget = 0;
            }
            if (newTarget == movePoints.Length)
            {
                newTarget--;
            }

            if (newTarget != target)
            {
                target = newTarget;
            }

            StartCoroutine(ElevatorActivation());
        }
    }

    IEnumerator ElevatorActivation()
    {
        player.GetComponent<PlayerController>().moving = false;
        player.GetComponent<PlayerController>().canMove = false;
        moving = true;
        yield return new WaitForSeconds(2.4f);
        moving = false;
        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<PlayerController>().moving = true;
    }
}

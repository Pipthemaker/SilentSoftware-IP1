using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    public float moveSpeed;

    private bool canMove = true;
    private bool Pause = true;

    public Transform[] targetPositions;
    private int target = 0;

    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPositions[target].transform.position, Time.deltaTime * moveSpeed);
        }
        if (transform.position.y == targetPositions[target].position.y)
        {
            if (Pause)
            {
                StartCoroutine(Wait());
                Pause = false;
            }
        }
    }

    IEnumerator Wait()
    {
        canMove = false;
        yield return new WaitForSeconds(2);
        target++;
        if (target == targetPositions.Length)
        {
            target = 0;
        }
        canMove = true;
        Pause = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            other.transform.parent = null;
        }
    }
}

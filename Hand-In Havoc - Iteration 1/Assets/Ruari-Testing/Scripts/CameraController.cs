using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    public Transform bottomLimit;

    public Vector2 stairWarpPoint;

    private float moveSpeed;

    private bool stairMovement = false;

    //public float followSpeed;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update()
    {
        if (!stairMovement) //Follow The Player
        {
            transform.position = player.transform.position;
        }
        else //Move upward according to stair position
        {
            //transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, stairWarpPoint, moveSpeed * Time.deltaTime);
        }

        if (transform.position.y < bottomLimit.position.y && bottomLimit != null)
        {
            transform.position = new Vector2(transform.position.x, bottomLimit.position.y);
        }
    }

    public void StairMove(Vector2 movePoint)
    {
        stairWarpPoint = movePoint;

        if (gameObject.transform.position.y < movePoint.y)
        {
            moveSpeed = (movePoint.y - transform.position.y) / 2;
            StartCoroutine(StairCameraMovement());
        }
    }

    IEnumerator StairCameraMovement()
    {
        stairMovement = true;
        yield return new WaitForSeconds(2);
        stairMovement = false;
    }
}

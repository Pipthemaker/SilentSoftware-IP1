using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform bottomLimit;

    public float moveSpeed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * moveSpeed);

        if (transform.position.x < leftLimit.position.x)
        {
            transform.position = new Vector2(leftLimit.position.x, gameObject.transform.position.y);
        }

        if (transform.position.x > rightLimit.position.x)
        {
            transform.position = new Vector2(rightLimit.position.x, gameObject.transform.position.y);
        }

        if (transform.position.y < bottomLimit.position.y)
        {
            transform.position = new Vector2(gameObject.transform.position.x, bottomLimit.position.y);
        }
    }
}

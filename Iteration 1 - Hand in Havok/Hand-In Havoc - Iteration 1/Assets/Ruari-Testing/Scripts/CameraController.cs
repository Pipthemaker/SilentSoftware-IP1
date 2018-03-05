using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    public Transform bottomLimit;

    //public float followSpeed;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        if (transform.position.y < bottomLimit.position.y && bottomLimit != null)
        {
            transform.position = new Vector2(transform.position.x, bottomLimit.position.y);
        }
    }
}

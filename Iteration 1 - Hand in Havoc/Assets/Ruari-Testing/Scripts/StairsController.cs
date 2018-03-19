using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    private GameObject player;
    public GameObject mainCamera;

    public Transform warpPoint;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    { 

	}

    public void Activate()
    {
        StartCoroutine(ActivateStairs());
    }

    IEnumerator ActivateStairs()
    {
        player.GetComponent<PlayerController>().moving = false;
        player.GetComponent<PlayerController>().canMove = false;
        int storedDirection = PlayerController.direction;
        PlayerController.direction = 0;
        mainCamera.GetComponent<CameraController>().StairMove(warpPoint.position);

        yield return new WaitForSeconds(2);
        
        if (storedDirection == 1)
        {
            storedDirection = -1;
        }
        else
        {
            storedDirection = 1;
        }

        player.transform.position = warpPoint.transform.position;
        PlayerController.direction = storedDirection;
        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<PlayerController>().moving = true;
    }
}

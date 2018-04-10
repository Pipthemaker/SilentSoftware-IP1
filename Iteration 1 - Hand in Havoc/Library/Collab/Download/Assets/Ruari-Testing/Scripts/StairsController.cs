using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    public GameObject mainCamera;
    public List<GameObject> playerComponents;
    private GameObject player;

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

        for (int i = 0; i < 10; i ++)
        {
            foreach (GameObject component in playerComponents)
            {
                component.GetComponent<SpriteRenderer>().color = new Color(component.GetComponent<SpriteRenderer>().color.r, component.GetComponent<SpriteRenderer>().color.b, component.GetComponent<SpriteRenderer>().color.g, 0);
            }
        }

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

        for (int i = 0; i < 10; i++)
        {
            foreach (GameObject component in playerComponents)
            {
                component.GetComponent<SpriteRenderer>().color = new Color(component.GetComponent<SpriteRenderer>().color.r, component.GetComponent<SpriteRenderer>().color.b, component.GetComponent<SpriteRenderer>().color.g, 250);
            }
        }

        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<PlayerController>().moving = true;
    }
}

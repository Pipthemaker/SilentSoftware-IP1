using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    private GameObject player;

    public Transform warpPointBottom;
    public Transform warpPointTop;

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
        player.SetActive(false);
        yield return new WaitForSeconds(2);
        
        if (PlayerController.direction == 1)
        {
            PlayerController.direction = -1;
        }
        else
        {
            PlayerController.direction = 1;
        }

        if (player.transform.position.y < gameObject.transform.position.y)
        {
            player.transform.position = warpPointTop.transform.position;
        }
        else
        {
            player.transform.position = warpPointBottom.transform.position;
        }
        player.SetActive(true);
    }
}

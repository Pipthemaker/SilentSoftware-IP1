using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    public GameObject mainCamera;
    public List<GameObject> playerComponentsMain;
    public List<GameObject> playerComponentsBack;

    private GameObject player;
    public GameObject playerBack;

    public Transform warpPoint;

    public AudioClip stairSound;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");

        foreach (GameObject component in playerComponentsBack)
        {
            component.GetComponent<SpriteRenderer>().color = new Color(component.GetComponent<SpriteRenderer>().color.r, component.GetComponent<SpriteRenderer>().color.b, component.GetComponent<SpriteRenderer>().color.g, 0);
        }
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
        GetComponent<AudioSource>().PlayOneShot(stairSound);
        player.transform.position = new Vector3(gameObject.transform.position.x + 3.5f, gameObject.transform.position.y + 7, player.transform.position.z);
        player.GetComponent<PlayerController>().moving = false;
        player.GetComponent<PlayerController>().canMove = false;
        player.GetComponent<PlayerController>().StopPlayerHit();
        int storedDirection = PlayerController.direction;
        PlayerController.direction = 0;
        mainCamera.GetComponent<CameraController>().StairMove(warpPoint.position);
        playerBack.GetComponent<Animator>().SetBool("Stairs", true);

        foreach (GameObject component in playerComponentsMain)
        {
            component.GetComponent<SpriteRenderer>().color = new Color(component.GetComponent<SpriteRenderer>().color.r, component.GetComponent<SpriteRenderer>().color.b, component.GetComponent<SpriteRenderer>().color.g, 0);
        }
        foreach (GameObject component in playerComponentsBack)
        {
            component.GetComponent<SpriteRenderer>().color = new Color(component.GetComponent<SpriteRenderer>().color.r, component.GetComponent<SpriteRenderer>().color.b, component.GetComponent<SpriteRenderer>().color.g, 250);
        }

        if (storedDirection == 1)
        {
            storedDirection = -1;
        }
        else
        {
            storedDirection = 1;
        }
        PlayerController.direction = storedDirection;

        yield return new WaitForSeconds(2);

        player.transform.position = warpPoint.transform.position;

        foreach (GameObject component in playerComponentsMain)
        {
            component.GetComponent<SpriteRenderer>().color = new Color(component.GetComponent<SpriteRenderer>().color.r, component.GetComponent<SpriteRenderer>().color.b, component.GetComponent<SpriteRenderer>().color.g, 250);
        }
        foreach (GameObject component in playerComponentsBack)
        {
            component.GetComponent<SpriteRenderer>().color = new Color(component.GetComponent<SpriteRenderer>().color.r, component.GetComponent<SpriteRenderer>().color.b, component.GetComponent<SpriteRenderer>().color.g, 0);
        }
        
        playerBack.GetComponent<Animator>().SetBool("Stairs", false);
        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<PlayerController>().moving = true;
    }
}

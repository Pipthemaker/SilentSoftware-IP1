using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int life = 3;

    public bool canBeHurt = true;
    public bool dead;
    
    public float moveSpeed;

    public int currentFloor;

    public GameObject respawnScreen;

    public Material mat;

    // Use this for initialization
    void Start ()
    {
        //Makes sure that the player's visible
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 255);
    }

    // Update is called once per frame
    void Update()
    {
    
        // Mobile Controls
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                transform.position = new Vector3((gameObject.transform.position.x - (moveSpeed * Time.deltaTime)), gameObject.transform.position.y, gameObject.transform.position.z);
                //Debug.Log("Moved Left");
            }
            if (Input.mousePosition.x > Screen.width / 2)
            {
                transform.position = new Vector3((gameObject.transform.position.x + (moveSpeed * Time.deltaTime)), gameObject.transform.position.y, gameObject.transform.position.z);
                //Debug.Log("Moved Right");
            }
        }


        //PC Controls
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3((gameObject.transform.position.x - (moveSpeed * Time.deltaTime)), gameObject.transform.position.y, gameObject.transform.position.z);
            //Debug.Log("Moved Left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3((gameObject.transform.position.x + (moveSpeed * Time.deltaTime)), gameObject.transform.position.y, gameObject.transform.position.z);
            //Debug.Log("Moved Right");
        }

        if (life == 0)
        {
            dead = true;
        }

        if (dead)
        {
            gameObject.transform.parent = null;
            respawnScreen.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            currentFloor = other.gameObject.GetComponent<FloorData>().floorNumber;
        }

        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);

            if (canBeHurt)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().currentEnemies--;
                life--;
                GameObject.Find("LevelManager").GetComponent<LevelManager>().lives = life;
                GameObject.Find("LevelManager").GetComponent<LevelManager>().UpdateLife();
                StartCoroutine(OnHit());
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Stairs" && Input.GetKeyDown(KeyCode.W))
        {
            other.GetComponent<StairScript>().StairsMove(gameObject);
        }
    }

    IEnumerator OnHit()
    {
        canBeHurt = false;
        for (int i = 0; i < 5; i++)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
            yield return new WaitForSeconds(0.1f);
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 255);
            yield return new WaitForSeconds(0.1f);
        }
        canBeHurt = true;
    }
}

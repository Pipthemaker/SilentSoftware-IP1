using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownController : MonoBehaviour
{
    public GameObject tutText; 

    public float slowdownSpeed = 0.35f;

    public Camera mainCamera;

	// Use this for initialization
	void Start ()
    {
        tutText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {;

            mainCamera.GetComponent<Animator>().SetBool("Tutorial", true);

            Time.timeScale = slowdownSpeed;

            tutText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            mainCamera.GetComponent<Animator>().SetBool("Tutorial", false);

            Time.timeScale = 1f;

            tutText.SetActive(false);

            gameObject.SetActive(false);
        }
    }
}

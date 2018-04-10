using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public bool tutorial;

    public Camera mainCamera;

    public List<GameObject> tutorialObjects;

	// Use this for initialization
	void Start ()
    {
        foreach (GameObject tutObject in tutorialObjects)
        {
            tutObject.SetActive(false);
        }

        Time.timeScale = 0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mainCamera.GetComponent<Animator>().SetBool("Tutorial", false);
        }
    }

    public void TutorialNo()
    {
        foreach (GameObject tutObject in tutorialObjects)
        {
            tutObject.SetActive(false);
        }
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void TutorialYes()
    {
        foreach (GameObject tutObject in tutorialObjects)
        {
            tutObject.SetActive(true);
        }
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}

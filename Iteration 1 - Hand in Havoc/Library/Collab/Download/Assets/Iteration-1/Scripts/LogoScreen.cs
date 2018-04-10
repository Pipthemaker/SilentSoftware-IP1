using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScreen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        //Immediately starts the coroutine
        StartCoroutine(WaitThenLoad());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Coroutine Waits for the length of the scene animation then loads the MainMenu
    IEnumerator WaitThenLoad()
    {
        yield return new WaitForSeconds(2f);
        //GetComponent<AudioSource>().Play();
        //yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene("MainMenu");
    }
}

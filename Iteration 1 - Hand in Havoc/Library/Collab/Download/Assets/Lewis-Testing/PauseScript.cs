using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{
    bool paused = false; //bool so show wether the game is paused or not
	public GameObject pauseObject; //gameobjects that will show on the screen when the game is paused
    public string currentScene;

	void Start()
    {
		Time.timeScale = 1; //makes sure that the game runs at it's regular speed at the start of the level

        currentScene = SceneManager.GetActiveScene().name;
		HidePaused ();
	}
    
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = TogglePause();
        }
	}

	private void ShowPaused()
    {
        pauseObject.SetActive(true);
	}

	private void HidePaused()
    {
        pauseObject.SetActive(false);
    }

	private bool TogglePause()
    {
		if (Time.timeScale == 0f)
        {
			Time.timeScale = 1f;
			HidePaused ();
			return(false);
		}
        else
        {
			Time.timeScale = 0f;
            ShowPaused();
            return (true);
		}

	}

	private void PauseControl()
    {
		if(Time.timeScale ==1)
		{
			Time.timeScale = 0;
			ShowPaused ();
		}
        else if (Time.timeScale == 0)
        {
			Time.timeScale = 1;
			HidePaused();
		}
	}

    public void PauseButton()
    {
        paused = TogglePause();
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentScene.ToString());
    }

    public void MainMenu()
    {
        GameObject audioController = GameObject.Find("MusicManager(Clone)");
        audioController.GetComponent<SoundController>().ChangeMusic(1);

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

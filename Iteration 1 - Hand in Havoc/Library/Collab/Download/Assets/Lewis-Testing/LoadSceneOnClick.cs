using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    private GameObject gameData;

    public GameObject soundManager;

    // Use this for initialization
    void Start ()
    {
        //Creates the gamedata gameobject to store user data (if one doesn't already exist)
        gameData = GameObject.Find("GameData");
        if (gameData == null)
        {
            gameData = new GameObject("GameData");
            gameData.AddComponent<GameDataScript>();
        }

        GameObject soundmanager = GameObject.Find("MusicManager(Clone)");
        if (soundmanager == null)
        {
            Instantiate(soundManager);
        }
    }

    public void LoadMainLevel()
    {
        StartCoroutine(ButtonClick());
        gameData.GetComponent<GameDataScript>().score = 0;
		SceneManager.LoadScene ("Level_1");
	}

    public void LoadOptions()
    {
        StartCoroutine(ButtonClick());
        SceneManager.LoadScene("OptionsScene");
    }
    
    public void LoadHiScores()
    {
        StartCoroutine(ButtonClick());
        gameData.GetComponent<GameDataScript>().score = 0;
        SceneManager.LoadScene("Hi-Score");
    }

    public void OpenWebPage()
    {
        StartCoroutine(ButtonClick());
        Application.OpenURL("https://www.gcu.ac.uk/");
    }
    
    public void QuitGame()
    {
        StartCoroutine(ButtonClick());
        Application.Quit();
    }

    IEnumerator ButtonClick()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
    }
}

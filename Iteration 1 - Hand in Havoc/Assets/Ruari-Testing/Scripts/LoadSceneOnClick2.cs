using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick2 : MonoBehaviour
{
    public GameObject menuUiObjects;
    public GameObject optionsUiObjects;
    public GameObject hiScoreUiObjects;
    public GameObject creditsUiObjects;
    public GameObject mainCamera;
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

        Time.timeScale = 1f;
    }

    public void LoadMainLevel()
    {
        StartCoroutine(ButtonClick());
        gameData.GetComponent<GameDataScript>().score = 0;
        gameData.GetComponent<GameDataScript>().retrys = 1;
        SceneManager.LoadScene ("Level_1-Intro");
    }

    public void LoadOptions()
    {
        StartCoroutine(ButtonClick());
        //SceneManager.LoadScene("OptionsScene");
        StartCoroutine(CameraMoveOptions());
    }
    
    public void LoadHiScores()
    {
        StartCoroutine(ButtonClick());
        gameData.GetComponent<GameDataScript>().score = 0;
        //SceneManager.LoadScene("Hi-Score");
        StartCoroutine(CameraMoveHiScores());
    }

    public void LoadMainMenu()
    {
        StartCoroutine(ButtonClick());
        //SceneManager.LoadScene("Hi-Score");
        StartCoroutine(CameraMoveMainMenu());
    }

    public void LoadCredits()
    {
        StartCoroutine(ButtonClick());
        StartCoroutine(CameraMoveCredits());
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

    IEnumerator CameraMoveOptions()
    {
        menuUiObjects.SetActive(false);
        mainCamera.GetComponent<Animator>().SetInteger("State", 1);
        yield return new WaitForSeconds(1);
        optionsUiObjects.SetActive(true);
    }

    IEnumerator CameraMoveHiScores()
    {
        creditsUiObjects.SetActive(false);
        menuUiObjects.SetActive(false);
        mainCamera.GetComponent<Animator>().SetInteger("State", 2);
        yield return new WaitForSeconds(1);
        hiScoreUiObjects.SetActive(true);
    }

    IEnumerator CameraMoveMainMenu()
    {
        optionsUiObjects.SetActive(false);
        hiScoreUiObjects.SetActive(false);
        mainCamera.GetComponent<Animator>().SetInteger("State", 0);
        yield return new WaitForSeconds(1);
        menuUiObjects.SetActive(true);
    }

    IEnumerator CameraMoveCredits()
    {
        hiScoreUiObjects.SetActive(false);
        mainCamera.GetComponent<Animator>().SetInteger("State", 3);
        yield return new WaitForSeconds(1);
        creditsUiObjects.SetActive(true);
    }

    IEnumerator ButtonClick()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
    }
}

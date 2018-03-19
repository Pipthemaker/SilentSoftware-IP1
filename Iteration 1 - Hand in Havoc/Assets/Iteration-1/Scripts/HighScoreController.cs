using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreController : MonoBehaviour
{
    public int inputScore;
    public int[] highScoreArray = new int[5];

    public Text[] highScores = new Text[5];

    public GameObject[] highlights = new GameObject[5];

    public AudioClip buttonClick;

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScoreArray[i] = PlayerPrefs.GetInt("HighScore" + i);
            highScores[i].text = (i + 1) + ": " + highScoreArray[i];
        }

        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            inputScore = gameData.GetComponent<GameDataScript>().score;
        }

        UpdateHighScoreArray();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
    
    public void UpdateHighScoreArray()
    {
        int i = 0;

        while (i < highScoreArray.Length)
        {
            if (highScoreArray[i] <= inputScore)
            {
                break;
            }
            i++;
        }

        //Moves All Items In An Array down 1 Index
        for (int j = highScoreArray.Length - 1; j > i; j--)
        {
            highScoreArray[j] = highScoreArray[j - 1];
        }

        if (i < highScoreArray.Length)
        {
            highScoreArray[i] = inputScore;
            UpdateScoreBoard();
        }

        foreach (GameObject highlight in highlights)
        {
            highlight.SetActive(false);
        }

        if (inputScore != 0)
        {
            highlights[i].SetActive(true);
        }
    }

    //Method For Updating The Text On The Screen
    private void UpdateScoreBoard()
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            highScores[i].text = (i + 1) + ": " + highScoreArray[i];
            PlayerPrefs.SetInt("HighScore" + i, highScoreArray[i]);
        }
    }

    //Methods For Buttons In The Scene
    public void RestartGame()
    {
        StartCoroutine(ButtonClick());

        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameData.GetComponent<GameDataScript>().score = 0;
        }

        SceneManager.LoadScene("Level_1");
    }

    public void LoadMainMenu()
    {
        StartCoroutine(ButtonClick());
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        StartCoroutine(ButtonClick());
        Application.Quit();
    }

    IEnumerator ButtonClick()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClick);
        yield return new WaitForSeconds(buttonClick.length);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLossController : MonoBehaviour
{
    public GameObject winText;
    public GameObject lossText;
    public GameObject checkUI;

    public Text retryButtonText;
    public Text scoreText;

    private string nextScene;

    public Button retryButton;

    public static bool winLoss; //True if win, False If Loss

	// Use this for initialization
	void Start ()
    {
        GameObject gameData = GameObject.Find("GameData");

        retryButtonText.text = "Retry Year (" + gameData.GetComponent<GameDataScript>().retrys + ")";
        scoreText.text = "Score : " + gameData.GetComponent<GameDataScript>().score;
        nextScene = gameData.GetComponent<GameDataScript>().nextScene;

        if(winLoss)
        {
            winText.SetActive(true);
            lossText.SetActive(false);
        }
        else
        {
            winText.SetActive(false);
            lossText.SetActive(true);
        }

        if (gameData.GetComponent<GameDataScript>().retrys == 0)
        {
            retryButton.interactable = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OpenCheck()
    {
        checkUI.SetActive(true);
    }

    public void CloseCheck()
    {
        checkUI.SetActive(false);
    }

    public void ConfirmCheck()
    {
        GameObject gameData = GameObject.Find("GameData");
        gameData.GetComponent<GameDataScript>().retrys--;
        gameData.GetComponent<GameDataScript>().score = 0;
        SceneManager.LoadScene(gameData.GetComponent<GameDataScript>().lastScene);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool timerStart = false;
    private bool timerWarned = false;
    public bool lastDoor;

    public int currentScore;

    private float timerValue;
    public float maxTimerValue;
    
    public Text timerText;
    public Text ScoreText;

    public string nextScene;

    public AudioClip levelWin;
    public AudioClip levelLoss;

	// Use this for initialization
	void Start ()
    {
        timerValue = maxTimerValue;
        timerText.text = Mathf.Ceil(timerValue).ToString();
        timerStart = false;

        GameObject gameData = GameObject.Find("GameData");
        currentScore = gameData.GetComponent<GameDataScript>().score;
        ScoreText.text = "Score = " + currentScore;

        GameObject audioController = GameObject.Find("MusicManager(Clone)");
        audioController.GetComponent<SoundController>().ChangeMusic(2);

        ScoreText.text = "Score = " + currentScore;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (timerStart)
        {
            timerValue -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timerValue).ToString();

            if (timerValue <= 30)
            {
                timerText.color = new Color(255, timerText.color.g - 0.05f * Time.deltaTime, timerText.color.b - 0.05f * Time.deltaTime, timerText.color.a);
            }
        }

        if (timerValue <= 10 && timerWarned == false)
        {
            StartCoroutine(TimerWarning());
            timerWarned = true;
        }

        if (timerValue <= 0)
        {
            StartCoroutine(LevelLoss());
        }
    }

    public void LastDoor()
    {
        lastDoor = true;
        StartCoroutine(LevelEnd());
    }

    public void UpdateScoreText()
    {
        ScoreText.text = "Score = " + currentScore;
    }

    IEnumerator TimerWarning()
    {
        for (int i = 0; i < 5; i++)
        {
            timerText.color = new Color(timerText.color.r, timerText.color.g, timerText.color.b, 0);
            yield return new WaitForSeconds(0.5f);
            timerText.color = new Color(timerText.color.r, timerText.color.g, timerText.color.b, 255);
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < 10; i++)
        {
            timerText.color = new Color(timerText.color.r, timerText.color.g, timerText.color.b, 0);
            yield return new WaitForSeconds(0.25f);
            timerText.color = new Color(timerText.color.r, timerText.color.g, timerText.color.b, 255);
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator LevelEnd()
    {
        //Stops The Player And Makes It So That He Cannot Start Moving Again
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().moving = false;
        player.GetComponent<PlayerController>().canMove = false;

        //Gives The Player's Score To The GameData So That It Can Be Carried Between Scenes
        GameObject gameData = GameObject.Find("GameData");
        gameData.GetComponent<GameDataScript>().score = currentScore;
        gameData.GetComponent<GameDataScript>().lastScene = SceneManager.GetActiveScene().name.ToString();
        gameData.GetComponent<GameDataScript>().nextScene = nextScene;

        //Stops The Timer
        timerStart = false;

        //Run Animation For Level End && Wait For Animation Run Time
        GetComponent<AudioSource>().PlayOneShot(levelWin);
        yield return new WaitForSeconds(levelWin.length);
        GameObject audioController = GameObject.Find("MusicManager(Clone)");
        audioController.GetComponent<SoundController>().ChangeMusic(1);
        WinLossController.winLoss = true;
        SceneManager.LoadScene("WinLoss_Scene");
    }

    IEnumerator LevelLoss()
    {
        GetComponent<AudioSource>().PlayOneShot(levelLoss);
        yield return new WaitForSeconds(levelLoss.length);
        GameObject gameData = GameObject.Find("GameData");
        gameData.GetComponent<GameDataScript>().score = currentScore;
        gameData.GetComponent<GameDataScript>().lastScene = SceneManager.GetActiveScene().name.ToString();
        WinLossController.winLoss = false;
        SceneManager.LoadScene("WinLoss_Scene");
    }
}

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

    private float timerValue = 60; //Set To Be A Minute
    
    public Text timerText;
    public Text ScoreText;

	// Use this for initialization
	void Start ()
    {
        timerStart = false;

        GameObject gameData = GameObject.Find("GameData");
        currentScore = gameData.GetComponent<GameDataScript>().score;

        GameObject audioController = GameObject.Find("MusicManager(Clone)");
        audioController.GetComponent<SoundController>().ChangeMusic(2);

        ScoreText.text = "Score = " + currentScore;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (lastDoor)
        {
            StartCoroutine(LevelEnd());
        }

        if (timerStart)
        {
            timerValue -= Time.deltaTime;
            timerText.text = "Timer = " + Math.Round(timerValue, 3);

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
            GameObject gameData = GameObject.Find("GameData");
            gameData.GetComponent<GameDataScript>().score = currentScore;
            SceneManager.LoadScene("Hi-Score");
        }
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

        //Stops The Timer
        timerStart = false;

        //Run Animation For Level End && Wait For Animation Run Time
        yield return new WaitForSeconds(1);
        GameObject audioController = GameObject.Find("MusicManager(Clone)");
        audioController.GetComponent<SoundController>().ChangeMusic(1);
        SceneManager.LoadScene("Hi-Score");
    }
}

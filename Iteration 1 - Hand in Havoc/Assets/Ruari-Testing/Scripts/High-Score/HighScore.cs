using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public int inputScore;
    public int[] highScoreArray = new int[5];

    public Text[] highScores = new Text[5];

    // Use this for initialization
    void Start () 
    {
        GameObject gameData = GameObject.Find("GameData");

        if (gameData != null)
        {
            inputScore = gameData.GetComponent<GameDataScript>().score;
        }

        for (int i = 0; i < highScores.Length; i++)
        {
            highScoreArray[i] = PlayerPrefs.GetInt("HighScore" + i);
            highScores[i].text = i + ": " + highScoreArray[i];
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
    }

    private void UpdateScoreBoard()
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            highScores[i].text = i + ": " + highScoreArray[i];
            PlayerPrefs.SetInt("HighScore" + i, highScoreArray[i]);
        }
    }
}

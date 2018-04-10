using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelIntroScript : MonoBehaviour
{
    public GameObject year;
    public GameObject score;

    public int currentScore;

    public Text scoreText;

    public string level;

	// Use this for initialization
	void Start ()
    {
        GameObject gameData = GameObject.Find("GameData");
        currentScore = gameData.GetComponent<GameDataScript>().score;

        scoreText.text = "Current Score : " + currentScore;

        year.SetActive(false);
        score.SetActive(false);
        
        StartCoroutine(SceneAnimation());
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    IEnumerator SceneAnimation()
    {
        yield return new WaitForSeconds(1);
        
        year.SetActive(true);
        yield return new WaitForSeconds(1);
        
        score.SetActive(true);
        yield return new WaitForSeconds(1);
        
        SceneManager.LoadScene(level);
    }
}

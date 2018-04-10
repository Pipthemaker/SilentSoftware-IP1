using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour
{
    public int score;
    public int retrys = 1;

    public string lastScene;
    public string nextScene;

    public bool tutorial;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}

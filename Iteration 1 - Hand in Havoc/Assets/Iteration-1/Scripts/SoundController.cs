using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip menuMusic;
    public AudioClip gameMusic;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        ChangeMusic(1);

        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeMusic(int track)
    {
        switch(track)
        {
            case (1): //Menu Music
                audioSource.clip = menuMusic;
                audioSource.Play();
                break;

            case (2): // Game Music
                audioSource.clip = gameMusic;
                audioSource.Play();
                break;
        }
    }
}

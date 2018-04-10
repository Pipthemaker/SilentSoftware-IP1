using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public GameObject instructionsScreen;
    public GameObject checkScreen;

    public AudioClip buttonClick;

    public Slider masterSoundSlider;
    public Slider musicSoundSlider;
    public Slider sfxSoundSlider;

    public AudioMixer gameMixer;

	// Use this for initialization
	void Start ()
    {
        instructionsScreen.SetActive(false);
        checkScreen.SetActive(false);

        float masterValue = 0;
        gameMixer.SetFloat("MasterVolume", masterValue);
        masterSoundSlider.value = masterValue;

        float musicValue = 0;
        gameMixer.SetFloat("MusicVolume", musicValue);
        musicSoundSlider.value = musicValue;

        float sfxValue = 20;
        gameMixer.SetFloat("SFXVolume", sfxValue);
        sfxSoundSlider.value = sfxValue;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeMixer()
    {
        float masterValue = masterSoundSlider.value;
        gameMixer.SetFloat("MasterVolume", masterValue);

        float musicValue = musicSoundSlider.value;
        gameMixer.SetFloat("MusicVolume", musicValue);

        float sfxValue = sfxSoundSlider.value;
        gameMixer.SetFloat("SFXVolume", sfxValue);
    }

    public void OpenInstructions()
    {
        StartCoroutine(ButtonClick());
        instructionsScreen.SetActive(true);
    }

    public void CloseInstructions()
    {
        StartCoroutine(ButtonClick());
        instructionsScreen.SetActive(false);
    }

    public void OpenCheckScreen()
    {
        StartCoroutine(ButtonClick());
        checkScreen.SetActive(true);
    }

    public void ChoseNo()
    {
        StartCoroutine(ButtonClick());
        checkScreen.SetActive(false);
    }

    public void ChoseYes()
    {
        StartCoroutine(ButtonClick());
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, 0);
        }

        checkScreen.SetActive(false);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(ButtonClick());
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator ButtonClick()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClick);
        yield return new WaitForSeconds(buttonClick.length);
    }
}

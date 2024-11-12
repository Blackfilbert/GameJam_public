using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] AudioSource stepSource;
    [SerializeField] AudioSource startSound;
    [SerializeField] private GameObject mutedSound; 
    [SerializeField] private GameObject unmutedSound;
    [SerializeField] private GameObject mutedMusic; 
    [SerializeField] private GameObject unmutedMusic;
    [SerializeField] private GameObject tutorial;

    private void Start() {
        Time.timeScale = 0f;
    }

    public void PlayButtonPressed() {
        SceneManager.LoadScene("Comix");
    }

    public void RestartButtonPressed() {
        SceneManager.LoadScene("Game");
    }

    public void CredditsButtonPressed() {
        SceneManager.LoadScene("Credits");
    }

    public void BackButtonPressed() {
        SceneManager.LoadScene("MainMenu");
    }

    public void SoundVolumeButton() {
        
    }

    public void SoundVolumeChange(float sliderValue) {
        audioMixer.SetFloat("soundVolume", sliderValue);

        if(sliderValue == -80) {
            mutedSound.SetActive(true);
            unmutedSound.SetActive(false);
        } else {
            mutedSound.SetActive(false);
            unmutedSound.SetActive(true);
        }
    }

    public void MusicVolumeChange(float sliderValue) {
        audioMixer.SetFloat("musicVolume", sliderValue);
        if(sliderValue == -80) {
            mutedMusic.SetActive(true);
            unmutedMusic.SetActive(false);
        } else {
            mutedMusic.SetActive(false);
            unmutedMusic.SetActive(true);
        }
    }

    public void TutorialButton() {
        Time.timeScale = 1f;
        tutorial.SetActive(false);
        stepSource.Play();
        startSound.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lostScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text endGameScore;
    public int endGameNumber;

    [SerializeField] private AudioSource stepSound;
    [SerializeField] private AudioSource rotateSound;
    [SerializeField] private AudioSource winSound;
    
    private PointsConverter pointsConverter;

    private void Start() {
        pointsConverter = GameObject.FindGameObjectWithTag("Portal").GetComponent<PointsConverter>();
    }

    private void Update() {
        score.text = "Score: " + pointsConverter.score + "/" + endGameNumber;

        // if(endGameNumber == pointsConverter.score) {
        //     winSound.Play();
        // }

        if(player == null) {
            stepSound.Stop();
            rotateSound.Stop();
            endGameScore.text = pointsConverter.score + "/" + endGameNumber;
            lostScreen.SetActive(true);
            endGameScore.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        if(pointsConverter.score == endGameNumber) {
            stepSound.Stop();
            rotateSound.Stop();
            endGameScore.text = pointsConverter.score + "/" + endGameNumber;
            winScreen.SetActive(true);
            endGameScore.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        Restart();
        Exit();
    }

    private void Restart() {
        if(player == null && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Game");
        }
    }

    private void Exit() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

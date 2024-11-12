using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsConverter : MonoBehaviour
{
    [SerializeField] private GameObject[] portals;
    [SerializeField] private int portalsSpawnNumber;
    [SerializeField] private GameObject tailPrefab;
    public int score;

    [SerializeField] private AudioSource portalSound;
    [SerializeField] private AudioClip winSound;

    private SnakeController snakeController;
    private EndGame endGame;

    private void Start() {
        endGame = GameObject.Find("EndGameManager").GetComponent<EndGame>();
        snakeController = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeController>();
    }

    private void Update() {
        if(score == endGame.endGameNumber - 1) {
            portalSound.clip = winSound;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player" && snakeController.segments.Count != 0) {
            for(int i = 0; i <= snakeController.segments.Count - 1; i++) {
                Destroy(snakeController.segments[snakeController.segments.Count - 1].gameObject);
                snakeController.segments.Remove(snakeController.segments[snakeController.segments.Count - 1]);
                score++;
                portalSound.Play();
            }
        }
    }
}

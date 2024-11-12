using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTeleportation : MonoBehaviour
{
    [SerializeField] private GameObject pipe1;
    [SerializeField] private GameObject pipe2;
    [SerializeField] private GameObject pipePoint1;
    [SerializeField] private GameObject pipePoint2;

    private SnakeController snakeController;

    private void Start() {
        snakeController = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeController>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && gameObject == pipe1 || other.gameObject.CompareTag("Shielded") && gameObject == pipe1) {
            other.gameObject.transform.position = pipePoint2.transform.position;
            foreach(var tail in snakeController.segments) {
                tail.position = pipePoint2.transform.position;
            }
        }
        if(other.gameObject.CompareTag("Player") && gameObject == pipe2 || other.gameObject.CompareTag("Shielded") && gameObject == pipe2) {
            other.gameObject.transform.position = pipePoint1.transform.position;
            foreach(var tail in snakeController.segments) {
                tail.position = pipePoint1.transform.position;
            }
        }
    }
}

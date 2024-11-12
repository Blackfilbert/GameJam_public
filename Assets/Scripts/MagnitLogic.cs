using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnitLogic : MonoBehaviour
{
    private SnakeController snakeController;

    private void Start() {
        snakeController = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Point"))
        {
            Destroy(other.gameObject);
            snakeController.CreateSegment();
        }
    }
}

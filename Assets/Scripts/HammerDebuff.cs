using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerDebuff : MonoBehaviour
{
    [SerializeField] private int destroyWall;

    [SerializeField] private AudioSource pickUp;

    private BerserkController berserkController;
    private SnakeController snakeController;

    private void Start() {
        snakeController = GameObject.Find("Player").GetComponent<SnakeController>();
        berserkController = GameObject.FindGameObjectWithTag("Berserk").GetComponent<BerserkController>();

        pickUp = snakeController.buffPickUp;
    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" || other.tag == "Shielded") {
            pickUp.Play();
            Destroy(gameObject);
            berserkController.wallCounter = destroyWall;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagniteActiveBuff : MonoBehaviour
{
    public GameObject magniteIcon;
    private SnakeController snakeController;
    private IconManager iconManager;

    [SerializeField] private AudioSource pickUp;

    private void Start() {
        snakeController = GameObject.Find("Player").GetComponent<SnakeController>();
        iconManager = GameObject.FindGameObjectWithTag("IconManager").GetComponent<IconManager>();
        magniteIcon = iconManager.activeMagniteIcon;

        pickUp = snakeController.buffPickUp;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" || other.tag == "Shielded") {
            BuffPickUp();
        }
    }

    private void BuffPickUp() {
        pickUp.Play();
        snakeController.activeBuff = "Magnite";
        magniteIcon.SetActive(true);
        iconManager.inactiveIcon.SetActive(false);
        Destroy(gameObject);
    }
}

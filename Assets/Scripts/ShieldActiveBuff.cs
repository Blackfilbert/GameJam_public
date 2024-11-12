using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActiveBuff : MonoBehaviour
{
    public GameObject shieldIcon;
    private SnakeController snakeController;
    private IconManager iconManager;

    [SerializeField] private AudioSource pickUp;

    private void Start() {
        snakeController = GameObject.Find("Player").GetComponent<SnakeController>();
        iconManager = GameObject.FindGameObjectWithTag("IconManager").GetComponent<IconManager>();
        shieldIcon = iconManager.activeShieldIcon;

        pickUp = snakeController.buffPickUp;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" || other.tag == "Shielded") {
            BuffPickUp();
        }
    }

    private void BuffPickUp() {
        pickUp.Play();
        snakeController.activeBuff = "Shield";
        shieldIcon.SetActive(true);
        iconManager.inactiveIcon.SetActive(false);
        Destroy(gameObject);
    }
}

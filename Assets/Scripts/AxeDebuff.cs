using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDebuff : MonoBehaviour
{
    [SerializeField] private float debuffDuration;
    private float timeDebuffStart;
    public GameObject berserk;
    [SerializeField] private Vector3 berserkScale;
    private Vector3 defaultBerserkScale = new Vector3(1, 1, 1);

    [SerializeField] private AudioSource pickUp;

    private IconManager iconManager;
    private SnakeController snakeController;

    private void Start() {
        snakeController = GameObject.Find("Player").GetComponent<SnakeController>();
        berserk = GameObject.FindGameObjectWithTag("Berserk");
        iconManager = GameObject.FindGameObjectWithTag("IconManager").GetComponent<IconManager>();

        pickUp = snakeController.buffPickUp;
    }
    
    private void Update() {
        if(Time.time - timeDebuffStart > debuffDuration) {
                berserk.transform.localScale = defaultBerserkScale;

                iconManager.axeActive.SetActive(false);
                iconManager.axeInactive.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" || other.tag == "Shielded") {
            pickUp.Play();
            timeDebuffStart = Time.time;
            berserk.transform.localScale = berserkScale;
            Destroy(gameObject);

            iconManager.axeActive.SetActive(true);
            iconManager.axeInactive.SetActive(false);
        }
    }
}

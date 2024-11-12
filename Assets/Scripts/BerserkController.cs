using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkController : MonoBehaviour
{
    [SerializeField] private float attackRate;
    [SerializeField] private float attackPower;
    [SerializeField] private GameObject stunned;
    [SerializeField] private GameObject moved;
    [SerializeField] private GameObject destroyedWallPrefab;
    [SerializeField] private float rotateSpeed;
    private bool isMove;
    private float lastAttackTime;
    private float stopAttackTime = 2f;
    private float attackTime;
    private Vector2 direction;
    [NonSerialized] public int wallCounter;

    [SerializeField] AudioSource attackSound;
    [SerializeField] AudioSource rotateSound;
    [SerializeField] AudioSource skeletonDieSound;
    [SerializeField] private AudioSource playerDieSound;

    private Rigidbody2D _rb;
    private SnakeController snakeController;
    private IconManager iconManager;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        snakeController = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeController>();
        iconManager = GameObject.FindGameObjectWithTag("IconManager").GetComponent<IconManager>();
        lastAttackTime = Time.time;
    }

    private void Update() {
        if(isMove == true) {
            
            moved.SetActive(true);
            stunned.SetActive(false);
            gameObject.transform.Rotate(0, 0, rotateSpeed);

            
        } else {
            moved.SetActive(false);
            stunned.SetActive(true);
            rotateSound.Play();
        }
        
        if(Time.time - attackTime > stopAttackTime) {
            isMove = false;
            attackTime = Time.time;
            _rb.velocity = new Vector2(0, 0);
            RandomMovementBerserk();
        }

        if(wallCounter > 0) {
            iconManager.hammerActive.SetActive(true);
            iconManager.hammerInactive.SetActive(false);
        } else {
            iconManager.hammerActive.SetActive(false);
            iconManager.hammerInactive.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Point") || other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Tail")) {
            int tailIndex = snakeController.segments.IndexOf(other.gameObject.transform);
            for(int i = tailIndex; i < snakeController.segments.Count - 1; i++) {
                Destroy(snakeController.segments[i].gameObject);
                snakeController.segments.Remove(snakeController.segments[i]);
            }
        }

        if(other.gameObject.CompareTag("Wall") && wallCounter > 0) {
            Instantiate(destroyedWallPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            wallCounter--;
        }

        if(other.gameObject.CompareTag("Point") || other.gameObject.CompareTag("Tail")) {
            skeletonDieSound.Play();
        }

        if(other.gameObject.CompareTag("Player")) {
            playerDieSound.Play();
        }
    }

    private void RandomMovementBerserk() {
        Vector2 randomDirection = new Vector2(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100));
        direction = randomDirection;
        if(Time.time - lastAttackTime > attackRate) {
            isMove = true;
            _rb.AddForce(direction * attackPower);
            lastAttackTime = Time.time;
            attackSound.Play();
        }
    }
}

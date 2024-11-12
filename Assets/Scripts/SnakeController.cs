        using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public List<Transform> segments;
    public string activeBuff;
    private float timeWhenBuffActivated;
    [SerializeField] private float smoothingFactor = 5f;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float buffsDuration;
    [SerializeField] private float tailPartsDistance;
    [SerializeField] private GameObject tailPartPrefab;
    [SerializeField] private GameObject magnite;
    [SerializeField] private GameObject shield;

    [SerializeField] private AudioSource skeletonPickUp;
    public AudioSource buffPickUp;

    private IconManager iconManager;

    private void Start()
    {
        iconManager = GameObject.FindGameObjectWithTag("IconManager").GetComponent<IconManager>();
        segments = new List<Transform>();
    }

    private void Update()
    {
        RotateHeadMovement();
        // MoveTail();
        MoveTailAnother();
        ActivateBuffs();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Point"))
        {
            skeletonPickUp.Play();
            Destroy(other.gameObject);
            CreateSegment();
        }
    }

    private void RotateHeadMovement() {
        float h = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -h );

        transform.position = transform.position + transform.right * moveSpeed * Time.deltaTime;
        magnite.transform.position = transform.position;
    }

    private void MoveTail()
    {
        float sqrDistance = Mathf.Sqrt(tailPartsDistance);
        Vector3 previousPosition = transform.position;

        foreach(var tail in segments) {
            if((tail.position - previousPosition).sqrMagnitude > sqrDistance) {
                Vector3 currentTailPosition = tail.position;
                tail.position = previousPosition;
                previousPosition = currentTailPosition;
                tail.rotation = gameObject.transform.rotation;
            } else {
                break;
            }
        }
    }

    private void MoveTailAnother() {
        float sqrDistance = Mathf.Pow(tailPartsDistance, 2);
        Vector3 previousPosition = transform.position; 
        Quaternion previousRotation = transform.rotation;

        foreach (var tail in segments) { 
            if ((tail.position - previousPosition).sqrMagnitude > sqrDistance) { 
                tail.position = Vector3.Lerp(tail.position, previousPosition, Time.deltaTime * smoothingFactor); 
                tail.rotation = Quaternion.Slerp(tail.rotation, previousRotation, Time.deltaTime * smoothingFactor);
            
                previousPosition = tail.position;
                previousRotation = tail.rotation; 
            } else { 
                break; 
            } 
        } 
    }

    public void CreateSegment()
    {
        GameObject segment = Instantiate(tailPartPrefab, transform.position, Quaternion.identity);
        segments.Add(segment.transform);
    }

    private void ActivateBuffs() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            switch(activeBuff) {
                case "Shield" :
                    iconManager.shieldActive.SetActive(true);
                    iconManager.shieldInactive.SetActive(false);
                    shield.SetActive(true);
                    timeWhenBuffActivated = Time.time;
                    activeBuff = null;
                    foreach(var tail in segments) {
                        tail.tag = "Shielded";
                    }
                    gameObject.tag = "Shielded";
                    tailPartPrefab.tag = "Shielded";
                    break;
                case "Magnite" :
                    iconManager.magniteActive.SetActive(true);
                    iconManager.magniteInactive.SetActive(false);
                    timeWhenBuffActivated = Time.time;
                    activeBuff = null;
                    magnite.SetActive(true);
                    break;
            }
        }
        
        if(Time.time - timeWhenBuffActivated > buffsDuration) {
            foreach(var tail in segments) {
                tail.tag = "Tail";
            }

            if(iconManager.shieldActive == true) {
                iconManager.shieldActive.SetActive(false);
                iconManager.shieldInactive.SetActive(true);
                shield.SetActive(false);
            }
            if(iconManager.magniteActive == true) {
                iconManager.magniteActive.SetActive(false);
                iconManager.magniteInactive.SetActive(true);
            }
            
            gameObject.tag = "Player";
            tailPartPrefab.tag = "Tail";
            magnite.SetActive(false);
        }

        if(activeBuff != "Shield") {
            iconManager.activeShieldIcon.SetActive(false);
            iconManager.inactiveIcon.SetActive(true);
        }
        if(activeBuff != "Magnite") {
            iconManager.activeMagniteIcon.SetActive(false);
            iconManager.inactiveIcon.SetActive(true);
        }
    }
}

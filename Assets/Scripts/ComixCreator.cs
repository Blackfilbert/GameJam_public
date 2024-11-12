using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComixCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> comix;
    private bool isActive = true;
    private int count = 0;
    private float lastFrameTime;
    private float timer = 2f;

    private void Start() {
        lastFrameTime = Time.time;
    }


    private void Update() {
        NextFrame();
        NextFrameByTimer();
    }

    private void NextFrameByTimer() {
        for(int i = count; i < comix.Count; i++) {
            if(isActive == true) {
                lastFrameTime = Time.time;
                comix[i].SetActive(true);
                isActive = false;
            }
        }

        if(Time.time - lastFrameTime > timer) {
            count++;
            isActive = true;
        }

        if(count > comix.Count) {
            SceneManager.LoadScene("Game");
        }
    }

    private void NextFrame() {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            for(int i = count; i < comix.Count; i++) {
                if(isActive == true) {
                    comix[i].SetActive(true);
                    isActive = false;
                }
                
            }
            isActive = true;
            count++;
            if(count > comix.Count) {
                SceneManager.LoadScene("Game");
            }
        }
    }
}

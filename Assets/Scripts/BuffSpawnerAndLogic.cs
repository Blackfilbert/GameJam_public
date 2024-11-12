using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawnerAndLogic : MonoBehaviour
{
    [SerializeField] private float timeBeforeSpawn;
    [SerializeField] private List<GameObject> buffList;
    private GameObject buffPrefab;
    private float lastSpawnTime;

    private void Start() {
        lastSpawnTime = Time.time;
    }

    private void Update() {
        RandomBuff();
        BuffSpawn();
    }

    private void BuffSpawn() {
        if(Time.time - lastSpawnTime > timeBeforeSpawn) {
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            Instantiate(buffPrefab, spawnPosition, Quaternion.identity);
            lastSpawnTime = Time.time;
        }
    }

    private void RandomBuff() {
        buffPrefab = buffList[Random.Range(0, buffList.Count)];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private int minSpawnRange;
    [SerializeField] private int maxSpawnRange;
    private int randomPointsNumber;

    private void Update() {
        if(GameObject.FindGameObjectsWithTag("Point").Length == 0) {
            randomPointsNumber = Random.Range(minSpawnRange, maxSpawnRange);
            for(int i = 0; i < randomPointsNumber; i++) {
                SpawnFood();
            }
            
        }
    }

    void SpawnFood()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        Instantiate(pointPrefab, spawnPosition, Quaternion.identity); 
    }
}

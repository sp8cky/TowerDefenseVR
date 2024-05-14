using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// spawns enemies
public class Spawner : MonoBehaviour {
    public List<GameObject> enemyPrefabs;
    public float spawnInterval = 5f; 

    IEnumerator Start() {
        while (true) {
            SpawnEnemies();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemies() {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject randomEnemyPrefab = enemyPrefabs[randomIndex];
        Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);
    }

}
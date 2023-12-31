using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] enemyPos;
    void Start()
    {
       SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SpawnEnemies()
    //{
    //    for (int i = 0; i < enemyPos.Length; i++)
    //    {
    //        //GameObject spawnedEnemy = Instantiate(enemyPrefabs[0], enemyPos[i].position, enemyPos[i].rotation);
    //        int prefabIndex = i % 2; // 0 for even, 1 for odd
    //        GameObject spawnedEnemy = Instantiate(enemyPrefabs[prefabIndex], enemyPos[i].position, enemyPos[i].rotation);
    //    }
    //}
    public void SpawnEnemies()
    {
        int enemyPrefabIndex = 0; // Start with the first enemy prefab

        for (int i = 0; i < enemyPos.Length; i++)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefabs[enemyPrefabIndex], enemyPos[i].position, enemyPos[i].rotation);

            // Move to the next enemy prefab index
            enemyPrefabIndex = (enemyPrefabIndex + 1) % enemyPrefabs.Length;
        }
    }

}

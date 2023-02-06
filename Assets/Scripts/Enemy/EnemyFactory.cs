using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    public List<EnemyStatus> enemies;
    [SerializeField]
    private GameObject enemyPrefab;
    Vector3 spawnPosition;

    public bool enemiesSpawned = false;

    private void Start()
    {
        int spawnPointX = Random.Range(-10, 0);
        int spawnPointZ = Random.Range(0, 10);
        spawnPosition = new Vector3(spawnPointX, 1, spawnPointZ);
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            CreateEnemy(i);
        }
        enemiesSpawned = true;
    }

    GameObject CreateEnemy(int i)
    {
        var enemyStatus = enemies[i];
        var instance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        instance.GetComponent<MeshRenderer>().material = enemyStatus.material;
        instance.GetComponent<EnemyAction>().speed = enemyStatus.speed;
        return instance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private List<EnemyStatus> enemies;
    [SerializeField]
    private GameObject enemyPrefab;

    public int numberOfEnemies;
}

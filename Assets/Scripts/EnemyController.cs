using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;

    public float speed = 5.0f;
    public int numberOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
    }
}
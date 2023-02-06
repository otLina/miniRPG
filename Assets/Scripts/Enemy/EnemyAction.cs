using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction: MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;

    public float speed;

    void Start()
    {
        player = GameObject.Find("RPG-Character");
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            rb = gameObject.GetComponent<Rigidbody>();
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            rb.AddForce(lookDirection * speed);
        }
    }
}
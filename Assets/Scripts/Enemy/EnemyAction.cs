using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction: MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private GameObject player;

    public float speed = 1.0f;

    void Start()
    {

    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            rb = this.GetComponent<Rigidbody>();
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            rb.AddForce(lookDirection * speed);
        }
    }
}
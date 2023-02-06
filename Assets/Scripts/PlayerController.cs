using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;

    private Vector3 currentPos;
    private float speed = 10.0f;
    private float boundary = 45.0f;

    public GameObject sword;

    public bool hasSword = false;


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerMovement();
    }

    //method to move the player character
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertialInput = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0, vertialInput * speed * Time.deltaTime);

        if(Input.GetKey("up"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            playerAnim.SetBool("isRunning", true);
        }
        else if(Input.GetKey("down"))
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            playerAnim.SetBool("isRunning", true);
        }
        else if (Input.GetKey("right"))
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
            playerAnim.SetBool("isRunning", true);
        }
        else if (Input.GetKey("left"))
        {
            transform.rotation = Quaternion.Euler(0, -90f, 0);
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        Vector3 diff = transform.position - currentPos;
        currentPos = transform.position;
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Debug.Log("sword!");
            Destroy(other.gameObject);
            sword.SetActive(true);
            hasSword = true;

            playerAnim.SetBool("hasSword", true);
        }
    }

    void ConstrainPlayerMovement()
    {
        if (transform.position.x < -boundary)
        {
            transform.position = new Vector3(-boundary, transform.position.y, transform.position.z);
        }
        if (transform.position.x > boundary)
        {
            transform.position = new Vector3(boundary, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -boundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundary);
        }
        if (transform.position.z > boundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundary);
        }
    }
}

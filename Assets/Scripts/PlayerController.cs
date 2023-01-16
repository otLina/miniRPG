using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public NPCController NPCController;
    //[SerializeField]
    //private EnemyController enemyController;
    [SerializeField]
    private ItemController itemController;
    //[SerializeField]
    //private GameObject focalPoint;
    [SerializeField]
    private Animator playerAnim;

    private Vector3 currentPos;
    private float speed = 10.0f;
    private float boundary = 45.0f;
    private Rigidbody playerRb;

    public GameObject sword;
    public GameObject[] enemies;
    public GameObject[] items;
    [SerializeField]
    private GameObject NPC1;
    [SerializeField]
    private GameObject NPC2;

    public bool hasSword = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        items = GameObject.FindGameObjectsWithTag("Item");

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerMovement();
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertialInput = Input.GetAxis("Vertical");

        //transform.Translate(horizontalInput * Time.deltaTime * speed, 0, 0, Space.World);
        //transform.Translate(0, 0, vertialInput * Time.deltaTime * speed, Space.World);

        transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0, vertialInput * speed * Time.deltaTime);

        //if(Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
        //{
        //    playerAnim.SetBool("isRunning", true);
        //}
        //else
        //{
        //    playerAnim.SetBool("isRunning", false);
        //}
        if(Input.GetKey("up"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
            playerAnim.SetBool("isRunning", true);
        }
        else if(Input.GetKey("down"))
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            //transform.position -= new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
            playerAnim.SetBool("isRunning", true);
        }
        else if (Input.GetKey("right"))
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
            //transform.position += new Vector3(0, 0, vertialInput * speed * Time.deltaTime);
            playerAnim.SetBool("isRunning", true);
        }
        else if (Input.GetKey("left"))
        {
            transform.rotation = Quaternion.Euler(0, -90f, 0);
            //transform.position -= new Vector3(0, 0, vertialInput * speed * Time.deltaTime);
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

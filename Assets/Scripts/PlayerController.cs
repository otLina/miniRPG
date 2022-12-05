using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public NPCController NPCController;
    public EnemyController enemyController;
    public ItemController itemController;
    private GameObject focalPoint;

    private float speed = 10.0f;
    private float boundary = 45.0f;
    private Rigidbody playerRb;

    public GameObject swordIndicator;
    public GameObject[] enemies;
    public GameObject[] items;
    public GameObject NPC1;
    public GameObject NPC2;

    private bool battleQuest = false;
    public bool hasSword = false;
    private int defeatEnemy = 0;

    private bool itemQuest = false;
    private int itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        items = GameObject.FindGameObjectsWithTag("Item");
        NPC1 = GameObject.Find("NPC1");
        NPC2 = GameObject.Find("NPC2");

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerMovement();

        swordIndicator.transform.position = transform.position + new Vector3(0.8f, 0.5f, 0);
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertialInput = Input.GetAxis("Vertical");

        transform.Translate(focalPoint.transform.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(focalPoint.transform.forward * vertialInput * Time.deltaTime * speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Destroy(other.gameObject);
            swordIndicator.SetActive(true);
            hasSword = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartQuest(collision);

        //バトルクエスト
        if (battleQuest)
        {
            if(defeatEnemy == 0)
            {
                foreach (GameObject enemy in enemies)
                {
                    enemy.SetActive(true);
                }
                BattleQuest(collision);
            }
            else if (defeatEnemy != enemyController.numberOfEnemies)
            {
                BattleQuest(collision);
            }
            else if (defeatEnemy == enemyController.numberOfEnemies & collision.gameObject == NPC1)
            {
                FinishQuest(collision);
            }
        }

        //アイテムクエスト
        if (itemQuest)
        {
            if(itemCount == 0)
            {
                foreach (GameObject item in items)
                {
                    item.SetActive(true);
                }
                ItemQuest(collision);
            }
            else if(itemCount != itemController.numberOfItems)
            {
                ItemQuest(collision);
            }
            else if(itemCount == itemController.numberOfItems & collision.gameObject == NPC2)
            {
                FinishQuest(collision);
            }
        }
    }

    private void StartQuest(Collision collision)
    {
        if (collision.gameObject == NPC1)
        {
            NPC1.GetComponent<NPCController>().DisplayStartQuestDialog();
            battleQuest = true;
        }
        else if (collision.gameObject == NPC2)
        {
            NPC2.GetComponent<NPCController>().DisplayStartQuestDialog();
            itemQuest = true;
        }
    }

    private void FinishQuest(Collision collision)
    {
        if (collision.gameObject == NPC1)
        {
            NPC1.GetComponent<NPCController>().DisplayFinishQuestDialog();
            battleQuest = false;
        }
        else if (collision.gameObject == NPC2)
        {
            NPC2.GetComponent<NPCController>().DisplayFinishQuestDialog();
            itemQuest = false;
        }
    }

    private void BattleQuest(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy") & hasSword)
        {
            Destroy(col.gameObject);
            defeatEnemy++;
        }
    }

    private void ItemQuest(Collision col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            Destroy(col.gameObject);
            itemCount++;
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

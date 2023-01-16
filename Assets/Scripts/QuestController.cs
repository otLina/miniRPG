using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private EnemyFactory enemyFactory;
    [SerializeField]
    private ItemController itemController;
    public GameObject[] enemies;
    public GameObject[] items;

    [SerializeField]
    private GameObject NPC1;
    [SerializeField]
    private GameObject NPC2;

    private bool battleQuest = false;

    private int defeatEnemy = 0;

    private bool itemQuest = false;
    private int itemCount = 0;

    void Start()
    {
        //playerRb = GetComponent<Rigidbody>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        items = GameObject.FindGameObjectsWithTag("Item");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartQuest(collision);

        //バトルクエスト
        if (battleQuest)
        {
            if (defeatEnemy == 0)
            {
                foreach (GameObject enemy in enemies)
                {
                    enemy.SetActive(true);
                }
                BattleQuest(collision);
            }
            else if (defeatEnemy != enemyFactory.numberOfEnemies)
            {
                BattleQuest(collision);
            }
            else if (defeatEnemy == enemyFactory.numberOfEnemies & collision.gameObject == NPC1)
            {
                FinishQuest(collision);
            }
        }

        //アイテムクエスト
        if (itemQuest)
        {
            if (itemCount == 0)
            {
                foreach (GameObject item in items)
                {
                    item.SetActive(true);
                }
                ItemQuest(collision);
            }
            else if (itemCount != itemController.numberOfItems)
            {
                ItemQuest(collision);
            }
            else if (itemCount == itemController.numberOfItems & collision.gameObject == NPC2)
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

    private void BattleQuest(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") & playerController.hasSword)
        {
            Destroy(collision.gameObject);
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
}

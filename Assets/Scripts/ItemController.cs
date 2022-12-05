using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int numberOfItems;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            numberOfItems = GameObject.FindGameObjectsWithTag("Item").Length;
        }
    }
}

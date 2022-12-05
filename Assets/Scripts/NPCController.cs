using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public GameObject startQuestDialog;
    public GameObject finishQuestDialog;
    private float displayTime = 4.0f;
    float timerDisplay = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        startQuestDialog.SetActive(false);
        finishQuestDialog.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0)
            {
                startQuestDialog.SetActive(false);
                finishQuestDialog.SetActive(false);
            }
        }
    }

    public void DisplayStartQuestDialog()
    {
        timerDisplay = displayTime;
        startQuestDialog.SetActive(true);
    }

    public void DisplayFinishQuestDialog()
    {
        timerDisplay = displayTime;
        finishQuestDialog.SetActive(true);
    }
}

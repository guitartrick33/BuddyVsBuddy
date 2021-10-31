using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTreeOpen : MonoBehaviour
{
    public Canvas TalentTree;
    bool isOpened = false;
    public GameObject notification;
    private GameObject player;
    void Start()
    {
        TalentTree.enabled = true;
        player = GameObject.FindWithTag("Dummy");
        player.GetComponent<FireLaser>().enabled = false;
    }

    public void ShowTalentTree()
    {
        player.GetComponent<FireLaser>().enabled = false;
        TalentTree.enabled = true;
    }

    public void HideTalentTree()
    {
        TalentTree.enabled = false;
        player.GetComponent<FireLaser>().enabled = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if(isOpened == false)
            {
                TalentTree.enabled = true;
                isOpened = true;
            }
            else
            {
                TalentTree.enabled = false;
                isOpened = false;
            }
        }

        if (player.GetComponent<PlayerResources>().currentTalentPoints > 0)
        {
            notification.SetActive(true);
        }
        else
        {
            notification.SetActive(false);
        }
    }
    
    
}

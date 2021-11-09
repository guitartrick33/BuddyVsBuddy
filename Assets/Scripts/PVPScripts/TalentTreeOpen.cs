using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTreeOpen : MonoBehaviour
{
    public List<GameObject> canvases;
    bool isOpened = false;
    public GameObject notification;
    private GameObject player;
    public GameObject currentCanvas;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Dummy");
        if (player.GetComponent<PlayerMovement>().classEnum == ClassEnum.MAGE)
        {
            player.GetComponent<FireLaser>().enabled = false;
        }
        foreach (GameObject canvas in canvases)
        {
            if (canvas.GetComponent<TalentClassType>().classType == player.GetComponent<PlayerMovement>().classEnum)
            {
                currentCanvas = canvas;
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }

    public void ShowTalentTree()
    {
        if (player.GetComponent<PlayerMovement>().classEnum == ClassEnum.MAGE)
        {
            player.GetComponent<FireLaser>().enabled = false;
        }
        currentCanvas.SetActive(true);
    }

    public void HideTalentTree()
    {
        currentCanvas.SetActive(false);
        if (player.GetComponent<PlayerMovement>().classEnum == ClassEnum.MAGE)
        {
            player.GetComponent<FireLaser>().enabled = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if(isOpened == false)
            {
                ShowTalentTree();
                isOpened = true;
            }
            else
            {
                HideTalentTree();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTreeOpen : MonoBehaviour
{
    public Canvas TalentTree;
    bool isOpened = false;
    void Start()
    {
        TalentTree.enabled = true;
    }

    public void ShowTalentTree()
    {
        TalentTree.enabled = true;
    }

    public void HideTalentTree()
    {
        TalentTree.enabled = false;
    }
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.N))
        // {
        //     if(isOpened == false)
        //     {
        //         TalentTree.enabled = true;
        //         isOpened = true;
        //     }
        //     else
        //     {
        //         TalentTree.enabled = false;
        //         isOpened = false;
        //     }
        // }
                    
    }
}

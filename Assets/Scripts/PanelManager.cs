using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels;
    public int selectedPanel;

    private void Awake()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void NextCharacter()
    {
        panels[selectedPanel].SetActive(false);
        selectedPanel = (selectedPanel + 1) % panels.Length;
        panels[selectedPanel].SetActive(true);
    }

    public void PrevCharacter()
    {
        panels[selectedPanel].SetActive(false);
        selectedPanel--;
        if(selectedPanel < 0)
        {
            selectedPanel += panels.Length;
        }
        panels[selectedPanel].SetActive(true);
    }

    public void Close()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}

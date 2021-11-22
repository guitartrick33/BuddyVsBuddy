using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public bool isSelected;
    private LobbyManager lobbyManager;
    public Image mapImage;
    public string levelName;
    public string mapName;

    [SerializeField] public Image border;

    private void Start()
    {
        lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
    }

    public void SelectMap()
    {
        isSelected = true;
        border.enabled = true;
        foreach (GameObject g in lobbyManager.maps)
        {
            if (gameObject != g)
            {
                g.GetComponent<MapManager>().isSelected = false;
                g.GetComponent<MapManager>().mapImage.color = lobbyManager.normalMapColor;
            }
        }
        mapImage.color = lobbyManager.highlightMapColor;
    }

    private void Update()
    {
        if (isSelected)
        {
            border.enabled = true;
        }
        else
        {
            border.enabled = false;
        }
    }
}

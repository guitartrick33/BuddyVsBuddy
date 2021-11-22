using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNickName : MonoBehaviour
{
    public string winnerNick;
    public string levelName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        winnerNick = string.Empty;
        levelName = string.Empty;
    }
}

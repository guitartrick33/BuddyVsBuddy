using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNickName : MonoBehaviour
{
    public string winnerNick;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        winnerNick = string.Empty;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePrefabManager : MonoBehaviour
{
    public Text playerNameText;
    public Text scoreCountText;

    public void SetPlayerText(string text)
    {
        playerNameText.text = text;
    }

    public void SetScoreText(string text)
    {
        scoreCountText.text = text;
    }
}

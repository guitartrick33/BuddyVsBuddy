using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{
    public List<GameObject> unlockedTalents;

    private void Start()
    {
        unlockedTalents.Clear();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    public List<GameObject> spellObjects;
    private GameObject player;
    private GameObject currentSpellObject;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Dummy");
        foreach (GameObject spellObject in spellObjects)
        {
            if (spellObject.GetComponent<TalentClassType>().classType == player.GetComponent<PlayerMovement>().classEnum)
            {
                currentSpellObject = spellObject;
                currentSpellObject.SetActive(true);
            }
            else
            {
                spellObject.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterLimit : MonoBehaviour
{
    public InputField mainInputField;
    public int maxCharacters = 12;
    void Start()
    {
        mainInputField.characterLimit = maxCharacters;
    }
}

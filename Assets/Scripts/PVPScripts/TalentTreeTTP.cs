using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TalentTreeTTP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;
    public Text tooltipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.text = message;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipText.text = "Talent Tree";  
    }
}

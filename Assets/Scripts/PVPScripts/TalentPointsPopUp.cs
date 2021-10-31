using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentPointsPopUp : MonoBehaviour
{
    private PlayerResources pr;

    public Image talentPopup;

    public Text talentPopupText;

    public Text talentPopUpDialogue;

    private float dialogueTimer;

    private bool isActive;
    void Start()
    {
        isActive = false;
        dialogueTimer = 5;
        talentPopUpDialogue.text = "";
        talentPopup.enabled = false;
        pr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
    }
    void Update()
    {
        if (pr.currentTalentPoints >= 1)
        {
            talentPopup.enabled = true;
            talentPopupText.text = $"Talent Points to spend: {pr.currentTalentPoints}";
        }
        else
        {
            talentPopup.enabled = false;
            talentPopupText.text = "";
        }

    }
}

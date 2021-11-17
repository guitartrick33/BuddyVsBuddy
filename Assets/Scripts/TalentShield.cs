using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentShield : MonoBehaviour
{
    private GameObject myPlayer;
    private PlayerResources pr;
    private GameObject gameManager;
    private TalentManager tm;
    private Shield shield;
    
    public TalentType type;
    public TalentTier tier;
    public int cost;
    public bool isUnlocked;
    
    private Text ttpErrors;
    private float textTimer;
    private float textTimerDuration = 2f;
    private bool textTimerBool = false;
    public Image unlockedOverlay;

    private GameObject prevTalent;
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Dummy");
        pr = myPlayer.GetComponent<PlayerResources>();
        shield = myPlayer.GetComponent<Shield>();
        isUnlocked = false;
        gameManager = GameObject.Find("GameManager");
        tm = gameManager.GetComponent<TalentManager>();
        ttpErrors = GameObject.FindGameObjectWithTag("ErrorTalentText").GetComponent<Text>();
        textTimer = textTimerDuration;
        prevTalent = null;
        unlockedOverlay.fillAmount = 1;
    }

    public void UnlockTalent()
    {
        //TIER1
        if (tier == TalentTier.TIER1 && type == TalentType.SHIELD)
        {
            if (isUnlocked == false && pr.currentTalentPoints >= cost)
            {
                tm.unlockedTalents.Add(gameObject);
                shield.shieldDuration += 0.25f;
                shield.shieldDurationTimer = shield.shieldDuration;
                isUnlocked = true;
                pr.currentTalentPoints -= cost;
                unlockedOverlay.fillAmount = 0;
                
            }     
            else if (pr.currentTalentPoints <= cost && !isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Not enough talent points!";
            }
            else if (isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Already Learned!";
            }
        }
        
        //TIER2
        else if (tier == TalentTier.TIER2 && type == TalentType.SHIELD)
        {
            foreach (GameObject g in tm.unlockedTalents)
            {
                if (g.GetComponent<TalentShield>() != null)
                {
                    if (g.GetComponent<TalentShield>().tier == TalentTier.TIER1 &&
                        g.GetComponent<TalentShield>().type == TalentType.SHIELD)
                    {
                        prevTalent = g;
                    }  
                }
            }
            if (!prevTalent.GetComponent<TalentShield>().isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Learn the Tier 1 first!";
            }
            if (isUnlocked == false && pr.currentTalentPoints >= cost && prevTalent.GetComponent<TalentShield>().isUnlocked)
            {
                tm.unlockedTalents.Add(gameObject);
                shield.shieldDuration += 0.25f;
                shield.shieldDurationTimer = shield.shieldDuration;
                isUnlocked = true;
                pr.currentTalentPoints -= cost;
                unlockedOverlay.fillAmount = 0;
            }
            else if (pr.currentTalentPoints < cost && !isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Not enough talent points!";
            }
            else if (isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Already Learned!";
            }
        }
        
        //TIER3
        else if (tier == TalentTier.TIER3 && type == TalentType.SHIELD)
        {
            foreach (GameObject g in tm.unlockedTalents)
            {
                if (g.GetComponent<TalentShield>() != null)
                {
                    if (g.GetComponent<TalentShield>().tier == TalentTier.TIER2 &&
                        g.GetComponent<TalentShield>().type == TalentType.SHIELD)
                    {
                        prevTalent = g;
                    }  
                }
            }
            if (!prevTalent.GetComponent<TalentShield>().isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Learn the Tier 1 first!";
            }
            if (isUnlocked == false && pr.currentTalentPoints >= cost && prevTalent.GetComponent<TalentShield>().isUnlocked)
            {
                shield.shieldDuration += 0.25f;
                shield.shieldDurationTimer = shield.shieldDuration;
                isUnlocked = true;
                pr.currentTalentPoints -= cost;
                unlockedOverlay.fillAmount = 0;
            }
            else if (pr.currentTalentPoints < cost && !isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Not enough talent points!";
            }
            else if (isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Already Learned!";
            }
        }
    }
    void FixedUpdate()
    {
        if (textTimerBool)
        {
            textTimer -= Time.deltaTime;
            if (textTimer <= 0)
            {
                ttpErrors.text = "";
                textTimerBool = false;
                textTimer = textTimerDuration;
            }
        }
    }
}

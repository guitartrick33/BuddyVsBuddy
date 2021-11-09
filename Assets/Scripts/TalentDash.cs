using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentDash : MonoBehaviour
{
    private GameObject myPlayer;
    private PlayerResources pr;
    private GameObject gameManager;
    private TalentManager tm;
    private Dash dash;
    
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
        dash = myPlayer.GetComponent<Dash>();
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
        if (tier == TalentTier.TIER1 && type == TalentType.DASH)
        {
            if (isUnlocked == false && pr.currentTalentPoints >= cost)
            {
                tm.unlockedTalents.Add(gameObject);
                dash.cooldown--;
                dash.cooldownTimer = dash.cooldown;
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
        else if (tier == TalentTier.TIER2 && type == TalentType.DASH)
        {
            foreach (GameObject g in tm.unlockedTalents)
            {
                if (g.GetComponent<TalentDash>() != null)
                {
                    if (g.GetComponent<TalentDash>().tier == TalentTier.TIER1 &&
                        g.GetComponent<TalentDash>().type == TalentType.DASH)
                    {
                        prevTalent = g;
                    }  
                }
            }
            if (!prevTalent.GetComponent<TalentDash>().isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Learn the Tier 1 first!";
            }
            if (isUnlocked == false && pr.currentTalentPoints >= cost && prevTalent.GetComponent<TalentDash>().isUnlocked)
            {
                tm.unlockedTalents.Add(gameObject);
                dash.cooldown--;
                dash.cooldownTimer = dash.cooldown;
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
        else if (tier == TalentTier.TIER3 && type == TalentType.DASH)
        {
            foreach (GameObject g in tm.unlockedTalents)
            {
                if (g.GetComponent<TalentDash>() != null)
                {
                    if (g.GetComponent<TalentDash>().tier == TalentTier.TIER2 &&
                        g.GetComponent<TalentDash>().type == TalentType.DASH)
                    {
                        prevTalent = g;
                    }  
                }
            }
            if (!prevTalent.GetComponent<TalentDash>().isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Learn the Tier 1 first!";
            }
            if (isUnlocked == false && pr.currentTalentPoints >= cost && prevTalent.GetComponent<TalentDash>().isUnlocked)
            {
                tm.unlockedTalents.Add(gameObject);
                dash.cooldown--;
                dash.cooldownTimer = dash.cooldown;
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentHealth1 : MonoBehaviour
{
    private GameObject myPlayer;
    private PlayerResources resources;
    private GameObject gameManager;
    private TalentManager tm;
    
    public TalentType type;
    public TalentTier tier;
    public int cost;
    public bool isUnlocked;
    public float healthAmount;
    
    private Text ttpErrors;
    private float textTimer;
    private float textTimerDuration = 2f;
    private bool textTimerBool = false;
    public Image unlockedOverlay;

    private GameObject prevTalent;
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Dummy");
        resources = myPlayer.GetComponent<PlayerResources>();
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
        if (tier == TalentTier.TIER1 && type == TalentType.HEALTH)
        {
            if (isUnlocked == false && resources.currentTalentPoints >= cost)
            {
                tm.unlockedTalents.Add(gameObject);
                resources.IncreaseHealth(healthAmount);
                resources.currentHealth = resources.maxHealth;
                isUnlocked = true;
                resources.currentTalentPoints -= cost;
                unlockedOverlay.fillAmount = 0;
                
            }     
            else if (resources.currentTalentPoints <= 0 && !isUnlocked)
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
        else if (tier == TalentTier.TIER2 && type == TalentType.HEALTH)
        {
            foreach (GameObject g in tm.unlockedTalents)
            {
                if (g.GetComponent<TalentHealth1>() != null)
                {
                    if (g.GetComponent<TalentHealth1>().tier == TalentTier.TIER1 &&
                        g.GetComponent<TalentHealth1>().type == TalentType.HEALTH)
                    {
                        prevTalent = g;
                    }  
                }
            }
            if (!prevTalent.GetComponent<TalentHealth1>().isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Learn the Tier 1 first!";
            }
            if (isUnlocked == false && resources.currentTalentPoints >= cost && prevTalent.GetComponent<TalentHealth1>().isUnlocked)
            {
                tm.unlockedTalents.Add(gameObject);
                resources.IncreaseHealth(healthAmount);
                resources.currentHealth = resources.maxHealth;
                isUnlocked = true;
                resources.currentTalentPoints -= cost;
                unlockedOverlay.fillAmount = 0;
            }
            else if (resources.currentTalentPoints < cost && !isUnlocked)
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
        else if (tier == TalentTier.TIER3 && type == TalentType.HEALTH)
        {
            foreach (GameObject g in tm.unlockedTalents)
            {
                if (g.GetComponent<TalentHealth1>() != null)
                {
                    if (g.GetComponent<TalentHealth1>().tier == TalentTier.TIER2 &&
                        g.GetComponent<TalentHealth1>().type == TalentType.HEALTH)
                    {
                        prevTalent = g;
                    }  
                }
            }
            if (!prevTalent.GetComponent<TalentHealth1>().isUnlocked)
            {
                textTimerBool = true;
                ttpErrors.text = "Learn the Tier 1 first!";
            }
            if (isUnlocked == false && resources.currentTalentPoints >= cost && prevTalent.GetComponent<TalentHealth1>().isUnlocked)
            {
                tm.unlockedTalents.Add(gameObject);
                resources.IncreaseHealth(healthAmount);
                resources.currentHealth = resources.maxHealth;
                isUnlocked = true;
                resources.currentTalentPoints -= cost;
                unlockedOverlay.fillAmount = 0;
            }
            else if (resources.currentTalentPoints < cost && !isUnlocked)
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
    
    // private GameObject player;
    // private PlayerResources pr;
    // private GameObject gameManager;
    // private TalentManager tm;
    //
    // public TalentType type;
    // public TalentTier tier;
    // public int cost;
    // public bool isUnlocked;
    // public float dmgIncrease;
    //
    // private Text ttpErrors;
    // private float textTimer;
    // private float textTimerDuration = 2f;
    // private bool textTimerBool = false;
    // public Image unlockedOverlay;
    //
    // private GameObject prevTalent;
    // private Fireball fireball;
    // void Start()
    // {
    //     player = GameObject.FindGameObjectWithTag("Dummy");
    //     pr = player.GetComponent<PlayerResources>();
    //     isUnlocked = false;
    //     gameManager = GameObject.Find("GameManager");
    //     tm = gameManager.GetComponent<TalentManager>();
    //     ttpErrors = GameObject.FindGameObjectWithTag("ErrorTalentText").GetComponent<Text>();
    //     textTimer = textTimerDuration;
    //     prevTalent = null;
    //     unlockedOverlay.fillAmount = 1;
    //     fireball = GameObject.FindGameObjectWithTag("Dummy").GetComponent<Fireball>();
    //
    // }
    //
    // public void UnlockTalent()
    // {
    //     //TIER1
    //     if (tier == TalentTier.TIER1 && type == TalentType.FIREBALLDMG)
    //     {
    //         if (isUnlocked == false && pr.currentTalentPoints >= cost)
    //         {
    //             tm.unlockedTalents.Add(gameObject);
    //             fireball.damage += dmgIncrease;
    //             isUnlocked = true;
    //             pr.currentTalentPoints -= cost;
    //             unlockedOverlay.fillAmount = 0;
    //             
    //         }     
    //         else if (pr.currentTalentPoints <= 0 && !isUnlocked)
    //         {
    //             textTimerBool = true;
    //             ttpErrors.text = "Not enough talent points!";
    //         }
    //         else if (isUnlocked)
    //         {
    //             textTimerBool = true;
    //             ttpErrors.text = "Already Learned!";
    //         }
    //     }
    //     
    //     //TIER2
    //     else if (tier == TalentTier.TIER2 && type == TalentType.FIREBALLDMG)
    //     {
    //         foreach (GameObject g in tm.unlockedTalents)
    //         {
    //             if (g.GetComponent<TalentFireBallDamage>() != null)
    //             {
    //                 if (g.GetComponent<TalentFireBallDamage>().tier == TalentTier.TIER1 &&
    //                     g.GetComponent<TalentFireBallDamage>().type == TalentType.FIREBALLDMG)
    //                 {
    //                     prevTalent = g;
    //                 }  
    //             }
    //         }
    //         if (!prevTalent.GetComponent<TalentFireBallDamage>().isUnlocked)
    //         {
    //             textTimerBool = true;
    //             ttpErrors.text = "Learn the Tier 1 first!";
    //         }
    //         if (isUnlocked == false && pr.currentTalentPoints >= cost && prevTalent.GetComponent<TalentFireBallDamage>().isUnlocked)
    //         {
    //             tm.unlockedTalents.Add(gameObject);
    //             fireball.damage += dmgIncrease;
    //             isUnlocked = true;
    //             pr.currentTalentPoints -= cost;
    //             unlockedOverlay.fillAmount = 0;
    //         }
    //         else if (pr.currentTalentPoints < cost && !isUnlocked)
    //         {
    //             textTimerBool = true;
    //             ttpErrors.text = "Not enough talent points!";
    //         }
    //         else if (isUnlocked)
    //         {
    //             textTimerBool = true;
    //             ttpErrors.text = "Already Learned!";
    //         }
    //     }
    //     
    //     //TIER3
    //     else if (tier == TalentTier.TIER3 && type == TalentType.FIREBALLDMG)
    //     {
    //         foreach (GameObject g in tm.unlockedTalents)
    //         {
    //             if (g.GetComponent<TalentFireBallDamage>() != null)
    //             {
    //                 if (g.GetComponent<TalentFireBallDamage>().tier == TalentTier.TIER2 &&
    //                     g.GetComponent<TalentFireBallDamage>().type == TalentType.FIREBALLDMG)
    //                 {
    //                     prevTalent = g;
    //                 }  
    //             }
    //         }
    //         if (!prevTalent.GetComponent<TalentFireBallDamage>().isUnlocked)
    //         {
    //             textTimerBool = true;
    //             ttpErrors.text = "Learn the Tier 1 first!";
    //         }
    //         if (isUnlocked == false && pr.currentTalentPoints >= cost && prevTalent.GetComponent<TalentFireBallDamage>().isUnlocked)
    //         {
    //             tm.unlockedTalents.Add(gameObject);
    //             fireball.damage += dmgIncrease;
    //             isUnlocked = true;
    //             pr.currentTalentPoints -= cost;
    //             unlockedOverlay.fillAmount = 0;
    //         }
    //         else if (pr.currentTalentPoints < cost && !isUnlocked)
    //         {
    //             textTimerBool = true;
    //             ttpErrors.text = "Not enough talent points!";
    //         }
    //         else if (isUnlocked)
    //         {
    //             textTimerBool = true;
    //             ttpErrors.text = "Already Learned!";
    //         }
    //     }
    //    
    // }
       
    }
}

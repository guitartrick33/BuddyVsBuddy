using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TalentTreeUnlock : MonoBehaviour
{
    private PlayerResources pr;
    private Meteor meteor;
    private Incinerate incinerate;

    public Text ttpErrors;
    private float textTimer;
    private float textTimerDuration = 2f;
    private bool textTimerBool = false;
    public Fireball fireball;
    private PlayerMovement pm;

    public bool TT1IsUnlocked = false;
    public bool TT2IsUnlocked = false;
    public bool TT3IsUnlocked = false;
    public bool TT4IsUnlocked = false;
    public bool TT5IsUnlocked = false;
    public bool TT6IsUnlocked = false;
    public bool TT11IsUnlocked = false;
    public bool TT21IsUnlocked = false;
    public bool TT31IsUnlocked = false;
    public bool TT41IsUnlocked = false;
    public bool TT51IsUnlocked = false;
    public bool TT61IsUnlocked = false;
    public bool TT111IsUnlocked = false;
    public bool TT211IsUnlocked = false;
    public bool TT311IsUnlocked = false;
    public bool TT411IsUnlocked = false;
    public bool TT511IsUnlocked = false;
    public bool TT611IsUnlocked = false;

    void Start()
    {
        pr = GameObject.FindGameObjectWithTag("Dummy").GetComponent<PlayerResources>();
        pm = GameObject.FindGameObjectWithTag("Dummy").GetComponent<PlayerMovement>();
        fireball = GameObject.FindGameObjectWithTag("Dummy").GetComponent<Fireball>();
        meteor = GameObject.FindGameObjectWithTag("Dummy").GetComponent<Meteor>();
        incinerate = GameObject.FindGameObjectWithTag("Dummy").GetComponent<Incinerate>();
        ttpErrors = GameObject.FindGameObjectWithTag("ErrorTalentText").GetComponent<Text>();
        textTimer = textTimerDuration;
    }

    public void UnlockTTHealth1()
    {
        if (TT1IsUnlocked == false && pr.currentTalentPoints > 0)
        {
            pr.IncreaseHealth(10f);
            TT1IsUnlocked = true;
            pr.currentTalentPoints--;
        }     
        else if (pr.currentTalentPoints <= 0 && !TT1IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT1IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTHealth2()
    {
        if (!TT1IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 1 first!";
        }
        if (TT11IsUnlocked == false && pr.currentTalentPoints > 1 && TT1IsUnlocked)
        {
            pr.IncreaseHealth(15f);
            pr.currentHealth = pr.maxHealth;
            TT11IsUnlocked = true;
            pr.currentTalentPoints -= 2;
        }
        else if (pr.currentTalentPoints <= 1 && !TT11IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT11IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    
    public void UnlockTTHealth3()
    {
        if (!TT11IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 2 first!";
        }
        if (TT111IsUnlocked == false && pr.currentTalentPoints > 2 && TT11IsUnlocked)
        {
            pr.IncreaseHealth(25f);
            TT111IsUnlocked = true;
            pr.currentTalentPoints -= 3;
        }
        else if (pr.currentTalentPoints <= 2 && !TT111IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT111IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTDamage1()
    {
        if (TT2IsUnlocked == false && pr.currentTalentPoints > 0)
        {
            TT2IsUnlocked = true;
            fireball.damage += 2;
            pr.currentTalentPoints--;
        }
        else if (pr.currentTalentPoints <= 0 && !TT2IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT2IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTDamage2()
    {
        if (!TT2IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 1 first!";
        }
        if (TT21IsUnlocked == false && pr.currentTalentPoints > 1 && TT2IsUnlocked)
        {
            TT21IsUnlocked = true;
            fireball.damage += 2;
            pr.currentTalentPoints -= 2;
        }
        else if (pr.currentTalentPoints <= 1 && !TT21IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT21IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    
    public void UnlockTTDamage3()
    {
        if (!TT21IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 1 first!";
        }
        if (TT211IsUnlocked == false && pr.currentTalentPoints > 2 && TT21IsUnlocked)
        {
            TT211IsUnlocked = true;
            fireball.damage += 2;
            pr.currentTalentPoints -= 3;
        }
        else if (pr.currentTalentPoints <= 2 && !TT211IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT211IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTCooldown1()
    {
        if (meteor.isOnCd)
        {
            textTimerBool = true;
            ttpErrors.text = "Wait for Meteor CD!";
        }
        if (TT3IsUnlocked == false && pr.currentTalentPoints > 0 && meteor.cooldownTime >= 12 && !meteor.isOnCd)
        {
            meteor.cooldownTime -= 2;
            TT3IsUnlocked = true;
            pr.currentTalentPoints--;
        }
        else if (pr.currentTalentPoints <= 0 && !TT3IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT3IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTCooldown2()
    {
        if (!TT3IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 1 first!";
        }
        if (TT31IsUnlocked == false && pr.currentTalentPoints > 1 && TT3IsUnlocked && !meteor.isOnCd)
        {
            meteor.cooldownTime -= 3;
            TT31IsUnlocked = true;
            pr.currentTalentPoints -= 2;
        }
        else if (pr.currentTalentPoints <= 1 && !TT31IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT31IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    
    public void UnlockTTCooldown3()
    {
        if (!TT31IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 2 first!";
        }
        if (TT311IsUnlocked == false && pr.currentTalentPoints > 2 && TT31IsUnlocked && !meteor.isOnCd)
        {
            meteor.cooldownTime -= 5;
            TT311IsUnlocked = true;
            pr.currentTalentPoints -= 3;
        }
        else if (pr.currentTalentPoints <= 2 && !TT311IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT311IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTExperience1()
    {
        if (TT4IsUnlocked == false && pr.currentTalentPoints > 0)
        {
            pm.extraJumpValue += 1;
            textTimerBool = true;
            ttpErrors.text = "";
            TT4IsUnlocked = true;
            pr.currentTalentPoints--;
        }
        else if (pr.currentTalentPoints <= 0 && !TT4IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT4IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTExperience2()
    {
        if (!TT4IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 1 first!";
        }
        if (TT41IsUnlocked == false && pr.currentTalentPoints > 1 && TT4IsUnlocked)
        {
            pm.extraJumpValue += 1;
            textTimerBool = true;
            ttpErrors.text = "";
            TT41IsUnlocked = true;
            pr.currentTalentPoints -= 2;
        }
        else if (pr.currentTalentPoints <= 2 && !TT41IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT41IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    
    public void UnlockTTExperience3()
    {
        if (!TT41IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 2 first!";
        }
        if (TT411IsUnlocked == false && pr.currentTalentPoints > 2 && TT41IsUnlocked)
        {
            pm.extraJumpValue += 1;
            textTimerBool = true;
            ttpErrors.text = "";
            TT411IsUnlocked = true;
            pr.currentTalentPoints -= 3;
        }
        else if (pr.currentTalentPoints <= 2 && !TT411IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT411IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTSpell1()
    {
        if (TT5IsUnlocked == false && pr.currentTalentPoints > 0)
        {
            incinerate.incinerateDuration += 3;
            TT5IsUnlocked = true;
            pr.currentTalentPoints--;
        }
        else if (pr.currentTalentPoints <= 0 && !TT5IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT5IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTSpell2()
    {
        if (!TT5IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 1 first!";
        }
        if (TT51IsUnlocked == false && pr.currentTalentPoints > 1 && TT5IsUnlocked)
        {
            incinerate.incinerateDuration += 3;
            TT51IsUnlocked = true;
            pr.currentTalentPoints -= 2;
        }
        else if (pr.currentTalentPoints <= 1 && !TT51IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT51IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    
    public void UnlockTTSpell3()
    {
        if (!TT51IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 2 first!";
        }
        if (TT511IsUnlocked == false && pr.currentTalentPoints > 2 && TT51IsUnlocked)
        {
            incinerate.cooldownTime -= 10;
            TT511IsUnlocked = true;
            pr.currentTalentPoints -= 3;
        }
        else if (pr.currentTalentPoints <= 2 && !TT511IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT511IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    public void UnlockTTFireBlast1()
    {
        if (TT6IsUnlocked == false && pr.currentTalentPoints > 0)
        {
            pm.speed += 0.5f;
            pm.jumpForce += 0.5f;
            TT6IsUnlocked = true;
            pr.currentTalentPoints--;
        }
        else if (pr.currentTalentPoints <= 0 && !TT6IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT6IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    
    public void UnlockTTFireBlast2()
    {
        if (!TT6IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 1 first!";
        }
        if (TT61IsUnlocked == false && pr.currentTalentPoints > 1 && TT6IsUnlocked)
        {
            pm.speed += 0.5f;
            pm.jumpForce += 0.5f;
            TT61IsUnlocked = true;
            pr.currentTalentPoints -= 2;
        }
        else if (pr.currentTalentPoints <= 1 && !TT61IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT61IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
        }
    }
    
    public void UnlockTTFireBlast3()
    {
        if (!TT61IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Learn the Tier 2 first!";
        }
        if (TT611IsUnlocked == false && pr.currentTalentPoints > 1 && TT61IsUnlocked)
        {
            pm.speed += 0.5f;
            pm.jumpForce += 0.5f;
            TT611IsUnlocked = true;
            pr.currentTalentPoints -= 3;
        }
        else if (pr.currentTalentPoints <= 1 && !TT611IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Not enough talent points!";
        }
        else if (TT611IsUnlocked)
        {
            textTimerBool = true;
            ttpErrors.text = "Already Learned!";
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

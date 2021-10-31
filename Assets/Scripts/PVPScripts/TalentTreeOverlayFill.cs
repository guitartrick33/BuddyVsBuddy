using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTreeOverlayFill : MonoBehaviour
{
    private TalentTreeUnlock ttu;
    private PlayerResources pr;
    public Image unlockedOverlay1;
    public Image unlockedOverlay2;
    public Image unlockedOverlay3;
    public Image unlockedOverlay4;
    public Image unlockedOverlay5;
    public Image unlockedOverlay6;
    public Image unlockedOverlay7;
    public Image unlockedOverlay8;
    public Image unlockedOverlay9;
    public Image unlockedOverlay10;
    public Image unlockedOverlay11;
    public Image unlockedOverlay12;
    public Image unlockedOverlay13;
    public Image unlockedOverlay14;
    public Image unlockedOverlay15;
    public Image unlockedOverlay16;
    public Image unlockedOverlay17;
    public Image unlockedOverlay18;
    

    void Start()
    {
        pr = GameObject.FindGameObjectWithTag("Dummy").GetComponent<PlayerResources>();
        ttu = gameObject.GetComponent<TalentTreeUnlock>();
        unlockedOverlay1.fillAmount = 1;
        unlockedOverlay2.fillAmount = 1;
        unlockedOverlay3.fillAmount = 1;
        unlockedOverlay4.fillAmount = 1;
        unlockedOverlay5.fillAmount = 1;
        unlockedOverlay6.fillAmount = 1;
        unlockedOverlay7.fillAmount = 1;
        unlockedOverlay8.fillAmount = 1;
        unlockedOverlay9.fillAmount = 1;
        unlockedOverlay10.fillAmount = 1;
        unlockedOverlay11.fillAmount = 1;
        unlockedOverlay12.fillAmount = 1;
        unlockedOverlay13.fillAmount = 1;
        unlockedOverlay14.fillAmount = 1;
        unlockedOverlay15.fillAmount = 1;
        unlockedOverlay16.fillAmount = 1;
        unlockedOverlay17.fillAmount = 1;
        unlockedOverlay18.fillAmount = 1;
    }
    void Update()
    {
        if (ttu.TT1IsUnlocked)
        {
            unlockedOverlay1.fillAmount = 0;
        }
        if (ttu.TT11IsUnlocked)
        {
            unlockedOverlay2.fillAmount = 0;
        }
        if (ttu.TT2IsUnlocked)
        {
            unlockedOverlay3.fillAmount = 0;
        }
        if (ttu.TT21IsUnlocked)
        {
            unlockedOverlay4.fillAmount = 0;
        }
        if (ttu.TT3IsUnlocked)
        {
            unlockedOverlay5.fillAmount = 0;
        }
        if (ttu.TT31IsUnlocked)
        {
            unlockedOverlay6.fillAmount = 0;
        }
        if (ttu.TT4IsUnlocked)
        {
            unlockedOverlay7.fillAmount = 0;
        }
        if (ttu.TT41IsUnlocked)
        {
            unlockedOverlay8.fillAmount = 0;
        }
        if (ttu.TT5IsUnlocked)
        {
            unlockedOverlay9.fillAmount = 0;
        }
        if (ttu.TT51IsUnlocked)
        {
            unlockedOverlay10.fillAmount = 0;
        }
        if (ttu.TT111IsUnlocked)
        {
            unlockedOverlay11.fillAmount = 0;
        }
        if (ttu.TT211IsUnlocked)
        {
            unlockedOverlay12.fillAmount = 0;
        }
        if (ttu.TT311IsUnlocked)
        {
            unlockedOverlay13.fillAmount = 0;
        }
        if (ttu.TT411IsUnlocked)
        {
            unlockedOverlay14.fillAmount = 0;
        }
        if (ttu.TT511IsUnlocked)
        {
            unlockedOverlay15.fillAmount = 0;
        }
        if (ttu.TT6IsUnlocked)
        {
            unlockedOverlay16.fillAmount = 0;
        }
        if (ttu.TT61IsUnlocked)
        {
            unlockedOverlay17.fillAmount = 0;
        }
        if (ttu.TT611IsUnlocked)
        {
            unlockedOverlay18.fillAmount = 0;
        }
    }
}

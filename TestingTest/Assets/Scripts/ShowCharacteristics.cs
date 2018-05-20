using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class ShowCharacteristics : MonoBehaviour {

    private bool isActive = false;

    TMP_Text powerCntrTxt;
    TMP_Text agilityCntrTxt;
    TMP_Text intelligenceCntrTxt;
    TMP_Text healthCntrTxt;
    TMP_Text damageCntrTxt;
    TMP_Text evasionCntrTxt;
    TMP_Text freePointsCntrTxt;

    private void Awake()
    {
        powerCntrTxt = GameObject.Find("PowerCntrTxt").GetComponent<TMP_Text>();
        agilityCntrTxt = GameObject.Find("AgilityCntrTxt").GetComponent<TMP_Text>();
        intelligenceCntrTxt = GameObject.Find("intelligenceCntrTxt").GetComponent<TMP_Text>();
        healthCntrTxt = GameObject.Find("HealthCntrTxt").GetComponent<TMP_Text>();
        damageCntrTxt = GameObject.Find("DamageCntrTxt").GetComponent<TMP_Text>();
        evasionCntrTxt = GameObject.Find("EvasionCntrTxt").GetComponent<TMP_Text>();
        freePointsCntrTxt = GameObject.Find("FreePointsCntrTxt").GetComponent<TMP_Text>();
    }

    public void ToogleCharsMenu()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
        if (isActive)
            LoadChars();
    }

    private void LoadChars()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(go.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                powerCntrTxt.text = go.GetComponent<HeroCharacteristics>().power.ToString();
                agilityCntrTxt.text = go.GetComponent<HeroCharacteristics>().agility.ToString();
                intelligenceCntrTxt.text = go.GetComponent<HeroCharacteristics>().inteintelligence.ToString();
                healthCntrTxt.text = go.GetComponent<HeroCharacteristics>().GetHealth().ToString();
                damageCntrTxt.text = go.GetComponent<HeroCharacteristics>().GetDamage().ToString();
                evasionCntrTxt.text = go.GetComponent<HeroCharacteristics>().GetEvasion().ToString();
                freePointsCntrTxt.text = go.GetComponent<HeroCharacteristics>().GetFreePoints().ToString();
            }
        }
    }

}

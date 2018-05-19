using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class HeroChoice : MonoBehaviour {

	public GameObject[] heroes;
	public string[] heroesNames;
	private short curnum = 0;

    TMP_Text powerCntrTxt;
    TMP_Text agilityCntrTxt;
    TMP_Text intelligenceCntrTxt;
    TMP_Text healthCntrTxt;
    TMP_Text damageCntrTxt;
    TMP_Text evasionCntrTxt;
    TMP_Text freePointsCntrTxt;

    int powerCntr;
    int agilityCntr;
    int intelligenceCntr;
    int healthCntrerCntr;
    double damageCntr;
    float evasionCntr;

    int freePoints;

    void Start()
	{
        powerCntrTxt = GameObject.Find("PowerCntrTxt").GetComponent<TMP_Text>();
        agilityCntrTxt = GameObject.Find("AgilityCntrTxt").GetComponent<TMP_Text>();
        intelligenceCntrTxt = GameObject.Find("intelligenceCntrTxt").GetComponent<TMP_Text>();
        healthCntrTxt = GameObject.Find("HealthCntrTxt").GetComponent<TMP_Text>();
        damageCntrTxt = GameObject.Find("DamageCntrTxt").GetComponent<TMP_Text>();
        evasionCntrTxt = GameObject.Find("EvasionCntrTxt").GetComponent<TMP_Text>();
        freePointsCntrTxt = GameObject.Find("FreePointsCntrTxt").GetComponent<TMP_Text>();

        ChangeHero(curnum);
    }

    void SetupUIComponents()
    {
        powerCntrTxt.text = powerCntr.ToString();
        agilityCntrTxt.text = agilityCntr.ToString();
        intelligenceCntrTxt.text = intelligenceCntr.ToString();
        healthCntrTxt.text = healthCntrerCntr.ToString();
        damageCntrTxt.text = damageCntr.ToString();
        evasionCntrTxt.text = (evasionCntr*100).ToString()+"%";
        freePointsCntrTxt.text = freePoints.ToString();
    }

    void SetupMainCharacteristics(int p, int a, int i)
    {
        powerCntr = p;
        agilityCntr = a;
        intelligenceCntr = i;
    }

    void ChangeHero(int num)
	{
		gameObject.GetComponentsInChildren<Image>()[1].sprite = heroes [num].GetComponent<SpriteRenderer>().sprite;
		gameObject.GetComponentInChildren<TMP_Text> ().text = heroesNames [num];

        HeroCharacteristics heroChars = heroes[num].GetComponent<HeroCharacteristics>();
        SetupMainCharacteristics(heroChars.power, heroChars.agility, heroChars.inteintelligence);
        damageCntr = heroChars.CalcDamage(intelligenceCntr);
        healthCntrerCntr = heroChars.CalcHealth(powerCntr);
        evasionCntr = heroChars.CalcEvasion(agilityCntr);
        freePoints = 0;
        SetupUIComponents();
	}

	public void NextHero()
	{
		if (curnum < (heroes.Length - 1)) 
		{
			curnum += 1;
		}
        ChangeHero(curnum);
	}

	public void PrevHero()
	{
		if (curnum != 0) 
		{
			curnum -= 1;
		}
        ChangeHero(curnum);
	}

    public void AcceptChoise()
    {
        var prefs = NetworkManagerCustom.singleton.spawnPrefabs;
        prefs[curnum + 1].GetComponent<HeroCharacteristics>().SetCharacteristics(powerCntr,agilityCntr,intelligenceCntr,healthCntrerCntr,damageCntr,evasionCntr);
        ClientScene.AddPlayer(curnum);
    }

    public void PowerUp()
    {
        if (freePoints > 0)
        {
            freePoints -= 1;
            powerCntr += 1;
            HeroCharacteristics heroChars = heroes[curnum].GetComponent<HeroCharacteristics>();
            healthCntrerCntr = heroChars.CalcHealth(powerCntr);
            SetupMainCharacteristics(powerCntr, agilityCntr, intelligenceCntr);
            SetupUIComponents();
        }
    }

    public void PowerDown()
    {
        if (powerCntr > 1)
        {
            freePoints += 1;
            powerCntr -= 1;
            HeroCharacteristics heroChars = heroes[curnum].GetComponent<HeroCharacteristics>();
            healthCntrerCntr = heroChars.CalcHealth(powerCntr);
            SetupMainCharacteristics(powerCntr, agilityCntr, intelligenceCntr);
            SetupUIComponents();
        }
    }

    public void AgilityUp()
    {
        if (freePoints > 0)
        {
            freePoints -= 1;
            agilityCntr += 1;
            HeroCharacteristics heroChars = heroes[curnum].GetComponent<HeroCharacteristics>();
            evasionCntr = heroChars.CalcEvasion(agilityCntr);
            SetupMainCharacteristics(powerCntr, agilityCntr, intelligenceCntr);
            SetupUIComponents();
        }
    }

    public void AgilityDown()
    {
        if (agilityCntr > 1)
        {
            freePoints += 1;
            agilityCntr -= 1;
            HeroCharacteristics heroChars = heroes[curnum].GetComponent<HeroCharacteristics>();
            evasionCntr = heroChars.CalcEvasion(agilityCntr);
            SetupMainCharacteristics(powerCntr, agilityCntr, intelligenceCntr);
            SetupUIComponents();
        }
    }

    public void intelligenceUp()
    {
        if (freePoints > 0)
        {
            freePoints -= 1;
            intelligenceCntr += 1;
            HeroCharacteristics heroChars = heroes[curnum].GetComponent<HeroCharacteristics>();
            damageCntr = heroChars.CalcDamage(intelligenceCntr);
            SetupMainCharacteristics(powerCntr, agilityCntr, intelligenceCntr);
            SetupUIComponents();
        }
    }

    public void intelligenceDown()
    {
        if (intelligenceCntr > 1)
        {
            freePoints += 1;
            intelligenceCntr -= 1;
            HeroCharacteristics heroChars = heroes[curnum].GetComponent<HeroCharacteristics>();
            damageCntr = heroChars.CalcDamage(intelligenceCntr);
            SetupMainCharacteristics(powerCntr, agilityCntr, intelligenceCntr);
            SetupUIComponents();
        }
    }

}

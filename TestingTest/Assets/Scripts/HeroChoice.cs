using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class HeroChoice : NetworkBehaviour {

	public Sprite[] heroesSprites;
	public string[] heroesNames;
	private int curnum = 0;

	void Start()
	{
		ChangeSprite (curnum);
	}

	void ChangeSprite(int num)
	{
		gameObject.GetComponentsInChildren<Image>()[1].sprite = heroesSprites [num];
		gameObject.GetComponentInChildren<TMP_Text> ().text = heroesNames [num]; 
	}

	public void NextHero()
	{
		if (curnum < (heroesSprites.Length - 1)) 
		{
			curnum += 1;
		} else 
		{
			curnum = 0;
		}
		ChangeSprite (curnum);
	}

	public void PrevHero()
	{
		if (curnum != 0) 
		{
			curnum -= 1;
			ChangeSprite (curnum);
		} else 
		{
			curnum = heroesSprites.Length - 1;
		}
		ChangeSprite (curnum);
	}

	public void AcceptChoise()
	{
		gameObject.SetActive (false);
        CmdChangePlayerSprite();
        ClientScene.AddPlayer (1);
	}

    [Command]
    void CmdChangePlayerSprite()
    {
        NetworkManagerCustom.singleton.playerPrefab.GetComponent<SpriteRenderer>().sprite = heroesSprites[curnum];
    }
}

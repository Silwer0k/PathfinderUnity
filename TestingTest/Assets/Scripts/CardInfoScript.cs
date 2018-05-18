using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Скрипт для управления внешним видом карты.

public class CardInfoScript : MonoBehaviour {
     
    public Card SelfCard;
    
    public Image Logo;
    public TextMeshProUGUI Name;

    // Скрывать карты другого игрока.
    public void HideCardInfo(Card card)
    {
        SelfCard = card;
        ShowCardInfo(card);
        //Logo.sprite = null;
        Logo.sprite = Resources.Load<Sprite>("Cards/Sprites/Enemy_Card");
        Name.text = "";
    }

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;

        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
    }

    private void Start()
    {
       //ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}

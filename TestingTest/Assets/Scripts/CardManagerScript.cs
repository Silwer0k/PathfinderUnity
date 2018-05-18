using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Скрипт для перечисления карт.

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Defence;

    public Card(string name, string logoPath, int attack, int defence)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defence = defence;
    }
     
}

// Хранение всех сущесвтвующих карт в игре.
public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScript : MonoBehaviour {
    

    public void Awake()
    {
        CardManager.AllCards.Add(item: new Card(name: "Swamp", logoPath: "Cards/Sprites/Swamp_Card", attack: 5, defence: 5));
        CardManager.AllCards.Add(item: new Card(name: "Electro Ball", logoPath: "Cards/Sprites/Electro_Ball", attack: 10, defence: 10));
        CardManager.AllCards.Add(item: new Card(name: "Acid Rain", logoPath: "Cards/Sprites/Acid_Rain", attack: 15, defence: 15));
        CardManager.AllCards.Add(item: new Card(name: "Infernal Fog", logoPath: "Cards/Sprites/Infernal_Fog", attack: 20, defence: 20));
    }
}

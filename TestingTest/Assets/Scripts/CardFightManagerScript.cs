using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Скрипт игрового процесса карточной игры.

// В данном классе хронятся списки карт.
public class CardGame
{
    public List<Card> EnemyDeck, PlayerDeck, // Колоды.
                      EnemyHand, PlayerHand, // Руки.
                      EnemyField, PlayerField; // Поля.

    public CardGame()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();

        EnemyHand = new List<Card>();
        PlayerHand = new List<Card>();

        EnemyField = new List<Card>();
        PlayerField = new List<Card>();
    }

    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        return list;
    }
}

public class CardFightManagerScript : MonoBehaviour {

    public CardGame CurrentGame;
    public Transform EnemyHand, PlayerHand;
    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public TextMeshProUGUI TurnTimeTxt;
    public Button EndTurnBtn;

    public bool IsPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }

	void Start ()
    {
        Turn = 0;
         
        CurrentGame = new CardGame();

        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        StartCoroutine(TurnFunc());
    }
	
    void GiveHandCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;

        Card card = deck[0];

        GameObject cardGO = Instantiate(CardPref, hand, false);

        // Показывать карту или рубашку в руке.
        if (hand == EnemyHand)
            cardGO.GetComponent<CardInfoScript>().HideCardInfo(card);
        else
            cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card);

        deck.RemoveAt(0);
    }

    // Контроль хода + в дальнейшем симуляция ходов противника.
    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();

        if (IsPlayerTurn)
        {
            while(TurnTime-- > 0)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            // На данный момент для тестировки, ждем до 27 секунд хода противника, и ход пропускается.
            while(TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        ChangeTurn();
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;

        EndTurnBtn.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
            GiveNewCards();

        StartCoroutine(TurnFunc());
    }

    // При ходе давать новую карту.
    void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Скрипт отвечает за поля, на которые можно перемещать карту.

// Параметры поля и руки.
public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    ENEMY_HAND,
    ENEMY_FIELD
}

public class DropPlaceScript : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public FieldType Type;

    public void OnDrop(PointerEventData eventData)
    {
        if (Type != FieldType.SELF_FIELD) // Если поле не наше, то возврат карты в руку.
            return;

        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>(); // Определяем дэфолт пэрент карты, которая была опущена над данным объектом и присваивать ему себя.

        if (card)
            card.DefaultParent = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || Type == FieldType.ENEMY_FIELD || Type == FieldType.ENEMY_HAND) // Выполняется, если объект перетягивается.
            return;

        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card)
            card.DefaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) // Выполняется, если объект перетягивается.
            return;

        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        // Если мы тянем карту за пределы поля, то она возвращается в руку.
        if (card && card.DefaultTempCardParent == transform)
            card.DefaultTempCardParent = card.DefaultParent;
    }
}

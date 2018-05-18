using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Camera MainCamera;
    Vector3 offset; // Хранит значение отступа центра карты от места карты, по которому был произведен клик мышью.
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    CardFightManagerScript CardFightManager;
    public bool isDraggable;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
        CardFightManager = FindObjectOfType<CardFightManagerScript>();

    }

    // Выполняется единожды, как только мы начнем перетягивать объект.
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position); // Текущая позиция карты - world-координаты позиции мыши.

        DefaultParent = DefaultTempCardParent = transform.parent;

        isDraggable = DefaultParent.GetComponent<DropPlaceScript>().Type == FieldType.SELF_HAND && // Истинно, только при перемещение карты своей руки.*/
                      CardFightManager.IsPlayerTurn; 

        if (!isDraggable)
            return;

        TempCardGO.transform.SetParent(DefaultParent); // Появление временной карты на месте поднятой.
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex()); // Принимает индекс текущей карты (подсветка места карты, которую мы выбрали).

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Выполняется каждый кадр, пока мы начнем тянем объект.
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;

        // В vector3 значение присвоим текущее значение карты.
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position); // Задаем текущее состояние мыши, которое хранится в eventdata.position.
        transform.position = newPos + offset;

        if (TempCardGO.transform.parent != DefaultTempCardParent)
            TempCardGO.transform.SetParent(DefaultTempCardParent);

        CheckPosition();
    }

    // Выполняется единожды, когда мы отпустим объект.
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;

        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());

        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform); // Возвращение временной карты за пределы экрана.
        TempCardGO.transform.localPosition = new Vector3(1415, 0);
    }

    // Перемещение временной карты, в зависимости от позиции выбранной карты.
    // Выполняется проходка по картам -> и сравниваем позицию по x, если позиция поднятой карты меньше, то она должна находится слева от этой карты, поэтому присваиваем индексу значение i, если идекс временной карты  меньше, чем наш новый индекс, то дикрементируем новый индекс.
    void CheckPosition()
    {
        int newIndex = DefaultTempCardParent.childCount;

        for (int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if (transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                    newIndex--;

                break;
            }
        }
        TempCardGO.transform.SetSiblingIndex(newIndex); // Установка нового индекса временной карте.
    }
}

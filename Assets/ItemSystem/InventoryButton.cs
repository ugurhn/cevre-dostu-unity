using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    public InventoryController inventory;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            inventory.ButtonUse(gameObject);
        } 
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.ButtonDrop(gameObject);
        }
    }
}

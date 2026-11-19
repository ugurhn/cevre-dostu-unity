using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGet : MonoBehaviour, IInteraction
{
    public delegate void CanGetItemEvent(BaseItem item);
    public static CanGetItemEvent gotItem;

    [SerializeField]
    BaseItem item;
    public string GetName()
    {
        return item.itemName;
    }

    public void Interaction()
    {
        gotItem(item);
        Destroy(gameObject);
    }
}

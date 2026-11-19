using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public float itemMass;
    public GameObject itemObject;
    public Sprite itemImage;
    public bool canStock;

    public abstract void Use();
}

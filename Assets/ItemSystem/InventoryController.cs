using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    List<InventorySlot> inventorySlots = new List<InventorySlot>();

    [SerializeField]
    Transform inventoryUI;

    [SerializeField]
    GameObject inventoryPanel;

    [SerializeField]
    MouseLook mouseLook;

    [SerializeField]
    GameObject buttonPrefab;

    private void OnEnable()
    {
        CanGet.gotItem += GotItem;
    }
    private void OnDisable()
    {
        CanGet.gotItem -= GotItem;
    }
    private void Start()
    {
        inventoryPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            Time.timeScale = inventoryPanel.activeSelf ? 0 : 1;
            mouseLook.ChangeCursorType(inventoryPanel.activeSelf);
        }
    }
    void GotItem(BaseItem item)
    {
        if (item.canStock)
        {
            List<BaseItem> forNowItems = new List<BaseItem>();
            foreach (InventorySlot slot in inventorySlots)
            {
                forNowItems.Add(slot.slotItem);
            }
            if (forNowItems.Contains(item)){
                inventorySlots[forNowItems.IndexOf(item)].SlotStok++;
            } 
            else
            {
                GameObject button = Instantiate(buttonPrefab, inventoryUI);
                button.name = item.itemName;
                button.GetComponent<InventoryButton>().inventory = this;
                button.GetComponent<Image>().sprite = item.itemImage;
                InventorySlot newSlot = new InventorySlot
                {
                    slotButton = button,
                    slotItem = item,
                    SlotStok = 1
                };
                inventorySlots.Add(newSlot);
            }
        }
        else
        {
            GameObject button = Instantiate(buttonPrefab, inventoryUI);
            button.name = item.itemName;
            button.GetComponent<InventoryButton>().inventory = this;
            button.GetComponent<Image>().sprite = item.itemImage;
            InventorySlot newSlot = new InventorySlot
            {
                slotButton = button,
                slotItem = item,
                SlotStok = 1
            };
            inventorySlots.Add(newSlot);
        }

    }
    public void ButtonUse(GameObject button)
    {
        foreach(InventorySlot slot in inventorySlots)
        {
            if(slot.slotButton == button)
            {
                slot.slotItem.Use();
                slot.SlotStok--;
                SlotController(slot);
                break;
            }
        }
    }
    public void ButtonDrop(GameObject button)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.slotButton == button)
            {
                Instantiate(slot.slotItem.itemObject,transform.position ,transform.rotation);
                slot.SlotStok--;
                SlotController(slot);
                break;
            }
        }
    }
    public void SlotController(InventorySlot slot)
    {
        if(slot.SlotStok < 1)
        {
            Destroy(slot.slotButton.gameObject);
            inventorySlots.Remove(slot);
        }
    }
}
public class InventorySlot
{
    public GameObject slotButton;
    public BaseItem slotItem;
    int slotStok;

    public int SlotStok
    {
        get
        {
            return slotStok;
        }
        set
        {
            slotStok = value;
            slotButton.GetComponentInChildren<Text>().text = (SlotStok > 1) ? SlotStok.ToString() : "";
        }
    }
}
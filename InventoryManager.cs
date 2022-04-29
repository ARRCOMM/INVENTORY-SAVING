using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour // newest 3/22/2022
{
    [Header("Inventory & Management")]
    public GameObject inventory;

    public Transform inventorySlotHolder;
    public Transform inventoryHotbarSlotHolder;

    public Transform cursor;
    public Vector3 offset;

    public List<bool> isFull;
    public List<Transform> slots;
    public List<Transform> hotbarSlots;

    public int currentSlot;

    [Header("Rarity & Items")]
    public Color[] rarityColors;
    public Color defaultSlotColor;

    private void Start() {
        InitializeInventory();
        SetSlotIDS();
        CheckSlots();
    }

    private void Update() {
        if(inventory.activeSelf == true) {
            cursor.position = Input.mousePosition + offset;
        }
        if(cursor.childCount > 0) {
            cursor.gameObject.SetActive(true);
        }else {
            cursor.gameObject.SetActive(false);
        }
    }

    void InitializeInventory() {
        //Sets Slots
        for (int i = 0; i < inventorySlotHolder.childCount; i++) {
            slots.Add(inventorySlotHolder.GetChild(i));
            isFull.Add(false);
        }
        if(inventoryHotbarSlotHolder != null) {
            for (int i = 0; i < inventoryHotbarSlotHolder.childCount; i++) {
                slots.Add(inventoryHotbarSlotHolder.GetChild(i));
                hotbarSlots.Add(inventoryHotbarSlotHolder.GetChild(i));
                isFull.Add(false);
            }
        } 
    }

    void SetSlotIDS() {
        for (int i = 0; i < slots.Count; i++) {
            if(slots[i].GetComponent<Slot>() != null) {
                slots[i].GetComponent<Slot>().ID = i;
            }
        }
    }

    void CheckSlots() {
        //Checks If Slots Are Full
        for (int i = 0; i < slots.Count; i++) {
            if(slots[i].childCount > 0) {
                isFull[i] = true;
            }else {
                isFull[i] = false;
            }
        }
    }

    public void CraftItem(int[] IDs, int[] IDsAmounts, GameObject outcome, int outcomeAmount) {
        //Collecting Info Weather Or Not Item can Be Crafted
        bool[] collected = new bool[IDs.Length];
        Transform[] collectedSlots = new Transform[IDs.Length];
        for (int x = 0; x < IDs.Length; x++) {
            for (int i = 0; i < slots.Count; i++) {
                if(isFull[i] == true) {
                    if(slots[i].GetChild(0).GetComponent<InventoryItem>().itemData.ID == IDs[x] && slots[i].GetChild(0).GetComponent<InventoryItem>().amount >= IDsAmounts[x]) {
                        collected[x] = true;
                        collectedSlots[x] = slots[i].GetChild(0);
                    }
                }
            }
        }
        for (int i = 0; i < collected.Length; i++) {
            if(collected[i] == false) {
                return;
            }
        }
        for (int i = 0; i < collectedSlots.Length; i++) {
            collectedSlots[i].GetComponent<InventoryItem>().amount -= IDsAmounts[i];
        }
        for (int i = 0; i < outcomeAmount; i++) {
            AddItem(outcome);
        }
    }

    public void AddItem(GameObject item) {
            for (int x = 0; x < slots.Count; x++) {
                if(isFull[x] == false) {
                    //Add The Item
                    Instantiate(item, slots[x]);
                    CheckSlots();
                    return;
                }else {
                    Debug.Log("Slot Is Full");
                }
            }
        Debug.Log("All Slots Are Full");
    }

    public void PickupDropInventory() {
        if(slots[currentSlot].childCount > 0 && cursor.childCount < 1) {
            //Put Inside Cursor
            Instantiate(slots[currentSlot].GetChild(0).gameObject, cursor);
            Destroy(slots[currentSlot].GetChild(0).gameObject);
        }else if(slots[currentSlot].childCount < 1 && cursor.childCount > 0) {
            //Put Inside Slot
            Instantiate(cursor.GetChild(0).gameObject, slots[currentSlot]);
            Destroy(cursor.GetChild(0).gameObject);
        }else if(slots[currentSlot].childCount > 0 && cursor.childCount > 0) {
            if(slots[currentSlot].GetChild(0).GetComponent<InventoryItem>().itemData.ID == cursor.GetChild(0).GetComponent<InventoryItem>().itemData.ID) {
                if(slots[currentSlot].GetChild(0).GetComponent<InventoryItem>().amount <= cursor.GetChild(0).GetComponent<InventoryItem>().itemData.maxStack - slots[currentSlot].GetChild(0).GetComponent<InventoryItem>().amount) {
                    slots[currentSlot].GetChild(0).GetComponent<InventoryItem>().amount += cursor.GetChild(0).GetComponent<InventoryItem>().amount;
                    Destroy(cursor.GetChild(0).gameObject);
                }
            }
        }
        CheckSlots();
    }
}

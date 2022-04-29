using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int ID;

    public InventoryManager manager;
    public Image renderer;

    private void Start() {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
        renderer = GetComponent<Image>();
    }

    private void Update() {
        if(manager.isFull[ID] == true) {
            renderer.color = manager.rarityColors[transform.GetChild(0).GetComponent<InventoryItem>().itemData.rarity];
        }
    }
    public void SetID() {
        manager.currentSlot = ID;
        manager.PickupDropInventory();
    }
}
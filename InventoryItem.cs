using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item itemData;
    public Text amountText;

    private Image iconRenderer;

    public int amount = 1;

    public void Update() {
        if (amount <= 1) {
            amountText.gameObject.SetActive(false);
        } else {
            amountText.gameObject.SetActive(true);
        }
        amountText.text = amount.ToString();
    }

    private void OnValidate() {
        if(iconRenderer == null) {
            iconRenderer = gameObject.GetComponent<Image>();
        }
        iconRenderer.sprite = itemData.icon;
    }
}
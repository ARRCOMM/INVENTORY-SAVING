using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBookTriggered : MonoBehaviour
{
    //This Script is to be put on the book which is to be picked up by the player

    [Header("Properties")]
    //The UI Component That Is Displayed On Screen, (The Book In Your Case)   
    public Image screenFill;
    //The Individual Sprite Used To Fill The screenFill
    public Sprite fill;
    //Sprite To Add To Your Inventory
    public Sprite item;
    //Name Of Book
    public string bookName;

    //Opens Screen When Collides With Player
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "HERO") {
            //Activates screenFill Image
            screenFill.gameObject.SetActive(true);
            //Fills screenFill With Selected Book
            screenFill.sprite = fill;
            //Adds Book To Inventory
            GameObject current = new GameObject(bookName, typeof(Image), typeof(RectTransform));
            current.GetComponent<Image>().sprite = item;
            GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>().AddItem(current);

            //Destroys Uneeded & PickedUp GameObjects
            Destroy(gameObject);
            Destroy(current); 
        }
    }
}

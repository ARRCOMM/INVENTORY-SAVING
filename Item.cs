using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  This is to put on an Item and view its  Name, ICON, ID number,
//   and the Maximum Stackable on/in an Inventory

[CreateAssetMenu(fileName = "Item", menuName = "Unity Inventory", order = 0)]
public class Item : ScriptableObject
{
    public int ID;
    public string name;
    public Sprite icon;
    public int rarity;
    public int maxStack;
}
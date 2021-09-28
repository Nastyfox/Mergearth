using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemSO", order = 2)]
public class ItemSO : ScriptableObject
{
    #region Variables
    public Item item;
    #endregion
}

[Serializable]
public class Item
{
    #region Variables
    public int itemID;
    public string itemName;
    public string itemDescription;
    public string itemSpriteName;
    public int healValue;
    public float speedPercentage;
    public int speedDuration;
    public int itemPrice;
    #endregion
}

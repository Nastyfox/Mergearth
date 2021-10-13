using System;
using System.Collections;

public abstract class ItemSO : PickupSO
{
    #region Variables
    public Item item;
    #endregion

    public override void Pickup()
    {
        Inventory.SharedInstance.AddItem(this);
    }

    public abstract IEnumerator UseItem();
}

[Serializable]
public class Item
{
    #region Variables
    public int itemID;
    public string itemName;
    public string itemDescription;
    public string itemSpriteName;
    public int itemPrice;
    #endregion
}

using System;
using System.Collections;

public class ItemSO : PickupSO
{
    #region Variables
    public Item item;
    #endregion

    public override void Pickup()
    {
        Inventory.SharedInstance.AddItem(this);
    }

    public virtual IEnumerator UseItem()
    {
        yield return null;
    }
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

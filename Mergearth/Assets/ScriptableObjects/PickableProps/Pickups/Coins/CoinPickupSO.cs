using UnityEngine;

[CreateAssetMenu(fileName = "CoinPickup", menuName = "ScriptableObjects/Pickups/CoinPickupSO")]
public class CoinPickupSO : PickupSO
{
    #region Variables
    public int pickupCoinValue;
    #endregion

    public override void Pickup()
    {
        //Play sound effect
        AudioManager.SharedInstance.PlaySoundEffect(this.soundEffect);

        //Add coin to inventory and save number of coins picked up on scene
        Inventory.SharedInstance.AddCoin(this.pickupCoinValue);
        LoadScene.SharedInstance.AddCoinForScene(this.pickupCoinValue);
    }
}

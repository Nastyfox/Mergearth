using UnityEngine;

[CreateAssetMenu(fileName = "HealPickup", menuName = "ScriptableObjects/Pickups/HealPickupSO")]
public class HealPickupSO : PickupSO
{
    #region Variables
    public int pickupHealValue;
    #endregion

    public override void Pickup()
    {
        //Play sound effect
        AudioManager.SharedInstance.PlaySoundEffect(this.soundEffect);

        //If the player is full health, don't do nothing
        if (PlayerStats.SharedInstance.GetPlayerHealth() >= Constants.PLAYERMAXHEALTH)
        {
            return;
        }
        else
        {
            PlayerHealth.SharedInstance.Heal(this.pickupHealValue);
        }
    }
}

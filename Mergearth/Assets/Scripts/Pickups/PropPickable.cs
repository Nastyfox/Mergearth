using UnityEngine;

public class PropPickable : MonoBehaviour
{
    #region Variables
    //Variables for item
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private PickupSO pickupSO;
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player gets in contact with pickup and if he's not climbing
        if (collision.gameObject.CompareTag("Player") && !PlayerMovement.SharedInstance.GetIsClimbing())
        {
            //If it's an item for inventory
            if (this.CompareTag("Item"))
            {
                Inventory.SharedInstance.AddItem(itemSO);
            }
            else
            {
                //If it's an heal pickup, we need to check player health
                if (this.CompareTag("Heal"))
                {
                    //If the player is full health, don't do nothing
                    if (PlayerStats.SharedInstance.GetPlayerHealth() >= Constants.PLAYERMAXHEALTH)
                    {
                        return;
                    }
                }
                    
                //Play sound effect
                AudioManager.SharedInstance.PlaySoundEffect(pickupSO.soundEffect);

                PlayerHealth.SharedInstance.Heal(pickupSO.pickupHealValue);

                //Add coin to inventory and save number of coins picked up on scene
                Inventory.SharedInstance.AddCoin(pickupSO.pickupCoinValue);
                LoadScene.SharedInstance.AddCoinForScene(pickupSO.pickupCoinValue);
            }
            
        Destroy(this.gameObject);

        }
    }
    #endregion
}

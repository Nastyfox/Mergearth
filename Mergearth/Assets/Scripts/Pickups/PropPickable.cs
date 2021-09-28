using UnityEngine;

public class PropPickable : MonoBehaviour
{
    #region Variables
    //Variables for audio playing
    [SerializeField] private AudioClip soundEffect;

    //Variables for item
    [SerializeField] private ItemSO itemToPickup;
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player gets in contact with pickup and if he's not climbing
        if (collision.gameObject.CompareTag("Player") && !PlayerMovement.SharedInstance.GetIsClimbing())
        {
            //The pickup is a coin
            if (this.gameObject.CompareTag("Coin"))
            {
                //Play sound effect
                AudioManager.SharedInstance.PlaySoundEffect(soundEffect);

                //Add coin to inventory and save number of coins picked up on scene
                Inventory.SharedInstance.AddCoin(Constants.BASICCOINVALUE);
                LoadScene.SharedInstance.AddCoinForScene(Constants.BASICCOINVALUE);
            }
            //The pickup is a heal
            else if(this.gameObject.CompareTag("Heal"))
            {
                //Add health to player if he is not max health
                if(PlayerHealth.SharedInstance.GetCurrentHealth() < PlayerHealth.SharedInstance.GetMaxHealth())
                {
                    PlayerHealth.SharedInstance.Heal(10);

                    //Play sound effect
                    AudioManager.SharedInstance.PlaySoundEffect(soundEffect);
                }
                else
                {
                    return;
                }
            }
            else if(this.gameObject.CompareTag("Item"))
            {
                Inventory.SharedInstance.AddItem(itemToPickup);
            }
            
            Destroy(this.gameObject);
        }
    }
    #endregion
}

using UnityEngine;

public class PropPickable : MonoBehaviour
{
    #region Variables
    //Variables for item
    [SerializeField] private PickupSO pickupSO;
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player gets in contact with pickup and if he's not climbing
        if (collision.gameObject.CompareTag("Player") && !PlayerMovement.SharedInstance.GetIsClimbing())
        {
            pickupSO.Pickup();
            
            Destroy(this.gameObject);

        }
    }
    #endregion
}

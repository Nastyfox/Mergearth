using UnityEngine;

[CreateAssetMenu(fileName = "ChestSO", menuName = "ScriptableObjects/Interactable/ChestSO")]
public class ChestSO : InteractableSO
{
    #region Variables
    public AudioClip soundEffect;
    public int minItemsNumber;
    public int maxItemsNumber;
    public int minPickupsNumber;
    public int maxPickupsNumber;
    public GameObject[] possibleItems;
    public GameObject[] possiblePickups;
    #endregion

    public override void Interact()
    {
        interactAnimator.SetTrigger("OpenChest");

        //Set a random number of item
        int itemsNumber = Random.Range(this.minItemsNumber, this.maxItemsNumber);

        for (int i = 0; i < itemsNumber; i++)
        {
            //Add a random item based on possible ones
            int itemType = Random.Range(0, this.possibleItems.Length);
            //Instantiate new item
            GameObject item = Instantiate(possibleItems[itemType]);
        }

        //Set a random number of pickups
        int pickupsNumber = Random.Range(this.minItemsNumber, this.maxItemsNumber);

        for (int i = 0; i < pickupsNumber; i++)
        {
            //Add a random pickup based on possible ones
            int pickupType = Random.Range(0, this.possiblePickups.Length);
            //Instantiate new item
            GameObject pickup = Instantiate(possiblePickups[pickupType]);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    #region Variables
    //Variable for instance of the inventory (singleton)
    public static Inventory SharedInstance;

    //Variables for coin counting
    private int coinCount;
    [SerializeField] private Text coinCountText;

    //Variables for inventory
    private List<ItemSO> inventoryItems;
    private ItemSO currentItemSO;
    [SerializeField] GameObject prefabItemButton;

    //Variables for UI
    private bool inventoryIsOpen;
    private GameObject inventoryPanel;
    [SerializeField] private GameObject itemsContainer;
    [SerializeField] private TMP_Text usedItemText;
    #endregion

    #region UnityMethods
    void Awake()
    {
        //Get the instance for inventory and set coins to 0
        SharedInstance = this;

        //Set variables
        coinCount = 0;
        inventoryItems = new List<ItemSO>();
    }

    private void Start()
    {
        //Get the inventory and deactivate it
        inventoryPanel = this.transform.GetChild(0).gameObject;
        inventoryPanel.SetActive(false);

        usedItemText.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !PauseMenu.SharedInstance.GetGameIsPaused())
        {
            if(!inventoryIsOpen)
            {
                //Pause game and open inventory
                PlayerMovement.SharedInstance.DeactivatePlayerInteractions();
                inventoryIsOpen = true;
                inventoryPanel.SetActive(true);
            }
            else
            {
                //Unpause game and close inventory
                PlayerMovement.SharedInstance.ActivatePlayerInteractions();
                inventoryIsOpen = false;
                inventoryPanel.SetActive(false);
            }
        }
    }
    #endregion

    #region Getters & Setters
    public int GetCoinCount()
    {
        return coinCount;
    }

    public void SetCoinCount(int value)
    {
        coinCount = value;
        DisplayCoins();
    }

    public List<ItemSO> GetItems()
    {
        return inventoryItems;
    }
    #endregion

    #region Methods
    public void AddCoin(int count)
    {
        //Add coin and display the number of coins
        coinCount += count;
        DisplayCoins();
    }

    public void RemoveCoin(int count)
    {
        //Delete coins and display the number of coins
        coinCount -= count;
        DisplayCoins();
    }

    private void DisplayCoins()
    {
        coinCountText.text = coinCount.ToString();
    }

    public void UseItem(ItemSO itemSO, GameObject itemButton)
    {
        //Set current item to use it
        currentItemSO = itemSO;

        //Increase player speed if necessary
        StartCoroutine(IncreasePlayerSpeed());

        //Heal player if necessary
        PlayerHealth.SharedInstance.Heal(currentItemSO.item.healValue);

        //Display item used
        StartCoroutine(DisplayUsedItem(itemSO.item.itemName));

        //Destroy the button
        Destroy(itemButton);

        //Remove item from the list
        inventoryItems.Remove(itemSO);
    }

    public void AddItem(ItemSO itemSO)
    {
        //Instantiate new button for item
        GameObject itemButton = Instantiate(prefabItemButton, itemsContainer.transform);

        //Add on click listener to use item when clicked
        itemButton.GetComponent<Button>().onClick.AddListener(delegate { UseItem(itemSO, itemButton); });
        //Deactivate price
        itemButton.transform.GetChild(1).gameObject.SetActive(false);
        itemButton.transform.GetChild(2).gameObject.SetActive(false);
        //Add the sprite for the item
        string spriteLocation = "Images/" + itemSO.item.itemSpriteName;
        itemButton.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteLocation);

        //If it's the first item, set it as selected
        if(inventoryItems.Count == 0)
        {
            SettingsMenu.SharedInstance.EventSystemSelectedElement(itemButton);
        }
        //Add item to the list
        inventoryItems.Add(itemSO);
    }
    #endregion

    #region IEnumerators
    private IEnumerator IncreasePlayerSpeed()
    {
        //Increase player's speed for a certain amount of time
        PlayerMovement.SharedInstance.IncreaseSpeedByPercentage(currentItemSO.item.speedPercentage);
        yield return new WaitForSeconds(currentItemSO.item.speedDuration);
        //Decrease player's speed after a certain amount of time
        PlayerMovement.SharedInstance.DecreaseSpeedByPercentage(currentItemSO.item.speedPercentage);
    }

    private IEnumerator DisplayUsedItem(string name)
    {
        usedItemText.text = name + " USED";
        usedItemText.enabled = true;
        yield return new WaitForSeconds(2);
        usedItemText.enabled = false;
    }
    #endregion
}

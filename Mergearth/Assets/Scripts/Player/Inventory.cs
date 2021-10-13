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
    private List<GameObject> inventoryButtons;

    //Variables for controls
    [SerializeField] private InputReaderSO inputReader = default;
    private bool inventoryInputTriggered;

    //Variables for navigation between buttons
    private GameObject inventoryFirstButton;
    #endregion

    #region UnityMethods
    void Awake()
    {
        //Get the instance for inventory and set coins to 0
        SharedInstance = this;

        //Set variables
        coinCount = 0;
        inventoryItems = new List<ItemSO>();
        inventoryButtons = new List<GameObject>();
    }

    private void OnEnable()
    {
        //Add listeners for player controls events invoked by inputreader

        //Open inventory
        inputReader.inventoryEvent += InventoryInputTriggered;

        //Close inventory
        inputReader.closeInventoryEvent += InventoryInputTriggered;
    }

    private void OnDisable()
    {
        //Remove listeners for player controls events invoked by inputreader

        //Open inventory
        inputReader.inventoryEvent -= InventoryInputTriggered;

        //Close inventory
        inputReader.closeInventoryEvent -= InventoryInputTriggered;
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
        //If there are items in the inventory, select the first one before opening the panel
        if(inventoryButtons.Count > 0)
            inventoryFirstButton = inventoryButtons[0];

        //If the player pressed the inventory input
        if (inventoryInputTriggered)
        {
            if (!inventoryIsOpen)
            {
                //Pause game and open inventory
                inventoryPanel.SetActive(true);
                inventoryIsOpen = true;

                //Enable UI controls only
                inputReader.EnableUIControlInput();

                //Select the first item
                SettingsMenu.SharedInstance.EventSystemSelectedElement(inventoryFirstButton);
            }
            else
            {
                //Unpause game and close inventory
                inventoryPanel.SetActive(false);
                inventoryIsOpen = false;

                //Enable player controls only
                inputReader.EnablePlayerControlInput();
            }
            inventoryInputTriggered = false;
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

        //Use item
        StartCoroutine(currentItemSO.UseItem());

        //Display item used
        StartCoroutine(DisplayUsedItem(currentItemSO.item.itemName));

        //Remove item from the list
        inventoryItems.Remove(currentItemSO);
        inventoryButtons.Remove(itemButton);

        //After using an item, go back to the first one
        inventoryFirstButton = inventoryButtons[0];
        //Select the first item
        SettingsMenu.SharedInstance.EventSystemSelectedElement(inventoryFirstButton);

        //Destroy the button
        Destroy(itemButton);
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

        //Add the button to the list of buttons
        inventoryButtons.Add(itemButton);
        //Add item to the list
        inventoryItems.Add(itemSO);
    }
    #endregion

    #region IEnumerators
    private IEnumerator DisplayUsedItem(string name)
    {
        usedItemText.text = name + " USED";
        usedItemText.enabled = true;
        yield return new WaitForSeconds(2);
        usedItemText.enabled = false;
    }
    #endregion

    #region InputMethods
    private void InventoryInputTriggered()
    {
        //If the game is not paused, open or close inventory
        if(!PauseMenu.SharedInstance.GetGameIsPaused())
        {
            inventoryInputTriggered = true;
        }
    }
    #endregion
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    #region Variables
    //Variable for instance of the shop manager (singleton)
    public static ShopManager SharedInstance;

    //Variables for shop items
    int itemsNumber;
    [SerializeField] GameObject prefabItemButton;
    [SerializeField] ItemSO[] possibleItems;

    //Variables for shop UI
    [SerializeField] private GameObject itemsContainer;
    [SerializeField] private TMP_Text buyItemText;
    [SerializeField] private GameObject shopPanel;
    private bool isShopOpen;
    private bool shopHasBeenOpened;

    //Variables for controls
    [SerializeField] private InputReaderSO inputReader = default;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        //Get the instance for dialog manager
        SharedInstance = this;
        buyItemText.enabled = false;

        //Disable shop UI for the moment
        shopPanel.SetActive(false);
    }
    #endregion

    #region Getters & Setters
    public bool GetShopHasBeenOpenened()
    {
        return shopHasBeenOpened;
    }

    public bool GetIsShopOpen()
    {
        return isShopOpen;
    }
    #endregion

    #region Methods
    public void AddItemsInShop()
    {
        //Enable shop UI
        shopPanel.SetActive(true);
        isShopOpen = true;
        shopHasBeenOpened = true;

        //Set a random number of item
        itemsNumber = Random.Range(Constants.MINITEMSHOP, Constants.MAXITEMSHOP);

        for(int i = 0; i < itemsNumber; i++)
        {
            //Add a random item based on possible ones
            int itemType = Random.Range(0, possibleItems.Length - 1);
            AddItem(possibleItems[itemType], i);
        }
    }

    private void AddItem(ItemSO itemSO, int itemNumber)
    {
        //Instantiate new button for item
        GameObject itemButton = Instantiate(prefabItemButton, itemsContainer.transform);

        //Add on click listener to use item when clicked
        itemButton.GetComponent<Button>().onClick.AddListener(delegate { BuyItem(itemSO, itemButton); });
        //Add the sprite for the item
        string spriteLocation = "Images/" + itemSO.item.itemSpriteName;
        itemButton.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteLocation);

        //Add price for the item
        itemButton.transform.GetChild(1).GetComponent<TMP_Text>().text = itemSO.item.itemPrice.ToString();

        //If it's the first item, set it as selected
        if(itemNumber == 0)
        {
            SettingsMenu.SharedInstance.EventSystemSelectedElement(itemButton);
        }
    }

    public void BuyItem(ItemSO itemSO, GameObject itemButton)
    {
        StopAllCoroutines();

        //If the player has enough money he can buy the item
        if (Inventory.SharedInstance.GetCoinCount() >= itemSO.item.itemPrice)
        {
            //Add item to inventory
            Inventory.SharedInstance.AddItem(itemSO);
            //Remove coins used from inventory
            Inventory.SharedInstance.RemoveCoin(itemSO.item.itemPrice);

            StartCoroutine(DisplayBoughtItem(itemSO.item.itemName));


            //Destroy the button
            Destroy(itemButton);
        }
        //If not, display that he doesn't have enough coins
        else
        {
            StartCoroutine(NotEnoughCoin());
        }
    }

    public void CloseShop()
    {
        //Deactivate shop UI
        shopPanel.SetActive(false);
        isShopOpen = false;

        //Activate player controls only
        inputReader.EnablePlayerControlInput();
    }

    public void OpenShop()
    {
        //Activate shop UI
        shopPanel.SetActive(true);
        isShopOpen = true;
    }
    #endregion

    #region IEnumerators
    private IEnumerator DisplayBoughtItem(string name)
    {
        buyItemText.text = name + " BOUGHT";
        buyItemText.enabled = true;
        yield return new WaitForSeconds(2);
        buyItemText.enabled = false;
    }

    private IEnumerator NotEnoughCoin()
    {
        buyItemText.text = "NOT ENOUGH COINS";
        buyItemText.enabled = true;
        yield return new WaitForSeconds(2);
        buyItemText.enabled = false;
    }
    #endregion
}

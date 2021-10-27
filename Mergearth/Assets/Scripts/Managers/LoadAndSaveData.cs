using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class LoadAndSaveData : MonoBehaviour
{
    #region Variables
    //Variable for instance of the data manager (singleton)
    public static LoadAndSaveData SharedInstance;

    //Variables for saving datas
    private int playerHealth;
    private int coinsCount;
    private string currentLevel;
    private int levelsUnlocked;
    private List<ItemSO> inventoryItems;
    private List<Item> itemsList;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        //Get the instance for data manager
        SharedInstance = this;

        //Set variables
        inventoryItems = new List<ItemSO>();
        itemsList = new List<Item>();

        //Load Menu Datas when the main menu is on
        LoadMenuDatas();
    }
    #endregion

    #region Getters & Setters
    public string GetSceneToLoadName()
    {
        return currentLevel;
    }

    public int GetLevelsUnlocked()
    {
        return levelsUnlocked;
    }
    #endregion

    #region Methods
    public void GetDatasToSave()
    {
        //Save current health as health
        playerHealth = PlayerStats.SharedInstance.GetPlayerHealth();

        //Save current number of coins
        coinsCount = Inventory.SharedInstance.GetCoinCount();

        //Save current level
        currentLevel = LoadScene.SharedInstance.GetSceneToSaveName();

        //Save levels unlocked only if it's more than the current level unlocked
        if (LoadScene.SharedInstance.GetLevelUnlocked() > levelsUnlocked)
        {
            levelsUnlocked = LoadScene.SharedInstance.GetLevelUnlocked();
        }

        //Save items in inventory
        inventoryItems.Clear();
        inventoryItems = Inventory.SharedInstance.GetItems();
        itemsList.Clear();

        foreach (ItemSO itemSO in inventoryItems)
        {
            itemsList.Add(itemSO.item);
        }


        SaveDatas();
    }

    private void SaveDatas()
    {
        //Open file to store datas
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");

        //Create new class with saved datas
        SaveData data = new SaveData();
        data.playerHealth = playerHealth;
        data.coinsCount = coinsCount;
        data.currentLevel = currentLevel;
        data.levelsUnlocked = levelsUnlocked;
        data.itemsList = new List<Item>(itemsList);

        //Serialize data in the save file
        bf.Serialize(file, data);

        //Close the file
        file.Close();
    }


    public void LoadGameDatas()
    {
        //If there is a save file, open it and get datas in it
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);

            //Deserialize datas saved
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            playerHealth = data.playerHealth;
            coinsCount = data.coinsCount;
            itemsList.Clear();
            inventoryItems.Clear();
            foreach (Item item in data.itemsList)
            {
                itemsList.Add(item);
            }
            foreach (Item item in itemsList)
            {
                ItemSO itemSO = ScriptableObject.CreateInstance("ItemSO") as ItemSO;
                itemSO.item = item;
                inventoryItems.Add(itemSO);
            }

            //Set current health to saved health
            PlayerStats.SharedInstance.SetPlayerHealth(playerHealth);

            //Set coin count to saved coin count
            Inventory.SharedInstance.SetCoinCount(coinsCount);

            //Set inventory items to saved items
            foreach (ItemSO item in inventoryItems)
            {
                Inventory.SharedInstance.AddItem(item);
            }
        }
        //If there is no save file
        else
        {
            //Set player health to max health
            PlayerStats.SharedInstance.SetPlayerHealth(Constants.PLAYERMAXHEALTH);

            //Set coins count to 0
            Inventory.SharedInstance.SetCoinCount(0);
        }
    }

    public void LoadMenuDatas()
    {
        //If there is a save file, open it and get datas in it
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);

            //Deserialize datas saved
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            currentLevel = data.currentLevel;
            levelsUnlocked = data.levelsUnlocked;
        }
        //If there is no save file
        else
        {
            //Set levels unlocked to 1
            levelsUnlocked = 1;

            //Set scene to load to first level
            currentLevel = Constants.FIRSTLEVEL;
        }
    }
    #endregion
}

[Serializable]
public class SaveData
{
    #region Variables
    //Datas to save
    public int playerHealth;
    public int coinsCount;
    public string currentLevel;
    public int levelsUnlocked;
    public List<Item> itemsList;
    #endregion
}

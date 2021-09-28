using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    #region Variables
    //Variables for buttons interaction
    [SerializeField] Button[] levelButtons;
    private int maxLevelUnlocked;
    #endregion

    #region UnityMethods
    public void Start()
    {
        //Get the max level unlocked by player
        maxLevelUnlocked = LoadAndSaveData.SharedInstance.GetLevelsUnlocked();

        //Make every level above that number unloadable
        for (int i = maxLevelUnlocked; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }
    #endregion

    #region Methods
    //Load the selected level when player click on button
    public void LoadSelectedLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    #endregion
}

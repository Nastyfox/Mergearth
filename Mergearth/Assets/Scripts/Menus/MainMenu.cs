using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    #region Variables
    //Variable for instance of the settings menu manager (singleton)
    public static MainMenu SharedInstance;

    //Variables for UI
    [SerializeField] private GameObject levelSelectionUI;
    [SerializeField] private GameObject settingsUI;

    //Variables for navigation between buttons
    [SerializeField] private GameObject mainMenuFirstButton, levelsFirstButton, closedLevelsFirstButton, settingsFirstButton, closedSettingsFirstButton;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        //Get the instance for settings menu
        SharedInstance = this;

        //Load datas from file for loading levels
        LoadAndSaveData.SharedInstance.LoadMenuDatas();
    }

    private void Start()
    { 
        //Close other panels at first
        CloseSelectionLevelPanel();
        CloseSettingsPanel();

        //Set first button of main menu as selected
        SettingsMenu.SharedInstance.EventSystemSelectedElement(mainMenuFirstButton);
    }
    #endregion

    #region Methods
    public void StartButton()
    {
        //Start the first level
        SceneManager.LoadScene(Constants.FIRSTLEVEL);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(LoadAndSaveData.SharedInstance.GetSceneToLoadName());
        LoadScene.SharedInstance.SetDataToLoad(true);
    }

    public void OpenSelectionLevelPanel()
    {
        levelSelectionUI.SetActive(true);

        //Set first button of levels menu as selected
        SettingsMenu.SharedInstance.EventSystemSelectedElement(levelsFirstButton);
    }

    public void CloseSelectionLevelPanel()
    {
        levelSelectionUI.SetActive(false);

        //Set first button of main menu as selected
        SettingsMenu.SharedInstance.EventSystemSelectedElement(closedLevelsFirstButton);
    }

    public void CloseSettingsPanel()
    {
        //Close settings panel
        settingsUI.SetActive(false);

        //Set settings button active
        SettingsMenu.SharedInstance.EventSystemSelectedElement(closedSettingsFirstButton);
    }

    public void OpenSettingsPanel()
    {
        //Open settings panel
        settingsUI.SetActive(true);

        //Set first button of settings active
        SettingsMenu.SharedInstance.EventSystemSelectedElement(settingsFirstButton);
    }
    #endregion
}

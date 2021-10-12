using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    #region Variables
    //Variable for instance of the pause menu manager (singleton)
    public static PauseMenu SharedInstance;

    //Variables for pause menu
    private bool gameIsPaused = false;
    [SerializeField] private TMP_Text savedText;

    //Variables to activate or deactivate pause UI
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject settingsUI;

    //Variables for navigation between buttons
    [SerializeField] private GameObject pauseFirstButton, settingsFirstButton, closedSettingsFirstButton;

    //Variables for controls
    [SerializeField] private InputReaderSO inputReader = default;
    private bool pauseInputTriggered;
    #endregion

    #region UnityMethods
    // Update is called once per frame
    void Update()
    {
        //If the player presses the pause input, invert pause mode
        if(pauseInputTriggered)
        {
            //Pause or unpause the game
            if (!gameIsPaused)
            {
                //Activate pause UI
                PauseGame();
                gameIsPaused = true;
            }
            else
            {
                //Deactivate pause UI
                ResumeGame();
                gameIsPaused = false;
            }

            pauseInputTriggered = false;
        }
    }

    private void Awake()
    {
        //Get the instance for pause menu manager
        SharedInstance = this;

        //Disable text at start
        savedText.enabled = false;

        //Deactivate UI
        pauseUI.SetActive(false);

        //Close other panels at start
        CloseSettingsPanel();
    }
    private void OnEnable()
    {
        //Add listeners for player controls events invoked by inputreader

        //Launch pause
        inputReader.pauseEvent += PauseInputTriggered;

        //Resume game
        inputReader.unpauseEvent += PauseInputTriggered;
    }

    private void OnDisable()
    {
        //Remove listeners for player controls events invoked by inputreader

        //Launch pause
        inputReader.pauseEvent -= PauseInputTriggered;

        //Resume game
        inputReader.unpauseEvent -= PauseInputTriggered;
    }
    #endregion

    #region Getters & Setters
    public bool GetGameIsPaused()
    {
        return gameIsPaused;
    }
    #endregion

    #region Methods
    public void PauseGame()
    {
        //Activate UI
        pauseUI.SetActive(true);

        //Enable UI controls only
        inputReader.EnableUIControlInput();

        //Stop time
        Time.timeScale = Constants.STOPPEDTIMESCALE;

        //Pause the music
        AudioManager.SharedInstance.PauseAudioSource();

        //Set first button of pause menu as selected
        SettingsMenu.SharedInstance.EventSystemSelectedElement(pauseFirstButton);
    }

    public void ResumeGame()
    {
        //Enable player controls only
        inputReader.EnablePlayerControlInput();

        //Resume time
        Time.timeScale = Constants.NORMALTIMESCALE;

        //Resume the music
        AudioManager.SharedInstance.ResumeAudioSource();

        //Say that game is unpaused
        pauseUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        //Resume game before loading main menu
        ResumeGame();

        //Load main menu
        GameOverMenu.SharedInstance.MainMenuButton();
    }

    public void SaveButton()
    {
        //Get current name scene 
        LoadScene.SharedInstance.SaveCurrentScene();
        //Save datas
        LoadAndSaveData.SharedInstance.GetDatasToSave();

        //Display saved text
        savedText.enabled = true;
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

    #region InputMethods
    private void PauseInputTriggered()
    {
        pauseInputTriggered = true;
    }
    #endregion
}

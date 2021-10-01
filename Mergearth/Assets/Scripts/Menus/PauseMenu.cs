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
    private Canvas canvasObject;
    [SerializeField] private GameObject settingsUI;

    //Variables for navigation between buttons
    [SerializeField] private GameObject pauseFirstButton, settingsFirstButton, closedSettingsFirstButton;
    #endregion

    #region UnityMethods
    // Update is called once per frame
    void Update()
    {
        //If the player press escape, pause or unpause the game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gameIsPaused)
            {
                //Activate pause UI
                canvasObject.enabled = true;
                PauseGame();
            }
            else
            {
                //Deactivate pause UI
                canvasObject.enabled = false;
                ResumeGame();
            }
        }
    }

    private void Awake()
    {
        //Get the instance for pause menu manager
        SharedInstance = this;

        //Disable text at start
        savedText.enabled = false;

        //Get the canvas and deactivate it
        canvasObject = this.GetComponent<Canvas>();
        canvasObject.enabled = false;

        //Close other panels at start
        CloseSettingsPanel();
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
        //Stop every movement and stop time
        PlayerMovement.SharedInstance.enabled = false;
        Time.timeScale = Constants.STOPPEDTIMESCALE;

        //Pause the music
        AudioManager.SharedInstance.PauseAudioSource();

        //Say that game is paused
        gameIsPaused = true;

        //Set first button of pause menu as selected
        SettingsMenu.SharedInstance.EventSystemSelectedElement(pauseFirstButton);
    }

    public void ResumeGame()
    {
        //Stop every movement and stop time
        PlayerMovement.SharedInstance.enabled = true;
        Time.timeScale = Constants.NORMALTIMESCALE;

        //Resume the music
        AudioManager.SharedInstance.ResumeAudioSource();

        //Say that game is unpaused
        gameIsPaused = false;
        canvasObject.enabled = false;
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
}

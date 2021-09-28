using TMPro;
using UnityEngine;

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
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        //Get the canvas and deactivate it
        canvasObject = this.GetComponent<Canvas>();
        canvasObject.enabled = false;
    }

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
    #endregion
}

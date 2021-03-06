using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    #region Variables
    //Variable for instance of the game over manager (singleton)
    public static GameOverMenu SharedInstance;

    //Variable to activate or deactivate game over UI
    [SerializeField] private GameObject gameOverUI;

    //Variables for navigation between buttons
    [SerializeField] private GameObject gameOverFirstButton;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        //Get the instance for health bar
        SharedInstance = this;

        //Deactivate UI at first
        gameOverUI.SetActive(false);
    }
    #endregion

    #region Methods
    public void RetryButton()
    {
        //Reload the current scene
        StartCoroutine(LoadScene.SharedInstance.LoadSceneByName(SceneManager.GetActiveScene().name, LoadScene.SharedInstance.GetLevelUnlocked(), true, false));

        //Deactivate gameover UI
        gameOverUI.SetActive(false);

        //Reactivate pause menu
        PauseMenu.SharedInstance.enabled = true;
    }

    public void MainMenuButton()
    {
        //When loading the main menu remove audio manager from destroyonload to avoid duplicate object
        Destroy(GameObject.Find("AudioManager"));
        Destroy(GameObject.Find("GameManager"));

        //Go to the main menu UI
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        //Quit the game
        Application.Quit();
    }

    public void GameOverUI()
    {
        //Display game over UI
        gameOverUI.SetActive(true);

        //Avoid pause ui to display over game over ui
        PauseMenu.SharedInstance.enabled = false;

        //Set first button of pause menu as selected
        SettingsMenu.SharedInstance.EventSystemSelectedElement(gameOverFirstButton);
    }
    #endregion
}
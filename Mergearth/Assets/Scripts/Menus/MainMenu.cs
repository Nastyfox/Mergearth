using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Variables
    //Variables for UI
    [SerializeField] private GameObject levelSelectionUI;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        CloseSelectionLevelPanel();
    }

    private void Start()
    {
        LoadAndSaveData.SharedInstance.LoadMenuDatas();
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
    }

    public void CloseSelectionLevelPanel()
    {
        levelSelectionUI.SetActive(false);
    }
    #endregion
}

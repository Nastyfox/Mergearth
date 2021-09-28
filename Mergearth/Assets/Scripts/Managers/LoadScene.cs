using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    #region Variables
    //Variable for instance of the load scene manager (singleton)
    public static LoadScene SharedInstance;

    //Variables for new scene loading
    private Animator fadeSystemAnimator;
    private float fadeDuration;

    //Variables to remember number of coins picked in the current scene
    private int coinsPickedUpCurrentScene;

    //Variables for loading data
    private bool dataToLoad;
    private string sceneToSaveName;
    private int levelUnlocked;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        //Get the instance for load scene manager
        SharedInstance = this;

        //Get the animator for fadesystem
        fadeSystemAnimator = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();

        //Get fadein duration
        foreach (AnimationClip clip in fadeSystemAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "FadeIn")
            {
                fadeDuration = clip.length;
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region Getters & Setters
    public int GetCoinsPickedUpCurrentScene()
    {
        return coinsPickedUpCurrentScene;
    }
    public Animator GetFadeAnimator()
    {
        return fadeSystemAnimator;
    }
    public bool GetDataToLoad()
    {
        return dataToLoad;
    }
    public void SetDataToLoad(bool value)
    {
        dataToLoad = value;
    }
    public string GetSceneToSaveName()
    {
        return sceneToSaveName;
    }
    public int GetLevelUnlocked()
    {
        return levelUnlocked;
    }
    #endregion

    #region Methods
    public void AddCoinForScene(int value)
    {
        coinsPickedUpCurrentScene += value;
    }

    public void SaveCurrentScene()
    {
        sceneToSaveName = SceneManager.GetActiveScene().name;
    }
    #endregion

    #region IEnumerators
    public IEnumerator LoadSceneByName(string sceneName, int levelNumber, bool fadeAnimation, bool loadData)
    {
        //Saving datas for next level
        sceneToSaveName = sceneName;
        levelUnlocked = levelNumber;
        LoadAndSaveData.SharedInstance.GetDatasToSave();

        //Deactivate player movement during loading
        PlayerMovement.SharedInstance.DeactivatePlayerInteractions();

        if(fadeAnimation)
        {
            //Launch fade animation
            fadeSystemAnimator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(fadeDuration);
        }
        
        //If we reload the same scene, the player is retrying the same level
        if(SceneManager.GetActiveScene().name == sceneName)
        {
            //Respawn the player with every variable back to normal
            PlayerHealth.SharedInstance.Respawn();

            //Restart idle animation
            PlayerHealth.SharedInstance.RestartAnimationAfterDeath();

            //Delete coins picked up during the scene
            Inventory.SharedInstance.RemoveCoin(LoadScene.SharedInstance.GetCoinsPickedUpCurrentScene());
        }

        SceneManager.LoadScene(sceneName);

        dataToLoad = loadData;

        //Reset coins picked up on scene
        coinsPickedUpCurrentScene = 0;

        //Reactivate player movement after loading
        PlayerMovement.SharedInstance.ActivatePlayerInteractions();
    }
    #endregion
}

using UnityEngine;

public class Credits : MonoBehaviour
{
    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        //Deactivate pause menu and game over menu while on main menu window
        PauseMenu.SharedInstance.enabled = false;
        GameOverMenu.SharedInstance.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If the player hits escape, end credits before the end
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndCredits();
        }
    }
    #endregion

    #region Methods
    public void EndCredits()
    {
        GameOverMenu.SharedInstance.MainMenuButton();
    }
    #endregion
}

using UnityEngine;

public class Credits : MonoBehaviour
{
    #region Variables
    private PlayerActions controls;
    #endregion
    #region UnityMethods
    private void Awake()
    {
        //Get all controls
        controls = new PlayerActions();

        controls.UIControl.Credits.performed += ctx => EndCredits();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    #endregion

    #region InputMethods
    public void EndCredits()
    {
        GameOverMenu.SharedInstance.MainMenuButton();
    }
    #endregion
}

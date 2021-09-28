using UnityEngine;

public class NPCDialog : MonoBehaviour
{

    #region Variables
    //Variable for instance of the dialog of the NPC (singleton)
    public static NPCDialog SharedInstance;

    [SerializeField] DialogSO dialog;

    private void Awake()
    {
        //Get the instance for dialog of the NPC
        SharedInstance = this;
    }

    #endregion

    #region Getters & Setters
    public DialogSO GetDialog()
    {
        return dialog;
    }
    #endregion
}

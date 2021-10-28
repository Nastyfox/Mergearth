using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/DialogSO")]
public class DialogSO : InteractableSO
{
    #region Variables
    public string NPCName;

    public string[] NPCSentences;

    public bool isShop;
    #endregion

    public override void Interact()
    {
        //Launch dialog and deactivate interaction text
        DialogManager.SharedInstance.StartDialog(this);
    }
}

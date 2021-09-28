using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/DialogSO", order = 1)]
public class DialogSO : ScriptableObject
{
    #region Variables
    public string NPCName;

    public string[] NPCSentences;

    public bool isShop;
    #endregion
}

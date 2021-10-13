using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HealPotionSO", menuName = "ScriptableObjects/Items/HealPotionSO")]
public class HealPotionSO : ItemSO
{

    #region Variables
    public int healValue;
    #endregion

    #region Methods
    public override IEnumerator UseItem()
    {
        //Heal player if necessary
        PlayerHealth.SharedInstance.Heal(healValue);
        yield return null;
    }
    #endregion
}

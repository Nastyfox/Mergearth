using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedPotionSO", menuName = "ScriptableObjects/Items/SpeedPotionSO")]
public class SpeedPotionSO : ItemSO
{

    #region Variables
    public float speedPercentage;
    public int speedDuration;
    #endregion

    #region Methods
    public override IEnumerator UseItem()
    {
        //Increase player's speed for a certain amount of time
        PlayerMovement.SharedInstance.IncreaseSpeedByPercentage(speedPercentage);
        yield return new WaitForSeconds(speedDuration);
        //Decrease player's speed after a certain amount of time
        PlayerMovement.SharedInstance.DecreaseSpeedByPercentage(speedPercentage);
    }
    #endregion
}

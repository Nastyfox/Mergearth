using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    #region Variables
    private PlatformEffector2D platformEffector2D;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        //Get the platform effector 2d
        platformEffector2D = this.GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        //If the player is climbing up, make platform crossable from bottom
        if(PlayerMovement.SharedInstance.GetVerticalMovement() > 0)
        {
            platformEffector2D.rotationalOffset = 0;
        }
        //If the player is climbing up, make platform crossable from top
        else if (PlayerMovement.SharedInstance.GetVerticalMovement() < 0)
        {
            platformEffector2D.rotationalOffset = 180;
        }
    }
    #endregion

    #region Methods
    #endregion
}

using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    #region Variables
    //Variable for platform effector
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
        if (PlayerMovement.SharedInstance.GetVerticalMovement() > 0)
        {
            //Change rotational offset of platform effector if it's not in the right position
            if (platformEffector2D.rotationalOffset != Constants.PLATFORMANGLEUP)
            {
                platformEffector2D.rotationalOffset = Constants.PLATFORMANGLEUP;
            }
        }
        //If the player is climbing up, make platform crossable from top
        if (PlayerMovement.SharedInstance.GetVerticalMovement() < 0)
        {
            //Change rotational offset of platform effector if it's not in the right position
            if (platformEffector2D.rotationalOffset != Constants.PLATFORMANGLEDOWN)
            {
                platformEffector2D.rotationalOffset = Constants.PLATFORMANGLEDOWN;
            }
        }
    }
    #endregion
}

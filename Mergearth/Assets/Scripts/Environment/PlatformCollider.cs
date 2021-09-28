using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    #region Variables
    [SerializeField] CompositeCollider2D platformCollider;
    #endregion

    #region Methods
    public void MakePlatformCrossable()
    {
        platformCollider.isTrigger = true;
    }

    public void MakePlatformUncrossable()
    {
        platformCollider.isTrigger = false;
    }
    #endregion
}

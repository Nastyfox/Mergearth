using UnityEngine;

[CreateAssetMenu(fileName = "Grid", menuName = "ScriptableObjects/GridSO")]
public class GridSO : ScriptableObject
{
    #region Variables
    //Variables for grid movement
    public float gridMoveSpeed;

    //Variables for grid position
    public float gridXStartPos;
    public float previousGridXPosNewSpawn;
    #endregion
}

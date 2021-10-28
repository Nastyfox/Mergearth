using UnityEngine;

public class GridController : MonoBehaviour
{

    #region Variables
    [SerializeField] GridSO gridSO;
    #endregion

    #region UnityMethods
    // Update is called once per frame
    void Update()
    {
        MoveGrid();
    }
    #endregion

    #region Methods
    private void MoveGrid()
    {
        //Make grid move left
        transform.Translate(Vector3.left * Time.deltaTime * gridSO.gridMoveSpeed); ;
    }
    #endregion
}

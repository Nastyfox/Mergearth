using UnityEngine;

public class GridController : MonoBehaviour
{

    #region Variables
    //Variables for grid movement
    [SerializeField] private float gridMoveSpeed = 5;
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
        transform.Translate(Vector3.left * Time.deltaTime * gridMoveSpeed);
    }
    #endregion
}

using UnityEngine;

public class GridController : MonoBehaviour
{

    #region Variables
    [SerializeField] GridSO gridSO;
    [SerializeField] GridManagerSO gridManagerSO;
    [SerializeField] BoxCollider2D gridCollider;
    #endregion

    #region UnityMethods
    // Update is called once per frame
    void Update()
    {
        MoveGrid();
    }

    private void Awake()
    {
        gridCollider.size = new Vector2(gridManagerSO.gridSize - 2, gridCollider.size.y);
    }
    #endregion

    #region Methods
    private void MoveGrid()
    {
        //Make grid move left
        transform.Translate(Vector3.left * Time.deltaTime * gridSO.gridMoveSpeed); ;
    }
    #endregion

    #region Colliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect that player is entering a new grid and set it parent to the grid
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Detect that player is exiting a new grid and set it parent to none
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
    #endregion
}

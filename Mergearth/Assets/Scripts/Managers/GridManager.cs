using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    #region Variables
    //Variables for grid generation
    [SerializeField] GridManagerSO gridManagerSO;
    private int randomGridPattern = 0;
    private Queue<GameObject> activeGrids;
    private GameObject oldestGrid;
    private GameObject newestGrid;
    private int previousGridType = 0;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        //Create new queue for grid patterns
        activeGrids = new Queue<GameObject>();
        for(int i = 0; i < gridManagerSO.maxNumberGrids; i++)
        {
            //Show the first grid pattern and put it at the start position 0,0,0
            SetGridActive();
            newestGrid.transform.position = new Vector3(i * gridManagerSO.gridSize, 0, 0);
        }

        oldestGrid = activeGrids.Peek();
    }

    private void Update()
    {
        //Change limit position on X Axis based on oldest grid position
        gridManagerSO.xPosLimitLeft = oldestGrid.transform.position.x + gridManagerSO.gridSize;
        gridManagerSO.xPosLimitRight = newestGrid.transform.position.x;

        //If the latest grid created goes beyond a certain point, make a new one appear
        if (newestGrid.transform.position.x <= gridManagerSO.previousGridXPosNewSpawn)
        {
            SetGridActive();
        }

        //If the oldest grid created goes to a certain point (out of view), make it disappear
        if (oldestGrid.transform.position.x <= -gridManagerSO.gridSize * gridManagerSO.maxNumberGrids)
        {
            activeGrids.Dequeue();
            oldestGrid.SetActive(false);
            oldestGrid = activeGrids.Peek();
        }
    }
    #endregion

    #region Methods
    private void SetGridActive()
    {
        //Create a new grid with a different type than the previous one
        do
        {
            //Select random grid pattern
            randomGridPattern = Random.Range(1, ObjectPooling.SharedInstance.GetPossibleGridPatternsCount() + 1);

        } while (previousGridType == randomGridPattern);

        //Save the grid type
        previousGridType = randomGridPattern;

        //Use precreated objects of object pooling to activate them when necessary
        GameObject grid = ObjectPooling.SharedInstance.GetGridObject(randomGridPattern);
        if (grid != null)
        {
            grid.transform.position = new Vector3(gridManagerSO.gridSize * gridManagerSO.maxNumberGrids, grid.transform.position.y, grid.transform.position.z);
            grid.SetActive(true);

            //Put the latest grid in the queue
            activeGrids.Enqueue(grid);
            //Make it the latest one
            newestGrid = grid;
        }
    }
    #endregion
}

using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    #region Variables
    //Variables for grid generation
    [SerializeField] GridSO gridSO;
    private int randomGridPattern;
    private Queue<GameObject> activeGrids;
    private GameObject oldestGrid;
    private GameObject newestGrid;
    private int previousGridType;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        //Create new queue for grid patterns
        activeGrids = new Queue<GameObject>();
        //Show the first grid pattern and put it at the start position 0,0,0
        SetGridActive();
        oldestGrid = activeGrids.Peek();
        oldestGrid.transform.position = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        //If the latest grid created goes beyond a certain point, make a new one appear
        if (newestGrid.transform.position.x <= gridSO.previousGridXPosNewSpawn)
        {
            SetGridActive();
        }

        //If the oldest grid created goes to a certain point (out of view), make it disappear
        if (oldestGrid.transform.position.x <= -gridSO.gridXStartPos)
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
        while(previousGridType == randomGridPattern)
        {
            //Select random grid pattern
            randomGridPattern = Random.Range(1, ObjectPooling.SharedInstance.GetPossibleGridPatternsCount() + 1);
        }

        //Save the grid type
        previousGridType = randomGridPattern;

        //Use precreated objects of object pooling to activate them when necessary
        GameObject grid = ObjectPooling.SharedInstance.GetGridObject(randomGridPattern);
        if (grid != null)
        {
            grid.transform.position = new Vector3(gridSO.gridXStartPos, grid.transform.position.y, grid.transform.position.z);
            grid.SetActive(true);

            //Put the latest grid in the queue
            activeGrids.Enqueue(grid);
            //Make it the latest one
            newestGrid = grid;
        }
    }
    #endregion
}

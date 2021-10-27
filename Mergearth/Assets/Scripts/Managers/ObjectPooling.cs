using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{

    #region Variables
    //Variable for instance of the object pooling manager (singleton)
    public static ObjectPooling SharedInstance;

    //Variables for grid patterns creation
    private List<GameObject> pooledGrids;
    [SerializeField] private List<GameObject> possibleGrids;
    [SerializeField] private int gridAmountToPool;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Awake()
    {
        //Get the instance for object pooling manager
        SharedInstance = this;
    }

    // Update is called once per frame
    void Start()
    {
        //Instantiate every necessary objects for the game
        pooledGrids = new List<GameObject>();
        GameObject tmp;

        foreach(GameObject grid in possibleGrids)
        {
            //Instantiation of each grid pattern X times
            for (int i = 0; i < gridAmountToPool; i++)
            {
                tmp = Instantiate(grid);
                tmp.SetActive(false);
                pooledGrids.Add(tmp);
            }
        }
    }
    #endregion

    #region Getters & Setters
    public int GetPossibleGridPatternsCount()
    {
        return possibleGrids.Count;
    }
    #endregion

    #region Methods
    public GameObject GetGridObject(int gridType)
    {
        //Set position in the list based on grid type to show
        int gridPositionInList = possibleGrids.Count * gridType - 2;

        //Return first inactive grid object of this type
        for (int i = gridPositionInList; i < gridPositionInList + gridAmountToPool; i++)
        {
            if (!pooledGrids[i].activeInHierarchy)
            {
                return pooledGrids[i];
            }
        }
        return null;
    }
    #endregion
}

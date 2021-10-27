using System.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    #region Variables
    //Variables for grid generation
    [SerializeField] private float gridRepeatingTime = 2.5f;
    private int randomGridPattern;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        //Start spawning grids
        StartCoroutine(SpawnGrids());
    }
    #endregion

    #region IEnumerators
    //Method to spawn cars at random positions
    private IEnumerator SpawnGrids()
    {
        while (true)
        {
            //Wait a certain time before randomly spawn a grid
            yield return new WaitForSeconds(gridRepeatingTime);
            randomGridPattern = Random.Range(1, ObjectPooling.SharedInstance.GetPossibleGridPatternsCount() + 1);

            //Use precreated objects of object pooling to activate them when necessary
            GameObject grid = ObjectPooling.SharedInstance.GetGridObject(randomGridPattern);
            if (grid != null)
            {
                grid.SetActive(true);
            }
        }
    }
    #endregion
}

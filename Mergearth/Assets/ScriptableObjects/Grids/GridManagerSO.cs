using UnityEngine;

[CreateAssetMenu(fileName = "GridManager", menuName = "ScriptableObjects/GridManagerSO")]
public class GridManagerSO : ScriptableObject
{

	#region Variables
	//Variables for grid position
	public float gridSize;
	public float previousGridXPosNewSpawn;

	//Variables for grid generation
	public int gridAmountToPool;
	public int maxNumberGrids;

	//Variables for limit movement on X Axis
	public float xPosLimitLeft;
	public float xPosLimitRight;
	#endregion
}

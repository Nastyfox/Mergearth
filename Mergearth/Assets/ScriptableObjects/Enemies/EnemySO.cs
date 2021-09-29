using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemySO", order = 2)]
public class EnemySO : ScriptableObject
{

	#region Variables
	public int enemyId;
	public int enemyHP;
	public int enemyDamage;
	#endregion
}

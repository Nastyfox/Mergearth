using UnityEngine;

public abstract class EnemySO : ScriptableObject
{
	#region Variables
	public int enemyId;
	public int maxHP;
	public int enemyDamage;
	public bool isFlying;
	public float speed;
	public float nextWaypointDistance;
	public float activationDistance;
	public float jumpNodeHeightRequirement;
	public float jumpModifier;
	public float jumpCheckOffset;
	#endregion
}

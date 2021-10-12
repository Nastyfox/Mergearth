using UnityEngine;

public abstract class PickupSO : ScriptableObject
{

	#region Variables
	public AudioClip soundEffect;
	#endregion

	public abstract void Pickup();
}

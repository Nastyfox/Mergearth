using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "ScriptableObjects/PickupSO", order = 3)]
public class PickupSO : ScriptableObject
{

	#region Variables
	public int pickupId;
	public int pickupHealValue;
	public int pickupCoinValue;
	public AudioClip soundEffect;
	public bool isForInventory;
	#endregion
}

using UnityEngine;

[CreateAssetMenu(fileName = "Interactable", menuName = "ScriptableObjects/InteractableSO", order = 3)]
public class InteractableSO : ScriptableObject
{

	#region Variables
	public int interactableId;
	public AudioClip soundEffect;
	public ItemSO[] possibleItems;
	public int maxItemsNumber;
	public int minItemsNumber;
	public PickupSO[] possiblePickups;
	public int maxPickupsNumber;
	public int minPickupsNumber;
	#endregion
}

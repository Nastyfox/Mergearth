using UnityEngine;

public abstract class InteractableSO : ScriptableObject
{
    #region Variables
    public bool multipleInteractions;
    public Animator interactAnimator;
    public string interactionText;
    #endregion

    #region Methods
    public abstract void Interact();


    public void SetAnimator(Animator animator)
    {
        interactAnimator = animator;
    }
    #endregion
}

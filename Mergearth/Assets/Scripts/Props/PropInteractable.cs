using UnityEngine;
using TMPro;

public class PropInteractable : MonoBehaviour
{
    #region Variables
    //Variables for interactable objects
    private TMP_Text interactionText;

    //Variables for chest opening
    [SerializeField] InteractableSO interactableSO;
    private bool interactionDone;

    //Variables for controls
    [SerializeField] private InputReaderSO inputReader = default;
    private bool isInteracting;

    //Variables for interaction
    private bool interactableSOInRange;
    #endregion

    #region UnityMethods
    // Update is called once per frame
    void Update()
    {
        //If player press E when he is close to an object he can interact with
        if (isInteracting)
        {
            if(interactableSOInRange)
            {
                //If the prop can be interacted with multiple times or if the interaction is not done yet, interact with it
                if ((!interactableSO.multipleInteractions && !interactionDone) || interactableSO.multipleInteractions)
                {
                    //Set animator component for scriptable object
                    interactableSO.SetAnimator(this.GetComponent<Animator>());

                    //Interact with the object
                    interactableSO.Interact();
                    interactionDone = true;
                }

                //Interaction is finished
                isInteracting = false;
            }
        }
    }

    private void Awake()
    {
        //Get the interaction text and deactivate it
        interactionText = GameObject.FindGameObjectWithTag("InteractionText").GetComponent<TMP_Text>();
        interactionText.enabled = false;
    }

    private void OnEnable()
    {
        //Add listeners for player controls events invoked by inputreader

        //Set interactions with props
        inputReader.interactEvent += OnInteract;
    }

    private void OnDisable()
    {
        //Remove listeners for player controls events invoked by inputreader

        //Set interactions with props
        inputReader.interactEvent -= OnInteract;
    }
    #endregion

    #region Collisions
    private void OnTriggerStay2D(Collider2D collision)
    {
        //If the player is in contact with interactable prop and if he's not climbing
        if (collision.gameObject.CompareTag("Player") && !PlayerMovement.SharedInstance.GetIsClimbing())
        {
            //Say that the interactable object is in range
            interactableSOInRange = true;
            //Object is interactable so display interaction text
            interactionText.text = interactableSO.interactionText;
            interactionText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If the player gets out with pickup that he can interact with
        if (collision.gameObject.CompareTag("Player"))
        {
            //Say that the interactable object is not in range
            interactableSOInRange = false;
            //Disable interaction text
            interactionText.enabled = false;
        }
    }
    #endregion

    #region InputMethods
    private void OnInteract()
    {
        isInteracting = true;
    }
    #endregion
}
